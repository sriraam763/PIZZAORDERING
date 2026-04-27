namespace PIZZAORDERING.Dto;

public class CouponResponseDto
{
    public int CouponId { get; set; }
    public string Code{ get; set; }
    public string Description { get; set; }
    public string DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal MinOrderAmount { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; }
    public int? RemainingUses {  get; set; }

}