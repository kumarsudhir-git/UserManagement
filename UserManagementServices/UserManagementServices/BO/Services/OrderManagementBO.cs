using UserManagementServices.BO.Interfaces;
using UserManagementServices.DAO.DBInterfaces;
using UserManagementServices.Models;

namespace UserManagementServices.BO.Services
{
    public class OrderManagementBO : IOrderManagementBO
    {
        private readonly IOrderManagementDAO _contextDAL;
        public OrderManagementBO(IOrderManagementDAO contextDAL) 
        {
            _contextDAL = contextDAL;
        }

        public async Task<OrderDetailDTO> GetOrderStatusAsync(string orderNumber)
        {

            OrderDetailDTO orderDetailDTO = await _contextDAL.GetOrderStatusAsync(orderNumber);
            return orderDetailDTO ?? new OrderDetailDTO();
            //string query = "SELECT OrderStatus FROM Orders WHERE OrderId = @Id";
            //
            //return result ?? new OrderDetailDTO();
        }
        public async Task<int> CreateOrderAsync(OrderDetailDTO order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            return await _contextDAL.CreateOrderAsync(order);
        }
    }
}
