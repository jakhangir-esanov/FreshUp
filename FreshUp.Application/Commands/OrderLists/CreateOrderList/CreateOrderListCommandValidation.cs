namespace FreshUp.Application.Commands.OrderLists.CreateOrderList;

public class CreateOrderListCommandValidation : AbstractValidator<CreateOrderListCommand>
{
    public CreateOrderListCommandValidation()
    {
        RuleFor(x => x.ProductName).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(3)
            .MaximumLength(50);
        RuleFor(x => x.Quantity).NotEmpty().NotNull();
        RuleFor(x => x.Price).NotEmpty().NotNull();
        RuleFor(x => x.ProductId).NotEmpty().NotNull();
        RuleFor(x => x.OrderId).NotEmpty().NotNull();
    }
}
