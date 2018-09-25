using MassTransit.Topology;

namespace Core
{
    public class NameFormatter : IEntityNameFormatter
    {
        public string FormatEntityName<T>()
        {
            return $"{typeof(T).Name.ToLower()}_event";
        }
    }
}
