using Application.Features.Enrollments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using Domain.Enums;

namespace Application.Features.Enrollments.Commands.Create;

public class CreateEnrollmentCommand : IRequest<CreatedEnrollmentResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public Guid? UserId { get; set; }
    public EnrollmentType? Type { get; set; }
    public required string Title { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetEnrollments"];

    public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, CreatedEnrollmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly EnrollmentBusinessRules _enrollmentBusinessRules;

        public CreateEnrollmentCommandHandler(IMapper mapper, IEnrollmentRepository enrollmentRepository,
                                         EnrollmentBusinessRules enrollmentBusinessRules)
        {
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _enrollmentBusinessRules = enrollmentBusinessRules;
        }

        public async Task<CreatedEnrollmentResponse> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            Enrollment enrollment = _mapper.Map<Enrollment>(request);

            await _enrollmentRepository.AddAsync(enrollment);

            CreatedEnrollmentResponse response = _mapper.Map<CreatedEnrollmentResponse>(enrollment);
            return response;
        }
    }
}