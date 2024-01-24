namespace FreshUp.Application.Commands.Inventories.CreateInventory;

public class CreateInventoryCommandValidation : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryCommandValidation()
    {
        RuleFor(x => x.ProductId).NotEmpty().NotNull();
        RuleFor(x => x.Quantity).NotEmpty().NotNull();
    }
}
