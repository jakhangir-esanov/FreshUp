

namespace FreshUp.Application.Commands.OrderLists.CreateOrderList;

public record CreateOrderListCommand : IRequest<OrderList>
{
    public CreateOrderListCommand(double quantity, long productId, long orderId)
    {
        Quantity = quantity;
        ProductId = productId;
        OrderId = orderId;
    }

    public double Quantity { get; set; }
    public long ProductId { get; set; }
    public long OrderId { get; set; }
}

public class CreateOrderListCommandHandler : IRequestHandler<CreateOrderListCommand, OrderList>
{
    private readonly IRepository<OrderList> repository;
    private readonly IRepository<Inventory> inventoryRepository;
    private readonly IRepository<Product> productRepository;
    public CreateOrderListCommandHandler(IRepository<OrderList> repository,
        IRepository<Inventory> inventoryRepository,
        IRepository<Product> productRepository)
    {
        this.repository = repository;
        this.inventoryRepository = inventoryRepository;
        this.productRepository = productRepository;
    }

    public async Task<OrderList> Handle(CreateOrderListCommand request, CancellationToken cancellationToken)
    {
        var orderList = await this.repository.SelectAsync(x => x.ProductId.Equals(request.ProductId));
        if (orderList is not null)
            throw new AlreadyExistException("OrderList is already exist!");

        var amount = await this.inventoryRepository.SelectAsync(x => x.ProductId.Equals(request.ProductId));
        if (amount.Quantity - request.Quantity < 0)
            throw new CustomException(429, "Bu maxsulot yetarlicha emas!");

        var product = await this.productRepository.SelectAsync(x => x.Id.Equals(request.ProductId))
            ?? throw new NotFoundException("This product was not found!");

        amount.Quantity = amount.Quantity - request.Quantity;

        if (product.Unit != Domain.Enums.Unit.kg || product.Unit != Domain.Enums.Unit.mg)
        {
            if (request.Quantity.GetType() == typeof(int))
            {
                throw new CustomException(433, "O'lchami kg va mg dan boshqa maxsulotlar bo'laklab sotilmaydi!");
            }
        }

        var newOrderList = new OrderList()
        {
            ProductName = product.Name,
            Quantity = request.Quantity,
            Volume = product.Volume,
            Unit = product.Unit,
            Price = product.Price * request.Quantity,
            ProductId = request.ProductId,
            OrderId = request.OrderId
        };

        this.inventoryRepository.Update(amount);
        await this.repository.InsertAsync(newOrderList);
        await this.repository.SaveAsync();


        return newOrderList;
    }
}
