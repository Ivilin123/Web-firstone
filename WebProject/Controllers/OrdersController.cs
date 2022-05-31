using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        //private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context, UserManager<User> userManager)
        {

            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var model = _context.Orders
                .Include(o => o.Product)
                .Include(o => o.User);

            return View(await model.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            OrdersVM model = new OrdersVM();
           
            model.UserId = _userManager.GetUserId(User);
            model.Products = _context.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = (p.Id == model.ProductId)
            }
            ).ToList();
          

           
            return View(model);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProductId,AmountOrdered,PriceOrder,OrderedOn")] OrdersVM order)
        {
            if (!ModelState.IsValid)
            {
                
                OrdersVM model = new OrdersVM();
                model.UserId = _userManager.GetUserId(User);
                model.Products = _context.Products.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString(),
                    Selected = (p.Id == model.ProductId)
                }
                ).ToList();
                return View(model);
            }
            Order modelToDB = new Order
            {
                ProductId = order.ProductId,
                UserId = _userManager.GetUserId(User),
                AmountOrdered = order.AmountOrdered,
                PriceOrder = order.PriceOrder,
                OrderedOn = order.OrderedOn
            };
            _context.Add(modelToDB);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }
            OrdersVM model = new OrdersVM();
            model.Id= order.Id; 
            model.ProductId = order.ProductId;
            model.AmountOrdered = order.AmountOrdered;
            model.PriceOrder = order.PriceOrder;
            model.OrderedOn = order.OrderedOn;
            model.Products = _context.Products.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name,
                Selected = p.Id == model.ProductId
            }).ToList();


            return View(model);


        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,AmountOrdered,PriceOrder,OrderedOn")] OrdersVM order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            Order modelToDB = await _context.Orders.FindAsync(id);
            if (modelToDB == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(order);
            }

              
            modelToDB.ProductId = order.ProductId;
            modelToDB.UserId = _userManager.GetUserId(User);
            modelToDB.AmountOrdered = order.AmountOrdered;
            modelToDB.PriceOrder = order.PriceOrder;
            modelToDB.OrderedOn = order.OrderedOn;
            try
            {
                _context.Update(modelToDB);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(modelToDB.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return RedirectToAction("Details", new { id = id });
           
            // return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
