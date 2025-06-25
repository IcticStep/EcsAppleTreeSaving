using UnityEngine;

namespace Code.Runtime.Gameplay.Apples.Factory
{
    public interface IApplesFactory
    {
        GameEntity CreateApple(Vector3 position);
    }
}