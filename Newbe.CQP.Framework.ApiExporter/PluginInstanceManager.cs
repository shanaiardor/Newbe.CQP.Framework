using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autofac;
using Newbe.CQP.Framework.Logging;

namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 插件实例管理器
    /// </summary>
    public static class PluginInstanceManager
    {
        private static IDictionary<string, IPluginBase> Instances { get; } = new Dictionary<string, IPluginBase>();
        private static IContainer Container { get; }

        static PluginInstanceManager()
        {
            var logger = LogProvider.GetLogger("PluginInstanceManager");
            logger.Info("开始加载插件");
            try
            {
                logger.Debug("开始构建IOC容器");
                var builder = new ContainerBuilder();
                string pluginName = GetPluginName();
                logger.Debug($"当前插件名称为{pluginName}");
                var pluginDllDir = Path.Combine("app/../", pluginName);
                if (!Directory.Exists(pluginDllDir))
                {
                    throw new NewbePluginDirectoryNotFoundException(pluginName);
                }
                var domainName = $"{pluginName}Domain";
                logger.Debug($"创建AppDomain{domainName}");
                foreach (var file in Directory.GetFiles(pluginDllDir, "*.dll"))
                {
                    builder.RegisterAssemblyTypes(Assembly.LoadFile(Path.GetFullPath(file)))
                        .AsImplementedInterfaces()
                        .AsSelf();
                    logger.Debug($"加载{file}到{domainName}");
                }
                //CoolApi全局唯一
                builder.Register(x => new CoolQApi()).AsImplementedInterfaces().SingleInstance();
                Container = builder.Build();
                logger.Debug("IOC容器构建完毕");
                Instances.Add(pluginName, Container.Resolve<IPluginBase>());
            }
            catch (Exception e)
            {
                logger.ErrorException(e.Message, e);
                throw;
            }
            logger.Info($"插件加载完毕，选取{Instances.GetType().FullName}为插件实现类");
        }

        private static string GetPluginName()
        {
            return Path.GetFileNameWithoutExtension(typeof(PluginInstanceManager).Assembly.CodeBase);
        }

        internal static IPluginBase GetInstance()
        {
            return Instances[GetPluginName()];
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