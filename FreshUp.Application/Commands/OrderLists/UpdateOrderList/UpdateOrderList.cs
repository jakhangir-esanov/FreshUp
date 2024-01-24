
namespace FreshUp.Application.Commands.OrderLists.UpdateOrderList;

public record UpdateOrderListCommand : IRequest<OrderList>
{
    public UpdateOrderListCommand(long id, string productName, double quantity, double price, long productId, long orderId)
    {
        Id = id;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
        ProductId = productId;
        OrderId = orderId;
    }

    public long Id { get; set; }
    public string ProductName { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public long ProductId { get; set; }
    public long OrderId { get; set; }
}

public class UpdateOrderListCommandHandler : IRequestHandler<UpdateOrderListCommand, OrderList>
{
    private readonly IRepository<OrderList> repository;

    public UpdateOrderListCommandHandler(IRepository<OrderList> repository)
    {
        this.repository = repository;
    }

    public async Task<OrderList> Handle(UpdateOrderListCommand request, CancellationToken cancellationToken)
    {
        var orderList = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("OrderList was not found!");

        orderList.ProductName = request.ProductName;
        orderList.Quantity = request.Quantity;
        orderList.Price = request.Price;
        orderList.ProductId = request.ProductId;
        orderList.OrderId = request.OrderId;    

        this.repository.Update(orderList);
        await this.repository.SaveAsync();

        return orderList;
    }
}
