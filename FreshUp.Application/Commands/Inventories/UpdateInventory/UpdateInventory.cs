﻿namespace FreshUp.Application.Commands.Inventories.UpdateInventory;

public record UpdateInventoryCommand : IRequest<Inventory>
{
    public UpdateInventoryCommand(long id, long productId, double quantity)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
    }

    public long Id { get; set; }
    public long ProductId { get; set; }
    public double Quantity { get; set; }
}

public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Inventory>
{
    private readonly IRepository<Inventory> repository;
    private readonly IRepository<InventoryHistory> inventoryHistoryRepository;
    public UpdateInventoryCommandHandler(IRepository<Inventory> repository, 
        IRepository<InventoryHistory> inventoryHistoryRepository)
    {
        this.repository = repository;
        this.inventoryHistoryRepository = inventoryHistoryRepository;
    }

    public async Task<Inventory> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Invnetory was not found!");

        inventory.ProductId = request.ProductId;
        inventory.Quantity = request.Quantity;

        if (request.Quantity - inventory.Quantity > 0)
        {
            var newInventoryHistory = new InventoryHistory()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity - inventory.Quantity
            };

            await this.inventoryHistoryRepository.InsertAsync(newInventoryHistory);
        }

        this.repository.Update(inventory);
        await this.repository.SaveAsync();

        return inventory;
    }
}
