using ImageResizer.Samples.Gallery.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ImageResizer.Samples.Gallery.Web.ViewModels
{
    public class PurchasePageViewModel
    {
        public List<Image> Images { get; set; }

        public PurchasePageViewModel()
        {
            string databasePath = HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
            using (StreamReader r = new StreamReader(databasePath))
            {
                string json = r.ReadToEnd();
                Images = JsonConvert.DeserializeObject<List<Image>>(json);
            }
            Images = Images ?? new List<Image>();

        }
    }

}