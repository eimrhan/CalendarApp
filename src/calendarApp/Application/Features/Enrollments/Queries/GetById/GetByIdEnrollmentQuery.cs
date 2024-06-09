using Application.Features.Enrollments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Enrollments.Queries.GetById;

public class GetByIdEnrollmentQuery : IRequest<GetByIdEnrollmentResponse>
{
    public Guid Id { get; set; }

    public class GetByIdEnrollmentQueryHandler : IRequestHandler<GetByIdEnrollmentQuery, GetByIdEnrollmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly EnrollmentBusinessRules _enrollmentBusinessRules;

        public GetByIdEnrollmentQueryHandler(IMapper mapper, IEnrollmentRepository enrollmentRepository, EnrollmentBusinessRules enrollmentBusinessRules)
        {
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _enrollmentBusinessRules = enrollmentBusinessRules;
        }

        public async Task<GetByIdEnrollmentResponse> Handle(GetByIdEnrollmentQuery request, CancellationToken cancellationToken)
        {
            Enrollment? enrollment = await _enrollmentRepository.GetAsync(predicate: e => e.Id == request.Id, cancellationToken: cancellationToken);
            await _enrollmentBusinessRules.EnrollmentShouldExistWhenSelected(enrollment);

            GetByIdEnrollmentResponse response = _mapper.Map<GetByIdEnrollmentResponse>(enrollment);
            return response;
        }
    }
}