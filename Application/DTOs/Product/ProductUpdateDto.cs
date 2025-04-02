namespace Application.DTOs.Product
{
    public class ProductUpdateDto
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int? stock { get; set; }
        public decimal? price { get; set; }
    }
}