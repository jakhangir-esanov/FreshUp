namespace FreshUp.Application.Commands.Categories.UpdateCategory;

public class UpdateCategoryCommandValidation : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(3)
            .MaximumLength(50);
        RuleFor(x => x.Description).NotNull()
            .MinimumLength(10)
            .MaximumLength(200);
    }
}
