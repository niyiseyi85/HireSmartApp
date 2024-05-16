using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Models.Domain
{
    public class Questions : Entity
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Question {  get; set; }
    }

}
