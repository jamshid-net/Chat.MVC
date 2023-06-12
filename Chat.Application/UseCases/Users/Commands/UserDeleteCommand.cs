using Chat.Application.Common.Exceptions;
using Chat.Application.Common.Interfaces;
using Chat.Domain.Entities;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Chat.Application.UseCases.Users.Commands;
public class UserDeleteCommand:IRequest<bool>
{
    public Guid UserId { get; init; }
    
}
public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IAppCache _cache;
    private readonly IConfiguration _configuration;

    public UserDeleteCommandHandler(IApplicationDbContext context, IAppCache cache, IConfiguration configuration)
    {
        _context = context;
        _cache = cache;
        _configuration = configuration;
    }

    public async Task<bool> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        
        _cache.Remove(_configuration.GetValue<string>("LazyCache:UserKey"));
        var entity =await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
        if(entity is null) 
            throw new NotFoundException(nameof(User),request.UserId);
        
        _context.Users.Remove(entity);
        if( await _context.SaveChangesAsync(cancellationToken)>0)
            return true;
        return false;

    }
}
