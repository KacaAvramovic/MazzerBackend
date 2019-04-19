using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Helpers;

namespace WebApi.Entities
{
    public class Game
    {

        int Id { get; set; }
        GameStates State { get; set; }
        List<User> Players { get; set; }
        Dictionary<User, int> Scores { get; set; }

        [NotMapped]
        public Dictionary<User, Position> Positions { get; set; }

        public Game()
        {
            Players = new List<User>();

        }

        public Game(List<User> users)
        {
            foreach (var player in users)
            {
                Players.Add(player);
                Positions.Add(player, new Position(0, 0));
            }
        }

    }
}
