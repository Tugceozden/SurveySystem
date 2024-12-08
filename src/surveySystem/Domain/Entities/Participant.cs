using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public  class Participant:Entity<Guid>
{
    public string Name { get; set; }
    public string  Surname { get; set; }
    public int  Age { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public Guid UserID { get; set; }




    public virtual  ICollection<SurveyResult> SurveyResults { get; set; }
    public virtual User User { get; set; }


}
