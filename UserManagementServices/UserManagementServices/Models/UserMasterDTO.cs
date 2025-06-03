using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementServices.Models
{
    public class UserMasterDTO
    {
        public int UserID { get; set; }

        [Required]
        [MaxLength(150)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Password { get; set; }
        
        [MaxLength(150)]
        public string oldPassword { get; set; }

        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        //public List<UserRole> UserRoles { get; set; }
    }

    public class UserMaster
    {
        public int UserID { get; set; }
        public string RoleName { get; set; }
        public int RoleID { get; set; }
    }

    public class LoginMaster
    {
        [Required]
        [MaxLength(150)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Password { get; set; }
    }
}
