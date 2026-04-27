namespace PIZZAORDERING.Dto;

public class InventoryResponseDto
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public int StockQuantity { get; set; }
    public int ReorderLevel { get; set; }
    public bool IsLow {  get; set; }
    public DateTime LastUpdated { get; set; }

}