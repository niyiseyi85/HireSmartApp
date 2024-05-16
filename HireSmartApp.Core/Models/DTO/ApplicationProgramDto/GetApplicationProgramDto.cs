using HireSmartApp.Core.Models.DTO.UserDto;

namespace HireSmartApp.Core.Models.DTO.ApplicationProgramDto
{
    public class GetApplicationProgramDto
    {
        public string Title { get; set; }
        public string ProgramDescription { get; set; }
        public List<GetUserDto> GetUserDtos { get; set; }
    }
}
