using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Models;
using static Humanizer.On;

namespace MTM_Holidays.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Holiday> Holidays { get; set; } = default!;
    public DbSet<Order_Holiday> Order_Holidays { get; set; } = default!;
    public DbSet<Picture> Pictures { get; set; } = default!;
    public DbSet<DiscountCode> DiscountCodes { get; set; } = default!;
    public DbSet<Address> Addresses { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<CardPayment> CardPayments { get; set; } = default!;


    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    
}

