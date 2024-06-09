using FluentValidation;

namespace Application.Features.Enrollments.Commands.Delete;

public class DeleteEnrollmentCommandValidator : AbstractValidator<DeleteEnrollmentCommand>
{
    public DeleteEnrollmentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}