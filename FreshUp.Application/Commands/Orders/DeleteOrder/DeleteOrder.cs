namespace FreshUp.Application.Commands.Orders.DeleteOrder;

public record DeleteOrderCommand : IRequest<bool>
{
    public DeleteOrderCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IRepository<Order> repository;

    public DeleteOrderCommandHandler(IRepository<Order> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Order was not found!");

        this.repository.Delete(order);
        await this.repository.SaveAsync();

        return true;
    }
}
