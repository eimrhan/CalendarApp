using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Enrollments.Queries.GetList;

public class GetListEnrollmentQuery : IRequest<GetListResponse<GetListEnrollmentListItemDto>>, ICacheRemoverRequest
{
    public PageRequest PageRequest { get; set; }
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetEnrollments"];

    public class GetListEnrollmentQueryHandler : IRequestHandler<GetListEnrollmentQuery, GetListResponse<GetListEnrollmentListItemDto>>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        public GetListEnrollmentQueryHandler(IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListEnrollmentListItemDto>> Handle(GetListEnrollmentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Enrollment> enrollments = await _enrollmentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListEnrollmentListItemDto> response = _mapper.Map<GetListResponse<GetListEnrollmentListItemDto>>(enrollments);
            return response;
        }
    }
}