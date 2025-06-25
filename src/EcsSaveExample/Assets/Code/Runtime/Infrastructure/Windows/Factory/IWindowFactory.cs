using Code.Runtime.Infrastructure.Windows.Api;

namespace Code.Runtime.Infrastructure.Windows.Factory
{
    internal interface IWindowFactory
    {
        BaseWindow CreateWindow(WindowTypeId windowTypeId);
    }
}