using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Models;

namespace MTM_Holidays.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Holiday> Holidays { get; set; }
    public DbSet<Order_Holiday> Order_Holidays { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<DiscountCode> DiscountCodes { get; set; }
    public DbSet<MTM_Holidays.Models.Address>? Address { get; set; }
    public DbSet<MTM_Holidays.Models.Order>? Order { get; set; }
}

