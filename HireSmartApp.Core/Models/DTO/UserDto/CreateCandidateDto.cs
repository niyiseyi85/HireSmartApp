using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Models.DTO.UserDto
{
    public class CreateCandidateDto
    {
        public string ProgramId { get; set; }
        public AddUserDto userDto { get; set; }
    }
}
