using Microsoft.AspNetCore.Mvc;
using WebAPI_Levinci.Models;

namespace WebAPI_Levinci.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<Users>>> GetAllUsers();
        Task<ServiceResponse<Users>> GetSingleUser(string strID);
        Task<ServiceResponse<List<Users>>> AddUser(Users user);
        Task<ServiceResponse<List<Users>>> UpdateUser(string strID, Users request);
        Task<ServiceResponse<List<Users>>> DeleteUser(string strID);
    }
}
