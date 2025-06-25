namespace Code.Runtime.Infrastructure.Identifiers
{
    internal interface IIdentifierService
    {
        int Next();
        void SetIdUsed(int id);
    }
}