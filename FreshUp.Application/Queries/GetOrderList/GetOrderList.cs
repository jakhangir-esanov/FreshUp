namespace FreshUp.Application.Queries.GetOrderList;

public record GetOrderListQuery : IRequest<OrderListResultDto>
{
    public long Id { get; set; }
}

public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, OrderListResultDto>
{
    private readonly IRepository<OrderList> repository;
    private readonly IMapper mapper;
    public GetOrderListQueryHandler(IRepository<OrderList> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<OrderListResultDto> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("OrderList was not found!");

        return mapper.Map<OrderListResultDto>(orderList);
    }
}
