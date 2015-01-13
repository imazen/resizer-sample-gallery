using ImageResizer;
using ImageResizer.Samples.Gallery.Web.Models;
using ImageResizer.Samples.Gallery.Web.Queries;
using ImageResizer.Samples.Gallery.Web.Services;
using ImageResizer.Samples.Gallery.Web.ViewModels;
using System;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

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
            Image image = null;

            if (Request.Files.Keys.Count == 1) {
                string name = Request.Files.Keys[0];
                var httpPostedFile = Request.Files[name];

                //Get image dimensions
                httpPostedFile.InputStream.Seek(0, SeekOrigin.Begin);
                var originalInfo = ImageResizer.ImageBuilder.Current.LoadImageInfo(httpPostedFile, null);
               
              
                // Save the original file
                var imageUploader = new ImageUploader();
                var fileName = imageUploader.SaveUploadedFileSafely(
                    baseDir: "~/Content/Images/Uploads/",
                    uploadFile: httpPostedFile,
                    whitelistedFormats: new string[] {
                        "gif", "jpg", "png", "tif"
                    }
                );

                

                image = new Image {
                    Id = new Guid(Path.GetFileNameWithoutExtension(fileName)),
                    FileName = fileName,
                    Author = Request["author"],
                    Description = Request["description"],
                    StoredWidth = (int)originalInfo["source.width"],
                    StoredHeight = (int)originalInfo["source.height"],
                };

                // Limit size, convert to jpg, and autorotate
                httpPostedFile.InputStream.Seek(0, SeekOrigin.Begin);
                var imageJob = new ImageJob(
                    httpPostedFile,
                    "~/Content/Images/Uploads/" + image.Id.ToString("N", NumberFormatInfo.InvariantInfo) + "_500x500.<ext>",
                    new Instructions("width=500;height=500;format=jpg;mode=max;autorotate=true;"));
                imageJob.CreateParentDirectory = true;
                imageJob.Build();

                //Save image record to database
                var saveImageQuery = new SaveImageQuery();
                saveImageQuery.Execute(image);

                return RedirectToAction("crop", "images", new RouteValueDictionary {
                    { "id", image.Id }
                });
            }
            //TODO: they didn't attach a file. Complain, or something.
            return RedirectToAction("index", "home");
        }
    }
}
