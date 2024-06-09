using NArchitecture.Core.Application.Responses;

namespace Application.Features.Enrollments.Commands.Delete;

public class DeletedEnrollmentResponse : IResponse
{
    public Guid Id { get; set; }
}