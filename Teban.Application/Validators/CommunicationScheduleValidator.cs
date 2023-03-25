using FluentValidation;
using Teban.Application.Models;

namespace Teban.Application.Validators;
public class CommunicationScheduleValidator : AbstractValidator<CommunicationSchedule>
{
    public CommunicationScheduleValidator()
    {
        RuleFor(x => x.Frequency)
            .NotNull();

        RuleFor(x => x.StartDate)
            .NotNull()
            .GreaterThanOrEqualTo(DateTime.Today);

        RuleFor(x => x.TebanUserId)
            .NotEmpty();

        RuleFor(x => x.ContactId)
            .GreaterThan(0);
    }
}
