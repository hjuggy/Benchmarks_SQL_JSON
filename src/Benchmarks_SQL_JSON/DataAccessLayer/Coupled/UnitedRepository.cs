using Benchmarks_SQL_JSON.Helper;
using Benchmarks_SQL_JSON.Models;
using Dapper;
using System;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;

namespace Benchmarks_SQL_JSON.DataAccessLayer.Coupled
{
    public class UnitedRepository
    {
        private const string path = "Resources.Coupled";

        private readonly IDbConnection _dbConnection;

        private static readonly Lazy<string> Create = ResourceLoader.ReadLazyByPath<UnitedRepository>(path);

        public UnitedRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<User> CreateAsync(User user)
        {
            using var transaction = _dbConnection.BeginTransaction();
            try
            {
                await CreateAsync(user, transaction);

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

        private async Task CreateAsync(User user, IDbTransaction transaction)
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
                JsonUser = JsonSerializer.Serialize(user)
        });

            var command = new CommandDefinition(
                Create.Value,
                createParams,
                transaction);

            await _dbConnection.ExecuteAsync(command);
        }
    }
}
