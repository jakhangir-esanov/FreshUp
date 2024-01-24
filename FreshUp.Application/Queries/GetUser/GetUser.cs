
namespace FreshUp.Application.Queries.GetUser;

public record GetUserQuery : IRequest<UserResultDto>
{
    public long Id { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResultDto>
{
    private readonly IRepository<User> repository;
    private readonly IMapper mapper;
    public GetUserQueryHandler(IRepository<User> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<UserResultDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("User was not found!");

        return mapper.Map<UserResultDto>(user); 
    }
}
