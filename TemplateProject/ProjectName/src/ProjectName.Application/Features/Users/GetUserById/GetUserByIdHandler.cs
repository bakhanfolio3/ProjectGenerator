//using ProjectName.Application.Common.Responses;
//using ProjectName.Application.Abstraction.Responses;
//using ProjectName.Interfaces.Contexts;
//using Mapster;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ProjectName.Application.Features.Users.GetUserById;

//public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, IResult<GetUserResponse>>
//{
//    private readonly IApplicationDbContext _context;


//    public GetUserByIdHandler(IApplicationDbContext context)
//    {
//        _context = context;
//    }
//    public async Task<IResult<GetUserResponse>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
//    {
//        var hero = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id,
//            cancellationToken: cancellationToken);
//        if (hero is null) return await Result<GetUserResponse>.FailAsync("No Record Found");
//        return hero.Adapt<IResult<GetUserResponse>>();
//    }
//}