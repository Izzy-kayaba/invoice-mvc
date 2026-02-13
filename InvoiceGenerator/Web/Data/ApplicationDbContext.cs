using InvoiceGenerator.Models.Entities;

using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Data;

/// <summary>
/// This class represents our database session.
/// It maps our Entities to database tables.
/// </summary>

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Represents the Invoices table in the database.
    /// </summary>
    public DbSet<Invoice> Invoices { get; set; }

    /// <summary>
    /// Represents the InvoiceItems table in the database.
    /// </summary>
    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    /// <summary>
    /// Configure relationships and constraints.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // One Invoice has many InvoiceItems
        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.Items)
            .WithOne(ii => ii.Invoice)
            .HasForeignKey(ii => ii.InvoiceId);
    }
}