using Chat.Application.UseCases.Users.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.UseCases.Users.UserValidations;
public class UserUpdateCommandValidation: AbstractValidator<UserUpdateCommand>
{
    public UserUpdateCommandValidation()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User id is required!");
    }
}
