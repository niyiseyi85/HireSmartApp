using AutoMapper;
using CSharpFunctionalExtensions;

using HireSmartApp.Core.Data.Repository.IRepository;
using HireSmartApp.Core.Extensions;
using HireSmartApp.Core.Extensions.Constants;
using HireSmartApp.Core.Models.Domain;
using HireSmartApp.Core.Models.DTO.UserDto;
using HireSmartApp.Core.Security;
using HireSmartApp.Core.Sevices.IService;

namespace HireSmartApp.Core.Sevices
{
    public class ApplicationService : IApplicationService
    {
        private readonly IPasswordService _passwordService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationService(IMapper mapper, IPasswordService passwordService,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _passwordService = passwordService;
            _unitOfWork = unitOfWork;        
        }
        public async Task<Result<ResponseModel>> CreateUserApplication(AddUserDto request)
        {
            var email = request.Email;
            if (request.RoleType != RoleType.Employer)
            {
                return Result.Failure<ResponseModel>(UserConstants.ErrorMessages.InvalidUserRole);
            }
            var response = new ResponseModel();

            var user = await _unitOfWork.UserRepository.GetFirstOrDefault(x => x.Email == request.Email);

            if (user != null)
            {
                return Result.Failure<ResponseModel>(UserConstants.ErrorMessages.UserExists);
            }

            user = _mapper.Map<User>(request);

            var role = await _unitOfWork.RoleRepository.GetFirstOrDefault(x => x.Name == request.RoleType.ToString());

            user.RoleId = role.RoleId;
            user.IsActive = true;      
            user.Salt = _passwordService.CreateSalt();
            user.HashPassword = _passwordService.CreateHash(request.Password, user.Salt);

            try
            {
                await _unitOfWork.UserRepository.Add(user);
                await _unitOfWork.SaveAsync();
                response.IsSuccessful = true;
                response.Message = UserConstants.Messages.UserSavedSuccessful;
            }
            catch (Exception ex)
            {
                return Result.Failure<ResponseModel>($"An error has occured - {ex.Message} : {ex.InnerException}");
            }
            return response;
        }
        public async Task<Result<ResponseModel>> EditApplication(EditUserDto request)
        {
            var response = new ResponseModel();

            var user = await _unitOfWork.UserRepository.GetFirstOrDefault(x =>x.Id.Equals(request.Id));
            if (user == null)
            {
                return Result.Failure<ResponseModel>(UserConstants.ErrorMessages.UserNotFoundWithID);
            }

            var question = _mapper.Map<List<Questions>>(request.QuestionDto);
            user.Questions = question;
            try
            {
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                response.IsSuccessful = true;
                response.Message = UserConstants.Messages.UserSavedSuccessful;
            }
            catch (Exception ex)
            {
                return Result.Failure<ResponseModel>($"An error has occured - {ex.Message} : {ex.InnerException}");
            }
            return response;
        }

        public async Task<Result<ResponseModel<List<Questions>>>> GetQuestionByQuestionType(string questionType)
        {
            var response = new ResponseModel<List<Questions>>();
            if (questionType == null)
            {
                return Result.Failure<ResponseModel<List<Questions>>>("Invalid questionType.");
            }
            
            var questionsList = await _unitOfWork.QuestionRepository.GetAll();

            var questions = questionsList.Where(q => q.Type == questionType);
            
            response.IsSuccessful = true;
            response.Data = (List<Questions>)questions;
            return response;
        }
        public async Task<Result<ResponseModel>> SubmitCandidateApplicationAsync(CreateCandidateDto request)
        {
            var response = new ResponseModel();
            var candidate = _mapper.Map<User>(request.userDto);
            candidate.RoleId = 3; //request.RoleId;
              

            await _unitOfWork.UserRepository.Add(candidate);

            var program = await _unitOfWork.ProgramRepository.GetFirstOrDefault(x => x.Id.Equals(request.ProgramId));
            try
            {
                if (program != null)
                {
                    program.Users.Add(candidate);
                     _unitOfWork.ProgramRepository.Update(program);
                    await _unitOfWork.SaveAsync();
                }
                response.IsSuccessful = true;
                response.Message = UserConstants.Messages.SubmitApplicationSucessful;
            }
            catch (Exception ex)
            {
                return Result.Failure<ResponseModel>($"An error has occured - {ex.Message} : {ex.InnerException}");

            }

            return response;   
        }

    }
}