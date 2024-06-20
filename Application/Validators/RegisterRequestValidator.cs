using Application.Models.Requests;
using FluentValidation;

namespace Application.Validators;

public class RegisterRequestValidator:AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Input a valid email address");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
        .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
        .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
        .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
    }
}
