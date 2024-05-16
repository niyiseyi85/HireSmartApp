using AutoMapper;

using CSharpFunctionalExtensions;
using HireSmartApp.Core.Data.Repository.IRepository;
using HireSmartApp.Core.Extensions;
using HireSmartApp.Core.Extensions.Constants;
using HireSmartApp.Core.Models.Domain;
using HireSmartApp.Core.Models.DTO.ApplicationProgramDto;

using HireSmartApp.Core.Sevices.IService;

namespace HireSmartApp.Core.Sevices
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProgramService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ResponseModel>> CreateProgramAsync(AddApplicationProgramDto request)
        {
            var response = new ResponseModel();

            var program = _mapper.Map<ApplicationProgram>(request);
            try
            {
                await _unitOfWork.ProgramRepository.Add(program);
                response.IsSuccessful = true;
                response.Message = UserConstants.Messages.UserSavedSuccessful;
            }
            catch (Exception ex)
            {
                return Result.Failure<ResponseModel>($"An error has occured - {ex.Message} : {ex.InnerException}");
            }
            return response;
        }
        public async Task<Result<ResponseModel<GetApplicationProgramDto>>> GetAllProgramsAsync()
        {
            var response = new ResponseModel<GetApplicationProgramDto>();

            var programList = await _unitOfWork.ProgramRepository.GetAll();
            var program = _mapper.Map<GetApplicationProgramDto>(programList);
            response.Data = program;
            return response;
        }


    }
}
