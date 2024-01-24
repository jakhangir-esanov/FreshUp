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

    public UpdateOrderCommandHandler(IRepository<Order> repository)
    {
        this.repository = repository;
    }

    public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Order was not found!");

        order.OrderDate = request.OrderDate;
        order.TotalAmount = request.TotalAmount;
        order.Status = request.Status;

        this.repository.Update(order);
        await this.repository.SaveAsync();

        return order;
    }
}
