using System;
using System.Text.Json.Serialization;

namespace Benchmarks_SQL_JSON.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public DateTime DayOfOrder { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        // for EF only 
        [JsonIgnore]
        public User User { get; set; }

        public Guid User_Id { get; set; }
    }
}
