

namespace FreshUp.Application.Commands.OrderLists.UpdateOrderList;

public record UpdateOrderListCommand : IRequest<OrderList>
{
    public UpdateOrderListCommand(long id, double quantity, long productId, long orderId)
    {
        Id = id;
        Quantity = quantity;
        ProductId = productId;
        OrderId = orderId;
    }

    public long Id { get; set; }
    public double Quantity { get; set; }
    public long ProductId { get; set; }
    public long OrderId { get; set; }
}

public class UpdateOrderListCommandHandler : IRequestHandler<UpdateOrderListCommand, OrderList>
{
    private readonly IRepository<OrderList> repository;
    private readonly IRepository<Inventory> inventoryRepository;
    private readonly IRepository<Product> productRepository;
    public UpdateOrderListCommandHandler(IRepository<OrderList> repository,
        IRepository<Inventory> inventoryRepository,
        IRepository<Product> productRepository)
    {
        this.repository = repository;
        this.inventoryRepository = inventoryRepository;
        this.productRepository = productRepository;
    }

    public async Task<OrderList> Handle(UpdateOrderListCommand request, CancellationToken cancellationToken)
    {
        var orderList = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("OrderList was not found!");

        var amount = await this.inventoryRepository.SelectAsync(x => x.ProductId.Equals(request.ProductId));
        if (amount.Quantity - request.Quantity < 0)
            throw new CustomException(429, "Bu maxsulot yetarlicha emas!");

        var product = await this.productRepository.SelectAsync(x => x.Id.Equals(request.ProductId))
            ?? throw new NotFoundException("This product was not found!");

        amount.Quantity -= request.Quantity;

        if (product.Unit != Domain.Enums.Unit.kg || product.Unit != Domain.Enums.Unit.mg)
        {
            if (request.Quantity.GetType() == typeof(int))
            {
                throw new CustomException(433, "O'lchami kg va mg dan boshqa maxsulotlar bo'laklab sotilmaydi!");
            }
        }

        orderList.ProductName = product.Name;
        orderList.Quantity = request.Quantity;
        orderList.Volume = product.Volume;
        orderList.Unit = product.Unit;
        orderList.Price = product.Price * request.Quantity;
        orderList.ProductId = request.ProductId;
        orderList.OrderId = request.OrderId;

        this.inventoryRepository.Update(amount);
        this.repository.Update(orderList);
        await this.repository.SaveAsync();

        return orderList;
    }
}
