namespace WebApi.Services.Mappers
{
    public abstract class CustomMapper<TSource, TDestination> : ICustomMapper<TSource, TDestination>
    {
        public abstract TDestination Map(TSource source);
        public IEnumerable<TDestination> Map(IEnumerable<TSource> sources)
        {
            return sources.Select(Map);
        }
    }
}
