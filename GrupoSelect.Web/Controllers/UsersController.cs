using GrupoSelect.Data.Context;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Services.Interface;
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

        public UsersController(GSDbContext context, IUserService userService, IProfileService profileService)
        {
            _userService = userService;
            _profileService = profileService;
        }

        [Authorize(Roles = "REPRESENTANTE,ADMINISTRATIVO")]
        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetAll(new Domain.Entity.User());

            ViewData["GS_LIST_USER_INDEX"] = result.Object;

            return View(new User());
        }

        [Authorize(Roles = "REPRESENTANTE")]
        [HttpPost]
        public async Task<IActionResult> Index([Bind("Id,Name,Email")] User filter)
        {
            var result = await _userService.GetAll(filter);

            ViewData["GS_LIST_USER_INDEX"] = result.Object;

            return View(filter);
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
        public async Task<IActionResult> Create(User user)
        {
            var result = _userService.Insert(user);

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
        public async Task<IActionResult> Edit(int id, User user)
        {
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

        // GET: Users/Delete/5
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
        public async Task<IEnumerable<SelectListItem>> GetProfileList(Profile filter)
        {
            var result = await _profileService.GetAll(filter);

            if (result.Success)
            {
                IList<SelectListItem> items = new List<SelectListItem>();

                foreach (Profile item in result.Object)
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
