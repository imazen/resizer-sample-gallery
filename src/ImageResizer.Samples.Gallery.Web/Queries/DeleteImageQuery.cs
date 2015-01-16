using ImageResizer.Samples.Gallery.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;

namespace ImageResizer.Samples.Gallery.Web.Queries
{
    public class DeleteImageQuery
    {
        public List<Image> Images { get; set; }

        public DeleteImageQuery()
        {
            string databasePath = HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
            using (StreamReader r = new StreamReader(databasePath))
            {
                string json = r.ReadToEnd();
                Images = JsonConvert.DeserializeObject<List<Image>>(json);
            }
        }

        public void Execute(Image image)
        {
            Images = Images.Where((img) => img.Id != image.Id).ToList();

            string databasePath = HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
            using (StreamWriter w = new StreamWriter(databasePath))
            {
                string json = JsonConvert.SerializeObject(Images);
                w.Write(json);
            }
        }
    }
}