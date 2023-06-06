using System;
using System.IO;
using System.Web;

namespace arboldecisiones.Classes
{
    public class FilesHelper
    {
        public static string UploadMultimedia(HttpPostedFileBase file, string folder)
        {
            var path = string.Empty;
            var pic = string.Empty;

            if (file != null)
            {
                //pic = Path.GetFileName(file.FileName);                
                pic = DateTime.Now.ToString("yyyyMMddHHmmssffff") + Path.GetExtension(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
                file.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            return pic;
        }
    }
}