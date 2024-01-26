namespace FreshUp.Application.Commands.OrderLists.UpdateOrderList;

public class UpdateOrderListCommandValidation : AbstractValidator<UpdateOrderListCommand>
{
    public UpdateOrderListCommandValidation()
    {
        RuleFor(x => x.Quantity).NotEmpty().NotNull();
        RuleFor(x => x.ProductId).NotEmpty().NotNull();
        RuleFor(x => x.OrderId).NotEmpty().NotNull();
    }
}
