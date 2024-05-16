using CSharpFunctionalExtensions;
using HireSmartApp.Core.Extensions;
using HireSmartApp.Core.Models.Domain;
using HireSmartApp.Core.Models.DTO.ApplicationProgramDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HireSmartApp.Core.Sevices.IService
{
    public interface IProgramService
    {
        Task<Result<ResponseModel<GetApplicationProgramDto>>> GetAllProgramsAsync();
        Task<Result<ResponseModel>> CreateProgramAsync(AddApplicationProgramDto request);
    }
}
