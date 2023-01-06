using WebAPI_Levinci.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace WebAPI_Levinci.Services.UserService
{
    public class UserService : IUserService
    {
        private LevinciContext _levinciContext;
        private static List<Users> lstUser = new List<Users>
        {
            new Users
            {
                strID = "1",
                strUserName = "nvntuan",
                strPassword = "123",
                strName = "Nhat Tuan",
                strRole = "Admin",
                strEmail = "nvntuan123@gmail.com"
            },
            new Users
            {
                strID = "2",
                strUserName = "nvh",
                strPassword = "456",
                strName = "Hung",
                strRole = "User",
                strEmail = "nvhun123@gmail.com"
            }
        };

        public UserService(LevinciContext levinciContext)
        {
            _levinciContext = levinciContext;
        }

        public async Task<ServiceResponse<List<Users>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<Users>>();
            //serviceResponse.Data = lstUser;
            serviceResponse.Data = await _levinciContext.users.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Users>> GetSingleUser(string strID)
        {
            var serviceResponse = new ServiceResponse<Users>();
            //Users? user = lstUser.FirstOrDefault(x => x.strID == strID);
            Users? user = await _levinciContext.users.FirstOrDefaultAsync(x => x.strID == strID);
            serviceResponse.Data = user;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Users>>> AddUser(Users user)
        {
            var serviceResponse = new ServiceResponse<List<Users>>();
            try
            {
                await _levinciContext.users.AddAsync(user);
                await _levinciContext.SaveChangesAsync();
                serviceResponse.Data = await _levinciContext.users.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.strMessage= ex.Message;
                serviceResponse.bSuccess = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Users>>> UpdateUser(string strID, Users request)
        {
            var serviceResponse = new ServiceResponse<List<Users>>();
            try
            {
                var user = await _levinciContext.users.FirstOrDefaultAsync(x => x.strID == strID);
                user.strUserName = request.strUserName;
                user.strPassword = request.strPassword;
                user.strName = request.strName;
                user.strRole = request.strRole;
                user.strEmail = request.strEmail;

                await _levinciContext.SaveChangesAsync();

                serviceResponse.Data = await _levinciContext.users.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.strMessage = ex.Message;
                serviceResponse.bSuccess = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Users>>> DeleteUser(string strID)
        {
            var serviceResponse = new ServiceResponse<List<Users>>();
            try
            {
                Users? user = await _levinciContext.users.FirstOrDefaultAsync(x => x.strID == strID);
                
                _levinciContext.users.Remove(user);

                await _levinciContext.SaveChangesAsync();

                serviceResponse.Data = await _levinciContext.users.ToListAsync();
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
