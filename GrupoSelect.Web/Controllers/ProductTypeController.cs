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
    public class ProductTypeController : Controller
    {
        private IProductTypeService _productTypeService;
        public readonly IMapper _mapper;

        public ProductTypeController(GSDbContext context, IProductTypeService productTypeService, IMapper mapper)
        {
            _productTypeService = productTypeService;
            _mapper = mapper;
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_TI)]
        public async Task<IActionResult> Index()
        {
            return View(new ProductTypeVM());
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_TI)]
        public async Task<IActionResult> Index(ProductTypeVM modelVM, int page, int qtPage)
        {
            var result = new PaginateResult<IEnumerable<ProductType>>();

            try
            {
                var filter = _mapper.Map<ProductType>(modelVM);

                result = await _productTypeService.GetAllPaginate(filter, page, qtPage);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_TI)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_TI)]
        public async Task<IActionResult> Create(ProductTypeVM modelVM)
        {
            try
            {
                var model = _mapper.Map<ProductType>(modelVM);

                var result = _productTypeService.Insert(model);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_TI)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _productTypeService.GetById(id);

                if (result.Success)
                {
                    return View(_mapper.Map<ProductTypeVM>(result.Object));
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
        [Authorize(Roles = Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_TI)]
        public async Task<IActionResult> Edit(int id, ProductTypeVM modelVM)
        {
            try
            {
                var model = _mapper.Map<ProductType>(modelVM);

                var result = _productTypeService.Update(model);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_TI)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = _productTypeService.Delete(id);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
