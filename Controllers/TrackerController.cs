using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TrackerController : Controller
    {
        private readonly WebApplicationDbContext _context;

        public TrackerController(WebApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trackerViewModels = await _context.Order.Include(m => m.OrderedModel)
                .Where(o => o.UserId == userId)
                .Select(order => new Tracker
                {
                    userId = userId,
                    order = order,
                    manufacture = _context.Manufacture.FirstOrDefault(m => m.OrderId == order.Id),
                    shipping = _context.Shipping.FirstOrDefault(s => s.OrderId == order.Id)
                })
                .ToListAsync();

            return View(trackerViewModels);
        }
    }
}
