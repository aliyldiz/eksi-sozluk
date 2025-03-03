using EksiSozluk.Common.Models.RequestModels;
using FluentValidation;
using FluentValidation.Validators;

namespace EksiSozluk.Api.Application.Features.Commands.User;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(i => i.EmailAddress)
            .NotNull()
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Email Address not a valid email address");
        
        RuleFor(i => i.Password)
            .NotNull()
            .MinimumLength(6)
            .WithMessage("Password must be at least {MinLenght} characters long");
    }
}