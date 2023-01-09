using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Levinci.Data;
using WebAPI_Levinci.Dtos;
using WebAPI_Levinci.Models;

namespace WebAPI_Levinci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authenRepository;

        public AuthController(IAuthRepository authenRepository)
        {
            _authenRepository = authenRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<string?>>> Register(LoginDto loginDto)
        {
            if (loginDto is null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            var response = await _authenRepository.Register(
                new Users { strUserName = loginDto.strUserName },
                loginDto.strPassword
                );
            if (!response.bSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        //[HttpPost("ExistsUser")]
        //public async Task<ActionResult<ServiceResponse<bool>>> Exists(string? strUserName)
        //{
        //    bool? response = await _authenRepository.UserExists(strUserName);
        //    if (!response.Value)
        //    {
        //        return BadRequest(response);
        //    }
        //    return Ok(response);
        //}

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<bool>>> Login(LoginDto loginDto)
        {
            var response = await _authenRepository.Login(loginDto.strUserName, loginDto.strPassword);
            if (!response.bSuccess)
            {
                return BadRequest(response);
            }

            if (response.Data!.strRole == "Admin") // Send Email
            {

            }
            return Ok(response);
        }        
    }
}
