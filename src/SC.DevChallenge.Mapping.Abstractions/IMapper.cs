namespace SC.DevChallenge.Mapping.Abstractions
{
    public interface IMapper
    {
        TDestination Map<TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source);
    }
}