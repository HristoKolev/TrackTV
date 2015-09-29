namespace TrackTV.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            AddScripts(bundles);
            AddStyles(bundles);
        }

        private static void AddScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.min.js", "~/Scripts/respond.js"));
        }

        private static void AddStyles(BundleCollection bundles)
        {
            string[] paths = { "~/Content/bootstrap.min.css", "~/Content/bootstrap.cosmo.min.css", "~/Content/site.css" };

            bundles.Add(new StyleBundle("~/Content/css").Include(paths));
        }
    }
}