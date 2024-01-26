namespace FreshUp.Application.Commands.Orders.UpdateOrder;

public class UpdateOrderCommandValidation : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidation()
    {
        RuleFor(x => x.Status).NotEmpty().NotNull();
    }
}
