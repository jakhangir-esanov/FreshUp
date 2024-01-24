namespace FreshUp.Application.Commands.Products.CreateProduct;

public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidation()
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
