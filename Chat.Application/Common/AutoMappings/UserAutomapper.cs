using AutoMapper;
using Chat.Application.Common.Models;
using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Common.AutoMappings;
public class UserAutomapper:Profile
{
    public UserAutomapper()
    {
        CreateMap<User,UserGetDto>().ReverseMap();
    }
}
