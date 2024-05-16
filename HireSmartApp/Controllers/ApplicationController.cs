using CSharpFunctionalExtensions;
using HireSmartApp.Core.Extensions;
using HireSmartApp.Core.Models.DTO.UserDto;
using HireSmartApp.Core.Sevices.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireSmartApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : BaseApiController
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService) 
        {
            _applicationService = applicationService;
        }

        [HttpPost("addEmployer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<ResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProgram(AddUserDto request)
        {
            var response = await _applicationService.CreateUserApplication(request);
            Result res = Result.Combine(response);
            if (res.IsFailure)
                return Error(res.Error);
            return Ok(response.Value);
        }

        [HttpPut("editApplication")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<ResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateApplication(EditUserDto request)
        {
            var response = await _applicationService.EditApplication(request);
            Result res = Result.Combine(response);
            if (res.IsFailure)
                return Error(res.Error);
            return Ok(response.Value);
        }
        [HttpPost("submitCandidate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<ResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SubmitCandidate(CreateCandidateDto request)
        {
            var response = await _applicationService.SubmitCandidateApplicationAsync(request);
            Result res = Result.Combine(response);
            if (res.IsFailure)
                return Error(res.Error);
            return Ok(response.Value);
        }
        [HttpGet("getQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<ResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQuestion(string request)
        {
            var response = await _applicationService.GetQuestionByQuestionType(request);
            Result res = Result.Combine(response);
            if (res.IsFailure)
                return Error(res.Error);
            return Ok(response.Value);
        }
    }
}
