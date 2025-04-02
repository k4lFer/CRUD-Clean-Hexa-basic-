namespace Application.DTOs.Order
{
    public class OrderDetailCreateDto
    {
        public Guid productId { get; set; }
        public int quantity { get; set; }
        
    }

}