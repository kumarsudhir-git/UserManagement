using UserManagementServices.Models;

namespace UserManagementServices.DAO.DBInterfaces
{
    public interface IUserManagementDAO
    {
        Task<int> CreateUserAsync(UserMasterDTO user);
        Task<UserMasterDTO> GetUserDetailsAsync(string userId);
        Task<int> SaveUserRole(RoleMasterDTO userRole);
        Task<UserMaster> ValidateUserAsync(string username, string password);
    }
}
