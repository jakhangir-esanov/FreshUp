namespace FreshUp.Application.Commands.Orders.CreateOrder;

public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidation()
    {
        RuleFor(x => x.TotalAmount).NotEmpty().NotNull();
        RuleFor(x => x.Status).NotEmpty().NotNull();
    }
}
