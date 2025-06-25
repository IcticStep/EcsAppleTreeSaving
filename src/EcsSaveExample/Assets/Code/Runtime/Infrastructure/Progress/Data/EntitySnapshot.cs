using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Code.Runtime.Infrastructure.Progress.Data
{
    [Serializable]
    public struct EntitySnapshot
    {
        [JsonProperty("c")]
        public List<IComponent> Components;

        [CanBeNull]
        public TComponent GetComponent<TComponent>()
            where TComponent : IComponent =>
            (TComponent)Components.FirstOrDefault(c => c is TComponent);
    }
}