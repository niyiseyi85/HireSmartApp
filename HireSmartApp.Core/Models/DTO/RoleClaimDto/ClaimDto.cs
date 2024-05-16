using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Models.DTO.RoleClaimDto
{
    public class ClaimDto
    {
        public int ClaimId { get; set; }

        /// <summary>
        ///  The claim name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The claim description
        /// </summary>
        public string Description { get; set; }
    }
}
