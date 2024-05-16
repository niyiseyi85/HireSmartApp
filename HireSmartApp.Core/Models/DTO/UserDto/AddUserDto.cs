using HireSmartApp.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Models.DTO.UserDto
{
    public class AddUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string Gender { get; set; }
        public string IDNumber { get; set; }
        public string QuestionType { get; set; }
        public string Password { get; set; }
        public RoleType RoleType { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
