using Application.Dtos;
using Domain.Abstractions;
using FluentValidation;
using MediatR;

namespace Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order):IRequest<Result<Guid>>;

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order.Amount).NotEmpty().GreaterThan(0);

            RuleFor(x => x.Order.CustomerName).NotEmpty();

            RuleFor(x => x.Order.Product).NotEmpty();
        }
    }
 
}
