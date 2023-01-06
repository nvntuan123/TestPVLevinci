using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Levinci.Models;
using WebAPI_Levinci.Services.UserService;

namespace WebAPI_Levinci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Users>>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{strID}")]
        public async Task<ActionResult<ServiceResponse<Users>>> GetSingleUser(string strID)
        {
            return Ok(await _userService.GetSingleUser(strID));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<Users>>>> AddUser(Users user)
        {
            //var result = _userService.AddUser(user);
            //return Ok(result);
            return Ok(await _userService.AddUser(user));
        }

        [HttpPut("{strID}")]
        public async Task<ActionResult<List<Users>>> UpdateUser(string strID, Users request)
        {
            return Ok(await _userService.UpdateUser(strID, request));
        }

        [HttpDelete("{strID}")]
        public async Task<ActionResult<List<Users>>> DeleteUser(string strID)
        {
            return Ok(await _userService.DeleteUser(strID));
        }
    }
}
