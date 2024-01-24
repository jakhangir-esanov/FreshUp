namespace FreshUp.Application.Commands.Inventories.UpdateInventory;

public class UpdateInventoryCommandValidation : AbstractValidator<UpdateInventoryCommand>
{
    public UpdateInventoryCommandValidation()
    {
        RuleFor(x => x.ProductId).NotEmpty().NotNull();
        RuleFor(x => x.Quantity).NotEmpty().NotNull();
    }
}
