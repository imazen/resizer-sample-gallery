using ImageResizer.Configuration;
using ImageResizer.Plugins;
using ImageResizer.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;

namespace ImageResizer.Samples.Gallery.Web.Services {
    public class ImageUploader {
        Config c;

        public ImageUploader() : this(Config.Current) {
        }

        public ImageUploader(Config c) {
            this.c = c;
        }

        /// <summary>
        /// Returns the name used for the file, without any path information. Name will be in "guid.ext" form. Will create any intermediate directories required.
        /// </summary>
        /// <param name="baseDir"></param>
        /// <param name="uploadFile"></param>
        /// <param name="unrecognizedImageExtension"></param>
        /// <param name="whitelistedFormats"></param>
        /// <returns></returns>
        public string SaveUploadedFileSafely(string baseDir, HttpPostedFileBase uploadFile, string unrecognizedImageExtension = ".unknown", string[] whitelistedFormats = null) {
            if (uploadFile == null) throw new ArgumentException("uploadFile is required. Ensure you are passing in an HttpPostedFile instance or similar");

            var uploadPath = uploadFile.FileName;
            var uploadStream = uploadFile.InputStream;

            var name = GenerateSafeImageName(uploadStream, uploadPath, unrecognizedImageExtension, whitelistedFormats);

            var dir = PathUtils.MapPathIfAppRelative(baseDir);

            var finalpath = Path.Combine(dir, name);

            string dirName = Path.GetDirectoryName(finalpath);
            if (!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);

            uploadFile.SaveAs(finalpath);

            return name;
        }

        private string GetExtension(string path) {
            int lastDot = path.LastIndexOfAny(new char[] { '.', '/', ' ', '\\', '?', '&', ':' });
            
            if (lastDot > -1 && path[lastDot] == '.') return path.Substring(lastDot + 1);
            
            return null;
        }

        private bool IsExtensionWhitelisted(string ext, string[] whitelistedFormats = null) {
            if (whitelistedFormats == null) return c.Pipeline.IsAcceptedImageType("." + ext);

            foreach (var f in whitelistedFormats)
                if (ext.Equals(f, StringComparison.OrdinalIgnoreCase)) return true;

            return false;
        }

        private string NormalizeExtension(string extension) {
            if (extension == null) return null;

            extension = extension.ToLowerInvariant();

            var mapping = new Dictionary<string, string> {
                {"jpeg","jpg"},
                {"jpe","jpg"},
                {"jif","jpg"},
                {"jfif","jpg"},
                {"jfi","jpg"},
                {"exif","jpg"},
                {"tiff","tif"},
                {"tff","tif"},
            };

            return mapping.ContainsKey(extension) ? mapping[extension] : extension;
        }

        private FileSignature GuessFileTypeBySignature(Stream s) {
            if (!(s.CanRead && s.CanSeek)) throw new ArgumentException("Stream must be seekable in order to guess the file type");

            List<FileSignature> signatures = new List<FileSignature>();
            foreach (var p in c.Plugins.GetAll<IFileSignatureProvider>())
                signatures.AddRange(p.GetSignatures());

            if (signatures.Count == 0) return null; //No signatures to compare!!

            //Sort by length, longest first
            signatures.Sort((FileSignature a, FileSignature b) => {
                return b.Signature.Length.CompareTo(a.Signature.Length);
            });

            //Copy the longest signature we may need to compare
            byte[] buffer = new byte[signatures[0].Signature.Length];
            int bytesRead = s.Read(buffer, 0, buffer.Length);
            s.Seek(bytesRead, SeekOrigin.Current);

            foreach (var sig in signatures) {
                if (bytesRead < sig.Signature.Length) continue; //Signature longer than file
                if (sig.Signature.Length < 1) continue; //Empty signature
                bool matches = true;
                for (int i = 0; i < sig.Signature.Length; i++) {
                    if (sig.Signature[i] != buffer[i]) {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                    return sig;
            }

            return null;
        }

        private string GetWhitelistedExtension(Stream image = null, string originalPath = null, string[] whitelistedFormats = null) {
            FileSignature sig = null;
            if (image != null && image.CanSeek && image.CanRead)
                sig = GuessFileTypeBySignature(image);

            var sigExt = sig != null ? NormalizeExtension(sig.PrimaryFileExtension) : null;

            if (sigExt != null && IsExtensionWhitelisted(sigExt, whitelistedFormats)) return sigExt;

            //Falback to untrusted ppath
            if (originalPath != null) {
                string ext = NormalizeExtension(GetExtension(originalPath));
                if (ext != null & IsExtensionWhitelisted(ext, whitelistedFormats)) return ext;
            }

            return null;
        }

        private string GenerateSafeImageName(Stream image = null, string originalPath = null, string unrecognizedImageExtension = ".unknown", string[] whitelistedFormats = null) {
            var ext = GetWhitelistedExtension(image, originalPath, whitelistedFormats) ?? unrecognizedImageExtension;
            if (ext == null) throw new ArgumentException("The provided image type is not recognized as a whitelisted format");
            
            return Guid.NewGuid().ToString("N", NumberFormatInfo.InvariantInfo) + "." + ext;
        }
    }
}