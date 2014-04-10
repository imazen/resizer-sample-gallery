using System;

namespace ImageResizer.Samples.Gallery.Web.Models {
    public class Image {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Author { get; set; }
    }
}