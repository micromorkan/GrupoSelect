using AutoMapper;
using GrupoSelect.Data.Context;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Model;
using GrupoSelect.Services.Interface;
using GrupoSelect.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GrupoSelect.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IProfileService _profileService;
        public readonly IMapper _mapper;

        public UsersController(GSDbContext context, IUserService userService, IProfileService profileService, IMapper mapper)
        {
            _userService = userService;
            _profileService = profileService;
            _mapper = mapper;
        }

        [Authorize(Roles = "REPRESENTANTE,ADMINISTRATIVO")]
        public async Task<IActionResult> Index()
        {
            return View(new UserVM());
        }

        [Authorize(Roles = "REPRESENTANTE")]
        [HttpPost]
        public async Task<IActionResult> Index(UserVM userVM, int page, int qtPage)
        {
            var filter = _mapper.Map<User>(userVM);

            var result = await _userService.GetAllPaginate(filter, page, qtPage);

            return Json(new
            {
                result = result,
            });
        }

        [Authorize(Roles = "ADMINISTRATIVO")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _userService.GetById(id);

            if (result.Success)
            {
                return View(result.Object);
            }
            else
            {
                ViewData["GS_MESSAGE_USER"] = result.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserVM userVM)
        {
            var user = _mapper.Map<User>(userVM);

            var result = _userService.Insert(user);

            //Result<User> result = new Result<User>();
            //result.Success = false;
            //result.Message = "teste erro";

            return Json(result);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _userService.GetById(id);

            if (result.Success)
            {
                return View(result.Object);
            }
            else
            {
                ViewData["GS_ERRORS_USER"] = result.Errors;
                ViewData["GS_MESSAGE_USER"] = result.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserVM userVM)
        {
            var user = _mapper.Map<User>(userVM);

            var result = _userService.Update(user);

            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["GS_ERRORS_USER"] = result.Errors;
                ViewData["GS_MESSAGE_USER"] = result.Message;

                return View(user);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.GetById(id);

            if (result.Success)
            {
                return View(result.Object);
            }
            else
            {
                ViewData["GS_ERRORS_USER"] = result.Errors;
                ViewData["GS_MESSAGE_USER"] = result.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = _userService.Delete(id);

            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["GS_ERRORS_USER"] = result.Errors;
                ViewData["GS_MESSAGE_USER"] = result.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IEnumerable<SelectListItem>> GetProfileList(Domain.Entity.Profile filter)
        {
            var result = await _profileService.GetAll(filter);

            if (result.Success)
            {
                IList<SelectListItem> items = new List<SelectListItem>();

                foreach (Domain.Entity.Profile item in result.Object)
                {
                    items.Add(new SelectListItem() { Value = item.Name, Text = item.Name, Selected = filter.Id == item.Id ? true : false });
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
