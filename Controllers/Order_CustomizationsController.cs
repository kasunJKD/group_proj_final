using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Order_CustomizationsController : Controller
    {
        private readonly WebApplicationDbContext _context;

        public Order_CustomizationsController(WebApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order_Customizations
        public async Task<IActionResult> Index()
        {
            var webApplicationDbContext = _context.Order_Customizations.Include(o => o.Customizations);
            return View(await webApplicationDbContext.ToListAsync());
        }

        // GET: Order_Customizations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order_Customizations == null)
            {
                return NotFound();
            }

            var order_Customizations = await _context.Order_Customizations
                .Include(o => o.Customizations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order_Customizations == null)
            {
                return NotFound();
            }

            return View(order_Customizations);
        }

        // GET: Order_Customizations/Create
        public IActionResult Create()
        {
            var customizations = _context.Customizations.ToList();

            // Populate ViewBag with Customizations data
            ViewBag.CustomizationsId = customizations;

            return View();
        }

        // POST: Order_Customizations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create([FromBody] List<Customizations>rows, int orderId)
        {
            /* if (ModelState.IsValid)
             {
                 _context.Add(order_Customizations);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             ViewData["CustomizationsId"] = new SelectList(_context.Customizations, "Id", "Id", order_Customizations.CustomizationsId);
             return View(order_Customizations);*/

            return Json(new { success = true, message = "Rows submitted successfully" });
        }

        // GET: Order_Customizations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order_Customizations == null)
            {
                return NotFound();
            }

            var order_Customizations = await _context.Order_Customizations.FindAsync(id);
            if (order_Customizations == null)
            {
                return NotFound();
            }
            ViewData["CustomizationsId"] = new SelectList(_context.Customizations, "Id", "Id", order_Customizations.CustomizationsId);
            return View(order_Customizations);
        }

        // POST: Order_Customizations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomizationsId,OrderId")] Order_Customizations order_Customizations)
        {
            if (id != order_Customizations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order_Customizations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Order_CustomizationsExists(order_Customizations.Id))
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
            ViewData["CustomizationsId"] = new SelectList(_context.Customizations, "Id", "Id", order_Customizations.CustomizationsId);
            return View(order_Customizations);
        }

        // GET: Order_Customizations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order_Customizations == null)
            {
                return NotFound();
            }

            var order_Customizations = await _context.Order_Customizations
                .Include(o => o.Customizations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order_Customizations == null)
            {
                return NotFound();
            }

            return View(order_Customizations);
        }

        // POST: Order_Customizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order_Customizations == null)
            {
                return Problem("Entity set 'WebApplicationDbContext.Order_Customizations'  is null.");
            }
            var order_Customizations = await _context.Order_Customizations.FindAsync(id);
            if (order_Customizations != null)
            {
                _context.Order_Customizations.Remove(order_Customizations);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Order_CustomizationsExists(int id)
        {
          return (_context.Order_Customizations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
