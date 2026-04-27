namespace PIZZAORDERING.Model
{
    public class OrderResponsDto
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string  Status { get; set; }
        public List<OrderItemDto> Items { get; set; }

    }
}
