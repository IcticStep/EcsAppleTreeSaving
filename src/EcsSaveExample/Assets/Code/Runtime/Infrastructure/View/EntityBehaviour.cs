using Code.Runtime.Infrastructure.View.Registrars;
using Entitas.Unity;
using UnityEngine;

namespace Code.Runtime.Infrastructure.View
{
    public class EntityBehaviour : MonoBehaviour, IEntityView
    {
        private GameEntity _entity;
        public GameEntity Entity => _entity;

        public void SetEntity(GameEntity entity)
        {
            _entity = entity;
            _entity.AddView(this);
            
            gameObject.Link(_entity);

            foreach(IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
                registrar.RegisterComponents();
        }

        public void ReleaseEntity()
        {
            foreach(IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
                registrar.UnregisterComponents();
            
            gameObject.Unlink();
            _entity = null;
        }
    }
}