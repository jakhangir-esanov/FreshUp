
using FreshUp.Application.Helpers;

namespace FreshUp.Application.Commands.Users.DeleteUser;

public record DeleteUserCommand : IRequest<bool>
{
    public DeleteUserCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IRepository<User> repository;
    public DeleteUserCommandHandler(IRepository<User> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("User was not found!");

        this.repository.Delete(user);
        await this.repository.SaveAsync();

        return true;
    }
}
