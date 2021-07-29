using Benchmarks_SQL_JSON.Helper;
using Benchmarks_SQL_JSON.Models;
using Dapper;
using System;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;

namespace Benchmarks_SQL_JSON.DataAccessLayer.SeparatedUseJson
{
    public class SubscriptionUsedJsonRepository
    {
        private const string path = "Resources.SeparatedUseJson.Subscription";

        private readonly IDbConnection _dbConnection;

        private static readonly Lazy<string> Create = ResourceLoader.ReadLazyByPath<SubscriptionUsedJsonRepository>(path);

        public SubscriptionUsedJsonRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        internal async Task CreateAsync(User user, IDbTransaction transaction)
        {
            var createParams = new DynamicParameters(new
            {
                UserId = user.Id,
                JsonSubscriptions = JsonSerializer.Serialize(user.Subscriptions)
            });

            var command = new CommandDefinition(
                Create.Value,
                createParams,
                transaction);

            await _dbConnection.ExecuteAsync(command);
        }
    }
}
