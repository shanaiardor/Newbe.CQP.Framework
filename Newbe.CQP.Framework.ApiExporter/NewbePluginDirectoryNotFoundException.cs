using System;

namespace Newbe.CQP.Framework
{
    public class NewbePluginDirectoryNotFoundException : Exception
    {
        public string PluginName { get; }

        public NewbePluginDirectoryNotFoundException(string pluginName)
        {
            PluginName = pluginName;
        }

        public override string Message => $"无法找到插件{PluginName}对应的dll目录";
    }
}