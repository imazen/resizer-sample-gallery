using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ImageResizer.Samples.Gallery.Web.Models;
using ImageResizer.Samples.Gallery.Web.Queries;

namespace ImageResizer.Samples.Gallery.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View(new ImageResizer.Samples.Gallery.Web.ViewModels.AdminViewModel());
        }
       
        public ActionResult Delete(Guid id) {
            var q = new GetImageQuery();
            Image img = q.Execute(id);
       
           
            var prefix = Path.GetFileNameWithoutExtension(img.FileName);
            var sourceDir = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Images/Uploads/");

            /*Console.WriteLine("GetFileNameWithoutExtension('{0}') returns '{1}'", 
                fileName, result);

            result = Path.GetFileName(path);
            Console.WriteLine("GetFileName('{0}') returns '{1}'", 
                path, result);
            */

         
            string[] picList = Directory.GetFiles(sourceDir, prefix + "*.*");

            // Copy picture files. 
            foreach (string f in picList)
            {
                System.IO.File.Delete(f);
            }

            var delete = new DeleteImageQuery();
            delete.Execute(img);
            return  RedirectToAction("index", "admin");
        }
        
    }
}