using AutoMapper;
using Chat.Application.Common.Models;
using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Common.AutoMappings;
public class MessageAutoMapper:Profile
{
    public MessageAutoMapper()
    {
        CreateMap<Message, MessageGetDto>().ReverseMap();
    }
}
