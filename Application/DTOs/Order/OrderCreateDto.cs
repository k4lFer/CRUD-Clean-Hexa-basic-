namespace Application.DTOs.Order
{
    public class OrderCreateDto
    {
        public Guid customerId { get; set; }
        public ICollection<OrderDetailCreateDto> orderDetails { get; set; } = new List<OrderDetailCreateDto>();

    }
}