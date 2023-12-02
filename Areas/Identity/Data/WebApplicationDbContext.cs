using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class WebApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public WebApplicationDbContext(DbContextOptions<WebApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);


    }

    public DbSet<Customizations> Customizations { get; set; }
    public DbSet<PlaneModels> PlaneModels { get; set; }
    public DbSet<Inventory> Inventory { get; set; }
    public DbSet<Manufacture> Manufacture { get; set; }
    public DbSet<Order_Customizations> Order_Customizations { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Shipping> Feedbacks { get; set; }
}
