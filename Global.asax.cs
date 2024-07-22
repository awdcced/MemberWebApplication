using MemberWebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MemberWebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthorizeRequest(object sender, System.EventArgs e)
        {
            HttpApplication App = (HttpApplication)sender;
            HttpContext ThisContext = App.Context;
            if (ThisContext.Request.IsAuthenticated == true) {
                FormsIdentity Id = (FormsIdentity)ThisContext.User.Identity;
                FormsAuthenticationTicket Ticket = Id.Ticket;
                Users ThisUser = JsonConvert.DeserializeObject<Users>(Ticket.UserData);
                string[] Roles = ThisUser.U_Role.ToString().Split(',');
                ThisContext.User = new GenericPrincipal(Id,Roles);
            }
        }
    }
}
