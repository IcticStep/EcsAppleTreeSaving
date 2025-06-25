namespace Code.Runtime.Infrastructure.GameStates.Api
{
    public interface IEnterablePayloadState<in TPayload> : IState
    {
        public void Enter(TPayload payload);
    }
}