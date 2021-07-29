using Benchmarks_SQL_JSON.Helper;
using Benchmarks_SQL_JSON.Models;
using Dapper;
using System;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;

namespace Benchmarks_SQL_JSON.DataAccessLayer.SeparatedUseJson
{
    public class OrderUsedJsonRepository
    {
        private const string path = "Resources.SeparatedUseJson.Order";

        private readonly IDbConnection _dbConnection;

        private static readonly Lazy<string> Create = ResourceLoader.ReadLazyByPath<OrderUsedJsonRepository>(path);

        public OrderUsedJsonRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task CreateAsync(User user, IDbTransaction transaction)
        {
            var createParams = new DynamicParameters(new
            {
                UserId = user.Id,
                JsonOrders = JsonSerializer.Serialize(user.Orders)
            });

            var command = new CommandDefinition(
                Create.Value,
                createParams,
                transaction);

            await _dbConnection.ExecuteAsync(command);
        }
    }
}
