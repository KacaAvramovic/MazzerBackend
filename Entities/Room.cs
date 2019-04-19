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

        IState<Room> State { get; set; }

        public Room(string name, int maxPlayersCount)
        {
            Name = name;
            MaxPlayersCount = maxPlayersCount;
            Players = new List<User>();
        }

        internal void SetState(IState<Room> state)
        {
            State = state;
        }
        
        internal bool IsFull()
        {
            return MaxPlayersCount >= Players.Count;
        }

        internal bool StartGame()
        {
            Game = new Game(Players);
            return true;
        }
    }
}