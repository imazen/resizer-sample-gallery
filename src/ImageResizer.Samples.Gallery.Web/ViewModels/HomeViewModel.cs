using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageResizer.Samples.Gallery.Web.ViewModels {
    public class HomeViewModel {

        public List<Image> Images { get; set; }
        
        public HomeViewModel() {
            string databasePath = HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
            using (StreamReader r = new StreamReader(databasePath)) {
                string json = r.ReadToEnd();
                Images = JsonConvert.DeserializeObject<List<Image>>(json);
            }
        }
    }

    public class Image {
        public string FileName { get; set; }
        public string Author { get; set; }
    }

    public class SaveImageQuery {
        public List<Image> Images { get; set; }
        
        public SaveImageQuery() {
            string databasePath = HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
            using (StreamReader r = new StreamReader(databasePath))
            {
                string json = r.ReadToEnd();
                Images = JsonConvert.DeserializeObject<List<Image>>(json);
            }
        }

        public void Execute(HttpPostedFileBase httpPostedFile, Image image) {
            var uploadDirectory = HttpContext.Current.Server.MapPath("~/Content/Images/Uploads/");
            httpPostedFile.SaveAs(uploadDirectory + image.FileName);
            Images.Add(image);

            string databasePath = HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
            using (StreamWriter w = new StreamWriter(databasePath)) {
                string json = JsonConvert.SerializeObject(Images);
                w.Write(json);
            }
        }
    }
}