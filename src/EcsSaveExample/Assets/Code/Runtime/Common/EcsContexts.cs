namespace Code.Runtime.Common
{
    internal static class EcsContexts
    {
        public static GameContext Game => Contexts.sharedInstance.game;
    }
}