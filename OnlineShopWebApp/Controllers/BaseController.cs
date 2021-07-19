using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using System;

namespace OnlineShopWebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string GetUserId()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                if (!HttpContext.Request.Cookies.ContainsKey(Constants.UnsignedUserId))
                {
                    var unsignedUserId = Guid.NewGuid().ToString();
                    HttpContext.Response.Cookies.Append(Constants.UnsignedUserId, unsignedUserId);
                    return unsignedUserId;
                }
                return HttpContext.Request.Cookies[Constants.UnsignedUserId];
            }
            return HttpContext.User.Identity.Name;
        }
    }
}
