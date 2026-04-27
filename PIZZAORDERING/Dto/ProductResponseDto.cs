namespace PIZZAORDERING.Model
{
    public class ProductResponseDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PackagingInfo { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public bool Veg { get; set; }
    }
}
