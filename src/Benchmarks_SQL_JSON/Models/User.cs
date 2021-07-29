using System;
using System.Collections.Generic;

namespace Benchmarks_SQL_JSON.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Country { get; set; }

        public string Sity { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public DateTime DayOfBirth { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}
