using Domain.Common;
using Domain.Enum;
using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class TOrder : BaseEntity
    {        
        public Guid customerId { get; private set; }
        public decimal totalPrice { get; private set; }
        public DateTime createdAt { get; private set; }
        public DateTime updatedAt { get; private set; }
        public OrderEnum status { get; private set; }
        
        private readonly ICollection<TOrderDetail> _orderDetails = new List<TOrderDetail>();

        public IReadOnlyCollection<TOrderDetail> OrderDetails => _orderDetails.ToList().AsReadOnly();
        public TCustomer? tCustomer { get; private set; }

        #region Constructors
        private TOrder() { }

        private TOrder(Guid customerId)
        {
            InitializeOrder(customerId);
        }
        #endregion

        #region Factory Methods
        public static TOrder Create(
            Guid customerId, 
            List<(Guid productId, int quantity, decimal unitPrice)> items)
        {
            var order = new TOrder(customerId);
            order.AddOrderItems(items);
            return order;
        }
        #endregion

        #region Public Methods
        public bool IsCancelled() => status == OrderEnum.Cancelled;
        public bool IsCompleted() => status == OrderEnum.Completed;
        public bool IsProcessed() => status == OrderEnum.Processing;
        public void CompleteOrder() => UpdateStatus(OrderEnum.Completed);
        public void ProcessOrder() => UpdateStatus(OrderEnum.Processing);
        public void CancelOrder() => UpdateStatus(OrderEnum.Cancelled);

        public void AddOrderDetail(Guid productId, int quantity, decimal unitPrice)
        {
            var detail = TOrderDetail.Create(id, productId, quantity, unitPrice);
            _orderDetails.Add(detail);
            UpdateOrderValues();
        }
        #endregion

        #region Private Methods
        private void InitializeOrder(Guid customerId)
        {
            this.customerId = customerId;
            status = OrderEnum.Pending;
            totalPrice = 0;
            SetCreationTimestamp();
        }

        private void AddOrderItems(IEnumerable<(Guid productId, int quantity, decimal unitPrice)> items)
        {
            foreach (var (productId, quantity, unitPrice) in items)
            {
                AddOrderDetail(productId, quantity, unitPrice);
            }
        }

        private void UpdateStatus(OrderEnum newStatus)
        {
            status = newStatus;
            UpdateModificationTimestamp();
        }

        private void UpdateOrderValues()
        {
            CalculateTotalPrice();
            UpdateModificationTimestamp();
        }

        private void CalculateTotalPrice()
        {
            totalPrice = _orderDetails.Sum(od => od.subtotal);
        }

        private void SetCreationTimestamp()
        {
            createdAt = DateTime.UtcNow;
            updatedAt = DateTime.UtcNow;
        }

        private void UpdateModificationTimestamp()
        {
            updatedAt = DateTime.UtcNow;
        }
        #endregion
    }
}