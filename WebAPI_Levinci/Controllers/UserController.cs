using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Levinci.Dtos;
using WebAPI_Levinci.Models;
using WebAPI_Levinci.Services.UserService;

namespace WebAPI_Levinci.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("GetSingleUser")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetSingleUser(string strID)
        {
            return Ok(await _userService.GetSingleUser(strID));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> AddUser(AddUserDto user)
        {
            //var result = _userService.AddUser(user);
            //return Ok(result);
            return Ok(await _userService.AddUser(user));
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<List<GetUserDto>>> UpdateUser(UpdateUserDto request)
        {
            return Ok(await _userService.UpdateUser(request));
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<List<GetUserDto>>> DeleteUser(string strID)
        {
            return Ok(await _userService.DeleteUser(strID));
        }
    }
}
