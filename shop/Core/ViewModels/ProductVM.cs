using Microsoft.AspNetCore.Http;
using shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop.Core.ViewModels
{
    public class ProductVM : Product
    {
       
        public IFormFile photoBin { get; set; }


    }
}
