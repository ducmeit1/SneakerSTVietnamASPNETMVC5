using System.Web.Mvc;

namespace SneakerSTVietnamMVC.Areas.AdminCP
{
    public class AdminCPAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminCP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminCP_default",
                "AdminCP/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "SneakerSTVietnamMVC.Areas.AdminCP.Controllers" }
            );
        }
    }
}