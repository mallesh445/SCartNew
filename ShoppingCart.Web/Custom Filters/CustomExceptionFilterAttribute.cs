using ShoppingCart.Web.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Web.Custom_Filters
{
    public class CustomExceptionFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        ErrorHistory objErrorHistory = new ErrorHistory();
        ErrorHistoryBO objErrorHistoryBO = new ErrorHistoryBO();
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                // Code to log the error in database goes here..\
                objErrorHistory.ActionName = actionName;
                objErrorHistory.ControllerName = controllerName;
                objErrorHistory.ErrorMessage = model.Exception.Message;
                objErrorHistory.IsActive = true;
                objErrorHistoryBO.InsertErrorHistory(objErrorHistory);
                if (string.IsNullOrEmpty(Helper.UserData) || Helper.RoleId == 2)
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        MasterName = "~/Views/Shared/UserLayout.cshtml",
                        ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    };
                    filterContext.ExceptionHandled = true;
                }
                else
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Areas/Admin/Views/Shared/Error.cshtml",
                        MasterName = "~/Areas/Admin/Views/Shared/_Layout.cshtml",
                        ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    };
                    filterContext.ExceptionHandled = true;
                }
            }
        }
    }
}