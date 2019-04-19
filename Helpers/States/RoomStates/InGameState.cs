﻿using System;
using WebApi.Entities;

namespace WebApi.Helpers.States
{
    public class InGameState : IState<Room>
    {
      
        public void GoToNextState(Room room)
        {
            Console.Write("room is in in game state");

            room.Game = new Game(room.Players);


            room.SetState(this);
        }

       

        public void GameOver()
        { }






    }
}