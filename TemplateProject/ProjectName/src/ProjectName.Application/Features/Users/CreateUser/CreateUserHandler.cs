//using ProjectName.Application.Common.Responses;
//using ProjectName.Application.Abstraction.Responses;
//using ProjectName.Domain.Entities.Identity;
//using ProjectName.Interfaces.Contexts;
//using Mapster;
//using MediatR;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ProjectName.Application.Features.Users.CreateUser;

//public class CreateUserHandler : IRequestHandler<CreateUserRequest, IResult<GetUserResponse>>
//{
//    private readonly IApplicationDbContext _context;
    
    
//    public CreateUserHandler(IApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<IResult<GetUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
//    {
//        var created = request.Adapt<User>();
//        _context.Users.Add(created);
//        await _context.SaveChangesAsync(cancellationToken);
//        return created.Adapt<IResult<GetUserResponse>>();
//    }
//}