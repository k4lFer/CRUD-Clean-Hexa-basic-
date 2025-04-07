namespace Application.DTOs.Order
{
    public class OrderDetailsResponseDto
    {
        public Guid id { get; set; }
        public string productName { get; set; }
        public decimal unitPrice { get; set; }
        public int quantity { get; set; }
        public decimal subtotal { get; set; }   
    }
}
