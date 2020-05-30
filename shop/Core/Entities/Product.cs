using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace shop.Core.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string PicPath { get; set; }
        public string CategoryName { get; set; }

    }

}
