using Application.Features.Enrollments.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Enrollments.Rules;

public class EnrollmentBusinessRules : BaseBusinessRules
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ILocalizationService _localizationService;

    public EnrollmentBusinessRules(IEnrollmentRepository enrollmentRepository, ILocalizationService localizationService)
    {
        _enrollmentRepository = enrollmentRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, EnrollmentsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task EnrollmentShouldExistWhenSelected(Enrollment? enrollment)
    {
        if (enrollment == null)
            await throwBusinessException(EnrollmentsBusinessMessages.EnrollmentNotExists);
    }

    public async Task EnrollmentIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Enrollment? enrollment = await _enrollmentRepository.GetAsync(
            predicate: e => e.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await EnrollmentShouldExistWhenSelected(enrollment);
    }
}