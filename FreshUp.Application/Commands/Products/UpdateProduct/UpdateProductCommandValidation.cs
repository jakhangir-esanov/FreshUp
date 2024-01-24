namespace FreshUp.Application.Commands.Products.UpdateProduct;

public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(3)
            .MaximumLength(50);
        RuleFor(x => x.Price).NotEmpty().NotNull();
        RuleFor(x => x.Unit).NotEmpty().NotNull();
        RuleFor(x => x.Description).NotEmpty().NotNull()
            .MinimumLength(10)
            .MaximumLength(200);
        RuleFor(x => x.CategoryId).NotEmpty().NotNull();
    }
}
