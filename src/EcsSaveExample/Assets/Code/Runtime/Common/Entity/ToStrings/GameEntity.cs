using System;
using System.Linq;
using Code.Runtime.Common.Entity.ToStrings;
using Entitas;
using UnityEngine;

// ReSharper disable once CheckNamespace
public sealed partial class GameEntity : INamedEntity
{
    private EntityPrinter _printer;

    public override string ToString()
    {
        if(_printer == null)
            _printer = new EntityPrinter(this);

        _printer.InvalidateCache();

        return _printer.BuildToString();
    }

    public string EntityName(IComponent[] components)
    {
        try
        {
            if(components.Length == 1)
                return components[0].GetType().Name;

            foreach(IComponent component in components)
            {
                switch(component.GetType().Name)
#pragma warning disable CS1522 // Empty switch block
                {
                    
                }
#pragma warning restore CS1522 // Empty switch block
            }
        }
        catch(Exception exception)
        {
            Debug.LogError(exception.Message);
        }

        return components.First().GetType().Name;
    }

    public string BaseToString() =>
        base.ToString();
}