using CSharpFunctionalExtensions;
using HireSmartApp.Core.Extensions;
using HireSmartApp.Core.Models.DTO.ApplicationProgramDto;
using HireSmartApp.Core.Sevices.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireSmartApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : BaseApiController
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }
        
        [HttpPost("createProgram")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<ResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProgram(AddApplicationProgramDto request)
        {
            var response = await _programService.CreateProgramAsync(request);
            Result res = Result.Combine(response);
            if (res.IsFailure)
                return Error(res.Error);
            return Ok(response.Value);
        }

        [HttpGet("getAllProgram")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<ResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProgram()
        {
            var response = await _programService.GetAllProgramsAsync();
            Result res = Result.Combine(response);
            if (res.IsFailure)
                return Error(res.Error);
            return Ok(response.Value);
        }
    }
}
