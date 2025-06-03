using Microsoft.Data.SqlClient;
using System.Data;

namespace UserManagementServices.DAO.DBInterfaces
{
    public interface IOrderBaseDAO
    {
        Task<int> ExecuteNonQueryAsync(string procedureName, Dictionary<string, object> parameters, CommandType commandType = CommandType.StoredProcedure);
        Task<T?> ExecuteScalarAsync<T>(string procedureName, Dictionary<string, object> parameters, CommandType commandType = CommandType.StoredProcedure);
        Task<T?> ExecuteDataReaderAsync<T>(string procedureName, Dictionary<string, object> parameters, Func<SqlDataReader, T> mapFunc, CommandType commandType = CommandType.StoredProcedure);
        Task<List<T>> ExecuteReaderAsync<T>(string procedureName, Dictionary<string, object> parameters, CommandType commandType = CommandType.StoredProcedure) where T : new();
    }
}
