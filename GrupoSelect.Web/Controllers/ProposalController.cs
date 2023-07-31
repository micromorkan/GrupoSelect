using AutoMapper;
using GrupoSelect.Data.Context;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using GrupoSelect.Services.Service;
using GrupoSelect.Web.Helpers;
using GrupoSelect.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace GrupoSelect.Web.Controllers
{
    [Authorize]
    public class ProposalController : Controller
    {
        private ICreditService _creditService;
        private IProductTypeService _productTypeService;
        private ITableTypeService _tableTypeService;
        private IFinancialAdminService _financialAdminService;
        private IProposalService _proposalService;
        //private IClientService _clientService;

        public readonly IMapper _mapper;

        public ProposalController(GSDbContext context, ICreditService creditService,
                                                     IProductTypeService productTypeService,
                                                     ITableTypeService tableTypeService,
                                                     IFinancialAdminService financialAdminService,
                                                     IProposalService proposalService,
                                                     IMapper mapper)
        {
            _creditService = creditService;
            _productTypeService = productTypeService;
            _tableTypeService = tableTypeService;
            _financialAdminService = financialAdminService;
            _proposalService = proposalService;
            _mapper = mapper;
        }

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO)]
        public async Task<IActionResult> Index()
        {
            return View(new CreditVM());
        }

        [HttpPost]
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE)]
        [TypeFilter(typeof(ExceptionLog))]
        public async Task<IActionResult> Index(ProposalVM proposalVM, int page, int qtPage)
        {
            var result = new PaginateResult<IEnumerable<Proposal>>();

            try
            {
                var filter = _mapper.Map<Proposal>(proposalVM);

                result = await _proposalService.GetAllPaginate(filter, page, qtPage);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        public async Task<IActionResult> Create(ProposalVM proposalVM)
        {
            try
            {
                var proposal = _mapper.Map<Proposal>(proposalVM);

                var result = _proposalService.Insert(proposal);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _proposalService.GetById(id);

                if (result.Success)
                {
                    return View(_mapper.Map<ProposalVM>(result.Object));
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
        public async Task<IActionResult> Edit(int id, ProposalVM proposalVM)
        {
            try
            {
                var proposal = _mapper.Map<Proposal>(proposalVM);

                var result = _proposalService.Update(proposal);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
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
                        Text = item.TableTax + " - " + item.MembershipFee.ToString() + " - " + item.RemainingRate.ToString(),
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
        public async Task<IEnumerable<SelectListItem>> GetCreditList(Credit filter)
        {
            var result = await _creditService.GetAll(filter);

            if (result.Success)
            {
                IList<SelectListItem> items = new List<SelectListItem>();

                foreach (Credit item in result.Object)
                {
                    string formatedtotalvalue = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", item.TotalValue);
                    string formatedMembershipValue = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", item.MembershipValue);
                    string formatedPortionValue = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", item.PortionValue);

                    items.Add(new SelectListItem()
                    {
                        Value = item.Id.ToString(),
                        Text = formatedtotalvalue + " - " + formatedMembershipValue + " - " + formatedPortionValue,
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
    }
}
