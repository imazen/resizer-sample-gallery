using ImageResizer.Samples.Gallery.Web.Services;
using ImageResizer.Samples.Gallery.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageResizer.Samples.Gallery.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(new HomeViewModel());
        }

        public ActionResult Upload()
        {
            foreach (string name in Request.Files.Keys) {
                var httpPostedFile = Request.Files[name];

                var imageUploader = new ImageUploader();
                var fileName = imageUploader.SaveUploadedFileSafely(
                    baseDir: "~/Content/Images/Uploads/",
                    uploadFile: httpPostedFile,
                    whitelistedFormats: new string[] {
                        "gif", "jpg", "png", "tif"
                    }
                );
                
                var image = new Image {
                    FileName = fileName,
                    Author = Request["author"]
                };

                var saveImageQuery = new SaveImageQuery();
                saveImageQuery.Execute(image);
            }
            
            return RedirectToAction("Index");
        }
    }
}
