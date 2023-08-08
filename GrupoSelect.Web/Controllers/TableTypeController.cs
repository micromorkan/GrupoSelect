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
    public class TableTypeController : Controller
    {
        private ITableTypeService _tableTypeService;
        public readonly IMapper _mapper;

        public TableTypeController(GSDbContext context, ITableTypeService tableTypeService, IMapper mapper)
        {
            _tableTypeService = tableTypeService;
            _mapper = mapper;
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Index()
        {
            return View(new TableTypeVM());
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Index(TableTypeVM modelVM, int page, int qtPage)
        {
            var result = new PaginateResult<IEnumerable<TableType>>();

            try
            {
                var filter = _mapper.Map<TableType>(modelVM);

                result = await _tableTypeService.GetAllPaginate(filter, page, qtPage);

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
        public async Task<IActionResult> Create(TableTypeVM modelVM)
        {
            try
            {
                var model = _mapper.Map<TableType>(modelVM);

                var result = _tableTypeService.Insert(model);

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
                var result = await _tableTypeService.GetById(id);

                if (result.Success)
                {
                    return View(_mapper.Map<TableTypeVM>(result.Object));
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
        public async Task<IActionResult> Edit(int id, TableTypeVM modelVM)
        {
            try
            {
                var model = _mapper.Map<TableType>(modelVM);

                var result = _tableTypeService.Update(model);

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
                var result = _tableTypeService.Delete(id);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
