using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newbe.CQP.Framework;

namespace Newbe.CQP.Plugins.TestPlugin
{
    public class MyPlugin : PluginBase
    {
        public MyPlugin(ICoolQApi coolQApi) : base(coolQApi)
        {
        }

        public override string AppId => "Newbe.CQP.Plugins.TestPlugin";

        public override int ProcessPrivateMessage(int subType, int sendTime, long fromQQ, string msg, int font)
        {
            CoolQApi.SendPrivateMessage(fromQQ, msg);
            return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
        }
    }
}