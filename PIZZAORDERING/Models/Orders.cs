namespace PIZZAORDERING.Models;

public class Orders
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string OrderNumber { get; set; }
    public string Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public Guid CoupanId { get; set; }
    public string DelivaryAddress { get; set; }
    public string Notes { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime DeliveredAt { get; set; }
}