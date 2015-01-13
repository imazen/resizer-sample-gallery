using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ImageResizer.Samples.Gallery.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View(new ImageResizer.Samples.Gallery.Web.ViewModels.AdminViewModel());
        }
       
        /*public ActionResult Delete()
        {

            string sourceDir = @"c:\current";

            try
            {
                string[] picList = Directory.GetFiles(sourceDir, "*.jpg");

                // Copy picture files. 
                foreach (string f in picList)
                {
                    // Remove path from the file name. 
                    string fName = f.Substring(sourceDir.Length + 1);

                    // Use the Path.Combine method to safely append the file name to the path. 
                    // Will overwrite if the destination file already exists.
                    File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
                }

                // Delete source files that were copied. 
                foreach (string f in txtList)
                {
                    File.Delete(f);
                }
                foreach (string f in picList)
                {
                    File.Delete(f);
                }
            }

            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
        }*/

    }
}