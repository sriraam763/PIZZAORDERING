namespace PIZZAORDERING.Models;

public class Products
{
    public Guid Id { get; set; }
    public Guid CatogoryId { get; set; }
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string PakaginInfo { get; set; }
    public bool Veg { get; set; }
    public bool isAvailabel { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}