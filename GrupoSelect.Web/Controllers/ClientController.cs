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
    public class ClientController : Controller
    {
        private IClientService _clientService;
        public readonly IMapper _mapper;

        public ClientController(GSDbContext context, IClientService clientService,
                                                     IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO)]
        public async Task<IActionResult> Index()
        {
            return View(new ClientVM());
        }

        [HttpPost]
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE)]
        [TypeFilter(typeof(ExceptionLog))]
        public async Task<IActionResult> Index(ClientVM clientVM, int page, int qtPage)
        {
            var result = new PaginateResult<IEnumerable<Client>>();

            try
            {
                var filter = _mapper.Map<Client>(clientVM);

                result = await _clientService.GetAllPaginate(filter, page, qtPage);

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
        public async Task<IActionResult> Create(ClientVM clientVM)
        {
            try
            {
                var client = _mapper.Map<Client>(clientVM);
                client.UserId = Convert.ToInt32(User.GetId());
                var result = _clientService.Insert(client);

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
                var result = await _clientService.GetById(id);

                if (result.Success)
                {
                    return View(_mapper.Map<ClientVM>(result.Object));
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
        public async Task<IActionResult> Edit(int id, ClientVM clientVM)
        {
            try
            {
                var client = _mapper.Map<Client>(clientVM);

                var result = _clientService.Update(client);

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
                var result = _clientService.Delete(id);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
