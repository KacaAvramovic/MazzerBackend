namespace WebApi.Helpers.States
{
    public interface IState<T>
    {
        void GoToNextState(T o);
    }
}
