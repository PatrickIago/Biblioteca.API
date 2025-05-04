using Biblioteca.Application.DTO;
using FluentValidation;

namespace Biblioteca.Application.Validators;
public class BookValidator : AbstractValidator<BookDto>
{
    public BookValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .WithMessage("Name is required.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500);

        RuleFor(x => x.Genre)
            .IsInEnum();
    }
}
