namespace SC.DevChallenge.DataAccess.Abstractions.Entities
{
    public class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
