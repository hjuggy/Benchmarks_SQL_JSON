using Benchmarks_SQL_JSON.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Benchmarks_SQL_JSON.DataAccessLayer.EF
{
    public class EfRepository
    {
        private readonly AppTestContext dbAContext;

        public EfRepository()
        {
            dbAContext = new AppTestContext();
        }

        public async Task<User> CreateAsync(User user)
        {
            try
            {
                await dbAContext.Users.AddAsync(user);
                await dbAContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(JsonSerializer.Serialize(user));
                return null;
            }
        }
    }
}
