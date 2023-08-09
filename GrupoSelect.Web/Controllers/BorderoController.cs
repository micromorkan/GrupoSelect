using AutoMapper;
using GrupoSelect.Data.Context;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using GrupoSelect.Services.Service;
using GrupoSelect.Web.Helpers;
using GrupoSelect.Web.ViewModel;
using GrupoSelect.Web.Views.Shared.Reports.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorEngine;
using RazorEngine.Templating;
using System.Globalization;
using System.Security.Claims;

namespace GrupoSelect.Web.Controllers
{
    [Authorize]
    public class BorderoController : Controller
    {
        private IUserService _userService;
        private IBorderoService _borderoService;

        public readonly IMapper _mapper;

        public BorderoController(GSDbContext context, IUserService userService,
                                                      IBorderoService borderoService,
                                                      IMapper mapper)
        {
            _userService = userService;
            _borderoService = borderoService;
            _mapper = mapper;
        }

        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Index()
        {
            var bordero = new BorderoVM();

            return View(bordero);
        }

        [HttpPost]
        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        [TypeFilter(typeof(ExceptionLog))]
        public async Task<IActionResult> Index(BorderoVM proposalVM, int page, int qtPage)
        {
            try
            {
                var result = await _borderoService.GetAll(proposalVM.UserId, proposalVM.StartDate, proposalVM.EndDate);

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
                    items.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name + " - " + item.Profile, Selected = filter.Id == item.Id ? true : false });
                }

                return items;
            }
            else
            {
                return new List<SelectListItem>();
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> ViewPaytable(int userId, string startDate, string endDate) 
        {
            try
            {
                User user = (await _userService.GetById(userId)).Object;

                if (user.Profile == Constants.PROFILE_GERENTE)
                {
                    IEnumerable<Contract> contracts = (await _borderoService.GetAllManager(userId, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate))).Object;

                    BorderoForm borderoForm = new BorderoForm(contracts, user, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));

                    string cshtmlContent = System.IO.File.ReadAllText("Views\\Shared\\Reports\\BorderoForm.cshtml");
                    string renderedContent = Engine.Razor.RunCompile(cshtmlContent, Guid.NewGuid().ToString(), typeof(BorderoForm), borderoForm);

                    return Content(renderedContent, "text/html");
                }
                else
                {
                    IEnumerable<Contract> contracts = (await _borderoService.GetAll(userId, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate))).Object;

                    BorderoForm borderoForm = new BorderoForm(contracts, user, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));

                    string cshtmlContent = System.IO.File.ReadAllText("Views\\Shared\\Reports\\BorderoForm.cshtml");
                    string renderedContent = Engine.Razor.RunCompile(cshtmlContent, Guid.NewGuid().ToString(), typeof(BorderoForm), borderoForm);

                    return Content(renderedContent, "text/html");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
