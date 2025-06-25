namespace Code.Runtime.Infrastructure.GameStates.Api
{
    internal interface IEndOfFrameExitableState : IState
    {
        public void ExitOnEndOfFrame();
    }
}