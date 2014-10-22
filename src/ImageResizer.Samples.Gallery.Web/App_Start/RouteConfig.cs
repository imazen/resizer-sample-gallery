using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ImageResizer.Samples.Gallery.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ImageResizerest",
                url: "imageresizerest/{action}/{id}",
                defaults: new { controller = "ImageResizerest", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "BuyStuff",
                url: "buystuff/{action}/{id}",
                defaults: new { controller = "BuyStuff", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}