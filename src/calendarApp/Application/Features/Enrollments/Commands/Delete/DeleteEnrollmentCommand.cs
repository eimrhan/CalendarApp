using Application.Features.Enrollments.Constants;
using Application.Features.Enrollments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;

namespace Application.Features.Enrollments.Commands.Delete;

public class DeleteEnrollmentCommand : IRequest<DeletedEnrollmentResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public Guid Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetEnrollments"];

    public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, DeletedEnrollmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly EnrollmentBusinessRules _enrollmentBusinessRules;

        public DeleteEnrollmentCommandHandler(IMapper mapper, IEnrollmentRepository enrollmentRepository,
                                         EnrollmentBusinessRules enrollmentBusinessRules)
        {
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _enrollmentBusinessRules = enrollmentBusinessRules;
        }

        public async Task<DeletedEnrollmentResponse> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            Enrollment? enrollment = await _enrollmentRepository.GetAsync(predicate: e => e.Id == request.Id, cancellationToken: cancellationToken);
            await _enrollmentBusinessRules.EnrollmentShouldExistWhenSelected(enrollment);

            await _enrollmentRepository.DeleteAsync(enrollment!);

            DeletedEnrollmentResponse response = _mapper.Map<DeletedEnrollmentResponse>(enrollment);
            return response;
        }
    }
}