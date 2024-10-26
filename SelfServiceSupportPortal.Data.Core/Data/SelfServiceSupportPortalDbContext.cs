using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SelfServiceSupportPortal.Data.Model;

namespace SelfServiceSupportPortal.Data.Core.Data;

public class SelfServiceSupportPortalDbContext : IdentityDbContext<ApplicationUser>
{
    public SelfServiceSupportPortalDbContext(DbContextOptions<SelfServiceSupportPortalDbContext> options)
        : base(options)
    {
    }

    public DbSet<Location> Location { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<RegisterComplain> RegisterComplain { get; set; }
    public DbSet<ProductComplaint> ProductComplaint { get; set; }
    public DbSet<Complains> Complains { get; set; }
    public DbSet<Company> Company { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
