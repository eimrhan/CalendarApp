using Application.Features.Enrollments.Commands.Create;
using Application.Features.Enrollments.Commands.Delete;
using Application.Features.Enrollments.Commands.Update;
using Application.Features.Enrollments.Queries.GetById;
using Application.Features.Enrollments.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Enrollments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateEnrollmentCommand, Enrollment>();
        CreateMap<Enrollment, CreatedEnrollmentResponse>();

        CreateMap<UpdateEnrollmentCommand, Enrollment>();
        CreateMap<Enrollment, UpdatedEnrollmentResponse>();

        CreateMap<DeleteEnrollmentCommand, Enrollment>();
        CreateMap<Enrollment, DeletedEnrollmentResponse>();

        CreateMap<Enrollment, GetByIdEnrollmentResponse>();

        CreateMap<Enrollment, GetListEnrollmentListItemDto>();
        CreateMap<IPaginate<Enrollment>, GetListResponse<GetListEnrollmentListItemDto>>();
    }
}