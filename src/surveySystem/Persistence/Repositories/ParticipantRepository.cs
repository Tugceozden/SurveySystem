using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ParticipantRepository : EfRepositoryBase<Participant, Guid, BaseDbContext>, IParticipantRepository
{
    public ParticipantRepository(BaseDbContext context) : base(context)
    {
    }
}