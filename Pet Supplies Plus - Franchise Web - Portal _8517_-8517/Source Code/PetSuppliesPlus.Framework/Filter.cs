using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PetSuppliesPlus.Framework;
using System.Web.Mvc.Ajax;

namespace PetSuppliesPlus.Framework
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class IsAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)//OnActionExecuted
        {
            HttpContext Context = HttpContext.Current;
            if (!SessionHelper.IsUserLogin)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Items["IsSessionExpired"] = true;

                    filterContext.RequestContext.HttpContext.Response.StatusCode = 401;
                    filterContext.RequestContext.HttpContext.Response.End();

                    filterContext.Result = new JsonResult()
                    {
                        Data = new { PageStatus = "logout", HtmlResult = "" },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    string url = System.Configuration.ConfigurationSettings.AppSettings["SiteUrl"].ToString() + "home/index?id=";

                    filterContext.Result = new RedirectResult(url + filterContext.HttpContext.Request.Url.ToString());
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)//OnActionExecuted
        {

            HttpContext Context = HttpContext.Current;
            if (!SessionHelper.IsAdmin)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.RequestContext.HttpContext.Response.StatusCode = 401;
                    filterContext.RequestContext.HttpContext.Response.End();

                    filterContext.Result = new JsonResult()
                    {
                        Data = new { PageStatus = "logout", HtmlResult = "" },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    //filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    //{
                    //    {"Controller","Login"},{"Action","Index"}

                    //});
                    string url = System.Configuration.ConfigurationSettings.AppSettings["SiteUrl"].ToString() + "admin/account/index?id=";
                    filterContext.Result = new RedirectResult(url + filterContext.HttpContext.Request.Url.ToString());
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ErrorHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            //{
            //    return;
            //}

            //if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            //{
            //    return;
            //}

            //if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            //{
            //    return;
            //}

            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        Status = 1,
                        error = true,
                        Message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            // log the error by using your own method
            EventLogHandler.WriteLog(filterContext.Exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;


        }
    }

    
    public sealed class ValidateOnlyIncomingValuesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)//OnActionExecuted
        {
            var modelState = filterContext.Controller.ViewData.ModelState;
            var valueProvider = filterContext.Controller.ValueProvider;

            var keysWithNotIncommingValue = modelState.Keys.Where(x => !valueProvider.ContainsPrefix(x));
            foreach (var key in keysWithNotIncommingValue)
            {
                modelState[key].Errors.Clear();
            }
        }
    }
}
