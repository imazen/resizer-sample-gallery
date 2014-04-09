using ImageResizer.Samples.Gallery.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ImageResizer.Samples.Gallery.Web.Queries {
    public class SaveImageQuery {
        public List<Image> Images { get; set; }

        public SaveImageQuery() {
            string databasePath = HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
            using (StreamReader r = new StreamReader(databasePath)) {
                string json = r.ReadToEnd();
                Images = JsonConvert.DeserializeObject<List<Image>>(json);
            }
        }

        public void Execute(Image image) {
            Images.Add(image);

            string databasePath = HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
            using (StreamWriter w = new StreamWriter(databasePath)) {
                string json = JsonConvert.SerializeObject(Images);
                w.Write(json);
            }
        }
    }
}