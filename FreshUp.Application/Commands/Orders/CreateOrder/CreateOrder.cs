﻿using FreshUp.Domain.Enums;

namespace FreshUp.Application.Commands.Orders.CreateOrder;

public record CreateOrderCommand : IRequest<Order>
{
    public CreateOrderCommand(DateTime orderDate, Status status)
    {
        OrderDate = orderDate;
        Status = status;
    }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public Status Status { get; set; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly IRepository<Order> repository;

    public CreateOrderCommandHandler(IRepository<Order> repository)
    {
        this.repository = repository;
    }

    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await this.repository.SelectAsync(x => x.OrderDate.Equals(request.OrderDate));
        if (order is not null)
            throw new AlreadyExistException("Order is already exist!");

        var newOrder = new Order()
        {
            OrderDate = request.OrderDate,
            TotalAmount = 0,
            Status = request.Status
        };

        await this.repository.InsertAsync(newOrder);
        await this.repository.SaveAsync();

        return newOrder;
    }
}
