using ImageResizer.Samples.Gallery.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageResizer.Samples.Gallery.Web.Queries {
    public class GetImageQuery {
        private List<Image> Images { get; set; }

        public GetImageQuery() {
            string databasePath = HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
            using (StreamReader r = new StreamReader(databasePath)) {
                string json = r.ReadToEnd();
                Images = JsonConvert.DeserializeObject<List<Image>>(json);
            }
        }

        public Image Execute(Guid id) {
            return Images.Where(i => i.Id == id).SingleOrDefault();
        }
    }
}