using FluentValidation;

namespace Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order.customerId)
                .NotEmpty().WithMessage("El ID del cliente es requerido");

            RuleFor(x => x.Order.orderDetails)
                .NotEmpty().WithMessage("La orden debe tener al menos un detalle")
                .Must(details => details.All(d => d.quantity > 0))
                .WithMessage("Todas las cantidades deben ser mayores que cero");

            RuleFor(x => x.Order.orderDetails)
                .Must(details => details.GroupBy(d => d.productId).All(g => g.Count() == 1))
                .WithMessage("No puede haber productos duplicados en la orden");
        }
    }
}