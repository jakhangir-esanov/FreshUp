
namespace FreshUp.Application.Commands.Inventories.UpdateInventory;

public record UpdateInventoryCommand : IRequest<Inventory>
{
    public UpdateInventoryCommand(long id, long productId, long quantity)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
    }

    public long Id { get; set; }
    public long ProductId { get; set; }
    public long Quantity { get; set; }
}

public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Inventory>
{
    private readonly IRepository<Inventory> repository;

    public UpdateInventoryCommandHandler(IRepository<Inventory> repository)
    {
        this.repository = repository;
    }

    public async Task<Inventory> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Invnetory was not found!");

        inventory.ProductId = request.ProductId;
        inventory.Quantity = request.Quantity;

        this.repository.Update(inventory);
        await this.repository.SaveAsync();

        return inventory;
    }
}
