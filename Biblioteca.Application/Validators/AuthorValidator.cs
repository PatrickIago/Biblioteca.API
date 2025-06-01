using Biblioteca.Application.DTO;
using FluentValidation;

namespace Biblioteca.Application.Validators;
public class AuthorValidator : AbstractValidator<AuthorDto>
{
    public AuthorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("This Name is required");
    }
}
