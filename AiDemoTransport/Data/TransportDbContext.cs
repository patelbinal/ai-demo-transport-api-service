using Microsoft.EntityFrameworkCore;
using AiDemoTransport.Models;

namespace AiDemoTransport.Data;

public class TransportDbContext : DbContext
{
    public TransportDbContext(DbContextOptions<TransportDbContext> options) : base(options)
    {
    }
    
    public DbSet<Transport> Transports { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure Transport entity
        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CarrierId).IsRequired();
            entity.Property(e => e.PurchaseId).IsRequired();
            entity.Property(e => e.PickupLocationDetails).IsRequired().HasMaxLength(500);
            entity.Property(e => e.DeliveryLocationDetails).IsRequired().HasMaxLength(500);
            entity.Property(e => e.ScheduleDate).IsRequired();
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50).HasDefaultValue("Scheduled");
            
            // Add indexes for better query performance
            entity.HasIndex(e => e.CarrierId);
            entity.HasIndex(e => e.PurchaseId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.ScheduleDate);
        });
        
        // Add some seed data
        modelBuilder.Entity<Transport>().HasData(
            new Transport
            {
                Id = 1,
                CarrierId = 101,
                PurchaseId = 1001,
                PickupLocationDetails = "123 Main St, Downtown, City A - Contact: John Doe, Phone: +1-555-0123",
                DeliveryLocationDetails = "456 Business Ave, Corporate District, City B - Contact: Jane Smith, Phone: +1-555-0456",
                ScheduleDate = DateTime.UtcNow.AddDays(1),
                Status = "Scheduled"
            }
        );
    }
}