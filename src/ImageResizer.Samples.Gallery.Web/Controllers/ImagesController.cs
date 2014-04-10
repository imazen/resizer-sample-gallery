using ImageResizer.Samples.Gallery.Web.Models;
using ImageResizer.Samples.Gallery.Web.Queries;
using ImageResizer.Samples.Gallery.Web.ViewModels;
using System;
using System.Web.Mvc;

namespace ImageResizer.Samples.Gallery.Web.Controllers {
    public class ImagesController : Controller {
        public ActionResult Crop(Guid id) {
            var q = new GetImageQuery();
            Image image = q.Execute(id);
            return View(new CropViewModel { Image = image });
        }
    }
}
