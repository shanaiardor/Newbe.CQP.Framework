using System;

namespace Newbe.CQP.Framework
{
    public class PluginLoadException : Exception
    {
        public string PluginName { get; }

        public PluginLoadException(string pluginName)
        {
            PluginName = pluginName;
        }

        public override string Message => $"{PluginName}╡Е╪Ч╪стьй╖╟э";
    }
}