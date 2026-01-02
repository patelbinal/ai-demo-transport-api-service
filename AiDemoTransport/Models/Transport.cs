using System.ComponentModel.DataAnnotations;

namespace AiDemoTransport.Models;

public class Transport
{
    public int Id { get; set; } // Transport ID
    
    [Required]
    public int CarrierId { get; set; }

    [Required]
    public int PurchaseId { get; set; }

    [Required]
    public int OfferId { get; set; }
    
    [Required]
    [StringLength(500)]
    public string PickupLocationDetails { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string DeliveryLocationDetails { get; set; } = string.Empty;

    [Required]
    public DateTime ScheduleDate { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "Scheduled"; // Scheduled, InTransit, Delivered, Cancelled
}