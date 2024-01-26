
namespace FreshUp.Application.Queries.GetInventoryHistory;

public record GetInventoryHistoryQuery : IRequest<InventoryHistoryResultDto>
{
    public long Id { get; set; }
}

public class GetInventoryHistoryQueryHandler : IRequestHandler<GetInventoryHistoryQuery, InventoryHistoryResultDto>
{
    private readonly IRepository<InventoryHistory> repository;
    private readonly IMapper mapper;

    public GetInventoryHistoryQueryHandler(IRepository<InventoryHistory> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<InventoryHistoryResultDto> Handle(GetInventoryHistoryQuery request, CancellationToken cancellationToken)
    {
        var inventoryHistory = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Inventory History was not found!");

        return mapper.Map<InventoryHistoryResultDto>(inventoryHistory);
    }
}
