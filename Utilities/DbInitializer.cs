using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private WebApplicationDbContext _context;

        public DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, WebApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }



        public void Initialize()
        {
            
            if (!_roleManager.RoleExistsAsync(WebSiteRoles.Web_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Web_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Web_Customer)).GetAwaiter().GetResult();             

                _userManager.CreateAsync(new ApplicationUser
                {
                    Email = "Admin@gmail.com",
                    UserName = "Admin@gmail.com",
                    FirstName = "System",
                    LastName = "Admin",
                }, "Mate1234!").GetAwaiter().GetResult();
                var Appuser = _context.Users.FirstOrDefault(x => x.Email == "Admin@gmail.com");
                if (Appuser != null)
                {
                    _userManager.AddToRoleAsync(Appuser, WebSiteRoles.Web_Admin).GetAwaiter().GetResult();
                }
            }

            //seeding to tables
            _context.Database.EnsureCreated(); // Ensure database is created
            _context.Database.Migrate(); // Apply pending migrations
            
            // Create Identity users
           /* var user1 = new ApplicationUser
            {
                Email = "Doctor1@gmail.com",
                UserName = "Wasantha",
                FirstName = "Sarath",
                LastName = "Gunathilake",
                SpecialityId = 4,
                IsDoctor = true,
            };

            _userManager.CreateAsync(user1, "Mate1234!").GetAwaiter().GetResult();*/


     
        }
    }
}