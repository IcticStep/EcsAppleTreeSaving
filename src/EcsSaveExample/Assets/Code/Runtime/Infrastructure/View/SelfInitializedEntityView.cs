using Code.Runtime.Infrastructure.Identifiers;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Infrastructure.View
{
    internal class SelfInitializedEntityView : MonoBehaviour
    {
        public EntityBehaviour EntityBehaviour;

        private IIdentifierService _identifiers;
        private GameContext _game;

        [Inject]
        private void Construct(IIdentifierService identifiers, GameContext game)
        {
            _game = game;
            _identifiers = identifiers;
        }

        private void Awake()
        {
            GameEntity entity = _game
                .CreateEntity()
                .AddId(_identifiers.Next());

            EntityBehaviour.SetEntity(entity);
        }
    }
}