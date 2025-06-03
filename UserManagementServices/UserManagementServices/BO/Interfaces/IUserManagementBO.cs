using UserManagementServices.Models;

namespace UserManagementServices.BO.Interfaces
{
    public interface IUserManagementBO
    {
        Task<UserMaster> ValidateUserAsync(string username, string password);
        Task<string> GetUserRoleAsync(int userID);
        Task<bool> RegisterUserAsync(UserMasterDTO userManagement);
        Task<bool> ChangePasswordAsync(UserMasterDTO userManagement);
        Task<bool> DeleteUserAsync(int userID);
    }
}
