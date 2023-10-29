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
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorEngine;
using RazorEngine.Templating;
using System.Configuration;

namespace GrupoSelect.Web.Controllers
{
    [ServiceFilter(typeof(SecurityAttribute))]
    public class ProposalController : Controller
    {
        private ICreditService _creditService;
        private IProductTypeService _productTypeService;
        private ITableTypeService _tableTypeService;
        private IFinancialAdminService _financialAdminService;
        private IProposalService _proposalService;
        private IClientService _clientService;
        private IConfiguration _configuration;

        public readonly IMapper _mapper;

        public ProposalController(GSDbContext context, ICreditService creditService,
                                                     IProductTypeService productTypeService,
                                                     ITableTypeService tableTypeService,
                                                     IFinancialAdminService financialAdminService,
                                                     IProposalService proposalService,
                                                     IClientService clientService,
                                                     IMapper mapper,
                                                     IConfiguration configuration)
        {
            _creditService = creditService;
            _productTypeService = productTypeService;
            _tableTypeService = tableTypeService;
            _financialAdminService = financialAdminService;
            _proposalService = proposalService;
            _clientService = clientService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
        public async Task<IActionResult> Index()
        {
            var proposal = new ProposalVM();

            if (HttpContext.User.IsInRole(Constants.PROFILE_ADMINISTRATIVO))
            {
                //proposal.Status = Constants.PROPOSAL_STATUS_AC;
            }

            return View(proposal);
        }

        [HttpPost]
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
        [TypeFilter(typeof(ExceptionLog))]
        public async Task<IActionResult> Index(ProposalVM proposalVM, int page, int qtPage)
        {
            try
            {
                var filter = _mapper.Map<Proposal>(proposalVM);

                if (HttpContext.User.IsInRole(Constants.PROFILE_REPRESENTANTE))
                {
                    filter.UserId = Convert.ToInt32(User.GetId());
                }

                filter.FinancialAdminName = proposalVM.FinancialAdminId > 0 ? (await _financialAdminService.GetById(proposalVM.FinancialAdminId)).Object.Name : string.Empty;
                filter.TableTypeTax = proposalVM.TableTypeId > 0 ? (await _tableTypeService.GetById(proposalVM.TableTypeId)).Object.TableTax : string.Empty;
                filter.ProductTypeName = proposalVM.ProductTypeId > 0 ? (await _productTypeService.GetById(proposalVM.ProductTypeId)).Object.ProductName : string.Empty;

                var result = await _proposalService.GetAllPaginate(filter, page, qtPage, proposalVM.StartDate, proposalVM.EndDate);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
        [TypeFilter(typeof(ExceptionLog))]
        public async Task<IActionResult> Create(ProposalVM proposalVM)
        {
            try
            {
                var resultCredit = await _creditService.GetById(proposalVM.CreditId);

                if (resultCredit.Success)
                {
                    Credit credit = resultCredit.Object;

                    proposalVM.CreditMembershipValue = credit.MembershipValue;
                    proposalVM.CreditValue = credit.CreditValue;
                    proposalVM.CreditPortionValue = credit.PortionValue;
                    proposalVM.FinancialAdminName = credit.FinancialAdmin.Name;
                    proposalVM.TableTypeFee = credit.TableType.MembershipFee;
                    proposalVM.TableTypeCommission = credit.TableType.CommissionFee;
                    proposalVM.TableTypeManager = credit.TableType.ManagerFee;
                    proposalVM.TableTypeRate = credit.TableType.RemainingRate;
                    proposalVM.TableTypeTax = credit.TableType.TableTax;
                    proposalVM.ProductTypeName = credit.ProductType.ProductName;
                }
                else
                {
                    return Json(new Result<Proposal> { Success = false, Message = "Não foi possivel obter os dados do Crédito selecionado. Contate o setor de TI." });
                }

                proposalVM.Status = Constants.PROPOSAL_STATUS_AC;

                var proposal = _mapper.Map<Proposal>(proposalVM);

                proposal.UserId = Convert.ToInt32(User.GetId());

                var result = _proposalService.Insert(proposal);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _proposalService.GetById(id);

                if (result.Success)
                {
                    if (result.Object.Status != Constants.PROPOSAL_STATUS_AC)
                    {
                        TempData[Constants.SYSTEM_ERROR_KEY] = "Esse registro não pode ser editado pois possui o status de " + result.Object.Status;
                        return RedirectToAction(nameof(Index));
                    }

                    if (result.Object.UserId != Convert.ToInt32(User.GetId()) && User.GetProfile() == Constants.PROFILE_REPRESENTANTE)
                    {
                        TempData[Constants.SYSTEM_ERROR_KEY] = "Esse registro não pode ser editado pois é de outro representante";
                        return RedirectToAction(nameof(Index));
                    }

                    var proposalVM = _mapper.Map<ProposalVM>(result.Object);
                    var resultFinancialAdmin = await _financialAdminService.GetAll(new FinancialAdmin { Name = proposalVM.FinancialAdminName });
                    proposalVM.FinancialAdminId = resultFinancialAdmin.Object.First().Id;

                    var resultTableType = await _tableTypeService.GetAll(new TableType { TableTax = proposalVM.TableTypeTax, MembershipFee = proposalVM.TableTypeFee, RemainingRate = proposalVM.TableTypeRate });
                    proposalVM.TableTypeId = resultTableType.Object.First().Id;

                    var resultProductType = await _productTypeService.GetAll(new ProductType { ProductName = proposalVM.ProductTypeName });
                    proposalVM.ProductTypeId = resultProductType.Object.First().Id;

                    var resultCredit = await _creditService.GetAll(new Credit { FinancialAdminId = proposalVM.FinancialAdminId, TableTypeId = proposalVM.TableTypeId, ProductTypeId = proposalVM.ProductTypeId });
                    proposalVM.CreditId = resultCredit.Object.First().Id;

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
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
        public async Task<IActionResult> Edit(int id, ProposalVM proposalVM)
        {
            try
            {
                var resultProposal = await _proposalService.GetById(id);

                if (resultProposal.Success)
                {
                    if (resultProposal.Object.Status != Constants.PROPOSAL_STATUS_AC)
                    {
                        return Json(new Result<Proposal> { Success = false, Message = "Esse registro não pode ser editado pois possui o status de " + resultProposal.Object.Status });
                    }

                    var resultCredit = await _creditService.GetById(proposalVM.CreditId);

                    if (resultCredit.Success)
                    {
                        Credit credit = resultCredit.Object;

                        proposalVM.CreditMembershipValue = credit.MembershipValue;
                        proposalVM.CreditValue = credit.CreditValue;
                        proposalVM.CreditPortionValue = credit.PortionValue;
                        proposalVM.FinancialAdminName = credit.FinancialAdmin.Name;
                        proposalVM.TableTypeFee = credit.TableType.MembershipFee;
                        proposalVM.TableTypeCommission = credit.TableType.CommissionFee;
                        proposalVM.TableTypeManager = credit.TableType.ManagerFee;
                        proposalVM.TableTypeRate = credit.TableType.RemainingRate;
                        proposalVM.TableTypeTax = credit.TableType.TableTax;
                        proposalVM.ProductTypeName = credit.ProductType.ProductName;
                    }

                    var proposal = _mapper.Map<Proposal>(proposalVM);

                    proposal.UserId = Convert.ToInt32(User.GetId());

                    var result = _proposalService.Update(proposal);

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

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = _proposalService.Delete(id);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IEnumerable<SelectListItem>> GetProductTypeList(ProductType filter)
        {
            var result = await _productTypeService.GetAll(filter);

            if (result.Success)
            {
                IList<SelectListItem> items = new List<SelectListItem>();

                foreach (ProductType item in result.Object)
                {
                    items.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.ProductName, Selected = filter.Id == item.Id ? true : false });
                }

                return items;
            }
            else
            {
                return new List<SelectListItem>();
            }
        }

        [HttpPost]
        public async Task<IEnumerable<SelectListItem>> GetTableTypeList(TableType filter)
        {
            var result = await _tableTypeService.GetAll(filter);

            if (result.Success)
            {
                IList<SelectListItem> items = new List<SelectListItem>();

                foreach (TableType item in result.Object)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = item.Id.ToString(),
                        Text = item.TableTax + " - " + item.MembershipFee + " - " + item.RemainingRate,
                        Selected = filter.Id == item.Id ? true : false
                    });
                }

                return items;
            }
            else
            {
                return new List<SelectListItem>();
            }
        }

        [HttpPost]
        public async Task<IEnumerable<SelectListItem>> GetFinancialAdminList(FinancialAdmin filter)
        {
            var result = await _financialAdminService.GetAll(filter);

            if (result.Success)
            {
                IList<SelectListItem> items = new List<SelectListItem>();

                foreach (FinancialAdmin item in result.Object)
                {
                    items.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name, Selected = filter.Id == item.Id ? true : false });
                }

                return items;
            }
            else
            {
                return new List<SelectListItem>();
            }
        }

        [HttpPost]
        public async Task<IEnumerable<SelectListItem>> GetClientList(Client filter)
        {
            var result = await _clientService.GetAll(filter);

            if (result.Success)
            {
                IList<SelectListItem> items = new List<SelectListItem>();

                foreach (Client item in result.Object)
                {
                    items.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name, Selected = filter.Id == item.Id ? true : false });
                }

                return items;
            }
            else
            {
                return new List<SelectListItem>();
            }
        }

        [HttpPost]
        public async Task<IEnumerable<SelectListItem>> GetCreditList(Credit filter)
        {
            var result = await _creditService.GetAll(filter);

            if (result.Success)
            {
                IList<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem()
                {
                    Value = "",
                    Text = "-- Selecione --",
                    Selected = filter.Id == 0 ? true : false
                });

                foreach (Credit item in result.Object)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = item.Id.ToString(),
                        Text = "R$ " + item.CreditValue + " / R$ " + item.MembershipValue + " / R$ " + item.PortionValue,
                        Selected = filter.Id == item.Id ? true : false
                    });
                }

                return items;
            }
            else
            {
                return new List<SelectListItem>();
            }
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Check(int id)
        {
            try
            {
                var result = await _proposalService.Check(id, Convert.ToInt32(User.GetId()));

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
        public async Task<IActionResult> PrintProposal(int id)
        {
            try
            {
                Proposal proposal = (await _proposalService.GetById(id)).Object;

                RegistrationForm registrationForm = new RegistrationForm(proposal.Client, proposal, proposal.User);

                string cshtmlContent = System.IO.File.ReadAllText(_configuration["ReportConfig:Folder"] + "RegistrationForm.cshtml");
                string renderedContent = Engine.Razor.RunCompile(cshtmlContent, Guid.NewGuid().ToString(), typeof(RegistrationForm), registrationForm);

                return Content(renderedContent, "text/html");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
