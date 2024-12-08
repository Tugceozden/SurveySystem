using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ParticipationRepository : EfRepositoryBase<Participation, Guid, BaseDbContext>, IParticipationRepository
{
    public ParticipationRepository(BaseDbContext context) : base(context)
    {
    }
}