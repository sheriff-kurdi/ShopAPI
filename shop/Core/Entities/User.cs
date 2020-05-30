using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace shop.Core.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string picUrl { get; set; }
    }

    public class UserLoginVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
       // public string Token { get; set; }
    }

    public class UserVM
    {
        public string UserName { get; set; }
        public string picUrl { get; set; }
        public string Token { get; set; }
    }

}
