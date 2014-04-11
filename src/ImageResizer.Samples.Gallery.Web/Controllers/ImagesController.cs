using ImageResizer;
using ImageResizer.Samples.Gallery.Web.Models;
using ImageResizer.Samples.Gallery.Web.Queries;
using ImageResizer.Samples.Gallery.Web.ViewModels;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace ImageResizer.Samples.Gallery.Web.Controllers {
    public class ImagesController : Controller {
        [HttpGet]
        public ActionResult Crop(Guid id) {
            var q = new GetImageQuery();
            Image image = q.Execute(id);
            return View(new CropViewModel { Image = image });
        }

        [HttpPost]
        public ActionResult Crop(Guid id, string cropUrl) {
            var q = new GetImageQuery();
            Image image = q.Execute(id);
            ImageBuilder.Current.Build("~/" + Util.PathUtils.RemoveQueryString(cropUrl), "~/Content/Images/Uploads/" + image.Id.ToString("N", NumberFormatInfo.InvariantInfo) + "_cropped.jpg", new Instructions(cropUrl));
            return RedirectToAction("Crop");
        }
    }
}
