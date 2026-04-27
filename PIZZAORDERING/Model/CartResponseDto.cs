namespace PIZZAORDERING.Model
{
    public class CartResponseDto
    {
        public int CartId { get; set; }
        public List<CartItemDto> Items { get; set; }
        public decimal Subtotal { get; set; }
        public string? AppliedCoupon { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
