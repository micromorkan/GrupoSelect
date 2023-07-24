using AutoMapper;
using GrupoSelect.Data.Context;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using GrupoSelect.Web.Helpers;
using GrupoSelect.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GrupoSelect.Web.Controllers
{
    [Authorize]
    public class FinancialAdminController : Controller
    {
        private IFinancialAdminService _financialAdminService;
        public readonly IMapper _mapper;

        public FinancialAdminController(GSDbContext context, IFinancialAdminService financialAdminService, IMapper mapper)
        {
            _financialAdminService = financialAdminService;
            _mapper = mapper;
        }

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO)]
        public async Task<IActionResult> Index()
        {
            return View(new FinancialAdminVM { Active = true });
        }

        [HttpPost]
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE)]
        [TypeFilter(typeof(ExceptionLog))]
        public async Task<IActionResult> Index(FinancialAdminVM modelVM, int page, int qtPage)
        {
            var result = new PaginateResult<IEnumerable<FinancialAdmin>>();

            try
            {
                var filter = _mapper.Map<FinancialAdmin>(modelVM);

                result = await _financialAdminService.GetAllPaginate(filter, page, qtPage);

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FinancialAdminVM modelVM)
        {
            try
            {
                var model = _mapper.Map<FinancialAdmin>(modelVM);

                var result = _financialAdminService.Insert(model);

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
                var result = await _financialAdminService.GetById(id);

                if (result.Success)
                {
                    return View(_mapper.Map<FinancialAdminVM>(result.Object));
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
        public async Task<IActionResult> Edit(int id, FinancialAdminVM modelVM)
        {
            try
            {
                var model = _mapper.Map<FinancialAdmin>(modelVM);

                var result = _financialAdminService.Update(model);

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
                var result = _financialAdminService.Delete(id);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
