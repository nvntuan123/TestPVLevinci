using WebAPI_Levinci.Dtos;
using WebAPI_Levinci.Models;

namespace WebAPI_Levinci.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string?>> Register(Users? user, string? strPassword);
        Task<ServiceResponse<Users?>> Login(string? strUserName, string? strPassword);
        Task<bool?> UserExists(string? strUserName);
    }
}
