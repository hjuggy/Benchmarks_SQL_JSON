using Benchmarks_SQL_JSON.Helper;
using Benchmarks_SQL_JSON.Models;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Benchmarks_SQL_JSON.DataAccessLayer.SeparatedUseJson
{
    public class UserUsedJsonRepository
    {
        private const string path = "Resources.Separated.User";

        private readonly IDbConnection _dbConnection;
        private readonly OrderUsedJsonRepository orderRepository;
        private readonly SubscriptionUsedJsonRepository subscriptionRepository;

        private static readonly Lazy<string> Create = ResourceLoader.ReadLazyByPath<UserUsedJsonRepository>(path);

        public UserUsedJsonRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;

            orderRepository = new OrderUsedJsonRepository(dbConnection);
            subscriptionRepository = new SubscriptionUsedJsonRepository(dbConnection);
        }

        public async Task<User> CreateAsync(User user)
        {
            using var transaction = _dbConnection.BeginTransaction();
            try
            {
                await CreateUserAsync(user, transaction);
                await CreateOrdersAsync(user, transaction);
                await CreateSubscriptionsAsync(user, transaction);

                transaction.Commit();
                return user;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.Message);
                Console.WriteLine(JsonSerializer.Serialize(user));
                return null;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private async Task CreateSubscriptionsAsync(User user, IDbTransaction transaction)
        {
            if (user.Subscriptions?.Any() != true)
            {
                return;
            }

            await subscriptionRepository.CreateAsync(user, transaction);
        }

        private async Task CreateOrdersAsync(User user, IDbTransaction transaction)
        {
            if (user.Orders?.Any() != true)
            {
                return;
            }

            await orderRepository.CreateAsync(user, transaction);
        }

        private async Task CreateUserAsync(User user, IDbTransaction transaction)
        {
            var createParams = new DynamicParameters(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Phone,
                user.Country,
                user.Sity,
                user.Address,
                user.Gender,
                user.DayOfBirth,
            });

            var command = new CommandDefinition(
                Create.Value,
                createParams,
                transaction);

            await _dbConnection.ExecuteAsync(command);
        }
    }
}
