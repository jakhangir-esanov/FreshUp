
using FreshUp.Application.Helpers;
using System.Data;

namespace FreshUp.Application.Commands.Users.UpdateUser;

public record UpdateUserCommand : IRequest<User>
{
    public UpdateUserCommand(long id, string userName, string password, string email, string phoneNumber, Role role)
    {
        Id = id;
        UserName = userName;
        Password = password;
        Email = email;
        PhoneNumber = phoneNumber;
        Role = role;
    }

    public long Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly IRepository<User> repository;
    public UpdateUserCommandHandler(IRepository<User> repository)
    {
        this.repository = repository;
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("User was not found!");

        var hashResult = PasswordHasher.Hash(request.Password);

        user.UserName = request.UserName;
        user.Password = hashResult;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Role = request.Role;

        this.repository.Update(user);
        await this.repository.SaveAsync();

        return user;
    }
}
