namespace SC.DevChallenge.DataAccess.Abstractions.Entities
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}
