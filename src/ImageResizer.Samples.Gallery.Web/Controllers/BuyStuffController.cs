using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageResizer.Samples.Gallery.Web.Controllers
{
    public class BuyStuffController : Controller
    {
        // GET: BuyStuff
        public ActionResult Index()
        {
            return View(new ImageResizer.Samples.Gallery.Web.ViewModels.BuyStuffViewModel());
        }
    }
}