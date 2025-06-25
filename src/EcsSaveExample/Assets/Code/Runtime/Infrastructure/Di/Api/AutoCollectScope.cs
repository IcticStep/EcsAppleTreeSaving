using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyBox;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Infrastructure.Di.Api
{
    internal class AutoCollectScope : LifetimeScope
    {
        public List<MonoInstaller> Installers;
        
        protected override void Configure(IContainerBuilder builder)
        {
            Installers.ForEach(installer => installer.InstallBindings(builder));
            OnConfigure(builder);
        }

        protected virtual void OnConfigure(IContainerBuilder builder) { }

#if UNITY_EDITOR
        [ButtonMethod]
        public void CollectAutoInjectGameObjects()
        {
            GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            Stack<GameObject> gameObjectsToCheck = new(rootGameObjects);
            List<GameObject> objectsNeedInjection = new();
            
            while(gameObjectsToCheck.Count > 0)
            {
                GameObject @object = gameObjectsToCheck.Pop();
                
                bool injectionNeeded = InjectionNeeded(@object);
                if(injectionNeeded)
                {
                    objectsNeedInjection.Add(@object);
                }
                else
                {
                    foreach(Transform child in @object.transform)
                        gameObjectsToCheck.Push(child.gameObject);
                }
            }

            // ReSharper disable once RedundantBaseQualifier
            base.autoInjectGameObjects = objectsNeedInjection;
            EditorUtility.SetDirty(this);
        }
        
        private bool InjectionNeeded(GameObject @object)
        {
            MonoBehaviour[] monoBehaviours = @object.GetComponents<MonoBehaviour>();
            foreach(MonoBehaviour monoBehaviour in monoBehaviours)
            {
                Type type = monoBehaviour.GetType();
                MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                bool injectionNeeded = methods.Any(method => method.GetCustomAttributes(typeof(InjectAttribute), false).Length > 0);
                if(injectionNeeded)
                    return true;
            }

            return false;
        }
#endif // UNITY_EDITOR
    }
}
