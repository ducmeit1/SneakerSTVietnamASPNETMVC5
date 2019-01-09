using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SneakerSTVietnamMVC.Models.DataView;
using SneakerSTVietnamMVC.Helpers;
using SneakerSTVietnamMVC.Models;

namespace SneakerSTVietnamMVC.Controllers
{
    [AuthorizerController]
    public class HomeController : Controller
    {
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();
        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }

        public ActionResult AccessDenied()
        {
            if (Session["user"] != null)
            {
                ViewUserDataModel user = (ViewUserDataModel)Session["user"];
                if (user.RoleID == 4)
                {
                    ViewBag.Banned = "Your account has been banned! <br />";
                }
            }
            return View();
        }
    }
}