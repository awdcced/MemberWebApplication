using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberWebApplication.Controllers
{
    [Authorize(Roles = "1,2")]
    public class ShopAdminController : Controller
    {
        // GET: ShopAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}