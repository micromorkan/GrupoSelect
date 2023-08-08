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

namespace GrupoSelect.Web.Controllers
{
    [Authorize]
    public class CreditController : Controller
    {
        private ICreditService _creditService;
        private IProductTypeService _productTypeService;
        private ITableTypeService _tableTypeService;
        private IFinancialAdminService _financialAdminService;
        public readonly IMapper _mapper;

        public CreditController(GSDbContext context, ICreditService creditService,
                                                     IProductTypeService productTypeService,
                                                     ITableTypeService tableTypeService,
                                                     IFinancialAdminService financialAdminService,
                                                     IMapper mapper)
        {
            _creditService = creditService;
            _productTypeService = productTypeService;
            _tableTypeService = tableTypeService;
            _financialAdminService = financialAdminService;
            _mapper = mapper;
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Index()
        {
            return View(new CreditVM());
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Index(CreditVM creditVM, int page, int qtPage)
        {
            var result = new PaginateResult<IEnumerable<Credit>>();

            try
            {
                var filter = _mapper.Map<Credit>(creditVM);

                result = await _creditService.GetAllPaginate(filter, page, qtPage);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Create(CreditVM creditVM)
        {
            try
            {
                var credit = _mapper.Map<Credit>(creditVM);

                var result = _creditService.Insert(credit);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _creditService.GetById(id);

                if (result.Success)
                {
                    return View(_mapper.Map<CreditVM>(result.Object));
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
        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Edit(int id, CreditVM creditVM)
        {
            try
            {
                var credit = _mapper.Map<Credit>(creditVM);

                var result = _creditService.Update(credit);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = _creditService.Delete(id);

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
                        Text = item.TableTax + " - " + item.MembershipFee + " - " + item.RemainingRate + " - " + item.CommissionFee,
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
    }
}
