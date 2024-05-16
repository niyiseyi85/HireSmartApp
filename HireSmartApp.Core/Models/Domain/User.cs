using HireSmartApp.Core.Models.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Models.Domain
{
    public class User : Entity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string Gender { get; set; }
        public string IDNumber { get; set; }
        public string QuestionType {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
        public bool IsActive { get; set; }
        public List<Questions> Questions { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
