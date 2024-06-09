using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Enrollments.Commands.Create;

public class CreatedEnrollmentResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public EnrollmentType Type { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}