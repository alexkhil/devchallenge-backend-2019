namespace SC.DevChallenge.DataAccess.Abstractions.Domain
{
    public class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
