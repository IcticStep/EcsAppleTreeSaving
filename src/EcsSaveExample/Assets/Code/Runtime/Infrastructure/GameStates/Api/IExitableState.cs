namespace Code.Runtime.Infrastructure.GameStates.Api
{
    internal interface IExitableState : IState
    {
        public void Exit();
    }
}