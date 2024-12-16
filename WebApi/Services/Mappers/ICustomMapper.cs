namespace WebApi.Services.Mappers
{
    public interface ICustomMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
        IEnumerable<TDestination> Map(IEnumerable<TSource> source);
    }
}
