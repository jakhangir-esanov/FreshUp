
namespace FreshUp.Application.Commands.OrderLists.CreateOrderList;

public record CreateOrderListCommand : IRequest<OrderList>
{
    public CreateOrderListCommand(string productName, double quantity, double price, long productId, long orderId)
    {
        ProductName = productName;
        Quantity = quantity;
        Price = price;
        ProductId = productId;
        OrderId = orderId;
    }

    public string ProductName { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public long ProductId { get; set; }
    public long OrderId { get; set; }
}

public class CreateOrderListCommandHandler : IRequestHandler<CreateOrderListCommand, OrderList>
{
    private readonly IRepository<OrderList> repository;

    public CreateOrderListCommandHandler(IRepository<OrderList> repository)
    {
        this.repository = repository;
    }

    public async Task<OrderList> Handle(CreateOrderListCommand request, CancellationToken cancellationToken)
    {
        var orderList = await this.repository.SelectAsync(x => x.ProductId.Equals(request.ProductId));
        if (orderList is not null)
            throw new AlreadyExistException("OrderList is already exist!");

        var newOrderList = new OrderList()
        {
            ProductName = request.ProductName,
            Quantity = request.Quantity,
            Price = request.Price,
            ProductId = request.ProductId,
            OrderId = request.OrderId
        };

        await this.repository.InsertAsync(newOrderList);
        await this.repository.SaveAsync();

        return newOrderList;
    }
}
