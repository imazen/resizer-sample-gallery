using ImageResizer;
using ImageResizer.Samples.Gallery.Web.Models;
using ImageResizer.Samples.Gallery.Web.Queries;
using ImageResizer.Samples.Gallery.Web.Services;
using ImageResizer.Samples.Gallery.Web.ViewModels;
using System;
using System.IO;
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

                // Save the original file
                var imageUploader = new ImageUploader();
                var fileName = imageUploader.SaveUploadedFileSafely(
                    baseDir: "~/Content/Images/Uploads/",
                    uploadFile: httpPostedFile,
                    whitelistedFormats: new string[] {
                        "gif", "jpg", "png", "tif"
                    }
                );
                
                var image = new Image {
                    Id = new Guid(Path.GetFileNameWithoutExtension(fileName)),
                    FileName = fileName,
                    Author = Request["author"]
                };

                // Limit size, convert to jpg, and autorotate
                httpPostedFile.InputStream.Seek(0, SeekOrigin.Begin);
                var imageJob = new ImageJob(httpPostedFile, "~/Content/Images/Uploads/Modified/<guid>.<ext>", new Instructions("width=500;height=500;format=jpg;mode=max;autorotate=true;"));
                imageJob.CreateParentDirectory = true;
                imageJob.Build();

                var saveImageQuery = new SaveImageQuery();
                saveImageQuery.Execute(image);
            }
            
            return RedirectToAction("Index");
        }
    }
}
