using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Newbe.CQP.Framework.Logging;

namespace Newbe.CQP.Framework
{
    public static class PluginInstanceManager
    {
        private static readonly ILog Logger = LogProvider.GetLogger("PluginInstanceManager");
        private static IPluginBase Instance { get; }
        private static IContainer Container { get; }

        static PluginInstanceManager()
        {
            Logger.Info("开始加载插件");

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsImplementedInterfaces().AsSelf();
            Container = builder.Build();
            Instance = Container.Resolve<IPluginBase>();
            Logger.Info($"插件加载完毕，选取{Instance.GetType().FullName}为插件实现类");
        }

        public static IPluginBase GetInstance()
        {
            return Instance;
        }
    }
}