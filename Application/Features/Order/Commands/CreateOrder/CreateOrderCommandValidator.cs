using Domain.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        public CreateOrderCommandValidator(ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            
            RuleFor(x => x.Order.customerId)
                .NotEmpty().WithMessage("El ID del cliente es requerido")
                .MustAsync(async (id, cancellation) => await _customerRepository.ExistsByIdAsync(id, cancellation)!)
                .WithMessage("El cliente no existe");

            RuleFor(x => x.Order.orderDetails)
                .NotEmpty().WithMessage("La orden debe tener al menos un detalle")
                .Must(details => details.All(d => d.quantity > 0))
                .WithMessage("Todas las cantidades deben ser mayores que cero");

            // Validar la existencia del producto, como que la cantidad solicitada no exceda a la del producto
            /*RuleFor(x => x.Order.orderDetails)
                 .MustAsync(async (details, cancellation) =>
                 {
                     var productsDict = (await _productRepository.GetByIdsAsync(
                         details.Select(d => d.productId).Distinct().ToList(), cancellation))
                         .ToDictionary(p => p.id);

                     var errors = details.Select(d => productsDict.TryGetValue(d.productId, out var p)
                         ? p.stock >= d.quantity ? null : $"Stock insuficiente: {p.name} (disponible: {p.stock}, solicitado: {d.quantity})"
                         : $"Producto {d.productId} no encontrado").Where(e => e != null).ToList();

                     if (errors.Any()) ErrorMessage = string.Join("; ", errors);
                     return !errors.Any();
                 })
                 .WithMessage(x => ErrorMessage);

             */
            RuleFor(x => x.Order.orderDetails)
             .CustomAsync(async (details, context, cancellation) =>
             {
                 var productIds = details.Select(d => d.productId).Distinct().ToList();
                 var products = await _productRepository.GetByIdsAsync(productIds, cancellation);
                 var productsDict = products.ToDictionary(p => p.id);

                 foreach (var detail in details)
                 {
                     if (!productsDict.TryGetValue(detail.productId, out var product))
                     {
                         context.AddFailure($"Producto {detail.productId} no encontrado");
                         continue;
                     }

                     if (product.stock < detail.quantity)
                     {
                         context.AddFailure(
                             $"Stock insuficiente para {product.name} " +
                             $"(disponible: {product.stock}, solicitado: {detail.quantity})");
                     }
                 }
             });
            RuleFor(x => x.Order.orderDetails)
                .Must(details => details.GroupBy(d => d.productId).All(g => g.Count() == 1))
                .WithMessage("No puede haber productos duplicados en la orden");
        }
    }
}