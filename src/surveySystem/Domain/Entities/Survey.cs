using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public  class Survey:Entity<Guid>
{
    public string Name { get; set; }
    public string  Description { get; set; }
    public Guid ParticipantId { get; set; }

    public virtual ICollection<Question> Questions { get; set; }
    public virtual Participant Participant { get; set; }



}
