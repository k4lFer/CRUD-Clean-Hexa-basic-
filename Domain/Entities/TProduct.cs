using Domain.Common;
using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class TProduct : BaseEntity
    {        
        public string name { get; private set; }
        public string? description { get; private set; }
        public int stock { get; private set; }
        public decimal price { get; private set; }
        public DateTime createdAt { get; private set; }
        public DateTime updatedAt { get; private set; }

        // Propiedades privadas para encapsulación
        private readonly ICollection<TOrderDetail> _orderDetails = new List<TOrderDetail>();
        
        // Colección de solo lectura
        public IReadOnlyCollection<TOrderDetail> OrderDetails => _orderDetails.ToList().AsReadOnly();


        // Constructor privado sin parámetros para EF Core
        private TProduct() { }
        
        // Constructor privado para controlar la creación
        private TProduct(string Name, string Description, int Stock, decimal Price)
        {
            name = Name;
            description = Description;
            stock = Stock;
            price = Price;
            SetCreationTimestamp();
        }

        // Método de fábrica para crear un nuevo producto
        public static TProduct Create(string name, string description, int initialStock, decimal price)
        {
            return new TProduct(name, description, initialStock, price);
        }

        // Método para disminuir el stock
        public void DecreaseStock(int quantity)
        {
            stock -= quantity;
            UpdateModificationTimestamp();
        }

        // Método para aumentar el stock
        public void IncreaseStock(int quantity)
        {
            stock += quantity;
            UpdateModificationTimestamp();
        }

        // Método para actualizar el precio
        public void UpdatePrice(decimal newPrice)
        {
            price = newPrice;
            UpdateModificationTimestamp();
        }

        public void Update(string name, string description, int stock, decimal price)
        {
            this.name = name ?? this.name;
            this.description = description ?? this.description;
            this.stock = stock != default ? stock : this.stock;
            this.price = price != default ? price : this.price; 
            UpdateModificationTimestamp();
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

    }
}