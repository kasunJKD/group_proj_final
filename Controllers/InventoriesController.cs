using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly WebApplicationDbContext _context;

        public InventoriesController(WebApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
              return _context.Inventory != null ? 
                          View(await _context.Inventory.ToListAsync()) :
                          Problem("Entity set 'WebApplicationDbContext.Inventory'  is null.");
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AvailableCount,Price_Per_Unit,CreatedDateTime,UpdatedDateTime")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                inventory.CreatedDateTime = DateTime.Now;
                inventory.UpdatedDateTime = DateTime.Now;
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }
        public IActionResult GeneratePDFPlayer()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                PdfPTable table = new PdfPTable(6);


                PdfPCell cell1 = new PdfPCell(new Phrase("Name"));
                PdfPCell cell2 = new PdfPCell(new Phrase("AvailableCount"));
                PdfPCell cell3 = new PdfPCell(new Phrase("Price_Per_Unit"));
                PdfPCell cell4 = new PdfPCell(new Phrase("CreatedDateTime"));
                PdfPCell cell5 = new PdfPCell(new Phrase("UpdatedDateTime"));
                //PdfPCell cell6 = new PdfPCell(new Phrase("Status"));
                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);
                table.AddCell(cell4);
                table.AddCell(cell5);
               // table.AddCell(cell6);

                var inventory = _context.Inventory.ToList(); // Make sure you adjust this line to match how you retrieve orders



                foreach (var item in inventory)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(item.Name.ToString()));
                    PdfPCell celll2 = new PdfPCell(new Phrase(item.AvailableCount.ToString()));
                    PdfPCell celll3 = new PdfPCell(new Phrase(item.Price_Per_Unit.ToString()));
                    PdfPCell celll4 = new PdfPCell(new Phrase(item.CreatedDateTime.ToString()));
                    PdfPCell celll5 = new PdfPCell(new Phrase(item.UpdatedDateTime.ToString()));
                 //   PdfPCell celll6 = new PdfPCell(new Phrase(item.Status.ToString()));

                    table.AddCell(cell);
                    table.AddCell(celll2);
                    table.AddCell(celll3);
                    table.AddCell(celll4);
                    table.AddCell(celll5);
                   // table.AddCell(celll6);
                }


                document.Add(table);
                document.Close();
                writer.Close();

                var constant = ms.ToArray();
                return File(constant, "application/vnd", "Orders.pdf");

            }
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AvailableCount,Price_Per_Unit,CreatedDateTime,UpdatedDateTime")] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    inventory.UpdatedDateTime = DateTime.Now;
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.Id))
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
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventory == null)
            {
                return Problem("Entity set 'WebApplicationDbContext.Inventory'  is null.");
            }
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventory.Remove(inventory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
          return (_context.Inventory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
