namespace PIZZAORDERING.Model
{
    public class CreateProductDto
    {
        public int CategoryId { get; set; }
        public int? BrandId{ get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PackagingInfo{ get; set; }
        public int InitialStock { get; set; }
        public string ImageUrl { get; set; }
    }
}
