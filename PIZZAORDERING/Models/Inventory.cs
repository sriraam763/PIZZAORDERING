namespace PIZZAORDERING.Models;

public class Inventory
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int StockQuantity { get; set; }
    public int RecordLEvel { get; set; }
    public DateTime LastUPDated { get; set; }
}