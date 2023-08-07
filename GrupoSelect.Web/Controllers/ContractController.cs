using AutoMapper;
using GrupoSelect.Data.Context;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using GrupoSelect.Web.Helpers;
using GrupoSelect.Web.ViewModel;
using GrupoSelect.Web.Views.Shared.Reports.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RazorEngine;
using RazorEngine.Templating;

namespace GrupoSelect.Web.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {
        private IContractService _contractService;

        public readonly IMapper _mapper;

        public ContractController(GSDbContext context, IContractService contractService,
                                                     IMapper mapper)
        {
            _contractService = contractService;
            _mapper = mapper;
        }

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_ADVOGADO)]
        public async Task<IActionResult> Index()
        {
            var proposal = new ContractVM();

            if (HttpContext.User.IsInRole(Constants.PROFILE_ADMINISTRATIVO))
            {
                proposal.Status = Constants.CONTRACT_STATUS_PA;
            }

            return View(proposal);
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_ADVOGADO)]
        public async Task<IActionResult> Index(ContractVM contractVM, int page, int qtPage)
        {
            try
            {
                var filter = _mapper.Map<Contract>(contractVM);

                filter.Proposal = new Proposal();

                if (HttpContext.User.IsInRole(Constants.PROFILE_REPRESENTANTE))
                {
                    filter.Proposal.UserId = Convert.ToInt32(User.GetId());
                }

                filter.ContractNum = string.IsNullOrEmpty(filter.ContractNum) ? string.Empty : filter.ContractNum.ToUpper();

                var result = await _contractService.GetAllPaginate(filter, page, qtPage, contractVM.StartDate, contractVM.EndDate);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _contractService.GetById(id);

                if (result.Success)
                {
                    if (result.Object.Status != Constants.CONTRACT_STATUS_AD && result.Object.Status != Constants.CONTRACT_STATUS_CR)
                    {
                        TempData[Constants.SYSTEM_ERROR_KEY] = "Esse registro não pode ser editado pois possui o status de " + result.Object.Status;
                        return RedirectToAction(nameof(Index));
                    }

                    var proposalVM = _mapper.Map<ContractVM>(result.Object);

                    return View(proposalVM);
                }
                else
                {
                    TempData[Constants.SYSTEM_ERROR_KEY] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData[Constants.SYSTEM_ERROR_KEY] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Edit(int id, ContractVM contractVM)
        {
            try
            {
                var resultContract = await _contractService.GetById(id);

                if (resultContract.Success)
                {
                    Contract contract = resultContract.Object;

                    if (contract.Status != Constants.CONTRACT_STATUS_AD && contract.Status != Constants.CONTRACT_STATUS_CR)
                    {
                        return Json(new Result<Contract> { Success = false, Message = "Esse registro não pode ser editado pois possui o status de " + contract.Status });
                    }

                    if (contractVM.ContractConsultancyFormFile == null || 
                        contractVM.ContractFinancialAdminFormFile == null || 
                        (contract.Proposal.User.BranchWithoutAdm && contractVM.VideoAgreeFormFile == null))
                    {
                        return Json(new Result<Contract> { Success = false, Message = "Selecione todos os arquivos antes salvar!" });
                    }

                    using (var fichaMS = new MemoryStream())
                    {
                        await contractVM.ContractConsultancyFormFile.CopyToAsync(fichaMS);

                        // Upload the file if less than 2 MB
                        //if (fichaMS.Length < 2097152)
                        //{

                        contract.ContractConsultancy = fichaMS.ToArray();
                        contract.ContractConsultancyFileType = contractVM.ContractConsultancyFormFile.ContentType;
                        //}
                        //else
                        //{
                        //    ModelState.AddModelError("File", "The file is too large.");
                        //}
                    }

                    using (var docMS = new MemoryStream())
                    {
                        await contractVM.ContractFinancialAdminFormFile.CopyToAsync(docMS);

                        contract.ContractFinancialAdmin = docMS.ToArray();
                        contract.ContractFinancialAdminFileType = contractVM.ContractFinancialAdminFormFile.ContentType;
                    }

                    if (contract.Proposal.User.BranchWithoutAdm)
                    {
                        using (var videoMS = new MemoryStream())
                        {
                            await contractVM.VideoAgreeFormFile.CopyToAsync(videoMS);

                            contract.VideoAgree = videoMS.ToArray();
                            contract.VideoAgreeFileType = contractVM.VideoAgreeFormFile.ContentType;
                        }
                    }

                    var result = _contractService.Update(contract);

                    return Json(result);
                }
                else
                {
                    return Json(new Result<Contract> { Success = false, Message = "Esse registro não foi encontrado!" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Check(int id)
        {
            try
            {
                var result = await _contractService.GetById(id);

                if (result.Success)
                {
                    if (result.Object.Status != Constants.CONTRACT_STATUS_PA)
                    {
                        TempData[Constants.SYSTEM_ERROR_KEY] = "Esse registro não pode ser analisado pois possui o status de " + result.Object.Status;
                        return RedirectToAction(nameof(Index));
                    }

                    var contractVM = _mapper.Map<ContractVM>(result.Object);
                    contractVM.Id = id;

                    return View(contractVM);
                }
                else
                {
                    TempData[Constants.SYSTEM_ERROR_KEY] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData[Constants.SYSTEM_ERROR_KEY] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Check(int id, ContractVM contractVM)
        {
            try
            {
                var resultContract = await _contractService.GetById(id);

                if (resultContract.Success)
                {
                    Contract contract = resultContract.Object;

                    if (contract.Status != Constants.CONTRACT_STATUS_PA)
                    {
                        return Json(new Result<Contract> { Success = false, Message = "Esse registro não pode ser analisado pois possui o status de " + contract.Status });
                    }

                    if (!contract.Proposal.User.BranchWithoutAdm && contractVM.VideoAgreeFormFile == null && contractVM.Status == Constants.CONTRACT_STATUS_CA)
                    {
                        return Json(new Result<Contract> { Success = false, Message = "Selecione o vídeo antes salvar!" });
                    }

                    var model = _mapper.Map<Contract>(contractVM);

                    if (!contract.Proposal.User.BranchWithoutAdm && contractVM.Status == Constants.CONTRACT_STATUS_CA)
                    {
                        using (var videoMS = new MemoryStream())
                        {
                            await contractVM.VideoAgreeFormFile.CopyToAsync(videoMS);

                            model.VideoAgree = videoMS.ToArray();
                            model.VideoAgreeFileType = contractVM.VideoAgreeFormFile.ContentType;
                        }
                    }

                    var result = _contractService.Check(model, Convert.ToInt32(User.GetId()));

                    return Json(result);
                }
                else
                {
                    return Json(new Result<Contract> { Success = false, Message = "Esse registro não foi encontrado!" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpPost]
        //public async Task<IEnumerable<SelectListItem>> GetClientList(Client filter)
        //{
        //    var result = await _clientService.GetAll(filter);

        //    if (result.Success)
        //    {
        //        IList<SelectListItem> items = new List<SelectListItem>();

        //        foreach (Client item in result.Object)
        //        {
        //            items.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name, Selected = filter.Id == item.Id ? true : false });
        //        }

        //        return items;
        //    }
        //    else
        //    {
        //        return new List<SelectListItem>();
        //    }
        //}

        [TypeFilter(typeof(ExceptionLog))]
        public async Task<IActionResult> PrintContract(int id)
        {
            try
            {
                Contract contract = (await _contractService.GetById(id)).Object;

                ContractForm contractForm = new ContractForm(contract.Proposal.Client, contract.Proposal, contract.Proposal.User);

                string cshtmlContent = System.IO.File.ReadAllText("Views\\Shared\\Reports\\ContractForm.cshtml");
                string renderedContent = Engine.Razor.RunCompile(cshtmlContent, "CONTRATO", typeof(ContractForm), contractForm);

                return Content(renderedContent, "text/html");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> DownloadContract(int id)
        {
            byte[] bytes;
            string fileName, contentType;
            var result = await _contractService.GetById(id);

            if (result.Success)
            {
                var contract = result.Object;

                fileName = "CONTRATO_" + contract.Proposal.Client.Name.Replace(" ", "_") + "." + contract.ContractConsultancyFileType.Split("/")[1];
                contentType = contract.ContractConsultancyFileType;
                bytes = contract.ContractConsultancy;

                return File(bytes, contentType, fileName);
            }

            TempData[Constants.SYSTEM_ERROR_KEY] = "Não foi possível obter o arquivo.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadDocuments(int id)
        {
            byte[] bytes;
            string fileName, contentType;
            var result = await _contractService.GetById(id);

            if (result.Success)
            {
                var contract = result.Object;

                fileName = "DOCUMENTOS_" + contract.Proposal.Client.Name.Replace(" ", "_") + "." + contract.ContractFinancialAdminFileType.Split("/")[1];
                contentType = contract.ContractFinancialAdminFileType;
                bytes = contract.ContractFinancialAdmin;

                return File(bytes, contentType, fileName);
            }

            TempData[Constants.SYSTEM_ERROR_KEY] = "Não foi possível obter o arquivo.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadVideo(int id)
        {
            byte[] bytes;
            string fileName, contentType;
            var result = await _contractService.GetById(id);

            if (result.Success)
            {
                var contract = result.Object;

                fileName = "VIDEO_" + contract.Proposal.Client.Name.Replace(" ", "_") + "." + contract.VideoAgreeFileType.Split("/")[1];
                contentType = contract.VideoAgreeFileType;
                bytes = contract.VideoAgree;

                return File(bytes, contentType, fileName);
            }

            TempData[Constants.SYSTEM_ERROR_KEY] = "Não foi possível obter o arquivo.";
            return RedirectToAction(nameof(Index));
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_GERENTE + "," + Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                var result = _contractService.Cancel(id, Convert.ToInt32(User.GetId()));

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
