namespace WebAPI_Levinci.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<Users?>> Login(string? strUserName, string? strPassword);
        Task<bool?> UserExists(string? strUserName);
    }
}
