using UnityEngine;

namespace Code.Runtime.Common.Extensions
{
    internal static class Vector2IntExtensions
    {
        public static Vector2Int Lerp(this Vector2Int vector, Vector2Int max, float t) =>
            new(
                Mathf.RoundToInt(Mathf.Lerp(vector.x, max.x, t)),
                Mathf.RoundToInt(Mathf.Lerp(vector.y, max.y, t))
            );
    }
}