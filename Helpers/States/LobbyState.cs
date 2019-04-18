using System;
using WebApi.Entities;

namespace WebApi.Helpers.States
{
    public class LobbyState : IState
    {
        public void DoAction(Room room)
        {
            Console.Write("room is in lobby state");
            room.SetState(this);
        }
    }
}
