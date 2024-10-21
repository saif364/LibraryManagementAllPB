using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryManagement.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            
        }
        public IActionResult JsonSuccess(string messages,string methodName)
        {
            var controllerName = ControllerContext?.RouteData?.Values["controller"]?.ToString();
            return Json(new
            {
                success = true,
                status = (int)HttpStatusCodeEnum.Created,
                message = messages,
                redirectUrl = Url.Action(methodName, controllerName) 
            });
        }
        public IActionResult JsonBadRequest(string messages)
        {
            var controllerName = ControllerContext?.RouteData?.Values["controller"]?.ToString();
            return Json(new
            {
                success = true,
                status = (int)HttpStatusCodeEnum.BadRequest,
                message = messages,
                redirectUrl = Url.Action(GetControllerMethodName(), controllerName) 
            });
        }

        public IActionResult JsonInternalServerError(string messages)
        {
            var controllerName = ControllerContext?.RouteData?.Values["controller"]?.ToString();
            return Json(new
            {
                success = false,
                status = (int)HttpStatusCodeEnum.BadRequest,
                message = messages,
               // redirectUrl = Url.Action(GetControllerMethodName(), controllerName)
            });
        }

        public IActionResult JsonNotFound(string methodName="")
        {
            var controllerName = ControllerContext?.RouteData?.Values["controller"]?.ToString();
            var url = string.IsNullOrEmpty(methodName) ? "" : Url.Action(methodName, controllerName);
            return Json(new
            {
                success = true,
                status = (int)HttpStatusCodeEnum.NotFound,
                message = "Data Not Found!",
                redirectUrl = url
            });
        }

        public string GetControllerMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(2)?.GetMethod();
            return method.Name;
        }

    }


    public enum HttpStatusCodeEnum
    {
        Ok = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500
    }

}
