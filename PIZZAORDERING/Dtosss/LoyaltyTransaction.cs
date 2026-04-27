namespace PIZZAORDERING.Models;

public class LoyaltyTransaction
{
    public Guid TransactionId { get; set; }

    public Guid UserId { get; set; }

    public Guid? OrderId { get; set; }

    public Guid Points { get; set; } // +ve = earned, -ve = redeemed

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}