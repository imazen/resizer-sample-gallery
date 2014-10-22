using ImageResizer.Samples.Gallery.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ImageResizer.Samples.Gallery.Web.ViewModels
{
    public class ImageResizerestViewModel
    {
        public List<Image> Images { get; set; }

        public ImageResizerestViewModel()
        {
            string databasePath = HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
            using (StreamReader r = new StreamReader(databasePath))
            {
                string json = r.ReadToEnd();
                Images = JsonConvert.DeserializeObject<List<Image>>(json);
            }
            Images = Images ?? new List<Image>();

            //var a = new Tuple<int,string>(){ Item1 =5, Item2="hello"};

            //var b = new Utilities().Swap<string,int>(a);
        }
    }


    //public class Tuple<T, V>
    //{
    //    public T Item1 { get; set; }
    //    public V Item2 { get; set; }
    //    public string Title { get; set; }
    //}

    //public class Utilities
    //{
    //    public  Tuple<V, T> Swap<V, T>(Tuple<T, V> pair)
    //    {
    //        return new Tuple<V, T>() { Item1 = pair.Item2, Item2 = pair.Item1 };
    //    }
    //}
}