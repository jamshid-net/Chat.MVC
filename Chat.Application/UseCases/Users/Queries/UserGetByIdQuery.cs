using AutoMapper;
using Chat.Application.Common.Exceptions;
using Chat.Application.Common.Interfaces;
using Chat.Application.Common.Models;
using Chat.Domain.Entities;
using MediatR;

namespace Chat.Application.UseCases.Users.Queries;
public class UserGetByIdQuery:IRequest<UserGetDto>
{
    public Guid Id { get; init; }
}
public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, UserGetDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    public UserGetByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserGetDto> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
       var entity = await _context.Users.FindAsync(new object[] {request.Id}, cancellationToken); 
       if(entity is null) 
            throw new NotFoundException(nameof(User),request.Id);

      return  _mapper.Map<UserGetDto>(entity); 

    }
}
