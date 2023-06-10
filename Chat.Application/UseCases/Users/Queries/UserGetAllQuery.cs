using AutoMapper;
using Chat.Application.Common.Interfaces;
using Chat.Application.Common.Models;
using Chat.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.UseCases.Users.Queries;
public class UserGetAllQuery:IRequest<List<UserGetDto>>
{
}
public class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, List<UserGetDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserGetAllQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<UserGetDto>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
    {
        var entities =await _context.Users.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<UserGetDto>>(entities);
        return result;
        
    }
}
