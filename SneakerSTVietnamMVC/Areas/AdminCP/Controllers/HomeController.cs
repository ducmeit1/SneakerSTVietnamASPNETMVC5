using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SneakerSTVietnamMVC.Models;
using SneakerSTVietnamMVC.Models.DataView;
using SneakerSTVietnamMVC.Helpers;

namespace SneakerSTVietnamMVC.Areas.AdminCP.Controllers
{
    [AuthorizerController]
    public class HomeController : Controller
    {
        // GET: AdminCP/Home
        [AuthorizerController]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ViewLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string passwordEncrypt = HelpersSet.EncryptMD5(model.Password + model.Email);
            DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();
            User us = db.Users.SingleOrDefault(u => u.Email.Equals(model.Email) && u.Password.Equals(passwordEncrypt));
            if (us != null)
            {
                if (us.RoleID != 1)
                {
                    ViewBag.Message = "Access dinied! Username and password are invalid. Please try again.";
                    return View(model);
                }
                ViewUserDataModel userDataSession = new ViewUserDataModel() { RoleID = us.RoleID, UserID = us.UserID };
                Session["user"] = userDataSession;
                return RedirectToAction("Index", "Role");
            }
            ViewBag.Message = "Username or Password is invalid! Please try again.";
            return View(model);
        }


        public ActionResult Logout()
        {
            if (Session["user"] != null)
            {
                Session.Abandon();
            }
            return RedirectToAction("Index");
        }
    }
}