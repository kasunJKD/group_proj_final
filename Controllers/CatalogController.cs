using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class CatalogController : Controller
    {
        private readonly WebApplicationDbContext _context;
        private ICatalogRepository _catalogRepository;

        public CatalogController(WebApplicationDbContext context, ICatalogRepository catalogRepository)
        {
            _context = context;
            _catalogRepository = catalogRepository;
        }

        // GET: Catalog
        public async Task<IActionResult> Index()
        {
            var webApplicationDbContext = _context.PlaneModels.Include(p => p.EngineInventory).Include(p => p.FuselageInventory).Include(p => p.WingsInventory);
            return View(await _catalogRepository.GetPlaneModels());
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
            ViewData["EngineInventoryId"] = new SelectList(_context.Inventory, "Id", "Id");
            ViewData["FuselageInventoryId"] = new SelectList(_context.Inventory, "Id", "Id");
            ViewData["WingsInventoryId"] = new SelectList(_context.Inventory, "Id", "Id");
            return View();
        }

        // POST: Catalog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FuselageInventoryId,WingsInventoryId,Wing_Count,EngineInventoryId,Engine_Count,Max_Range,Length,Width,BasePrice,Image_url")] PlaneModels planeModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planeModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EngineInventoryId"] = new SelectList(_context.Inventory, "Id", "Id", planeModels.EngineInventoryId);
            ViewData["FuselageInventoryId"] = new SelectList(_context.Inventory, "Id", "Id", planeModels.FuselageInventoryId);
            ViewData["WingsInventoryId"] = new SelectList(_context.Inventory, "Id", "Id", planeModels.WingsInventoryId);
            return View(planeModels);
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
            ViewData["EngineInventoryId"] = new SelectList(_context.Inventory, "Id", "Id", planeModels.EngineInventoryId);
            ViewData["FuselageInventoryId"] = new SelectList(_context.Inventory, "Id", "Id", planeModels.FuselageInventoryId);
            ViewData["WingsInventoryId"] = new SelectList(_context.Inventory, "Id", "Id", planeModels.WingsInventoryId);
            return View(planeModels);
        }

        // POST: Catalog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FuselageInventoryId,WingsInventoryId,Wing_Count,EngineInventoryId,Engine_Count,Max_Range,Length,Width,BasePrice,Image_url")] PlaneModels planeModels)
        {
            if (id != planeModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            }
            ViewData["EngineInventoryId"] = new SelectList(_context.Inventory, "Id", "Id", planeModels.EngineInventoryId);
            ViewData["FuselageInventoryId"] = new SelectList(_context.Inventory, "Id", "Id", planeModels.FuselageInventoryId);
            ViewData["WingsInventoryId"] = new SelectList(_context.Inventory, "Id", "Id", planeModels.WingsInventoryId);
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
        [ValidateAntiForgeryToken]
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
