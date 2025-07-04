//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherEntityType;

    public static Entitas.IMatcher<GameEntity> EntityType {
        get {
            if (_matcherEntityType == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.EntityType);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherEntityType = matcher;
            }

            return _matcherEntityType;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Code.Runtime.Infrastructure.Progress.EntityType entityType { get { return (Code.Runtime.Infrastructure.Progress.EntityType)GetComponent(GameComponentsLookup.EntityType); } }
    public Code.Runtime.Gameplay.EntityTypeId EntityType { get { return entityType.Value; } }
    public bool hasEntityType { get { return HasComponent(GameComponentsLookup.EntityType); } }

    public GameEntity AddEntityType(Code.Runtime.Gameplay.EntityTypeId newValue) {
        var index = GameComponentsLookup.EntityType;
        var component = (Code.Runtime.Infrastructure.Progress.EntityType)CreateComponent(index, typeof(Code.Runtime.Infrastructure.Progress.EntityType));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceEntityType(Code.Runtime.Gameplay.EntityTypeId newValue) {
        var index = GameComponentsLookup.EntityType;
        var component = (Code.Runtime.Infrastructure.Progress.EntityType)CreateComponent(index, typeof(Code.Runtime.Infrastructure.Progress.EntityType));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveEntityType() {
        RemoveComponent(GameComponentsLookup.EntityType);
        return this;
    }
}
