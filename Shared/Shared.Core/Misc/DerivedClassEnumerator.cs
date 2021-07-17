using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shared.Core.Misc
{
    public static class DerivedClassEnumerator
    {
        public static IEnumerable<T> GetInstancesOf<T>()
        {
            var types = GetAllDerivedTypes<T>();
            if (types == null)
            {
                yield break;
            }

            foreach (var type in types)
            {
                yield return (T) Activator.CreateInstance(type);
            }
        }

        private static IEnumerable<Type> FindAllDerivedTypes<T>(Assembly assembly)
        {
            assembly.ThrowIfNull();

            var derivedType = typeof(T);
            return assembly.GetTypes().Where(t => t != derivedType && derivedType.IsAssignableFrom(t));
        }

        private static IEnumerable<Type> GetAllDerivedTypes<T>()
        {
            return FindAllDerivedTypes<T>(Assembly.GetAssembly(typeof(T)));
        }
    }
}