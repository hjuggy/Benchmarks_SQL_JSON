using System;
using System.Text.Json.Serialization;

namespace Benchmarks_SQL_JSON.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public string Description { get; set; }

        // for EF only 
        [JsonIgnore]
        public User User { get; set; }

        public Guid User_Id { get; set; }
    }
}
