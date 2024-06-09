using FluentValidation;

namespace Application.Features.Enrollments.Commands.Update;

public class UpdateEnrollmentCommandValidator : AbstractValidator<UpdateEnrollmentCommand>
{
    public UpdateEnrollmentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Type).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
    }
}