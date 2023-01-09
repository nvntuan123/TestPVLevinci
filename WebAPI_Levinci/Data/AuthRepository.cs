using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPI_Levinci.Dtos;
using WebAPI_Levinci.Models;

namespace WebAPI_Levinci.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly LevinciContext _levinciContext;
        private readonly IConfiguration _configuration;

        public AuthRepository(LevinciContext levinciContext, IConfiguration configuration)
        {
            _levinciContext = levinciContext;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string?>> Register(Users? user, string? strPassword)
        {
            var response = new ServiceResponse<string?>();
            if (string.IsNullOrEmpty(user!.strUserName) || string.IsNullOrWhiteSpace(strPassword))
            {
                response.bSuccess = false;
                response.strMessage = "User or Password is null, empty.";
                return response;
            }

            //var response = new ServiceResponse<string?>();
            if ((await UserExists(user.strUserName)).Value)
            {
                response.bSuccess = false;
                response.strMessage = "User already exists.";
                return response;
            }

            CreatePasswordHash(strPassword: strPassword, strPasswordHash: out byte[]? strPasswordHash, strPasswordSalt: out byte[]? strtPasswordSalt);

            int? iUserCount = null;
            if (_levinciContext.users == null)
            {
                iUserCount = 0;
            }
            else
            {
                _levinciContext.users.Count();
            }
            user.strID = (iUserCount + 1).ToString();
            //user.strID = 1.ToString();
            user.bPasswordHash = strPasswordHash;
            user.bPasswordSalt = strtPasswordSalt;

            _levinciContext.users.Add(user);
            await _levinciContext.SaveChangesAsync();
            //var response = new ServiceResponse<string?>();
            response.Data = user.strID;
            return response;
        }

        public async Task<ServiceResponse<Users?>> Login(string? strUserName, string? strPassword)
        {
            var response = new ServiceResponse<Users?>();
            var user = await _levinciContext.users.FirstOrDefaultAsync(predicate: u => u.strUserName!.ToLower().Equals(strUserName!.ToLower()));

            if (user is null)
            {
                response.bSuccess = false;
                response.strMessage = "User not found.";
            }
            else if (!VerifuPasswordHash(strPassword: strPassword!, passwordHash: user.bPasswordHash!, passwordSalt: user.bPasswordSalt!))
            {
                response.bSuccess = false;
                response.strMessage = "Wrong password.";
            }
            else
            {
                response.Data = user;
                response.strAccessToken = CreateToken(user);
            }
            return response;
        }

        public async Task<bool?> UserExists(string? strUserName)
        {
            if (await _levinciContext.users.AnyAsync(predicate: u => u.strUserName!.ToLower() == strUserName!.ToLower()))
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

        private void CreatePasswordHash(string? strPassword, out byte[]? strPasswordHash, out byte[]? strPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                strPasswordSalt = hmac.Key;
                strPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strPassword!));
            }
        }

        private string? CreateToken(Users? user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.strID!),
                new Claim(ClaimTypes.Name, user.strUserName!)
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
            {
                throw new Exception("AppSettings Token is null!");
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
            //return _configuration.GetSection("AppSettings:Token").Value;
        }
    }
}
