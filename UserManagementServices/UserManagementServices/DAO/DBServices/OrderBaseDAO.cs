using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using UserManagementServices.DAO.DBInterfaces;

namespace UserManagementServices.DAO.DBServices
{
    public class OrderBaseDAO : IOrderBaseDAO
    {
        private readonly Lazy<string> _lazyConnectionString;
        private readonly ILogger<OrderBaseDAO> _logger;
        public OrderBaseDAO(IConfiguration configuration, ILogger<OrderBaseDAO> logger)
        {
            _lazyConnectionString = new Lazy<string>(() =>
            {
                logger.LogInformation("Loading connection string for OrderDBConnection...");
                return configuration.GetConnectionString("OrderDBConnection")!;
            });

            _logger = logger;
        }

        private string ConnectionString => _lazyConnectionString.Value;

        public async Task<int> ExecuteNonQueryAsync(string procedureName, Dictionary<string, object> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = new SqlConnection(ConnectionString);
            //using var command = new SqlCommand(query, connection);
            using var command = new SqlCommand(procedureName, connection)
            {
                CommandType = commandType // <-- This is the fix
            };

            foreach (var param in parameters)
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);

            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<T?> ExecuteScalarAsync<T>(string procedureName, Dictionary<string, object> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = new SqlConnection(ConnectionString);
            //using var command = new SqlCommand(query, connection);
            using var command = new SqlCommand(procedureName, connection)
            {
                CommandType = commandType // <-- This is the fix
            };
            foreach (var param in parameters)
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result != null && result != DBNull.Value ? (T)result : default;
        }

        public async Task<T?> ExecuteDataReaderAsync<T>(string procedureName, Dictionary<string, object> parameters, Func<SqlDataReader, T> mapFunc, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(procedureName, connection) { CommandType = commandType };

            foreach (var param in parameters)
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return mapFunc(reader);
            }

            return default;
        }
        //public async Task<List<T>> ExecuteReaderAsync<T>(string query, Dictionary<string, object> parameters, Func<IDataReader, T> map)
        //{
        //    var result = new List<T>();

        //    using var connection = new SqlConnection(ConnectionString);
        //    using var command = new SqlCommand(query, connection);

        //    foreach (var param in parameters)
        //        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);

        //    await connection.OpenAsync();
        //    using var reader = await command.ExecuteReaderAsync();

        //    while (await reader.ReadAsync())
        //    {
        //        result.Add(map(reader));
        //    }

        //    return result;
        //}

        public async Task<List<T>> ExecuteReaderAsync<T>(string procedureName, Dictionary<string, object> parameters, CommandType commandType = CommandType.StoredProcedure) where T : new()
        {
            var result = new List<T>();

            using var connection = new SqlConnection(ConnectionString);
            //using var command = new SqlCommand(query, connection);
            using var command = new SqlCommand(procedureName, connection)
            {
                CommandType = commandType // <-- This is the fix
            };

            foreach (var param in parameters)
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (await reader.ReadAsync())
            {
                var obj = new T();

                foreach (var prop in props)
                {
                    if (!reader.HasColumn(prop.Name) || reader[prop.Name] is DBNull) continue;

                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name], prop.PropertyType));
                }

                result.Add(obj);
            }

            return result;
        }

        //private static bool HasColumn(this IDataRecord reader, string columnName)
        //{
        //    for (int i = 0; i < reader.FieldCount; i++)
        //    {
        //        if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
        //            return true;
        //    }
        //    return false;
        //}
    }
    public static class DataReaderExtensions
    {
        public static bool HasColumn(this IDataRecord reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
