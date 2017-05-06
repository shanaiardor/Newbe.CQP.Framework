using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Security;
using Autofac;
using Newbe.CQP.Framework.PluginLoader.Logging;

namespace Newbe.CQP.Framework.PluginLoader
{
    [Serializable]
    public class CrossAppDomainPluginLoader : MarshalByRefObject, IPluginBase
    {
        private static readonly ILog Logger = LogProvider.For<CrossAppDomainPluginLoader>();
        public string Message { get; private set; }

        internal static readonly int DefaultInitializeLeaseTimeInSeconds = 0;
        private IPluginBase _pluginBase;

        public override object InitializeLifetimeService()
        {
            var lease = (ILease) base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromSeconds(DefaultInitializeLeaseTimeInSeconds);
            }
            return lease;
        }

        private static void Debug(string msg)
        {
            Logger.Debug(msg);
#if CrossDomainLog

            File.AppendAllLines("d:/Newbe.CQP.Framework.log", new[] {msg});
#endif
        }

        public bool LoadPlugin(string pluginEntryPointDllFullFilename)
        {
            Debug($"当前AppDomain:{AppDomain.CurrentDomain.FriendlyName}，开始加载插件程序集：{pluginEntryPointDllFullFilename}");
            try
            {
                Assembly.Load(new AssemblyName
                {
                    CodeBase = pluginEntryPointDllFullFilename,
                });
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    Debug($"当前已加载程序集:{assembly.FullName}");
                }
                Debug($"程序集加载完毕,开始构建Container");
                var superBuilder = new ContainerBuilder();
                superBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsImplementedInterfaces()
                    .AsSelf();
                var superContainer = superBuilder.Build();
                var subBuilderRegisters = superContainer.Resolve<IAutofacContainerBuilder[]>().ToArray();
                var builder = new ContainerBuilder();
                builder.Register(x => new CoolQApi()).AsImplementedInterfaces().SingleInstance();
                builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsImplementedInterfaces()
                    .AsSelf();
                foreach (var autofacContainerBuilder in subBuilderRegisters)
                {
                    autofacContainerBuilder.Register(builder);
                }
                var container = builder.Build();
                Debug($"Container构建完毕，尝试获取{nameof(IPluginBase)}实现类");
                var impls = container.Resolve<IEnumerable<IPluginBase>>().ToArray();
                Debug($"实现类一共{impls.Length}个");
                _pluginBase = impls.First(x => !(x is CrossAppDomainPluginLoader));
                Debug($"获取到了{_pluginBase.GetType().Name}作为{nameof(IPluginBase)}的实现类，插件加载完毕");
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return false;
            }
        }

        string IPluginBase.ApiVersion => _pluginBase.ApiVersion;

        string IPluginBase.AppId => _pluginBase.AppId;

        string IPluginBase.AppInfo() => _pluginBase.AppInfo();

        int IPluginBase.CoolQExited() => _pluginBase.CoolQExited();

        int IPluginBase.Disabled() => _pluginBase.Disabled();

        int IPluginBase.Enabled() => _pluginBase.Enabled();

        int IPluginBase.Initialize(int authcode) => _pluginBase.Initialize(authcode);

        int IPluginBase.ProcessAddFriendRequest(int subType, int sendTime, long fromQq, string msg, int font) =>
            _pluginBase.ProcessAddFriendRequest(subType, sendTime, fromQq, msg, font);

        int IPluginBase.ProcessDiscussGroupMessage(int subType, int sendTime, long fromDiscuss, long fromQq, string msg,
            int font) => _pluginBase.ProcessDiscussGroupMessage(subType, sendTime, fromDiscuss, fromQq, msg, font);

        int IPluginBase.ProcessFriendsAdded(int subType, int sendTime, long fromQq) => _pluginBase.ProcessFriendsAdded(
            subType, sendTime, fromQq);

        int IPluginBase.ProcessGroupAdminChange(int subType, int sendTime, long fromGroup, long target) => _pluginBase
            .ProcessGroupAdminChange(subType, sendTime, fromGroup, target);

        int IPluginBase.ProcessGroupMemberDecrease(int subType, int sendTime, long fromGroup, long fromQq, long target)
            => _pluginBase.ProcessGroupMemberDecrease(subType, sendTime, fromGroup, fromQq, target);

        int IPluginBase.ProcessGroupMemberIncrease(int subType, int sendTime, long fromGroup, long fromQq, long target)
            => _pluginBase.ProcessGroupMemberIncrease(subType, sendTime, fromGroup, fromQq, target);

        int IPluginBase.ProcessGroupMessage(int subType, int sendTime, long fromGroup, long fromQq,
            string fromAnonymous, string msg, int font) => _pluginBase.ProcessGroupMessage(subType, sendTime, fromGroup,
            fromQq, fromAnonymous, msg, font);

        int IPluginBase.ProcessGroupUpload(int subType, int sendTime, long fromGroup, long fromQq, string file) =>
            _pluginBase.ProcessGroupUpload(subType, sendTime, fromGroup, fromQq, file);

        int IPluginBase.ProcessJoinGroupRequest(int subType, int sendTime, long fromGroup, long fromQq, string msg,
            string responseMark) => _pluginBase.ProcessJoinGroupRequest(subType, sendTime, fromGroup, fromQq, msg,
            responseMark);

        int IPluginBase.ProcessPrivateMessage(int subType, int sendTime, long fromQq, string msg, int font) =>
            _pluginBase.ProcessPrivateMessage(subType, sendTime, fromQq, msg, font);

        int IPluginBase.ProcessMenuClickA() => _pluginBase.ProcessMenuClickA();

        int IPluginBase.ProcessMenuClickB() => _pluginBase.ProcessMenuClickB();

        int IPluginBase.ProcessMenuClickC() => _pluginBase.ProcessMenuClickC();

        int IPluginBase.ProcessMenuClickD() => _pluginBase.ProcessMenuClickD();

        int IPluginBase.ProcessMenuClickE() => _pluginBase.ProcessMenuClickE();

        int IPluginBase.ProcessMenuClickF() => _pluginBase.ProcessMenuClickF();

        int IPluginBase.ProcessMenuClickG() => _pluginBase.ProcessMenuClickG();

        int IPluginBase.ProcessMenuClickH() => _pluginBase.ProcessMenuClickH();

        int IPluginBase.ProcessMenuClickI() => _pluginBase.ProcessMenuClickI();
    }
}