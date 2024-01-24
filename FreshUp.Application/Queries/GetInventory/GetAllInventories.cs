namespace FreshUp.Application.Queries.GetInventory;

public record GetAllInventoriesQuery : IRequest<IEnumerable<InventoryResultDto>>
{
}

public class GetAllInventoriesQueryHandler : IRequestHandler<GetAllInventoriesQuery, IEnumerable<InventoryResultDto>>
{
    private readonly IRepository<Inventory> repository;
    private readonly IMapper mapper;
    public GetAllInventoriesQueryHandler(IRepository<Inventory> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<InventoryResultDto>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
    {
        var invnetories = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<InventoryResultDto>>(invnetories);
        return Task.FromResult(res);
    }
}
