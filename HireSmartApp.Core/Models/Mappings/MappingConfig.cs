using AutoMapper;
using HireSmartApp.Core.Models.Domain;
using HireSmartApp.Core.Models.DTO.ApplicationProgramDto;
using HireSmartApp.Core.Models.DTO.QuestionDto;
using HireSmartApp.Core.Models.DTO.UserDto;
using HireSmartApp.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Models.Mappings
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<AddApplicationProgramDto, ApplicationProgram>().ReverseMap();
            CreateMap<GetApplicationProgramDto, ApplicationProgram>().ReverseMap();
            
            CreateMap<GetQuestionDto, Questions>().ReverseMap();

            CreateMap<AddUserDto, User>().ReverseMap();
            CreateMap<GetUserDto, User>().ReverseMap();
            CreateMap<EditUserDto, User>().ReverseMap();

            CreateMap<User, IdentityUser>();
        }
    }
}
