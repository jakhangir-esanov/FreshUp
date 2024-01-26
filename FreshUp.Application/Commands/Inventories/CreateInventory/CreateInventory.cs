
using Microsoft.AspNetCore.Http;

namespace FreshUp.Application.Commands.Inventories.CreateInventory;

public record CreateInventoryCommand : IRequest<Inventory>
{
    public CreateInventoryCommand(long productId, double quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public long ProductId { get; set; }
    public double Quantity { get; set; }
}

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Inventory>
{
    private readonly IRepository<Inventory> repository;
    private readonly IRepository<InventoryHistory> inventoryHistoryRepository;
    public CreateInventoryCommandHandler(IRepository<Inventory> repository, IRepository<InventoryHistory> inventoryHistoryRepository)
    {
        this.repository = repository;
        this.inventoryHistoryRepository = inventoryHistoryRepository;
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

        var newInventoryHistory = new InventoryHistory()
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        await this.repository.InsertAsync(newInventory);
        await this.inventoryHistoryRepository.InsertAsync(newInventoryHistory);
        await this.repository.SaveAsync();

        return newInventory;
    }
}
