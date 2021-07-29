using Benchmarks_SQL_JSON.DataAccessLayer.Coupled;
using Benchmarks_SQL_JSON.DataAccessLayer.Separated.Repositories;
using Benchmarks_SQL_JSON.Models;
using Bogus;
using Npgsql;
using System;
using System.Data;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Benchmarks_SQL_JSON.DataAccessLayer.EF;
using Benchmarks_SQL_JSON.DataAccessLayer.SeparatedUseJson;

namespace Benchmarks_SQL_JSON
{
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 100)]
    public class SqlJson : IDisposable
    {
        private readonly IDbConnection dbConnection;

        private readonly UserRepository userRepository;

        private readonly UserUsedJsonRepository userUsedJsonRepository;

        private readonly UnitedRepository unitedRepository;

        private readonly EfRepository efRepository;

        User testUserForDapper;

        User testUserForDapperJson;

        User testUserForEF;

        User testUserForDapperJsonSeparated;

        private readonly string DbConnectionStringName = "Server=localhost;Port=5432;Username=postgres;Database=SqlJson;Password=password123;SSLMode=Prefer";

        public SqlJson()
        {
            dbConnection = new NpgsqlConnection(DbConnectionStringName);

            userRepository = new UserRepository(dbConnection);

            unitedRepository = new UnitedRepository(dbConnection);

            userUsedJsonRepository = new UserUsedJsonRepository(dbConnection);

            efRepository = new EfRepository();

            dbConnection.Open();

            FirstRun();
        }

        [IterationSetup]
        public void IterationSetup()
        {
            testUserForDapper = GenerateTestUser();

            testUserForDapperJson = GenerateTestUser();

            testUserForEF = GenerateTestUserForEF();

            testUserForDapperJsonSeparated = GenerateTestUser();
        }

        [Benchmark]
        public async Task<User> CreatUseEF() => await efRepository.CreateAsync(testUserForEF);

        [Benchmark]
        public async Task<User> CreatUseDapper() => await userRepository.CreateAsync(testUserForDapper);

        [Benchmark]
        public async Task<User> CreatUseDapperJsonSeparated() => await userUsedJsonRepository.CreateAsync(testUserForDapperJsonSeparated);

        [Benchmark]
        public async Task<User> CreatUseDapperJson() => await unitedRepository.CreateAsync(testUserForDapperJson);

        private User GenerateTestUser()
        {
            var orders = new Order[10];

            for (int i = 0; i < orders.Length; i++)
            {
                orders[i] = new Faker<Order>()
                    .RuleFor(u => u.Id, f => f.Random.Uuid())
                    .RuleFor(u => u.Description, f => f.Lorem.Sentence(4, 5))
                    .RuleFor(u => u.Name, f => f.Lorem.Word())
                    .RuleFor(u => u.Price, f => f.Finance.Amount(0, 2500))
                    .RuleFor(u => u.DayOfOrder, f => f.Date.Between(DateTime.Now.AddDays(-2000), DateTime.Now))
                    .Generate();
            }

            var subscriptions = new Subscription[10];

            for (int i = 0; i < orders.Length; i++)
            {
                subscriptions[i] = new Faker<Subscription>()
                    .RuleFor(u => u.Id, f => f.Random.Uuid())
                    .RuleFor(u => u.Description, f => f.Lorem.Sentence(4, 5))
                    .RuleFor(u => u.Name, f => f.Lorem.Word())
                    .RuleFor(u => u.MaxPrice, f => f.Finance.Amount(1600, 2500))
                    .RuleFor(u => u.MinPrice, f => f.Finance.Amount(0, 1600))
                    .Generate();
            }


            var fakeUser = new Faker<User>()
                .RuleFor(u => u.Id, f => f.Random.Uuid())
                .RuleFor(u => u.Gender, f => f.Person.Gender.ToString())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Country, f => f.Address.Country())
                .RuleFor(u => u.Sity, f => f.Address.City())
                .RuleFor(u => u.Address, (f, u) => f.Address.StreetAddress())
                .RuleFor(u => u.Phone, (f, u) => f.Person.Phone)
                .RuleFor(u => u.DayOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(u => u.Orders, f => orders)
                .RuleFor(u => u.Subscriptions, f => subscriptions);

            return fakeUser.Generate();
        }

        private User GenerateTestUserForEF()
        {
            var user = GenerateTestUser();

            foreach (var order in user.Orders)
            {
                order.User = user;
                order.User_Id = user.Id;
            }

            foreach (var subscription in user.Subscriptions)
            {
                subscription.User = user;
                subscription.User_Id = user.Id;
            }

            return user;
        }

        private void FirstRun()
        {
            // to warm up the connection to DB
            IterationSetup();
            CreatUseEF().Wait();
            CreatUseDapper().Wait();
            CreatUseDapperJsonSeparated().Wait();
            CreatUseDapperJson().Wait();
        }

        public void Dispose()
        {
            dbConnection.Dispose();
            dbConnection.Close();
        }
    }
}
