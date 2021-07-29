using Benchmarks_SQL_JSON.Helper;
using Benchmarks_SQL_JSON.Models;
using Dapper;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Benchmarks_SQL_JSON.DataAccessLayer.Separated.Repositories
{
    public class OrderRepository
    {
        private const string path = "Resources.Separated.Order";

        private readonly IDbConnection _dbConnection;

        private static readonly Lazy<string> Create = ResourceLoader.ReadLazyByPath<OrderRepository>(path);

        public OrderRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task CreateAsync(Guid userId, Order order, IDbTransaction transaction)
        {
            var createParams = new DynamicParameters(new
            {
                UserId = userId,
                order.Id,
                order.Name,
                order.Price,
                order.Description,
                order.DayOfOrder
            });

            var command = new CommandDefinition(
                Create.Value,
                createParams,
                transaction);

            await _dbConnection.ExecuteAsync(command);
        }
    }
}
