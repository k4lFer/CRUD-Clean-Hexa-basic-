namespace Application.DTOs.Product
{
    public class ProductUpdateStock
    {
        public Guid productId { get; set; }
        public int stock { get; set; }
    }
}