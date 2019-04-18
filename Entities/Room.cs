using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Helpers.States;

namespace WebApi.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxPlayersCount { get; set; }
        public List<User> Players { get; set; }
        [NotMapped]
        public Game Game { get; set; }

        IState State { get; set; }

        public Room(string name, int maxPlayersCount)
        {
            Name = name;
            MaxPlayersCount = maxPlayersCount;
            Players = new List<User>();
        }

        internal void SetState(IState state)
        {
            State = state;
        }
    }
}