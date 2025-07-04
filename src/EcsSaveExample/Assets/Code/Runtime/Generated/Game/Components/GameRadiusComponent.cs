//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherRadius;

    public static Entitas.IMatcher<GameEntity> Radius {
        get {
            if (_matcherRadius == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Radius);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRadius = matcher;
            }

            return _matcherRadius;
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

    public Code.Runtime.Common.Radius radius { get { return (Code.Runtime.Common.Radius)GetComponent(GameComponentsLookup.Radius); } }
    public float Radius { get { return radius.Value; } }
    public bool hasRadius { get { return HasComponent(GameComponentsLookup.Radius); } }

    public GameEntity AddRadius(float newValue) {
        var index = GameComponentsLookup.Radius;
        var component = (Code.Runtime.Common.Radius)CreateComponent(index, typeof(Code.Runtime.Common.Radius));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceRadius(float newValue) {
        var index = GameComponentsLookup.Radius;
        var component = (Code.Runtime.Common.Radius)CreateComponent(index, typeof(Code.Runtime.Common.Radius));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveRadius() {
        RemoveComponent(GameComponentsLookup.Radius);
        return this;
    }
}
