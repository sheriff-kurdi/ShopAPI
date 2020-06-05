﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YallaMedia.Data;
using YallaMedia.Models;
using YallaMedia.Services.Util;
using YallaMedia.ViewModels;

namespace YallaMedia.Controllers
{
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public BlogsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Blogs For Managment
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.ToListAsync());
        }

        // GET: Blogs For Customer Listing
        public async Task<IActionResult> ListBlogs()
        {
            return View(await _context.Blogs.ToListAsync());
        }
        


        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM model)
        {
            if (ModelState.IsValid)
            {
                string imagePath = ImageHandeler.UploadImage(model.ImageBinary, hostingEnvironment.WebRootPath);

                Blog blog = new Blog
                {
                    Name = model.Name,
                    Content = model.Content,
                    Date = model.Date,
                    ImagePath = imagePath,
                    Writer = model.Writer,
                    Caption = model.Caption
                };

                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            BlogUpdateVM model = new BlogUpdateVM
            {
                ImagePath = blog.ImagePath,
                Name = blog.Name,
                Writer = blog.Writer,
                Id = blog.Id,
                Date = blog.Date,
                Content = blog.Content,
                Caption = blog.Caption
            };
            return View(model);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogUpdateVM blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (blog.ImageBinary != null && blog.ImagePath != null)
                {
                    bool output = ImageHandeler.DeleteImage(blog.ImagePath, hostingEnvironment.WebRootPath);
                    blog.ImagePath = ImageHandeler.UploadImage(blog.ImageBinary, hostingEnvironment.WebRootPath);
                }


                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog.ImagePath != null)
            {
                ImageHandeler.DeleteImage(blog.ImagePath, hostingEnvironment.WebRootPath);

            }



            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
//TODO Blogs Crud Auth
//TODO design
//TODO Roles


