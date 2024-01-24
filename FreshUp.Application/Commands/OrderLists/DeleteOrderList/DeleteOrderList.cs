
namespace FreshUp.Application.Commands.OrderLists.DeleteOrderList;

public record DeleteOrderListCommand : IRequest<bool>
{
    public DeleteOrderListCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteOrderListCommandHandler : IRequestHandler<DeleteOrderListCommand, bool>
{
    private readonly IRepository<OrderList> repository;

    public DeleteOrderListCommandHandler(IRepository<OrderList> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteOrderListCommand request, CancellationToken cancellationToken)
    {
        var orderList = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("OrderList was not found!");

        this.repository.Delete(orderList);
        await this.repository.SaveAsync();

        return true;
    }
}
