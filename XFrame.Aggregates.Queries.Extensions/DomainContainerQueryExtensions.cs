using System.Reflection;
using XFrame.DomainContainers;

namespace XFrame.Aggregates.Queries.Extensions
{
    public static class DomainContainerQueryExtensions
    {
        public static IDomainContainer AddQueries(
            this IDomainContainer domainContainer,
            params Type[] queryTypes)
        {
            return domainContainer.AddQueries(queryTypes);
        }

        public static IDomainContainer AddQueries(
            this IDomainContainer domainContainer,
            Assembly fromAssembly,
            Predicate<Type> predicate)
        {
            predicate = predicate ?? (t => true);
            var queryTypes = fromAssembly
                .GetTypes()
                .Where(t => !t.GetTypeInfo().IsAbstract && typeof(IQuery).GetTypeInfo().IsAssignableFrom(t))
                .Where(t => predicate(t));
            return domainContainer.AddTypes(queryTypes);
        }
    }
}
