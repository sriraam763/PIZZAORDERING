namespace PIZZAORDERING.Dto;

public class LoyaltyTransactionDto
{
    public int TransactionId { get; set; }
    public int Points { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt {  get; set; }

}