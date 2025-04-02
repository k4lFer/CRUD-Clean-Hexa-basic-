namespace Application.DTOs.Order
{
    public class OrderUpdateDto
    {
        public Guid id { get; set; }
        public Guid customerId { get; set; }
        public decimal totalPrice { get; set; }
    }
}