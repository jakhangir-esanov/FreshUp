
using FreshUp.Application.Helpers;

namespace FreshUp.Application.Commands.Users.CreateUser;

public record CreateUserCommand : IRequest<User>
{
    public CreateUserCommand(string userName, string password, string email, string phoneNumber, Role role)
    {
        UserName = userName;
        Password = password;
        Email = email;
        PhoneNumber = phoneNumber;
        Role = role;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IRepository<User> repository;

    public CreateUserCommandHandler(IRepository<User> repository)
    {
        this.repository = repository;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await this.repository.SelectAsync(x => x.Email.Equals(request.Email));
        if (user is not null)
            throw new AlreadyExistException("User is already exist!");

        var hashResult = PasswordHasher.Hash(request.Password);

        var newUser = new User()
        {
            UserName = request.UserName,
            Password = hashResult,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role
        };

        await this.repository.InsertAsync(newUser);
        await this.repository.SaveAsync();

        return newUser;
    }
}
