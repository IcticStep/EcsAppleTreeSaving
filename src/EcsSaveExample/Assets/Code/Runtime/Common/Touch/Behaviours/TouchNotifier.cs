using Code.Runtime.Infrastructure.View;

namespace Code.Runtime.Common.Touch.Behaviours
{
    internal sealed class TouchNotifier : EntityDependant
    {
        private void OnMouseDown()
        {
            if (Entity != null)
                Entity.isTouchedThisFrame = true;
        }
    }
}