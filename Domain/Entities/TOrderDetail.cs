using Domain.Common;

namespace Domain.Entities
{
    public class TOrderDetail : BaseEntity
    {
        public Guid orderId { get; private set; }
        public Guid productId { get; private set; }
        public int quantity { get; private set; }
        public decimal unitPrice { get; private set; }
        public decimal subtotal { get; private set; }

        // Relaciones (navegación)
        public TOrder? Order { get; private set; }
        public TProduct? Product { get; private set; }

        private TOrderDetail() { }

        // Constructor privado
        private TOrderDetail(Guid OrderId, Guid ProductId, int Quantity, decimal UnitPrice)
        {
            orderId = OrderId;
            productId = ProductId;
            quantity = Quantity;
            unitPrice = UnitPrice;
            subtotal = quantity * unitPrice;
        }

        // Método de fábrica
        public static TOrderDetail Create(Guid orderId, Guid productId, int quantity, decimal unitPrice)
        {
            return new TOrderDetail(orderId, productId, quantity, unitPrice);
        }
    }
}