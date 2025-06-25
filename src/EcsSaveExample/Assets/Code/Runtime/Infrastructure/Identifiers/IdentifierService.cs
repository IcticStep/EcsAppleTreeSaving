namespace Code.Runtime.Infrastructure.Identifiers
{
    internal class IdentifierService : IIdentifierService
    {
        private int _lastId = 1;

        public int Next() =>
            ++_lastId;
        
        public void SetIdUsed(int id)
        {
            if (id > _lastId)
                _lastId = id;
        }
    }
}