namespace Code.Runtime.Infrastructure.Windows.Api
{
    internal abstract class PayloadedWindow<TPayload> : BaseWindow
    {
        public abstract void Set(TPayload payload);
    }
}