namespace FreshUp.Application.Queries.GetOrder;

public record GetAllOrdersQuery : IRequest<IEnumerable<OrderResultDto>>
{
}

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderResultDto>>
{
    private readonly IRepository<Order> repository;
    private readonly IMapper mapper;
    public GetAllOrdersQueryHandler(IRepository<Order> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<OrderResultDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<OrderResultDto>>(orders);
        return Task.FromResult(res);
    }
}
