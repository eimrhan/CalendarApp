using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IEnrollmentRepository : IAsyncRepository<Enrollment, Guid>, IRepository<Enrollment, Guid>
{
}