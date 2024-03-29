﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly WebApplicationDbContext _context;

        public OrdersController(WebApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var webApplicationDbContext = _context.Order.Include(o => o.OrderedModel).Include(o => o.User);
            return View(await webApplicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.OrderedModel)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create(int? id)
        {
            if (id == null || _context.PlaneModels == null)
            {
                return NotFound();
            }

            var planeModels = _context.PlaneModels.FindAsync(id);
            ViewBag.BasePrice = planeModels.Result.BasePrice;
            ViewBag.Image_url = planeModels.Result.Image_url;
            ViewBag.Model_Name = planeModels.Result.Name;
            ViewBag.Model_Id = id;
            return View();
        }
        public IActionResult GeneratePDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                PdfPTable table = new PdfPTable(6);

                PdfPCell cell1 = new PdfPCell(new Phrase("Id"));
                PdfPCell cell2 = new PdfPCell(new Phrase("UserId"));
                PdfPCell cell3 = new PdfPCell(new Phrase("OrderedModel"));
                PdfPCell cell4 = new PdfPCell(new Phrase("Customization_Price"));
                PdfPCell cell5 = new PdfPCell(new Phrase("Total_Price"));
                PdfPCell cell6 = new PdfPCell(new Phrase("CreatedDateTime"));
                
                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);
                table.AddCell(cell4);
                table.AddCell(cell5);
                table.AddCell(cell6);

                //var bids = _bidInterface.GetAllBidsList().ToList();
                //var totalBoughtPlayersPrice = 0;
                var orders = _context.Order.ToList(); // Make sure you adjust this line to match how you retrieve orders

                foreach (var item in orders)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(item.Id.ToString()));
                   PdfPCell celll2 = new PdfPCell(new Phrase(item.UserId.ToString()));
                    PdfPCell celll3 = new PdfPCell(new Phrase(item.OrderedModelId.ToString()));
                    PdfPCell celll4 = new PdfPCell(new Phrase(item.Customization_Price.ToString()));
                    PdfPCell celll5 = new PdfPCell(new Phrase(item.Total_Price.ToString()));
                    PdfPCell celll6 = new PdfPCell(new Phrase(item.CreatedDateTime.ToString()));
                   

                    table.AddCell(cell);
                   table.AddCell(celll2);
                    table.AddCell(celll3);
                    table.AddCell(celll4);
                    table.AddCell(celll5);
                    table.AddCell(celll6);
                }






                document.Add(table);
                document.Close();
                writer.Close();

                var constant = ms.ToArray();
                return File(constant, "application/vnd", "SellsData.pdf");

            }
        }

        [HttpPost]
        public IActionResult GeneratePDFPlayer()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                PdfPTable table = new PdfPTable(6);


                PdfPCell cell1 = new PdfPCell(new Phrase("UserId"));
                PdfPCell cell2 = new PdfPCell(new Phrase("OrderedModel"));
                PdfPCell cell3 = new PdfPCell(new Phrase("Customization_Price"));
                PdfPCell cell4 = new PdfPCell(new Phrase("Total_Price"));
                PdfPCell cell5 = new PdfPCell(new Phrase("CreatedDateTime"));
                PdfPCell cell6 = new PdfPCell(new Phrase("Status"));
                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);
                table.AddCell(cell4);
                table.AddCell(cell5);
                table.AddCell(cell6);

                var orders = _context.Order.ToList(); // Make sure you adjust this line to match how you retrieve orders



                foreach (var item in orders)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(item.Id.ToString()));
                    PdfPCell celll2 = new PdfPCell(new Phrase(item.OrderedModel.ToString()));
                    PdfPCell celll3 = new PdfPCell(new Phrase(item.Customization_Price.ToString()));
                    PdfPCell celll4 = new PdfPCell(new Phrase(item.Total_Price.ToString()));
                    PdfPCell celll5 = new PdfPCell(new Phrase(item.CreatedDateTime.ToString()));
                    PdfPCell celll6 = new PdfPCell(new Phrase(item.Status.ToString()));

                    table.AddCell(cell);
                    table.AddCell(celll2);
                    table.AddCell(celll3);
                    table.AddCell(celll4);
                    table.AddCell(celll5);
                    table.AddCell(celll6);
                }


                document.Add(table);
                document.Close();
                writer.Close();

                var constant = ms.ToArray();
                return File(constant, "application/vnd", "Orders.pdf");

            }
        }

    

    // POST: Orders/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,OrderedModelId,CustomOrderId,Customization_Price,Total_Price,CreatedDateTime,UpdatedDateTime,Status")] Order order, int id)
        {
                var planeModels = _context.PlaneModels.FindAsync(id);

                Order order2 = new Order();
                order2.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                order2.OrderedModelId = order.OrderedModelId;
                order2.CreatedDateTime= DateTime.Now;
                order2.UpdatedDateTime= DateTime.Now;
                order2.Status = StatusData.Pending;
                order2.OrderedModelId = id;
                order2.Total_Price = planeModels.Result.BasePrice;
                _context.Add(order2);
                await _context.SaveChangesAsync();

                // Get the generated OrderId after saving
                int orderId = order2.Id;

                return RedirectToAction("Create", "Order_Customizations", new { orderId });
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["OrderedModelId"] = new SelectList(_context.PlaneModels, "Id", "Id", order.OrderedModelId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,OrderedModelId,CustomOrderId,Customization_Price,Total_Price,CreatedDateTime,UpdatedDateTime,Status")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            try
            {
                // Load the existing order from the database
                var existingOrder = await _context.Order.FindAsync(id);

                if (existingOrder == null)
                {
                    return NotFound();
                }

                // Update only the UpdatedDateTime field
                existingOrder.UpdatedDateTime = DateTime.Now;
                existingOrder.Status = order.Status;
                // Mark the entity as modified
                _context.Entry(existingOrder).State = EntityState.Modified;

                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            
            ViewData["OrderedModelId"] = new SelectList(_context.PlaneModels, "Id", "Id", order.OrderedModelId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.OrderedModel)
                .Include(o => o.User)
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
            if (_context.Order == null)
            {
                return Problem("Entity set 'WebApplicationDbContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
    
}
