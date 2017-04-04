using System;
using Autofac;
using Newbe.CQP.Framework.Logging;

namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 插件实例管理器
    /// </summary>
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

        internal static IPluginBase GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// 获取IOC容器
        /// </summary>
        /// <returns></returns>
        public static IContainer GetContainer()
        {
            return Container;
        }
    }
}