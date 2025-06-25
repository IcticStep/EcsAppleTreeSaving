using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Infrastructure.Di.Api
{
    public static class BuilderExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder RegisterNonLazy<T>(this IContainerBuilder builder, Lifetime lifetime = Lifetime.Singleton)
        {
            return RegisterNonLazy<T>(builder, null, lifetime);
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder RegisterNonLazy<T>(this IContainerBuilder builder, Action<T> executeAfterResolving, Lifetime lifetime = Lifetime.Singleton)
        {
            RegistrationBuilder registrationBuilder = builder.Register<T>(lifetime);
            
            builder.RegisterBuildCallback(container=>
            {
                T result = container.Resolve<T>();
                executeAfterResolving?.Invoke(result);
            });
            return registrationBuilder;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Instantiate<T>(this IObjectResolver resolver, Lifetime lifetime = Lifetime.Singleton)
        {
            RegistrationBuilder registrationBuilder = new(typeof(T), lifetime);
            Registration registration = registrationBuilder.Build();
            return (T)resolver.Resolve(registration);
        }

        public static T Instantiate<T>(this IObjectResolver resolver, Lifetime lifetime = Lifetime.Singleton, params object[] args)
        {
            RegistrationBuilder registrationBuilder = new(typeof(T), lifetime);

            foreach(object arg in args)
                registrationBuilder.WithParameter(arg.GetType(), arg);

            Registration registration = registrationBuilder.Build();
            return (T)resolver.Resolve(registration);
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject InstantiateAndInject([NotNull] this IObjectResolver resolver, [NotNull] GameObject prefab, Transform parent = null)
        {
            if (prefab == null)
                throw new NullReferenceException(nameof(prefab));
            
            bool prefabWasActive = prefab.activeSelf;
            prefab.SetActive(false);
            GameObject instance = resolver.Instantiate(prefab, parent);
            prefab.SetActive(prefabWasActive);
            instance.SetActive(prefabWasActive);
            return instance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TComponent InstantiateAndInject<TComponent>([NotNull] this IObjectResolver resolver, [NotNull] TComponent prefabComponent, Transform parent = null)
        where TComponent : MonoBehaviour
        {
            if(prefabComponent == null)
                throw new NullReferenceException(nameof(prefabComponent));
            
            GameObject prefab = prefabComponent.gameObject;
            
            bool prefabWasActive = prefab.activeSelf;
            prefab.SetActive(false);
            GameObject instance = resolver.Instantiate(prefab, parent);
            prefab.SetActive(prefabWasActive);
            instance.SetActive(prefabWasActive);
            return instance.GetComponent<TComponent>();
        }
    }}