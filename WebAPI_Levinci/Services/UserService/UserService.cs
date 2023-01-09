using WebAPI_Levinci.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using WebAPI_Levinci.Dtos;
using AutoMapper;

namespace WebAPI_Levinci.Services.UserService
{
    public class UserService : IUserService
    {
        private LevinciContext _levinciContext;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, LevinciContext levinciContext)
        {
            _mapper = mapper;
            _levinciContext = levinciContext;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var lstUser = await _levinciContext.users.ToListAsync();
            serviceResponse.Data = lstUser.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> GetSingleUser(string strID)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var lstUser = await _levinciContext.users.FirstOrDefaultAsync(u => u.strID == strID);
            serviceResponse.Data = _mapper.Map<GetUserDto>(lstUser);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            try
            {
                var user = _mapper.Map<Users>(newUser);

                // Password -> Hash, Salt
                CreatePasswordHash(strPassword: newUser.strPassword, strPasswordHash: out byte[]? strPasswordHash, strPasswordSalt: out byte[]? strtPasswordSalt);

                user.bPasswordHash = strPasswordHash;
                user.bPasswordSalt = strtPasswordSalt;

                _levinciContext.users.Add(user);
                await _levinciContext.SaveChangesAsync();

                serviceResponse.Data = await _levinciContext.users.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.strMessage= ex.Message;
                serviceResponse.bSuccess = false;
            }
            return serviceResponse;
        }

        private void CreatePasswordHash(string? strPassword, out byte[]? strPasswordHash, out byte[]? strPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                strPasswordSalt = hmac.Key;
                strPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strPassword!));
            }
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto request)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            try
            {
                var user = await _levinciContext.users.FirstOrDefaultAsync(u => u.strID == request.strID);
                if (user is null)
                {
                    throw new Exception($"User with ID '{request.strID}' not found.");
                }

                user.strUserName = request.strUserName;
                user.strName = request.strName;
                user.strRole = request.strRole;
                user.strEmail = request.strEmail;

                // Password -> Hash, Salt
                CreatePasswordHash(strPassword: request.strPassword, strPasswordHash: out byte[]? strPasswordHash, strPasswordSalt: out byte[]? strtPasswordSalt);

                user.bPasswordHash = strPasswordHash;
                user.bPasswordSalt = strtPasswordSalt;

                //_levinciContext.users.Update(user);
                await _levinciContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.strMessage = ex.Message;
                serviceResponse.bSuccess = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(string strID)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            try
            {
                var user = await _levinciContext.users.FirstOrDefaultAsync(u => u.strID == strID);
                if (user is null)
                {
                    throw new Exception($"User with ID '{strID}' not found.");
                }

                _levinciContext.users.Remove(user);
                await _levinciContext.SaveChangesAsync();

                serviceResponse.Data = await _levinciContext.users.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.strMessage = ex.Message;
                serviceResponse.bSuccess = false;
            }
            return serviceResponse;
        }
    }
}
