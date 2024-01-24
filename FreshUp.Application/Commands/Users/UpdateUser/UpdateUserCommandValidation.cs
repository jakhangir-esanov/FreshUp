namespace FreshUp.Application.Commands.Users.UpdateUser;

public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidation()
    {
        RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("This field is required!")
           .MinimumLength(3)
           .MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(4)
            .MaximumLength(20);
        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("This field is required!")
            .EmailAddress();
        RuleFor(x => x.PhoneNumber).NotEmpty().NotNull();
        RuleFor(x => x.Role).NotEmpty().NotNull();
    }
}
