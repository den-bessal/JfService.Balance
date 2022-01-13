using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JfService.Balance.Application.Extenions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Assembly> GetWithReferenced(this Assembly assembly)
        {
            var referencedAssemblies = assembly.GetReferencedAssemblies().Select(x => Assembly.Load(x));
            return referencedAssemblies.Concat(new Assembly[] { assembly });
        }
    }
}