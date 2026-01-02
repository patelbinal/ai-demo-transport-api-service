namespace AiDemoTransport.Models.Events;

public class TransportCreatedEvent
{
    public int TransportId { get; set; }
    public int CarrierId { get; set; }
    public int PurchaseId { get; set; }
    public string PickupLocationDetails { get; set; } = string.Empty;
    public string DeliveryLocationDetails { get; set; } = string.Empty;
    public DateTime ScheduleDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string EventId { get; set; } = Guid.NewGuid().ToString();
    public DateTime EventTimestamp { get; set; } = DateTime.UtcNow;
}