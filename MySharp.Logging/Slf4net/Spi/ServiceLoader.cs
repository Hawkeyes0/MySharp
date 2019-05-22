using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MySharp.Logging.Slf4net.Spi
{
    public static class ServiceLoader<T>
    {
        internal static List<T> Load()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName[] assemblyNames = assembly.GetReferencedAssemblies();

            List<T> list = new List<T>();

            list.AddRange(Load(assembly));
            foreach (AssemblyName assemblyName in assemblyNames)
            {
                list.AddRange(Load(Assembly.Load(assemblyName)));
            }

            return list;
        }

        private static IEnumerable<T> Load(Assembly assembly)
        {
            Type[] types = assembly.GetTypes().OrderBy(t => t.Namespace).ThenBy(t => t.Name).ToArray();
            Type baseType = typeof(T);

            foreach (Type t in types)
            {
                if (t.IsInterface || t.IsAbstract)
                    continue;

                Type[] interfaces = t.GetInterfaces();
                if (interfaces.Length == 0 || !interfaces.Contains(baseType))
                    continue;
                yield return (T)Activator.CreateInstance(t);
            }
        }
    }
}
