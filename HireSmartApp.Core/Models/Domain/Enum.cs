using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Models.Domain
{    
    public enum RoleType
    {
        Unassigned = 0,
        Administrator = 1,
        Employer = 2,
        Candidate = 3,
    }
    public enum QuestionType
    {
        MultipleChoice = 0,
        Paragraph,
        Dropdown,
        Date,
        Number
    }
}
