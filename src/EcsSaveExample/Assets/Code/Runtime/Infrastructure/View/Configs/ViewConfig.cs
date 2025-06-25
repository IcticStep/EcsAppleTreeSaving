using AYellowpaper.SerializedCollections;
using Code.Runtime.Gameplay;
using UnityEngine;

namespace Code.Runtime.Infrastructure.View.Configs
{
    [CreateAssetMenu(fileName = "ViewConfig", menuName = "Static data/ViewConfig", order = 0)]
    internal sealed class ViewConfig : ScriptableObject
    {
        [SerializedDictionary("Type", "View")]
        public SerializedDictionary<EntityTypeId, EntityBehaviour> Views = new();
    }
}