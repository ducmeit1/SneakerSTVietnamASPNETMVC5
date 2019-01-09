using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SneakerSTVietnamMVC.Models;
using SneakerSTVietnamMVC.Models.DataView;
using SneakerSTVietnamMVC.Models.DAO;
using SneakerSTVietnamMVC.Helpers;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SneakerSTVietnamMVC.Controllers
{
    [AuthorizerController]
    public class AccountController : Controller
    {
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();
        public ActionResult Index()
        {
            ViewUserDataModel us = (ViewUserDataModel)Session["user"];
            ViewBag.Invoice = db.Invoices.Where(m => m.UserID == us.UserID).OrderBy(m => m.InvoiceID).Take(5).ToList();
            return View(db.Users.Find(us.UserID));
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "Password")]ViewChangePassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ViewUserDataModel us = (ViewUserDataModel)Session["user"];
            User user = db.Users.Find(us.UserID);
            if (user == null)
            {
                return View("Error");
            }
            string currentPassword = HelpersSet.EncryptMD5(model.CurrentPassword + user.Email);
            if (!currentPassword.Equals(user.Password))
            {
                ViewBag.Message = "Current password is invalid. Try again!";
                return View(model);
            }
            if (!model.NewPassword.Equals(model.ConfirmNewPassword))
            {
                ViewBag.Message = "New password and new confirm password must be equals (same). Try again!";
                return View(model);
            }
            string newPassword = HelpersSet.EncryptMD5(model.NewPassword + user.Email);
            if (currentPassword.Equals(newPassword))
            {
                ViewBag.Message = "Current password and new password is equals (same). Try again!";
                return View(model);
            }
            user.Password = newPassword;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.Message = "Your password is changed!";
            return View();
        }

        public ActionResult EditAccount()
        {
            ViewUserDataModel us = (ViewUserDataModel)Session["user"];
            User user = db.Users.Find(us.UserID);
            if (user == null)
            {
                return View("Error");
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount(User user)
        {
            if (ModelState.IsValid)
            {
                ViewUserDataModel us = (ViewUserDataModel)Session["user"];
                User newUser = db.Users.Find(user.UserID);
                newUser.State = user.State;
                newUser.City = user.City;
                newUser.Address = user.Address;
                newUser.Country = user.Country;
                newUser.ReceiveEmail = user.ReceiveEmail;
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.PhoneNumber = user.PhoneNumber;
                db.Entry(newUser).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Updated Your Account";
                return View(newUser);
            }
            return View(user);
        }

        public ActionResult ViewInvoice()
        {
            ViewUserDataModel us = (ViewUserDataModel)Session["user"];
            ViewBag.Invoice = db.Invoices.Where(m => m.UserID == us.UserID).OrderBy(m => m.InvoiceID).ToList();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ViewLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string passwordEncrypt = HelpersSet.EncryptMD5(model.Password + model.Email);
            User us = db.Users.SingleOrDefault(u => u.Email.Equals(model.Email) && u.Password.Equals(passwordEncrypt));
            if (us != null)
            {
                if (us.Active == false)
                {
                    ViewBag.Confirm = String.Format("Your account isn't confirm email. You need to confirm your email before login. <br />Please click <a href=\'{0}\' title=\'Sent Confirm Email\'> here</a> to send email confirm.", Url.Action("SendConfirmEmail", "Account"));
                    return View(model);
                }
                ViewUserDataModel userDataSession = new ViewUserDataModel() { RoleID = us.RoleID, UserID = us.UserID };
                Session["user"] = userDataSession;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Username or Password is invalid! Please try again.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ViewRegisterModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (new UserDAO().FindEmail(user.Email))
            {
                ViewBag.DuplicateEmail = "This email is already exist!";
                return View(user);
            }
            string securityCodeHashMD5 = HelpersSet.EncryptMD5(HelpersSet.GenerateRandomString()) + HelpersSet.GenerateRandomString();
            user.Password = HelpersSet.EncryptMD5(user.Password + user.Email);
            if (new UserDAO().RegisterNewUser(user, securityCodeHashMD5))
            {
                ViewConfirmModel vcm = new UserDAO().GetConfirmInformation(user.Email);
                if (vcm.Active == false)
                {
                    var callBackUrl = Url.Action("ConfirmEmail", "Account", new { email = user.Email, userid = vcm.UserID, code = vcm.SecurityCode }, protocol: Request.Url.Scheme);
                    string messageEmail = String.Format("Hi {0}, <br /> Thank you for registed at SneakerST Vietnam. <br/> Please confirm your email before login by clicking <a href=\'{1}\' title=\'Confirm your email\'>here</a>. <br />Best regards.", user.FirstName, callBackUrl);
                    if (!HelpersSet.SendEmail(user.Email, "Confirm Email Address - SneakerST Vietnam", messageEmail))
                    {
                        new UserDAO().DeleteRegisterDataFailed(user.Email);
                        return View("Error");
                    }
                }
                return RedirectToAction("ConfirmRegister", "Account", new { email = user.Email });
            }
            return View("Error");
        }
        public ActionResult ConfirmRegister(string email)
        {
            if (email == null) return View("Error");
            ViewBag.Email = email;
            return View();
        }
        public ActionResult SendConfirmEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendConfirmEmail(ViewEmailConfirm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ViewConfirmModel vcm = new UserDAO().GetConfirmInformation(model.Email);
            if (vcm == null)
            {
                ViewBag.NotFound = "This account is not exist!";
                return View();
            }
            if (vcm.Active == false)
            {
                var callBackUrl = Url.Action("ConfirmEmail", "Account", new { userId = vcm.UserID, code = vcm.SecurityCode }, protocol: Request.Url.Scheme);
                string messageEmail = String.Format("Hi, <br /> Thank you for registed at SneakerST Vietnam. <br/> Please confirm your email before login by clicking <a href=\'{0}\' title=\'Confirm your email\'>here</a>. <br />Best regards.", callBackUrl);

                if (!HelpersSet.SendEmail(model.Email, "Confirm Email Address - SneakerST Vietnam", messageEmail)) return View("Error");
            }
            else
            {
                ViewBag.Confirmed = "Your email address confirmed. You needn't confirm it again!";
                return View();
            }
            ViewBag.Success = "An email confirm from us sent to your email address. Please check it!";
            return View();
        }

        public ActionResult ConfirmEmail(int? userId, string code)
        {
            if (userId == null || code == null) return View("Error");
            ViewActiveModel vam = null;
            try
            {
                vam = new UserDAO().UserIsActive(userId.Value, code);
            }
            catch (Exception)
            {
                return View("Error");
            }
            if (vam != null)
            {
                if (vam.Active == false)
                {
                    if (new UserDAO().UpdateActiveUser(vam.UserID))
                    {
                        ViewBag.Success = "Your email address confirmed!";
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    ViewBag.Error = "Your email address confirmed. You needn't confirm it again!";
                }
            }
            else
            {
                ViewBag.Error = "Incorrect information to confirm your email! Please try again.";
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["user"] != null)
            {
                if (Session["basket"] != null)
                {
                    new SessionController().SessionDestroyed();
                }
                Session.Abandon();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}