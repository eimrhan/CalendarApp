using Application.Features.Enrollments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using Domain.Enums;

namespace Application.Features.Enrollments.Commands.Update;

public class UpdateEnrollmentCommand : IRequest<UpdatedEnrollmentResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public EnrollmentType? Type { get; set; }
    public required string Title { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetEnrollments"];

    public class UpdateEnrollmentCommandHandler : IRequestHandler<UpdateEnrollmentCommand, UpdatedEnrollmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly EnrollmentBusinessRules _enrollmentBusinessRules;

        public UpdateEnrollmentCommandHandler(IMapper mapper, IEnrollmentRepository enrollmentRepository,
                                         EnrollmentBusinessRules enrollmentBusinessRules)
        {
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _enrollmentBusinessRules = enrollmentBusinessRules;
        }

        public async Task<UpdatedEnrollmentResponse> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            Enrollment? enrollment = await _enrollmentRepository.GetAsync(predicate: e => e.Id == request.Id, cancellationToken: cancellationToken);
            await _enrollmentBusinessRules.EnrollmentShouldExistWhenSelected(enrollment);
            enrollment = _mapper.Map(request, enrollment);

            await _enrollmentRepository.UpdateAsync(enrollment!);

            UpdatedEnrollmentResponse response = _mapper.Map<UpdatedEnrollmentResponse>(enrollment);
            return response;
        }
    }
}