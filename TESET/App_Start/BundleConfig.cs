﻿using System.Web;
using System.Web.Optimization;

namespace TESET
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

           
            
            bundles.Add(new StyleBundle("~/Content/capo").Include(
         "~/Content/bootstrap-table.min.css",
         "~/Content/bootstrap-modal.css",
         "~/Content/bootstrap-modal-bs3patch.css",
         "~/Content/dataTables.bootstrap.css"
         ));

            bundles.Add(new ScriptBundle("~/bundles/ViewUsers").Include(
             "~/Scripts/MyScripts/ViewUsers.js"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-modal").Include(
             "~/Scripts/bootstrap-modal.js",
             "~/Scripts/bootstrap-modalmanager.js"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/jquery-dataTables").Include(
                         "~/Scripts/jquery.dataTables.js",
                         "~/Scripts/dataTables.bootstrap.js",
                         "~/Scripts/fnReloadAjax.js"
                    ));

        }
    }
}
