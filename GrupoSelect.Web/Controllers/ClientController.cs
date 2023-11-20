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
    [ServiceFilter(typeof(SecurityAttribute))]
    public class ClientController : Controller
    {
        private IClientService _clientService;
        private IUserService _userService;
        public readonly IMapper _mapper;

        public ClientController(GSDbContext context, IClientService clientService,
                                                     IUserService userService,
                                                     IMapper mapper)
        {
            _clientService = clientService;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
        public async Task<IActionResult> Index()
        {
            return View(new ClientVM());
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
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

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
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

        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
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
        [Authorize(Roles = Constants.PROFILE_REPRESENTANTE + "," + Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_DIRETOR + "," + Constants.PROFILE_GERENTE)]
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
        [Authorize(Roles = Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_DIRETOR)]
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

        [HttpPost]
        public async Task<IEnumerable<SelectListItem>> GetUserList(User filter)
        {
            var result = await _userService.GetAll(filter);

            if (result.Success)
            {
                IList<SelectListItem> items = new List<SelectListItem>();

                foreach (User item in result.Object)
                {
                    items.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Representation, Selected = filter.Id == item.Id ? true : false });
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
