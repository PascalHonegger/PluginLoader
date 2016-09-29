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

			var allTypes =
				assemblies.Where(assembly => assembly != null)
					.SelectMany(assembly => assembly.GetTypes());

			var pluginTypes = allTypes
				.Where(type => type.IsClass && !type.IsAbstract)
				.Where(type => typeof(T).IsAssignableFrom(type));

			return pluginTypes.Select(type => (T) Activator.CreateInstance(type)).ToList();
		}
	}
}