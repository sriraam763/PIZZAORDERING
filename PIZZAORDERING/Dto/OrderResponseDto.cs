namespace PIZZAORDERING.Dto;

public class OrderResponseDto
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; }
    public string  Status { get; set; }
    // public List<OrderItemDto> Items { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; }
    public DateTime PlacedAt {  get; set; }
    public DateTime? DeliveredAt { get; set; }
    public int PointsEarned { get; set; }
}