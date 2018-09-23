using MassTransit.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
