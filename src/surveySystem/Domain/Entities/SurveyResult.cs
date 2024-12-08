using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public  class SurveyResult:Entity<Guid>
{

    public Guid ?SurveyId { get; set; }
    public Guid ParticipantId { get; set; }



    public virtual Survey? Survey { get; set; }
    public virtual Participant Participant { get; set; }



}
