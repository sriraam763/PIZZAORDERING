namespace PIZZAORDERING.Dto;

public class LoyaltyBalanceDto
{
    public string UserId { get; set; }
    public int CurrentBalance { get; set; }
    public List<LoyaltyTransactionDto> Transactions { get; set; } 
}