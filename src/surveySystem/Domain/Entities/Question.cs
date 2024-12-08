using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Question:Entity<Guid>
{
    public string QuestionText { get; set; }
    public Guid SurveyId { get; set; }
    public int Order { get; set; }  // Sorunun anketteki sırası
    public string Options { get; set; }
    public string?  Answer { get; set; }


    public virtual Survey Survey { get; set; }



}
