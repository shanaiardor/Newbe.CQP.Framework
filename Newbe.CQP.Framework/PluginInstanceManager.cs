using System;
using Autofac;
using Newbe.CQP.Framework.Logging;

namespace Newbe.CQP.Framework
{
    public static class PluginInstanceManager
    {
        private static IPluginBase Instance { get; }
        private static IContainer Container { get; }

        static PluginInstanceManager()
        {
            var logger = LogProvider.GetLogger("PluginInstanceManager");
            logger.Info("开始加载插件");
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsImplementedInterfaces().AsSelf();
            //CoolApi全局唯一
            builder.Register(x => new CoolQApi()).AsImplementedInterfaces().SingleInstance();
            Container = builder.Build();
            Instance = Container.Resolve<IPluginBase>();
            logger.Info($"插件加载完毕，选取{Instance.GetType().FullName}为插件实现类");
        }

        public static IPluginBase GetInstance()
        {
            return Instance;
        }
    }
}