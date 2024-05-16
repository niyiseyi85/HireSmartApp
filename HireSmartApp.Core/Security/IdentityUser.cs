using HireSmartApp.Core.Models.Domain;
using HireSmartApp.Core.Models.DTO.RoleClaimDto;

namespace HireSmartApp.Core.Security
{
    public class IdentityUser : User
    {
        public string RoleName { get; set; }
        public List<ClaimDto> Claims { get; set; } = new List<ClaimDto>();
    }
}
