namespace SC.DevChallenge.DataAccess.Abstractions.Domain
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}
