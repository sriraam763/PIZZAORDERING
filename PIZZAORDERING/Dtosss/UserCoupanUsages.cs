namespace PIZZAORDERING.Models;

public class UserCoupanUsages
{
    public Guid UsageId { get; set; }

    public Guid UserId { get; set; }
    
    public Guid CouponId { get; set; }

    public Guid OrderId { get; set; }

    public DateTime UsedAt { get; set; } = DateTime.UtcNow;
}