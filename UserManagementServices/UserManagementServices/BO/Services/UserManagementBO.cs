using UserManagementServices.BO.Interfaces;
using UserManagementServices.DAO.DBInterfaces;
using UserManagementServices.Models;

namespace UserManagementServices.BO.Services
{
    public class UserManagementBO : IUserManagementBO
    {
        private readonly IUserManagementDAO _contextDAL;
        public UserManagementBO(IUserManagementDAO contextDAL)
        {
            _contextDAL = contextDAL;
        }

        public Task<bool> ChangePasswordAsync(UserMasterDTO userManagement)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(int userID)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserRoleAsync(int userID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterUserAsync(UserMasterDTO userManagement)
        {
            throw new NotImplementedException();
        }

        public async Task<UserMaster> ValidateUserAsync(string username, string password)
        {
            return await _contextDAL.ValidateUserAsync(username, password);
        }
    }
}
