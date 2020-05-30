using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Entities;
using shop.Services.Utilties;

namespace shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public ImageUploadController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        public class FileUploadAPI
        {
            public IFormFile files { get; set; }
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult<Product> UploadFile()
        {
            //get From header the files
            //article https://www.talkingdotnet.com/upload-file-angular-5-asp-net-core-2-1-web-api/
            var file = Request.Form.Files[0];

            var http = Request;
            string webRootPath = environment.WebRootPath;

            var imagePath = FileHttp.HttpUploadFile(file, webRootPath);
            Product p = new Product
            {
                PicPath = imagePath,
                Name = Request.Form["Name"]
            };

            return p;


        }
    }
}