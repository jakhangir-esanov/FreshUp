namespace FreshUp.Application.Queries.GetProduct;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductResultDto>>
{
}

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResultDto>>
{
    private readonly IRepository<Product> repository;
    private readonly IMapper mapper;
    public GetAllProductsQueryHandler(IRepository<Product> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<ProductResultDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<ProductResultDto>>(products);
        return Task.FromResult(res);
    }
}
