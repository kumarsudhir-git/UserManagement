using UserManagementServices.Models;

namespace UserManagementServices.BO.Interfaces
{
    public interface IOrderManagementBO
    {
        Task<OrderDetailDTO> GetOrderStatusAsync(string orderNumber);
    }
}
