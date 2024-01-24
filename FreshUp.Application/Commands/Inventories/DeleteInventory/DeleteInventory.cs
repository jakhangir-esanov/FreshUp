
namespace FreshUp.Application.Commands.Inventories.DeleteInventory;

public record DeleteInventoryCommand : IRequest<bool>
{
    public DeleteInventoryCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, bool>
{
    private readonly IRepository<Inventory> repository;

    public DeleteInventoryCommandHandler(IRepository<Inventory> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventroy = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Inventory was not found!");

        this.repository.Delete(inventroy);
        await this.repository.SaveAsync();

        return true;
    }
}
