namespace PIZZAORDERING.Dto;

public class UpdateInventoryDto
{
    public string ProductId { get; set; }
    public int StockQuantity { get; set; }
    public int? ReorderLevel { get; set; }

}