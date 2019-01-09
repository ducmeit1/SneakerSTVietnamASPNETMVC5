using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SneakerSTVietnamMVC.Models;
using SneakerSTVietnamMVC.Models.DataView;

namespace SneakerSTVietnamMVC.Helpers
{
    public class AuthorizerController : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            String[] businessListName = GetBusinessDataListBySession();
            String[] businessListCantAccessIfLogined = { "Login", "Register", "SendConfirmEmail", "ConfirmRegister", "ConfirmEmail" };
            string actionName = filterContext.Controller.ToString() + "-" + filterContext.ActionDescriptor.ActionName;
            Console.WriteLine(actionName);
            if (HttpContext.Current.Session["user"] != null)
            {
                ViewUserDataModel user = (ViewUserDataModel)HttpContext.Current.Session["user"];
                if (user.RoleID == 1 && actionName.Contains("SneakerSTVietnamMVC.Areas.AdminCP.Controllers.HomeController-Index"))
                {
                    filterContext.Result = new RedirectResult("~/AdminCP/Role");
                }
                foreach (string x in businessListCantAccessIfLogined)
                {
                    if (actionName.Contains(x)) filterContext.Result = new RedirectResult("~/Home");
                }
            }
            if (!businessListName.Contains(actionName))
            {
                filterContext.Result = new RedirectResult("~/Home/AccessDenied");
            }
        }

        public String[] GetBusinessDataListBySession()
        {
            List<string> businessListName = new List<string>();
            List<BusinessDataAuthorizeView> businessData = null;
            BusinessDAO businessDAO = new BusinessDAO();
            if (HttpContext.Current.Session["user"] == null)
            {
                businessData = businessDAO.GetBusinessDataNameForAuthorizeByRoleID(3);
            }
            else
            {
                ViewUserDataModel user = (ViewUserDataModel)HttpContext.Current.Session["user"];
                businessData = businessDAO.GetBusinessDataNameForAuthorizeByRoleID(user.RoleID);
            }
            if (businessData != null)
            {
                foreach (BusinessDataAuthorizeView b in businessData)
                {
                    businessListName.Add(b.AreaName +"."+ b.BusinessName);
                }
            }
            return businessListName.ToArray();
        }
    }
}
