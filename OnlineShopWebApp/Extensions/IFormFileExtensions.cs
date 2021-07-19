using System.Drawing;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing.Drawing2D;

namespace OnlineShopWebApp.Extensions
{
    public static class IFormFileExtensions
    {
        public static Image ResizeImage(this IFormFile imageFile, int width, int heigth)
        {
            using(var stream = new MemoryStream())
            {
                imageFile.CopyTo(stream);
                using (var image = Image.FromStream(stream))
                {
                    return ResizeImage(image, width, heigth);
                }
            }
        }

        private static Image ResizeImage(Image image, int width, int heigth)
        {
            var bitmap = new Bitmap(width, heigth);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.DrawImage(image, 0, 0, width, heigth);
            }
            return bitmap;
        }
    }
}
