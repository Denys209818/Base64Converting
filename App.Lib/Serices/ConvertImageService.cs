using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace App.Lib.Serices
{
    public static class ConvertImageService
    {
        public static string ConvertToBase64(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                using (Image bmp = Bitmap.FromFile(path))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bmp.Save(ms, bmp.RawFormat);
                        string result = Convert.ToBase64String(ms.ToArray());
                        return result;
                    }
                }
            }
            return String.Empty;
        }

        public static Bitmap ConvertToBitmap(string base64)
        {
            if (!string.IsNullOrEmpty(base64))
            {
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64)))
                {
                    using (Image image = Image.FromStream(ms))
                    {
                        return new Bitmap(image);
                    }
                }
            }
            return new Bitmap(50, 50);
        }
    }
}
