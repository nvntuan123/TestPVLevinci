using Microsoft.AspNetCore.Mvc;
using WebAPI_Levinci.Dtos;
using WebAPI_Levinci.Models;

namespace WebAPI_Levinci.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<GetUserDto>> GetSingleUser(string strID);
        Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto user);
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto request);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(string strID);
    }
}
