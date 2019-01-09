using System;
using System.Web;
using System.Web.Optimization;

namespace SneakerSTVietnamMVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/extendscript").Include(
                "~/Template/Scripts/wow.min.js",
                "~/Template/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Template/Css/main.css",
                      "~/Template/Css/animate.css",
                      "~/Template/Css/Themes/default-theme.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
                 "~/Template/Css/bootstrap.css",
                 "~/Template/Css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/AdminCP/LinearIcons").Include(
            "~/Template/Css/LinearIcons/style.css"));

            bundles.Add(new StyleBundle("~/Content/AdminCP/css").Include(
                "~/Template/Css/Themes/admincp-theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/admincpscripts").Include(
                "~/Template/Scripts/AdminCP/main.js"));

            bundles.Add(new StyleBundle("~/Content/css/superslide").Include(
                "~/Template/Css/superslides.css"));

            bundles.Add(new ScriptBundle("~/Scripts/superslide").Include(
                "~/Template/Scripts/jquery.superslides.min.js"));
        }
    }
}