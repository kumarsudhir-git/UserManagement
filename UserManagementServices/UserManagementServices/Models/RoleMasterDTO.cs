using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementServices.Models
{
    public class RoleMasterDTO
    {
        public int RoleID { get; set; }
        [Required]
        [MaxLength(150)]
        public string RoleName { get; set; }
        public bool IsActive { get; set; } = true;
        public int UserID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
