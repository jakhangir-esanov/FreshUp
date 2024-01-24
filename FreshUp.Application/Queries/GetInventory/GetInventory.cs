namespace FreshUp.Application.Queries.GetInventory;

public record GetInventoryQuery : IRequest<InventoryResultDto>
{
    public long Id { get; set; }
}

public class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, InventoryResultDto>
{
    private readonly IRepository<Inventory> repository;
    private readonly IMapper mapper;
    public GetInventoryQueryHandler(IRepository<Inventory> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<InventoryResultDto> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
    {
        var inventory = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Inventory was not found!");

        return mapper.Map<InventoryResultDto>(inventory);
    }
}
