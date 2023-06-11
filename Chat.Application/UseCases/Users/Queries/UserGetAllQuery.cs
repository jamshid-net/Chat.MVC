using AutoMapper;
using Chat.Application.Common.Interfaces;
using Chat.Application.Common.Models;
using Chat.Domain.Entities;
using LazyCache;
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
    private readonly IAppCache _cache;

    public UserGetAllQueryHandler(IApplicationDbContext context, IMapper mapper, IAppCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<List<UserGetDto>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
    {
        var cachedResult = _cache.TryGetValue("users", out List<UserGetDto> CachedResult);
        if (cachedResult)
        {
            return CachedResult;
        }
        else
        {
            var entities = await _context.Users.AsNoTracking().ToListAsync(cancellationToken);
            var result = _mapper.Map<List<UserGetDto>>(entities);
            _cache.Add("users", result, TimeSpan.FromSeconds(100));
            return result;
        }
       
        
    }
}
