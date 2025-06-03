using UserManagementServices.DAO.DBInterfaces;
using UserManagementServices.Models;

namespace UserManagementServices.DAO.DBServices
{
    public class OrderManagementDAO : IOrderManagementDAO
    {
        private const string GetOrderDetails = "usp_GetOrderDetails";
        private readonly IOrderBaseDAO _orderBaseDAO;
        public OrderManagementDAO(IOrderBaseDAO orderBaseDAO)
        {
            this._orderBaseDAO = orderBaseDAO;
        }

        // Implement methods specific to OrderManagementDAO here, using orderBaseDAO as needed.
        public async Task<int> CreateOrderAsync(OrderDetailDTO order)
        {
            var query = "INSERT INTO Orders (Id, CustomerName, OrderDate) VALUES (@Id, @CustomerName, @OrderDate)";
            var parameters = new Dictionary<string, object>
            {
                { "@Id", order.OrderId },
                { "@CustomerName", order.CustomerName },
                { "@OrderDate", order.OrderDate }
            };
            return await _orderBaseDAO.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<OrderDetailDTO> GetOrderStatusAsync(string orderNumber)
        {
            var parameters = new Dictionary<string, object> { { "@OrderNumber", orderNumber } };

            OrderDetailDTO result = await _orderBaseDAO.ExecuteDataReaderAsync<OrderDetailDTO>(GetOrderDetails, parameters, reader => new OrderDetailDTO
            {
                //OrderId = reader.GetString(reader.GetOrdinal("OrderId")),
                OrderNumber = reader.GetString(reader.GetOrdinal("OrderNumber")),
                OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                //OrderStatus = (OrderStatus)reader.GetString(reader.GetOrdinal("OrderStatus")),
                ShippingAddress = reader.GetString(reader.GetOrdinal("ShippingAddress")),
                BillingAddress = reader.GetString(reader.GetOrdinal("BillingAddress")),
                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
            });
            return result;
        }
    }
}
