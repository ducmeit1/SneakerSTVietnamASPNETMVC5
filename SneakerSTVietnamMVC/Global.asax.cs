using SneakerSTVietnamMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SneakerSTVietnamMVC
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

        protected void Session_Start()
        {
            Session["user"] = null;
            Session["basket"] = null;
            Console.WriteLine(Session.SessionID + " Created");
        }

        protected void Session_End()
        {
            Console.WriteLine(Session.SessionID + " Destroyed");
            new SessionController().SessionDestroyed();
        }
    }
}
