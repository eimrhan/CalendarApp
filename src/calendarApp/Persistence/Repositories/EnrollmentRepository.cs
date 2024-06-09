using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EnrollmentRepository : EfRepositoryBase<Enrollment, Guid, BaseDbContext>, IEnrollmentRepository
{
    public EnrollmentRepository(BaseDbContext context) : base(context)
    {
    }
}