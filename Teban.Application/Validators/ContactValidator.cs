using FluentValidation;
using Teban.Application.Models;

namespace Teban.Application.Validators;
public class ContactValidator : AbstractValidator<Contact>
{
    public ContactValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();
    }
}
