using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq3
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Languages { get; set; }

        public User()
        {
            Languages = new List<string>();
        }
    }

    class Phone
    {
        public string Name { get; set; }
        public string Company { get; set; }
    }

    class Program
    {
        private static List<User> users = new List<User>
        {
            new User {Name = "Том", Age = 23, Languages = new List<string> {"английский", "немецкий"}},
            new User {Name = "Боб", Age = 27, Languages = new List<string> {"английский", "французский"}},
            new User {Name = "Джон", Age = 29, Languages = new List<string> {"английский", "испанский"}},
            new User {Name = "Элис", Age = 24, Languages = new List<string> {"испанский", "немецкий"}}
        };

        private static List<Phone> phones = new List<Phone>
        {
            new Phone {Name="Lumia 430", Company="Microsoft" },
            new Phone {Name="Mi 5", Company="Xiaomi" },
            new Phone {Name="LG G 3", Company="LG" },
            new Phone {Name="iPhone 5", Company="Apple" },
            new Phone {Name="Lumia 930", Company="Microsoft" },
            new Phone {Name="iPhone 6", Company="Apple" },
            new Phone {Name="Lumia 630", Company="Microsoft" },
            new Phone {Name="LG G 4", Company="LG" }
        };

        static void Main1(string[] args)
        {
            var phoneGroups = phones.GroupBy(p => p.Company);

            foreach (IGrouping<string, Phone> g in phoneGroups)
            {
                Console.WriteLine(g.Key);

                foreach (var t in g)
                {
                    Console.WriteLine(t.Name);
                }
            }

            //
            // Microsoft (key)
            // Lumia 430
            // Lumia 930
            // Lumia 630
            //
            // Xiaomi  (key)
            // Mi 5
            //
            // LG  (key)
            // LG G 3
            // LG G4
            //
            // Apple  (key)
            // iPhone 5
            // iPhone 6

            //--------------------------------------
            var phoneGroups2 = phones.GroupBy(p => p.Company)
                .Select(g => new
                {
                    Name = g.Key,
                    Count = g.Count(),
                    Phones = "A, B, C" // ?
                });
        }
    }
}