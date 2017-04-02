using System;
using System.Linq;

namespace Newbe.CQP.Framework
{
    public static class PluginInstanceManager
    {
        private static PluginBase Instance { get; }

        static PluginInstanceManager()
        {
            var pluginType =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes().Where(a => a.IsAssignableFrom(typeof(PluginBase)) && !a.IsAbstract))
                    .FirstOrDefault();
            if (pluginType == null)
            {
                throw new Exception($"无法找到{typeof(PluginBase).Name}的实现类");
            }
            Instance = (PluginBase) Activator.CreateInstance(pluginType);
        }

        public static PluginBase GetInstance()
        {
            return Instance;
        }
    }
}