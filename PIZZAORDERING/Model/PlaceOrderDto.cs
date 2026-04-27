namespace PIZZAORDERING.Model
{
    public class PlaceOrderDto
    {
        public string DeliveryAddress { get; set; }
        public string?  CouponCode { get; set; }
        public string? Notes { get; set; }
    }
}
