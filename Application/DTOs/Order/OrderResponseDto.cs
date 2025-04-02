namespace Application.DTOs.Order
{
    public class OrderResponseDto
    {
        public Guid id { get; set; }
        public decimal totalPrice { get; set; }
        public DateTime createdAt { get; set; }
        public ICollection<OrderDetailsResponseDto> OrderDetails { get; set; } = new List<OrderDetailsResponseDto>();

    }
}
