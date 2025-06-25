namespace Code.Runtime.Infrastructure.GameStates.Api
{
    internal interface IUpdateableState : IState
    {
        public void Update();
    }
}