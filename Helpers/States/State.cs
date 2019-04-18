using WebApi.Entities;

namespace WebApi.Helpers.States
{
    public interface IState
    {
        void DoAction(Room room);
    }
}
