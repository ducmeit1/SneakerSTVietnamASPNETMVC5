using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SneakerSTVietnamMVC.Models;
using SneakerSTVietnamMVC.Helpers;

namespace SneakerSTVietnamMVC.Areas.AdminCP.Controllers
{
    [AuthorizerController]
    public class RoleController : Controller
    {
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();

        // GET: AdminCP/Role
        public ActionResult Index()
        {
            var roleList = db.Roles.AsEnumerable().OrderBy(m => m.RoleID);
            List<SelectListItem> itemsRole = new List<SelectListItem>();
            itemsRole.Add(new SelectListItem()
            {
                Text = "Select Role",
                Value = ""
            });
            foreach (var item in roleList)
            {
                itemsRole.Add(new SelectListItem()
                {
                    Text = item.RoleName,
                    Value = item.RoleID.ToString()
                });
            }
            ViewBag.RoleList = itemsRole;
            return View();
        }

        public string GetBusiness()
        {
            int roleID = int.Parse(Request["roleid"]);
            var businessListGranted = new BusinessDAO().GetBusinessGrantedByRoleID(roleID);
            var businessListNotGranted = new BusinessDAO().GetBusinessNotGrantedByRoleID(roleID);
            var businessData = businessListGranted;
            businessData.AddRange(businessListNotGranted);
            string responseData = "";
            foreach (var item in businessData)
            {
                item.AreaName = item.AreaName == "SneakerSTVietnamMVC.Areas.AdminCP.Controllers" ? "AdminCP" : "Client";
                if (item.IsGranted) responseData += @"<tr><td><input type='checkbox' value=" + item.BusinessID + " checked onchange='UpdateBusinessData(this.value)'/></td>"
                                                 + "<td><b>" + item.BusinessName + "</b></td>"
                                                 + "<td style='color: #ff0000'>" + item.AreaName + "</td>"
                                                 + "<td>" + item.BusinessDescription + "</td></tr>";
                else responseData += @"<tr><td><input type='checkbox' value=" + item.BusinessID + " onchange='UpdateBusinessData(this.value)'/></td>"
                                                 + "<td><b>" + item.BusinessName + "</b></td>"
                                                 + "<td style='color: #ff0000'>" + item.AreaName + "</td>"
                                                 + "<td>" + item.BusinessDescription + "</td></tr>";
            }
            return responseData;
        }

        public string UpdateBusiness()
        {
            int roleID = int.Parse(Request["roleid"]);
            int businessID = int.Parse(Request["businessid"]);
            string responseData = "";
            if (new BusinessDAO().UpdateBusinessRole(businessID, roleID))
            {
                responseData += "<div class='alert alert-success alert-dismissable' id='business'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Updated Business</div>";
            }
            else
            {
                responseData += "<div class='alert alert-danger alert-dismissable' id='business'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Danger: Update Business Failed</div>";
            }
            return responseData;
        }

        //Update Role List
        public ActionResult UpdateRole()
        {
            string[] spacenamesCollect = { "SneakerSTVietnamMVC.Areas.AdminCP.Controllers", "SneakerSTVietnamMVC.Controllers" };
            RefectlectionHelpers r = new RefectlectionHelpers();
            foreach (string sp in spacenamesCollect)
            {
                List<Type> listControllerType = r.GetControllers(sp);
                List<string> listActionOld = db.Businesses.Select(c => c.BusinessName).ToList();
                foreach (var c in listControllerType)
                {
                    List<string> listActionInController = r.GetActions(c);
                    foreach (var a in listActionInController)
                    {

                        if (!listActionOld.Contains(c.Name + "-" + a))
                        {
                            Business b = new Business() { BusinessName = c.Name + "-" + a, BusinessDescription = "No description", AreaName = c.Namespace };
                            db.Businesses.Add(b);
                        }
                    }
                }
            }
            db.SaveChanges();
            TempData["Updated"] = "Updated Business!";
            return RedirectToAction("Index", "Role");
        }
    }
}
