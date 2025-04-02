namespace Application.DTOs.Product
{
    public class ProductCreateDto
    {
        public string name { get; set; }
        public string? description { get; set; }
        public int stock { get; set; }
        public decimal price { get; set; }
    }
}