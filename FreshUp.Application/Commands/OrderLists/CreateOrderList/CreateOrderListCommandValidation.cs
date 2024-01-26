namespace FreshUp.Application.Commands.OrderLists.CreateOrderList;

public class CreateOrderListCommandValidation : AbstractValidator<CreateOrderListCommand>
{
    public CreateOrderListCommandValidation()
    {
        
        RuleFor(x => x.Quantity).NotEmpty().NotNull();
        RuleFor(x => x.ProductId).NotEmpty().NotNull();
        RuleFor(x => x.OrderId).NotEmpty().NotNull();
    }
}
