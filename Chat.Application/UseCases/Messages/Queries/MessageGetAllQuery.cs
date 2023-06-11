using AutoMapper;
using Chat.Application.Common.Interfaces;
using Chat.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.UseCases.Messages.Queries;
public class MessageGetAllQuery:IRequest<List<MessageGetDto>>
{
}
public class MessageGetAllQueryHandler : IRequestHandler<MessageGetAllQuery, List<MessageGetDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MessageGetAllQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MessageGetDto>> Handle(MessageGetAllQuery request, CancellationToken cancellationToken)
    {
        var entities =await  _context.Messages.AsNoTracking().ToListAsync(cancellationToken);
        var result = _mapper.Map<List<MessageGetDto>>(entities);

        return result;
    }
}
