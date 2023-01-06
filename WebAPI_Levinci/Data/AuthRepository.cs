using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebAPI_Levinci.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly LevinciContext _levinciContext;

        public AuthRepository(LevinciContext levinciContext)
        {
            _levinciContext = levinciContext;
        }

        public async Task<ServiceResponse<Users?>> Login(string? strUserName, string? strPassword)
        {
            var response = new ServiceResponse<Users?>();
            Users? user = await _levinciContext.users.FirstOrDefaultAsync(u => u.strUserName.ToLower().Equals(strUserName.ToLower()));

            if (user is null)
            {
                response.bSuccess = false;
                response.strMessage = "User not found.";
            }
            else
            {
                response.Data = user;
            }
            return response;
        }

        public async Task<bool?> UserExists(string? strUserName)
        {
            if (await _levinciContext.users.AnyAsync(u => u.strUserName.ToLower() == strUserName.ToLower()))
            {
                return true;
            }
            return false;
        }

        private bool VerifuPasswordHash(string strPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strPassword));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
