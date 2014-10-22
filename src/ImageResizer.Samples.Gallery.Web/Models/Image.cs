using System;

namespace ImageResizer.Samples.Gallery.Web.Models {
    public class Image {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Author { get; set; }
        public int StoredWidth { get; set; }
        public int StoredHeight { get; set; }
        public string EditQuery { get; set; }
        public string MimeType { get; set; }
        public string Description { get; set; }

        public string GetQueryForCropping(int? defaultWidth)
        {
            var i = new Instructions(EditQuery ?? "");
            i.CropXUnits = null;
            i.CropYUnits = null;
            i.CropRectangle = null;
            i.Width = defaultWidth;
            return i.ToQueryString();
        }
    }
}