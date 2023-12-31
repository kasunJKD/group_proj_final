using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                    Address = "AeroSoftHQ",
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

            var InventorySeed = new Inventory[]
           {
               new Inventory {Name="First Model t", AvailableCount=10,Price_Per_Unit=10,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory {Name="First Model rr", AvailableCount=20,Price_Per_Unit=103,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory {Name="First Model asd", AvailableCount=23,Price_Per_Unit=140,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },

                new Inventory { Name="second Model t", AvailableCount=10,Price_Per_Unit=10,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory {Name="second Model rr", AvailableCount=20,Price_Per_Unit=103,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory {Name="second Model asd", AvailableCount=23,Price_Per_Unit=140,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },

                new Inventory { Name="third Model t", AvailableCount=10,Price_Per_Unit=10,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory {Name="third Model rr", AvailableCount=20,Price_Per_Unit=103,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory {Name="third Model asd", AvailableCount=23,Price_Per_Unit=140,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },

               new Inventory { Name="4th Model t", AvailableCount=10,Price_Per_Unit=10,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory { Name="4th Model rr", AvailableCount=20,Price_Per_Unit=103,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory { Name="4th Model asd", AvailableCount=23,Price_Per_Unit=140,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },

               new Inventory { Name="5th Model t", AvailableCount=10,Price_Per_Unit=10,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory { Name="5th Model rr", AvailableCount=20,Price_Per_Unit=103,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory { Name="5th Model asd", AvailableCount=23,Price_Per_Unit=140,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },

               new Inventory { Name="6th Model t", AvailableCount=10,Price_Per_Unit=10,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory { Name="6th Model rr", AvailableCount=20,Price_Per_Unit=103,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory { Name="6th Model asd", AvailableCount=23,Price_Per_Unit=140,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },

               new Inventory { Name="7th Model t", AvailableCount=10,Price_Per_Unit=10,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory { Name="7th Model rr", AvailableCount=20,Price_Per_Unit=103,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory { Name="7th Model asd", AvailableCount=23,Price_Per_Unit=140,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },

               new Inventory { Name="8th Model t", AvailableCount=10,Price_Per_Unit=10,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory { Name="8th Model rr", AvailableCount=20,Price_Per_Unit=103,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
               new Inventory { Name="8th Model asd", AvailableCount=23,Price_Per_Unit=140,CreatedDateTime=DateTime.Now, UpdatedDateTime=DateTime.Now },
           };

            var ModelsSeed = new PlaneModels[]
            {
                new PlaneModels { Name = "First Model", FuselageInventoryId=1, WingsInventoryId=2, EngineInventoryId=3, Wing_Count=2,Engine_Count=2,Max_Range=200, Length=7,Width=1,BasePrice=100.5, },
                new PlaneModels { Name = "SECOND Model", FuselageInventoryId=4, WingsInventoryId=5, EngineInventoryId=6, Wing_Count=3,Engine_Count=4,Max_Range=600, Length=5,Width=11,BasePrice=200.5, },
                new PlaneModels { Name = "THIRD Model", FuselageInventoryId=7, WingsInventoryId=8, EngineInventoryId=9, Wing_Count=2,Engine_Count=1,Max_Range=300, Length=23,Width=23,BasePrice=400.5, },
                new PlaneModels { Name = "FOURTH Model", FuselageInventoryId=10, WingsInventoryId=11, EngineInventoryId=12, Wing_Count=5,Engine_Count=7,Max_Range=600, Length=23,Width=24,BasePrice=1025.5, },
                new PlaneModels { Name = "FIFTH Model", FuselageInventoryId=13, WingsInventoryId=14, EngineInventoryId=15, Wing_Count=2,Engine_Count=2,Max_Range=100, Length=24,Width=66,BasePrice=10023.5, },
                new PlaneModels { Name = "SIX Model", FuselageInventoryId=16, WingsInventoryId=17, EngineInventoryId=18, Wing_Count=2,Engine_Count=4,Max_Range=4400, Length=53,Width=23,BasePrice=12324.5, },
                new PlaneModels { Name = "SEVEN Model", FuselageInventoryId=19, WingsInventoryId=20, EngineInventoryId=21, Wing_Count=3,Engine_Count=5,Max_Range=2300, Length=63,Width=231,BasePrice=1125.5, },
                new PlaneModels { Name = "EIGHT Model", FuselageInventoryId=22, WingsInventoryId=23, EngineInventoryId=24, Wing_Count=2,Engine_Count=6,Max_Range=500, Length=23,Width=23,BasePrice=11235.5, },
            };

            var customizationseed = new Customizations[]
            {
                new Customizations {Name = "Customization 1", UnitPrice=24},
                 new Customizations {Name = "Customization 2", UnitPrice=200.53},
                  new Customizations {Name = "Customization 3", UnitPrice=300.4},
                   new Customizations {Name = "Customization 4", UnitPrice=400.4},
                    new Customizations {Name = "Customization 5", UnitPrice=500.4},
                     new Customizations {Name = "Customization 6", UnitPrice=200},
            };

            if (!_context.Inventory.Any())
            {
                _context.Inventory.AddRange(InventorySeed);
                _context.SaveChanges();
            }

            // Seed data
            if (!_context.PlaneModels.Any())
            {
                _context.PlaneModels.AddRange(ModelsSeed);
                _context.SaveChanges();
            }

            if (!_context.Customizations.Any())
            {
                _context.Customizations.AddRange(customizationseed);
                _context.SaveChanges();
            }


        }
    }
}