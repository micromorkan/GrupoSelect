using AutoMapper;
using GrupoSelect.Data.Context;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using GrupoSelect.Services.Service;
using GrupoSelect.Web.Helpers;
using GrupoSelect.Web.ViewModel;
using GrupoSelect.Web.Views.Shared.Reports.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorEngine;
using RazorEngine.Templating;
using System.Globalization;
using System.Security.Claims;

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

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO)]
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
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO)]
        [TypeFilter(typeof(ExceptionLog))]
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

                var result = await _contractService.GetAllPaginate(filter, page, qtPage, contractVM.StartDate, contractVM.EndDate);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_TI + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _contractService.GetById(id);

                if (result.Success)
                {
                    if (result.Object.Status != Constants.CONTRACT_STATUS_AD || result.Object.Status != Constants.CONTRACT_STATUS_CR)
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
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_TI + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Edit(int id, ContractVM contractVM)
        {
            try
            {
                var resultContract = await _contractService.GetById(id);

                if (resultContract.Success)
                {
                    if (resultContract.Object.Status != Constants.CONTRACT_STATUS_AD || resultContract.Object.Status != Constants.CONTRACT_STATUS_CR)
                    {
                        return Json(new Result<Contract> { Success = false, Message = "Esse registro não pode ser editado pois possui o status de " + resultContract.Object.Status });
                    }
                }
                
                var contract = _mapper.Map<Contract>(contractVM);

                contract.Proposal = new Proposal();                

                var result = _contractService.Update(contract);

                return Json(result);
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


        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_TI + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_DIRETOR)]
        public IActionResult CheckContract(ContractVM contractVM)
        {
            try
            {
                var contract = _mapper.Map<Contract>(contractVM);
                var result = _contractService.CheckContract(contract, Convert.ToInt32(User.GetId()));
             
                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

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

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_TI + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> CancelContract(int id)
        {
            try
            {
                var result = _contractService.CancelContract(id, Convert.ToInt32(User.GetId()));

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
