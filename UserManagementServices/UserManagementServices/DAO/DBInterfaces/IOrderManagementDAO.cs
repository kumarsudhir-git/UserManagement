using UserManagementServices.Models;

namespace UserManagementServices.DAO.DBInterfaces
{
    public interface IOrderManagementDAO
    {
        Task<int> CreateOrderAsync(OrderDetailDTO order);
        Task<OrderDetailDTO> GetOrderStatusAsync(string orderNumber);
    }
}
