using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Levinci.Data;

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

        [HttpPost("strUserName")]
        public async Task<ActionResult<ServiceResponse<bool>>> Exists(string? strUserName)
        {
            bool? response = await _authenRepository.UserExists(strUserName);
            if (!response.Value)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("strUserName, strPassword")]
        public async Task<ActionResult<ServiceResponse<bool>>> Login(string? strUserName, string? strPassword)
        {
            var response = await _authenRepository.Login(strUserName, strPassword);
            if (!response.bSuccess)
            {
                return BadRequest(response);
            }

            if (response.Data.strRole == "Admin") // Send Email
            {

            }
            return Ok(response);
        }
    }
}
