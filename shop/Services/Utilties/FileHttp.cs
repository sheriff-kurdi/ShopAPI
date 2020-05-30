using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace shop.Services.Utilties
{
    public class FileHttp
    {
        public static string HttpUploadFile(IFormFile file, string webRootPath)
        {
            string folderName = "Upload";
            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string uniqFileName = Guid.NewGuid().ToString() + "_" + fileName;
                string fullPath = Path.Combine(newPath, uniqFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return uniqFileName;

            }

            return "nothing";
        }

        public static void HttpDeleteFile(string filePath, string webRootPath)
        {
            filePath = Path.Combine(webRootPath, filePath);
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    throw(ex);
                }
            }
        }
    }

}
