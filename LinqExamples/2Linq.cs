using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq2
{
    class Phone
    {
        public string Name { get; set; }
        public string Company { get; set; }
    }

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
            new Phone {Name="Lumia 630", Company="Microsoft" },
            new Phone {Name="iPhone 6", Company="Apple"},
        };

        static void Main1(string[] args)
        {
            int[] numbers = {1, 2, 3, 4, 10, 34, 55, 66, 77, 88};
            IEnumerable<int> evens = numbers.Where(i => i % 2 == 0 && i > 10);

            // ------------
            var filteredUsers1 = users.Where(u => u.Age > 25);

            //----------------
            var names = users.Select(u => u.Name);

            //----------------
            var items = users.Select(u => new
            {
                FirstName = u.Name,
                DateOfBirth = DateTime.Now.Year - u.Age
            });

            //----------------
            var selectedUsers2 = users
                .SelectMany(u => u.Languages)
                .Where(u => u == "английский");

            //----------------
            var selectedUsers3 = users
                .SelectMany(u => u.Languages, (u, l) => new { User = u, Lang = l })
                .Where(u => u.Lang == "английский" && u.User.Age < 28)
                .Select(u=>u.User);

            // -------------------
            var people1 = from user in users
                          from phone in phones
                          select new { Name = user.Name, Phone = phone.Name };

            var people2 = users
                .SelectMany(user => phones, (user, phone) => new
                {
                    Name = user.Name,
                    Phone = phone.Name
                });

            //----------------------
            IEnumerable<int> sortedNumbers = numbers.OrderBy(i=>i);
            var sortedUsers = users.OrderBy(u => u.Name);
            var result = users.OrderBy(u => u.Name).ThenBy(u => u.Age);

            //--------------------
            string[] soft = { "Microsoft", "Google", "Apple"};
            string[] hard = { "Apple", "IBM", "Samsung"};

            var result1 = soft.Except(hard); // Microsoft, Google

            //--------------------
            var result2 = soft.Intersect(hard); // ?

            //--------------------
            var result3 = soft.Union(hard);

            //--------------------
            var result4 = soft.Concat(hard).Distinct();

            //-----------------
            int query = numbers.Aggregate((sum, next) => sum + next);

            //-----------------
            int size1 = numbers.Count();
            int size2 = numbers.Count(i => i % 2 == 0 && i > 10);

            //-----------------
            int sum1 = numbers.Sum();
            int min1 = numbers.Min();
            int min2 = users.Min(n => n.Age);

            int max1 = numbers.Max();
            int max2 = users.Max(n => n.Age);

            double avr1 = numbers.Average();
            double avr2 = users.Average(n => n.Age);

            //---------------
            var result6 = numbers.Take(3);
            var result7 = numbers.Skip(3);

            //-------------------
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };

            foreach (var t in teams.SkipWhile(x => x.StartsWith("Б")))
            {
                Console.WriteLine(t);
            }

            foreach (var t in teams.TakeWhile(x => x.StartsWith("Б")))
            {
                Console.WriteLine(t);
            }

            //-------------------------------
            bool result8 = users.All(u => u.Age > 20); // true
            bool result9 = users.Any(u => u.Age < 20); //false

            // lazy
            var x = users.Where(u => u.Age > 25);
            var y = users.Select(u => u.Name);

            // non-lazy
            // Count(), Average(), First(), FirstOrDefault(), Min(), Max()
            var x1 = users.Where(u => u.Age > 25).ToList();
            var y1 = users.Select(u => u.Name).ToArray();

            var result10 = numbers.Where(i => i > 0).Select(Factorial);
        }

        static int Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}