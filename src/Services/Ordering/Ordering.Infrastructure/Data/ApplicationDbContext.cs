using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Customer> Customers => Set<Customer>(); 
    public DbSet<Product> Products => Set<Product>(); 
    public DbSet<Order> Orders => Set<Order>(); 
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();


    override protected void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // IEntityTypeConfiguration
        base.OnModelCreating(builder);
    }
}