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
                
                var image = new Image {
                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(httpPostedFile.FileName),
                    Author = Request["author"]
                };

                var saveImageQuery = new SaveImageQuery();
                saveImageQuery.Execute(httpPostedFile, image);
            }
            
            return RedirectToAction("Index");
        }
    }
}
