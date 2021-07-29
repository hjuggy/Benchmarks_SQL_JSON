using Benchmarks_SQL_JSON.Helper;
using Benchmarks_SQL_JSON.Models;
using Dapper;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Benchmarks_SQL_JSON.DataAccessLayer.Separated.Repositories
{
    public class SubscriptionRepository
    {
        private const string path = "Resources.Separated.Subscription";

        private readonly IDbConnection _dbConnection;

        private static readonly Lazy<string> Create = ResourceLoader.ReadLazyByPath<SubscriptionRepository>(path);

        public SubscriptionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        internal async Task CreateAsync(Guid userId, Subscription subscription, IDbTransaction transaction)
        {
            var createParams = new DynamicParameters(new
            {
                UserId = userId,
                subscription.Id,
                subscription.Name,
                subscription.MaxPrice,
                subscription.MinPrice,
                subscription.Description,
            });

            var command = new CommandDefinition(
                Create.Value,
                createParams,
                transaction);

            await _dbConnection.ExecuteAsync(command);
        }
    }
}
