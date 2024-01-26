
namespace FreshUp.Application.Queries.GetInventoryHistory;

public record GetAllInventoryHistoriesQuery : IRequest<IEnumerable<InventoryHistoryResultDto>>
{
}

public class GetAllInventoryHistoriesQueryHandler : IRequestHandler<GetAllInventoryHistoriesQuery, IEnumerable<InventoryHistoryResultDto>>
{
    private readonly IRepository<InventoryHistory> repository;
    private readonly IMapper mapper;
    public GetAllInventoryHistoriesQueryHandler(IRepository<InventoryHistory> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<InventoryHistoryResultDto>> Handle(GetAllInventoryHistoriesQuery request, CancellationToken cancellationToken)
    {
        var inventoryHistories = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<InventoryHistoryResultDto>>(inventoryHistories);
        return Task.FromResult(res);
    }
}
