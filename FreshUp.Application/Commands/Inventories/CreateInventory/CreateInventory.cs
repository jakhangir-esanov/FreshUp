
namespace FreshUp.Application.Commands.Inventories.CreateInventory;

public record CreateInventoryCommand : IRequest<Inventory>
{
    public CreateInventoryCommand(long productId, long quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public long ProductId { get; set; }
    public long Quantity { get; set; }
}

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Inventory>
{
    private readonly IRepository<Inventory> repository;

    public CreateInventoryCommandHandler(IRepository<Inventory> repository)
    {
        this.repository = repository;
    }

    public async Task<Inventory> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await this.repository.SelectAsync(x => x.ProductId.Equals(request.ProductId));
        if (inventory is not null)
            throw new AlreadyExistException("Already exist!");

        var newInventory = new Inventory()
        { 
            ProductId = request.ProductId,
            Quantity = request.Quantity 
        };

        await this.repository.InsertAsync(newInventory);
        await this.repository.SaveAsync();

        return newInventory;
    }
}
