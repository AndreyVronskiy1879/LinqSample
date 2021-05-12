using System.Collections.Generic;
using System.Linq;

namespace Linq4
{
    class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
    }
    class Team
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }

    class Program
    {
        private static List<Team> teams = new List<Team>()
        {
            new Team { Name = "Бавария", Country ="Германия" },
            new Team { Name = "Барселона", Country ="Испания" }
        };

        private static List<Player> players = new List<Player>()
        {
            new Player {Name="Месси", Team="Барселона"},
            new Player {Name="Неймар", Team="Барселона"},
            new Player {Name="Роббен", Team="Бавария"}
        };

        static void Main1(string[] args)
        {
            var result = players.Join(
                teams,
                p => p.Team,
                t => t.Name,
                (p, t) => new
                {
                    Name = p.Name,
                    Team = p.Team,
                    Country = t.Country
                });

            //-----------------------------------
            var result2 = teams.GroupJoin(
                players,
                t => t.Name,
                pl => pl.Team,
                (team, pls) => new
                {
                    Name = team.Name,
                    Country = team.Country,
                    Players = pls.Select(p=>p.Name)
                });

            //-----------------------------------
            var result3 = players.Zip(
                teams,
                (player, team) => new
                {
                    Name = player.Name,
                    Team = team.Name,
                    Country = team.Country
                });
        }
    }
}