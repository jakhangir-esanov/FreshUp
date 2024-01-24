namespace FreshUp.Application.Queries.GetProduct;

public record GetProductQuery : IRequest<ProductResultDto>
{
    public long Id { get; set; }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResultDto>
{
    private readonly IRepository<Product> repository;
    private readonly IMapper mapper;
    public GetProductQueryHandler(IRepository<Product> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<ProductResultDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Product was not found!");

        return mapper.Map<ProductResultDto>(product);
    }
}
