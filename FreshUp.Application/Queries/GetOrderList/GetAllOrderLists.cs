
namespace FreshUp.Application.Queries.GetOrderList;

public record GetAllOrderListsQuery : IRequest<IEnumerable<OrderListResultDto>>
{
}

public class GetAllOrderListsQueryHandler : IRequestHandler<GetAllOrderListsQuery, IEnumerable<OrderListResultDto>>
{
    private readonly IRepository<OrderList> repository;
    private readonly IMapper mapper;
    public GetAllOrderListsQueryHandler(IRepository<OrderList> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<OrderListResultDto>> Handle(GetAllOrderListsQuery request, CancellationToken cancellationToken)
    {
        var orderLists = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<OrderListResultDto>>(orderLists);
        return Task.FromResult(res);
    }
}
