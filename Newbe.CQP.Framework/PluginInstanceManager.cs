using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newbe.CQP.Framework.Logging;

namespace Newbe.CQP.Framework
{
    public static class PluginInstanceManager
    {
        private static readonly ILog Logger = LogProvider.GetLogger("PluginInstanceManager");
        private static PluginBase Instance { get; }

        static PluginInstanceManager()
        {
            Logger.Info("开始加载插件");
            var pluginType =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes().Where(a => typeof(PluginBase).IsAssignableFrom(a) && !a.IsAbstract))
                    .FirstOrDefault();
            if (pluginType == null)
            {
                Logger.Error($"无法找到{typeof(PluginBase).Name}的实现类");
                throw new Exception();
            }
            Instance = (PluginBase) Activator.CreateInstance(pluginType);
            Logger.Info($"插件加载完毕，选取{pluginType.FullName}为插件实现类");
        }

        public static PluginBase GetInstance()
        {
            return Instance;
        }
    }
}