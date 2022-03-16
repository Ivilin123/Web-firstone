﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Author).Include(p => p.Category).Include(p => p.Publisher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ProductsVM model = new ProductsVM();

            model.Publisher = _context.Publishers.Select(x => new SelectListItem
            {
                Text = x.NamePublisher,
                Value = x.Id.ToString(),
                Selected = (x.Id == model.PublisherId)
            }
            ).ToList();


            model.Author = _context.Authors.Select(y => new SelectListItem
            {
                Text = y.FirstName,
                Value = y.Id.ToString(),
                Selected = (y.Id == model.AuthorId)
            }
            ).ToList();

            model.Category = _context.Categories.Select(z => new SelectListItem
            {
                Text = z.CategoryName,
                Value = z.Id.ToString(),
                Selected = (z.Id == model.CategoryId)
            }
            ).ToList();


            //ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FirstName");
            // ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            //ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "NamePublisher");
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AuthorId,Covers,Types,PublisherId,PublishingYear,CategoryId,Amount,Summary,ImageURL,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                

               
            }
            ProductsVM model = new ProductsVM();

            model.Publisher = _context.Publishers.Select(x => new SelectListItem
            {
                Text = x.NamePublisher,
                Value = x.Id.ToString(),
                Selected = (x.Id == model.PublisherId)
            }
            ).ToList();


            model.Author = _context.Authors.Select(y => new SelectListItem
            {
                Text = y.FirstName,
                Value = y.Id.ToString(),
                Selected = (y.Id == model.AuthorId)
            }
            ).ToList();

            model.Category = _context.Categories.Select(z => new SelectListItem
            {
                Text = z.CategoryName,
                Value = z.Id.ToString(),
                Selected = (z.Id == model.CategoryId)
            }
            ).ToList();

            return View(model);
            //Product modelToDB = new Product
            // {
            //  PublisherId = product.PublisherId,
            // AuthorId=product.AuthorId,
            // CategoryId=product.CategoryId

            //};

            //ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", product.AuthorId);
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            //ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Id", product.PublisherId);

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", product.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Id", product.PublisherId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AuthorId,Covers,Types,PublisherId,PublishingYear,CategoryId,Amount,Summary,ImageURL,Price")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", product.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Id", product.PublisherId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
