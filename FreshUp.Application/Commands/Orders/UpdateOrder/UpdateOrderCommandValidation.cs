namespace FreshUp.Application.Commands.Orders.UpdateOrder;

public class UpdateOrderCommandValidation : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidation()
    {
        RuleFor(x => x.TotalAmount).NotEmpty().NotNull();
        RuleFor(x => x.Status).NotEmpty().NotNull();
    }
}
