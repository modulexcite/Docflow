using System.Web;
using System.Web.Optimization;

namespace RapidDoc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.ui.widget.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                        "~/Scripts/FileUpload/tmpl.min.js",
                        "~/Scripts/FileUpload/load-image.min.js",
                        "~/Scripts/FileUpload/canvas-to-blob.min.js",
                        "~/Scripts/FileUpload/jquery.blueimp-gallery.min.js",
                        "~/Scripts/FileUpload/jquery.iframe-transport.js",
                        "~/Scripts/FileUpload/jquery.fileupload.js",
                        "~/Scripts/FileUpload/jquery.fileupload-process.js",
                        "~/Scripts/FileUpload/jquery.fileupload-image.js",
                        "~/Scripts/FileUpload/jquery.fileupload-audio.js",
                        "~/Scripts/FileUpload/jquery.fileupload-video.js",
                        "~/Scripts/FileUpload/jquery.fileupload-validate.js",
                        "~/Scripts/FileUpload/jquery.fileupload-ui.js",
                        "~/Scripts/FileUpload/fileUpload.js"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include(
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/locales/bootstrap-datepicker.ru.js",
                        "~/Scripts/locales/bootstrap-datepicker.kk.js",
                        "~/Scripts/GridMvc/gridmvc.min.js",
                        "~/Scripts/GridMvc/gridmvc.lang.ru.js",
                        "~/Scripts/GridMvc/gridmvc.lang.kk.js"));

            bundles.Add(new ScriptBundle("~/bundles/editentity").Include(
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/locales/bootstrap-datepicker.ru.js",
                        "~/Scripts/locales/bootstrap-datepicker.kk.js",
                        "~/Scripts/bootstrap-timepicker.js",
                        "~/Scripts/jquery.bootstrap-duallistbox.js",
                        "~/Scripts/bootstrap-select.js",
                        "~/Scripts/bootstrap-tagsinput.js",
                        "~/Scripts/typeahead.js",
                        "~/Scripts/hogan-{version}.js",
                        "~/Scripts/summernote.min.js",
                        "~/Scripts/summernote_locals/summernote-ru-RU.js",
                        "~/Scripts/summernote_locals/summernote-kk-KZ.js",
                        "~/Scripts/bootstrap-formhelpers-number.js",
                        "~/Scripts/jquery-ui.custom.min.js",
                        "~/Scripts/qrcode.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/bootstrap-datetimepicker.min.js",
                        "~/Scripts/bootstrap-datetimepicker.ru.js",
                        "~/Scripts/bootstrap-datetimepicker.kk.js"));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                        "~/Scripts/fullcalendar.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/scaffolding").Include(
                        "~/Scripts/scaffolding.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                        "~/Scripts/Chart.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                        "~/Scripts/main.js"));
        }
    }
}
