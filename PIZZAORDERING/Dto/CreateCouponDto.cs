namespace PIZZAORDERING.Dto;

public class CreateCouponDto
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string DiscountType { get; set; } 
    public decimal DiscountValue { get; set; }
    public decimal MinOrderAmount { get; set; }
    public decimal? MaxOrderAmount { get; set; }
    public int? UsageLimit{ get; set; }
    public int  PerUserLimit { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime ExpiresAt { get; set; }

}