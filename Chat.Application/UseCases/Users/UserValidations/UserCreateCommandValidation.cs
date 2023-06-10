using Chat.Application.UseCases.Users.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.UseCases.Users.UserValidations;
public class UserCreateCommandValidation: AbstractValidator<UserCreateCommand> 
{
    public UserCreateCommandValidation()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email is required!");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required!");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required!");
    }
}
