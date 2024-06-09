using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Enrollments;

public interface IEnrollmentService
{
    Task<Enrollment?> GetAsync(
        Expression<Func<Enrollment, bool>> predicate,
        Func<IQueryable<Enrollment>, IIncludableQueryable<Enrollment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Enrollment>?> GetListAsync(
        Expression<Func<Enrollment, bool>>? predicate = null,
        Func<IQueryable<Enrollment>, IOrderedQueryable<Enrollment>>? orderBy = null,
        Func<IQueryable<Enrollment>, IIncludableQueryable<Enrollment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Enrollment> AddAsync(Enrollment enrollment);
    Task<Enrollment> UpdateAsync(Enrollment enrollment);
    Task<Enrollment> DeleteAsync(Enrollment enrollment, bool permanent = false);
}
