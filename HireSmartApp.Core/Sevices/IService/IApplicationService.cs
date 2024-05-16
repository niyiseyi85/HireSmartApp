using CSharpFunctionalExtensions;
using HireSmartApp.Core.Extensions;
using HireSmartApp.Core.Models.Domain;
using HireSmartApp.Core.Models.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Sevices.IService
{
    public interface IApplicationService
    {
        Task<Result<ResponseModel>> SubmitCandidateApplicationAsync(CreateCandidateDto request);
        Task<Result<ResponseModel<List<Questions>>>> GetQuestionByQuestionType(string questionType);
        Task<Result<ResponseModel>> EditApplication(EditUserDto request);
        Task<Result<ResponseModel>> CreateUserApplication(AddUserDto request);
    }
}
