using AutoMapper;
using Chat.Application.Common.Exceptions;
using Chat.Application.Common.Interfaces;
using Chat.Application.Common.Models;
using Chat.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.UseCases.Messages.Queries;

public class MessageGetByIdQuery : IRequest<MessageGetDto>
{
    public Guid Id { get; init; }
}
public class MessageGetByIdQueryHandler : IRequestHandler<MessageGetByIdQuery, MessageGetDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MessageGetByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MessageGetDto> Handle(MessageGetByIdQuery request, CancellationToken cancellationToken)
    {
        
        var entity = await _context.Messages.FindAsync(new object[] { request.Id },cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Message), request.Id);
        var result = _mapper.Map<MessageGetDto>(entity);

        return result;
    }
}