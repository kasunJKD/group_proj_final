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
    public class CatalogController : Controller
    {
        private readonly WebApplicationDbContext _context;

        public CatalogController(WebApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Catalog
        public async Task<IActionResult> Index()
        {
            try
            {
                var planeModels = await _context.PlaneModels
                    .Include(p => p.EngineInventory)
                    .Include(p => p.FuselageInventory)
                    .Include(p => p.WingsInventory)
                    .Where(p =>
                        p.FuselageInventory.AvailableCount > 5 &&
                        p.WingsInventory.AvailableCount >= p.Wing_Count &&
                        p.EngineInventory.AvailableCount >= p.Engine_Count)
                    .ToListAsync();

                return View(planeModels);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return View(new List<PlaneModels>());
            }
        }

        // GET: Catalog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlaneModels == null)
            {
                return NotFound();
            }

            var planeModels = await _context.PlaneModels
                .Include(p => p.EngineInventory)
                .Include(p => p.FuselageInventory)
                .Include(p => p.WingsInventory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planeModels == null)
            {
                return NotFound();
            }

            return View(planeModels);
        }

        // GET: Catalog/Create
        public IActionResult Create()
        {
            ViewData["EngineInventoryId"] = new SelectList(_context.Inventory, "Id", "Name");
            ViewData["FuselageInventoryId"] = new SelectList(_context.Inventory, "Id", "Name");
            ViewData["WingsInventoryId"] = new SelectList(_context.Inventory, "Id", "Name");
            return View();
        }

        // POST: Catalog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,FuselageInventoryId,WingsInventoryId,Wing_Count,EngineInventoryId,Engine_Count,Max_Range,Length,Width,BasePrice,Image_url")] PlaneModels planeModels)
        {
            try
            {
                _context.Add(planeModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["EngineInventoryId"] = new SelectList(_context.Inventory, "Id", "Name", planeModels.EngineInventoryId);
                ViewData["FuselageInventoryId"] = new SelectList(_context.Inventory, "Id", "Name", planeModels.FuselageInventoryId);
                ViewData["WingsInventoryId"] = new SelectList(_context.Inventory, "Id", "Name", planeModels.WingsInventoryId);
                return View(planeModels);
            }
        }

        // GET: Catalog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlaneModels == null)
            {
                return NotFound();
            }

            var planeModels = await _context.PlaneModels.FindAsync(id);
            if (planeModels == null)
            {
                return NotFound();
            }
            ViewData["EngineInventoryId"] = new SelectList(_context.Inventory, "Id", "Name", planeModels.EngineInventoryId);
            ViewData["FuselageInventoryId"] = new SelectList(_context.Inventory, "Id", "Name", planeModels.FuselageInventoryId);
            ViewData["WingsInventoryId"] = new SelectList(_context.Inventory, "Id", "Name", planeModels.WingsInventoryId);
            return View(planeModels);
        }

        // POST: Catalog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id,PlaneModels planeModels)
        {
            if (id != planeModels.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(planeModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaneModelsExists(planeModels.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["EngineInventoryId"] = new SelectList(_context.Inventory, "Id", "Name", planeModels.EngineInventoryId);
            ViewData["FuselageInventoryId"] = new SelectList(_context.Inventory, "Id", "Name", planeModels.FuselageInventoryId);
            ViewData["WingsInventoryId"] = new SelectList(_context.Inventory, "Id", "Name", planeModels.WingsInventoryId);
            return View(planeModels);
        }

        // GET: Catalog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlaneModels == null)
            {
                return NotFound();
            }

            var planeModels = await _context.PlaneModels
                .Include(p => p.EngineInventory)
                .Include(p => p.FuselageInventory)
                .Include(p => p.WingsInventory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planeModels == null)
            {
                return NotFound();
            }

            return View(planeModels);
        }

        // POST: Catalog/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlaneModels == null)
            {
                return Problem("Entity set 'WebApplicationDbContext.PlaneModels'  is null.");
            }
            var planeModels = await _context.PlaneModels.FindAsync(id);
            if (planeModels != null)
            {
                _context.PlaneModels.Remove(planeModels);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaneModelsExists(int id)
        {
          return (_context.PlaneModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
