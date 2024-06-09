using NArchitecture.Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.Enrollments.Queries.GetList;

public class GetListEnrollmentListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public EnrollmentType Type { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}