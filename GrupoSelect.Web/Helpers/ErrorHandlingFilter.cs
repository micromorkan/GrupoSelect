using Azure;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;

namespace GrupoSelect.Web.Helpers
{
    public class ExceptionLog : ExceptionFilterAttribute, IActionFilter
    {
        private string ControllerName;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExceptionLog(IConfiguration configuration, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            this.ControllerName = context.Controller?.ToString().Split(".").Last();
        }

        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            if (Convert.ToBoolean(_configuration.GetSection(Constants.SYSTEM_SETTINGS)[Constants.SYSTEM_SETTINGS_REGISTERERRORLOG]))
            {
                ErrorLog log = new ErrorLog();

                var stringBuilder = new StringBuilder();
                stringBuilder.Append("{");

                foreach (var item in context.ModelState.Keys)
                {
                    stringBuilder.Append($"{item}:{context.ModelState[item]?.RawValue} ,");
                }

                stringBuilder.Append("}");

                //log.Object = JsonSerializer.Serialize(ex.Data[Constants.SYSTEM_EXCEPTION_OBJ]);
                log.Object = stringBuilder.ToString();
                log.Method = ex.TargetSite.DeclaringType?.FullName;
                log.Message = ex.Message.Length > 2000 ? ex.Message.Substring(0, 2000) : ex.Message;

                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    log.Username = _httpContextAccessor.HttpContext.User.Identity.Name;
                }

                _unitOfWork.ErrorLogs.Insert(log);
                _unitOfWork.ErrorLogs.Save();
            }

            context.ExceptionHandled = true;
            context.Result = new JsonResult(new
            {
                Success = false,
                Message = Constants.SYSTEM_ERROR_MSG
            });

            //var s = new StackTrace(context.Exception);
            //var r = s.GetFrame(0);
            //string actionName = GetMethodName(r.GetMethod());
            //context.HttpContext.Response.Redirect("...");
        }

        private string GetMethodName(System.Reflection.MethodBase method)
        {
            string _methodName = method.DeclaringType.FullName;

            if (_methodName.Contains(">") || _methodName.Contains("<"))
            {
                _methodName = _methodName.Split('<', '>')[1];
            }
            else
            {
                _methodName = method.Name;
            }

            return _methodName;
        }
    }
}
