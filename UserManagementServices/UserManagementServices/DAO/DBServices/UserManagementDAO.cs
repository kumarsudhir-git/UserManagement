using UserManagementServices.DAO.DBInterfaces;
using UserManagementServices.Models;

namespace UserManagementServices.DAO.DBServices
{
    public class UserManagementDAO : IUserManagementDAO
    {
        private const string CreateUser = "usp_SaveUser";
        private const string UserRole = "usp_SaveUserRole";
        private const string ValidateUser = "usp_ValidateUser";
        private readonly IOrderBaseDAO _orderBaseDAO;
        public UserManagementDAO(IOrderBaseDAO orderBaseDAO)
        {
            this._orderBaseDAO = orderBaseDAO;
        }
        // Example method implementation
        public async Task<int> CreateUserAsync(UserMasterDTO user)
        {
            var param = new Dictionary<string, object> {
                { "@UserID", user.UserID },
                { "@Password", user.Password },
                { "@RoleID", user.RoleID },
                { "@CreatedBy", user.CreatedBy },
                { "@ModifiedBy", user.ModifiedBy }
            };
            // Implementation for creating a user
            int result = await _orderBaseDAO.ExecuteNonQueryAsync(CreateUser, param);
            return result;
        }
        public async Task<UserMasterDTO> GetUserDetailsAsync(string userId)
        {
            // Implementation for getting user details
            throw new NotImplementedException();
        }

        public async Task<int> SaveUserRole(RoleMasterDTO roleMaster)
        {
            var param = new Dictionary<string, object> {
                { "@RoleID", roleMaster.RoleID},
                { "@RoleName", roleMaster.RoleName },
                { "@UserID", roleMaster.UserID }
            };
            // Implementation for creating a user
            int result = await _orderBaseDAO.ExecuteNonQueryAsync(UserRole, param);
            return result;
        }

        public async Task<UserMaster> ValidateUserAsync(string username, string password)
        {
            // Implementation for validating a user

            var param = new Dictionary<string, object> {
                { "@Username", username },
                { "@Password", password },
            };

            UserMaster result = await _orderBaseDAO.ExecuteDataReaderAsync<UserMaster>(ValidateUser, param, reader => new UserMaster
            {
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                RoleID = reader.GetInt32(reader.GetOrdinal("RoleID")),
                RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                // ... map other fields
            });
            return result;
        }
    }
}
