using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OWASP10_2021.Data
{
    public class Account
    {
        public static IEnumerable<Account> SeedData()
        {
            return new Account[] {
                new Account { AccountId = 1, OwnerName = "Central Bank Master Fund", SSN = "000000000", Balance = 9999999999999, Closed = false },
                new Account { AccountId = 1000, OwnerName = "Bruce Banner", SSN = "789456100", Balance = 101, Closed = false },
                new Account { AccountId = 1001, OwnerName = "Tony Stark", SSN = "234681641", Balance = 9999999, Closed = false },
                new Account { AccountId = 2000, OwnerName = "Thanos", SSN = "000000001", Balance = 1, Closed = true },
            };
        }

        public int AccountId { get; set; }
        public string OwnerName { get; set; }
        public string SSN { get; set; }
        public decimal Balance { get; set; }
        public bool Closed { get; set; }
    }
}
