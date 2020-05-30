using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop.InfraStructure.Data;
using shop.Core.Entities;
using shop.Services.Utilties;

namespace shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ProductsController(AppDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }




        // GET: api/Products
        [HttpGet]
       [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> Getproducts()
        {
            return await _context.products.ToListAsync();
        }




        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }





        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id)
        {
            //no  you gonna path it in form data

            // find and get the product by  id
            var product = await _context.products.FindAsync(id);

            // checked if it exixt
            if (id != int.Parse(Request.Form["id"]))
            {
                return NotFound();
            }
            if (product == null)
            {
                return NotFound();
            }


            // old image path
            string picPath = product.PicPath;  
            
            // check if user update image
            if (Request.Form.Files.Count > 0)
            {
                string webRootPath = hostingEnvironment.WebRootPath;

                //delete previos image
                FileHttp.HttpDeleteFile(product.PicPath, webRootPath);
                //add new image
                var file = Request.Form.Files[0];
                picPath = FileHttp.HttpUploadFile(file, webRootPath);

                //  Edit image path
                picPath = "Upload/" + picPath;
            }

  
            product.Name = Request.Form["name"];
            product.Price = int.Parse(Request.Form["price"]);
            product.PicPath = picPath;


            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


      


        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct()
        {

            //file upload
            var file = Request.Form.Files[0];
            string webRootPath = hostingEnvironment.WebRootPath;
            string picPath = FileHttp.HttpUploadFile(file, webRootPath);
            picPath = "Upload/" + picPath;
            Product product = new Product
            {
                Name = Request.Form["name"],
                PicPath = picPath ,
                Price = int.Parse(Request.Form["price"]),
                CategoryName = Request.Form["categoryName"]
            };

            _context.products.Add(product);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            return Ok();
        }




        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            //TODO Delete image from folder to
            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            string webRootPath = hostingEnvironment.WebRootPath;
            FileHttp.HttpDeleteFile(product.PicPath, webRootPath);

            _context.products.Remove(product);
            await _context.SaveChangesAsync();

            //return product;
	    return NoContent();
        }



        private bool ProductExists(int id)
        {
            return _context.products.Any(e => e.Id == id);
        }
    }
}
