using Entitas;

namespace Code.Runtime.Common.Entity.ToStrings
{
    internal interface INamedEntity : IEntity
    {
        string EntityName(IComponent[] components);
        string BaseToString();
    }
}