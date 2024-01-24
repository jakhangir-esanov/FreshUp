
namespace FreshUp.Application.Queries.GetOrder;

public record GetOrderQuery : IRequest<OrderResultDto>
{
    public long Id { get; set; }
}

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderResultDto>
{
    private readonly IRepository<Order> repository;
    private readonly IMapper mapper;
    public GetOrderQueryHandler(IRepository<Order> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<OrderResultDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await this.repository.SelectAsync(x => x.Id.Equals(request.Id), includes: new[] { "OrderLists" })
            ?? throw new NotFoundException("Order was not found!");

        return mapper.Map<OrderResultDto>(order);
    }
}
