using Application.Features.Enrollments.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Enrollments;

public class EnrollmentManager : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly EnrollmentBusinessRules _enrollmentBusinessRules;

    public EnrollmentManager(IEnrollmentRepository enrollmentRepository, EnrollmentBusinessRules enrollmentBusinessRules)
    {
        _enrollmentRepository = enrollmentRepository;
        _enrollmentBusinessRules = enrollmentBusinessRules;
    }

    public async Task<Enrollment?> GetAsync(
        Expression<Func<Enrollment, bool>> predicate,
        Func<IQueryable<Enrollment>, IIncludableQueryable<Enrollment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Enrollment? enrollment = await _enrollmentRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return enrollment;
    }

    public async Task<IPaginate<Enrollment>?> GetListAsync(
        Expression<Func<Enrollment, bool>>? predicate = null,
        Func<IQueryable<Enrollment>, IOrderedQueryable<Enrollment>>? orderBy = null,
        Func<IQueryable<Enrollment>, IIncludableQueryable<Enrollment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Enrollment> enrollmentList = await _enrollmentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return enrollmentList;
    }

    public async Task<Enrollment> AddAsync(Enrollment enrollment)
    {
        Enrollment addedEnrollment = await _enrollmentRepository.AddAsync(enrollment);

        return addedEnrollment;
    }

    public async Task<Enrollment> UpdateAsync(Enrollment enrollment)
    {
        Enrollment updatedEnrollment = await _enrollmentRepository.UpdateAsync(enrollment);

        return updatedEnrollment;
    }

    public async Task<Enrollment> DeleteAsync(Enrollment enrollment, bool permanent = false)
    {
        Enrollment deletedEnrollment = await _enrollmentRepository.DeleteAsync(enrollment);

        return deletedEnrollment;
    }
}
