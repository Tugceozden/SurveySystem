
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public  class Participation:Entity<Guid>
{
    public Guid PaticipantId { get; set; }
    public virtual Participant Participant { get; set; }
    public Guid ?SurveyId { get; set; }
    public virtual Survey? Survey { get; set; }
    public string? Answers { get; set; }



}
