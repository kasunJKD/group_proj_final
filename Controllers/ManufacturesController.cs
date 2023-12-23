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
    public class ManufacturesController : Controller
    {
        private readonly WebApplicationDbContext _context;

        public ManufacturesController(WebApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Manufactures
        public async Task<IActionResult> Index()
        {
            var webApplicationDbContext = _context.Manufacture.Include(m => m.Order);
            return View(await webApplicationDbContext.ToListAsync());
        }

        // GET: Manufactures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Manufacture == null)
            {
                return NotFound();
            }

            var manufacture = await _context.Manufacture
                .Include(m => m.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manufacture == null)
            {
                return NotFound();
            }

            return View(manufacture);
        }

        // GET: Manufactures/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id");
            return View();
        }

        // POST: Manufactures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,DeliveryDateTime,CreatedDateTime,UpdatedDateTime,Status")] Manufacture manufacture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manufacture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", manufacture.OrderId);
            return View(manufacture);
        }

        // GET: Manufactures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Manufacture == null)
            {
                return NotFound();
            }

            var manufacture = await _context.Manufacture.FindAsync(id);
            if (manufacture == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", manufacture.OrderId);
            return View(manufacture);
        }

        // POST: Manufactures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,DeliveryDateTime,CreatedDateTime,UpdatedDateTime,Status")] Manufacture manufacture)
        {
            var existingManufacture = await _context.Manufacture
            .Include(m => m.Order)           // Include the Order navigation property
                .ThenInclude(o => o.User)    // Include the User navigation property within the Order
            .FirstOrDefaultAsync(m => m.Id == id);

            if (existingManufacture == null)
                    {
                        return NotFound();
                    }

            try
            {
                // Update properties
                existingManufacture.UpdatedDateTime = DateTime.Now;
                existingManufacture.Status = manufacture.Status;

                // Update the entry in the context
                _context.Update(existingManufacture);

                // Save changes to the database
                await _context.SaveChangesAsync();
                if (manufacture.OrderId != 0 && manufacture.Status == StatusData.Done)
                {
                    var shipping = new Shipping
                    {
                        OrderId = manufacture.OrderId,
                        Estimated_DateTime = DateTime.Now.AddDays(5),
                        Address = existingManufacture.Order.User.Address,
                        UpdatedDateTime = DateTime.Now,
                        CreatedDateTime = DateTime.Now,
                        Status = StatusData.InProgress
                    };

                    _context.Shipping.Add(shipping);

                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                
                return View(manufacture); // You might want to return to the view with an error message
            }
        }

        /*var planeModels = _context.PlaneModels.FindAsync(id);

                Manufacture manufacture2 = new Manufacture();
                manufacture2.UpdatedDateTime = DateTime.Now;
                manufacture2.Status = StatusData.Pending;
                _context.Update(manufacture2);
                await _context.SaveChangesAsync();

                // Get the generated OrderId after saving
                int orderId = manufacture2.Id;

                return RedirectToAction("Update", "Order_Customizations", new { orderId });
           */
    }
    /*    public JsonResult CreateInsertValues([FromBody] List<Customizations_Input> rows, int orderId)
        {

            try
            {
                
                int orderIDD = 0;

                foreach (var row in rows)
                {
                    orderIDD = row.OrderId;
                    var order_Customizations = new Order_Customizations
                    {
                        CustomizationsId = row.Id,
                        OrderId = row.OrderId,
                    };

                    // Add the instance to the context
                    _context.Order_Customizations.Add(order_Customizations);

                 
                }

                // Update manufactures
                var order = _context.Order.FirstOrDefault(o => o.Id == orderIDD);
                

               
                if (orderIDD != 0)
                {

                    var manufacture = new Manufacture
                    {
                        OrderId = orderIDD,
                        UpdatedDateTime = DateTime.Now,
                        CreatedDateTime = DateTime.Now,
                        Status = StatusData.InProgress
                    };

                    _context.Manufacture.Add(manufacture);

                    _context.SaveChanges();

                }

                TempData["SuccessMessage"] = "Updated successfully.";

                // return Json(Url.Action("Index", "Catalog"));
                //return RedirectToAction("Index", "Catalog");

                //return new JsonResult(Ok());
                return Json(new { success = true, message = "Updated successfully.", Status = "success" });

            }
            catch (Exception ex)
            {
                // Handle any exceptions (log, return an error message, etc.)
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }

        }*/
        /*if (id != manufacture.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(manufacture);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufactureExists(manufacture.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }*/
        /*return RedirectToAction(nameof(Index));
    }
        ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", manufacture.OrderId);
        return View(manufacture);
    }*/

        /*// GET: Manufactures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Manufacture == null)
            {
                return NotFound();
            }

            var manufacture = await _context.Manufacture
                .Include(m => m.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manufacture == null)
            {
                return NotFound();
            }

            return View(manufacture);
        }

        // POST: Manufactures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Manufacture == null)
            {
                return Problem("Entity set 'WebApplicationDbContext.Manufacture'  is null.");
            }
            var manufacture = await _context.Manufacture.FindAsync(id);
            if (manufacture != null)
            {
                _context.Manufacture.Remove(manufacture);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManufactureExists(int id)
        {
          return (_context.Manufacture?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    
}
