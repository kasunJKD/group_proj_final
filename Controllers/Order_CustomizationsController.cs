﻿using System;
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
        public async Task<JsonResult> CreateInsertValues([FromBody] Customizations_Input_Extended val, int orderId)
        {

            try
            {
                double totalUnitPrice = 0;

                if (val.Data.Count > 0)
                {
                    foreach (var row in val.Data)
                    {
                        var order_Customizations = new Order_Customizations
                        {
                            CustomizationsId = row.Id,
                            OrderId = row.OrderId,
                        };

                        // Add the instance to the context
                        _context.Order_Customizations.Add(order_Customizations);

                        // Accumulate the total unit price
                        totalUnitPrice += row.UnitPrice;
                    }
                }


                // Update the order table with the total unit price
                var order = _context.Order
                    .Include(o => o.OrderedModel) // Include the related data from the 'orderModel' table
                    .FirstOrDefault(o => o.Id == val.OrderId);
                double orderTableTotal = order.Total_Price;

                if (order != null)
                {
                    order.Customization_Price = totalUnitPrice;
                    order.Total_Price = orderTableTotal + totalUnitPrice;
                    order.Status = StatusData.Done;
                    _context.SaveChanges();                    
                }

                if (val.OrderId != 0) { 

                    var manufacture = new Manufacture
                    {
                        OrderId = val.OrderId,
                        UpdatedDateTime = DateTime.Now,
                        CreatedDateTime = DateTime.Now,
                        Status = StatusData.InProgress
                    };

                    _context.Manufacture.Add(manufacture);

                    _context.SaveChanges();

                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (val.OrderId != 0)
                        {
                            // Update Fuselage Inventory
                            var inventoryItemFuselage = await _context.Inventory
                                                                      .FirstOrDefaultAsync(i => i.Id == order.OrderedModel.FuselageInventoryId);
                            if (inventoryItemFuselage != null)
                            {
                                inventoryItemFuselage.AvailableCount = Math.Max(0, inventoryItemFuselage.AvailableCount - 1);
                            }

                            // Update Wing Inventory
                            var inventoryItemWing = await _context.Inventory
                                                                  .FirstOrDefaultAsync(i => i.Id == order.OrderedModel.WingsInventoryId);
                            if (inventoryItemWing != null)
                            {
                                inventoryItemWing.AvailableCount = Math.Max(0, inventoryItemWing.AvailableCount - order.OrderedModel.Wing_Count);
                            }

                            // Update Engine Inventory
                            var inventoryItemEngine = await _context.Inventory
                                                                    .FirstOrDefaultAsync(i => i.Id == order.OrderedModel.EngineInventoryId);
                            if (inventoryItemEngine != null)
                            {
                                inventoryItemEngine.AvailableCount = Math.Max(0, inventoryItemEngine.AvailableCount - order.OrderedModel.Engine_Count);
                            }

                            // Save the changes once for all updates
                            await _context.SaveChangesAsync();

                            // Commit the transaction
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction on error
                        transaction.Rollback();
                        throw;
                    }
                }

                TempData["SuccessMessage"] = "Order submitted successfully.";

                // return Json(Url.Action("Index", "Catalog"));
                //return RedirectToAction("Index", "Catalog");

                //return new JsonResult(Ok());
                return Json(new { success = true, message = "Order submitted successfully.", Status = "success" });

            }
            catch (Exception ex)
            {
                // Handle any exceptions (log, return an error message, etc.)
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }

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

        [HttpPost]
        public JsonResult BackButton([FromBody] Customizations_Input_Extended val, int orderId)
        {

            try
            {
                // Assuming that val.OrderId is not null
                int orderIdToDelete = val.OrderId;

                var orderToDelete = _context.Order.FirstOrDefault(o => o.Id == orderIdToDelete);

                if (orderToDelete != null)
                {
                    // Remove the order from the context and mark it for deletion
                    _context.Order.Remove(orderToDelete);

                    // Save changes to persist the deletion
                    _context.SaveChanges();
                }

                TempData["SuccessMessage"] = "Ordering process reverted.";

                // return Json(Url.Action("Index", "Catalog"));
                //return RedirectToAction("Index", "Catalog");

                //return new JsonResult(Ok());
                return Json(new { success = true, message = "Ordering process reverted successfully.", Status = "success" });

            }
            catch (Exception ex)
            {
                // Handle any exceptions (log, return an error message, etc.)
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }

        }
    }
}
