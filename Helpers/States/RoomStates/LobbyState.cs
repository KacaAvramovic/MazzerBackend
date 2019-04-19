using System;
using WebApi.Entities;

namespace WebApi.Helpers.States
{
    public class LobbyState : IState<Room>
    {
        public void GoToNextState(Room room)
        {
            Console.Write("Room changed it's state from InLobby to InGame.");
            room.SetState(new InGameState());
        }
    }
}
