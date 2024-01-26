using FreshUp.Domain.Enums;

namespace FreshUp.Application.Commands.Orders.UpdateOrder;

public record UpdateOrderCommand : IRequest<Order>
{
    public UpdateOrderCommand(long id, DateTime orderDate, double totalAmount, Status status)
    {
        Id = id;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        Status = status;
    }

    public long Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public double TotalAmount { get; set; }
    public Status Status { get; set; }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Order>
{
    private readonly IRepository<Order> repository;
    private readonly IRepository<OrderList> orderListRepository;
    public UpdateOrderCommandHandler(IRepository<Order> repository, 
        IRepository<OrderList> orderListRepository)
    {
        this.repository = repository;
        this.orderListRepository = orderListRepository;
    }

    public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Order was not found!");

        List<OrderList> orderList = this.orderListRepository.SelectAll(x => x.OrderId.Equals(request.Id)).ToList()
            ?? throw new NotFoundException("Order was not found!");

        double totalAmount = 0;

        foreach (var i in orderList)
            totalAmount += i.Price;


        order.OrderDate = request.OrderDate;
        order.TotalAmount = totalAmount;
        order.Status = request.Status;

        this.repository.Update(order);
        await this.repository.SaveAsync();

        return order;
    }
}
