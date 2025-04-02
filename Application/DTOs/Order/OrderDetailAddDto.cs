namespace Application.DTOs.Order
{
    public class OrderDetailAddDto
    {
        public Guid orderId { get; set; }
        public Guid productId { get; set; }
        public int quantity { get; set; }
        
    }

}