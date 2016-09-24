using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PluginLoader
{
	public static class PluginLoader<T>
	{
		public static IEnumerable<T> LoadPlugins(string path)
		{
			if (!Directory.Exists(path)) return new List<T>();

			var dllFileNames = Directory.GetFiles(path, "*.dll");

			var assemblies = dllFileNames.Select(AssemblyName.GetAssemblyName).Select(Assembly.Load);

			var pluginType = typeof(T);
			ICollection<Type> pluginTypes =
				assemblies.Where(a => a != null)
					.SelectMany(a => a.GetTypes())
					.Where(t => t.IsClass && !t.IsAbstract)
					.Where(type => pluginType.IsAssignableFrom(type))
					.ToList();

			return pluginTypes.Select(type => (T) Activator.CreateInstance(type)).ToList();
		}
	}
}