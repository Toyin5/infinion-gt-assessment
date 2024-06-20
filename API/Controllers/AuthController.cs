using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthenticationService authenticationService, ILogger<AuthController> logger) : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpPost("register")]
        public async Task<ActionResult<Result>> Register(RegisterRequest request)
        {
            try {
                var response = await _authenticationService.Register(request);
                return Ok(response);
            }
            catch (FluentValidation.ValidationException Ex)
            {
                _logger.LogError(Ex.Message);
                return Result.Failure($"Validation Error: {Ex.Data.Values}");
            }
            catch (Exception Ex){
                _logger.LogError(Ex.Message);
                return Result.Failure("Register Failed!");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<Result>> Login(LoginRequest request)
        {
            try
            {
                var response = await _authenticationService.Login(request);
                return Ok(response);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return Result.Failure("Register Failed!");
            }
        }
    }
}
