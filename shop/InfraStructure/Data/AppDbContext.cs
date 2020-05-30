using Microsoft.EntityFrameworkCore;
using shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop.InfraStructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product>  products { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }

    }
}

//TODO dynamic <T> IRepo
//TODO emplement dataAcess