using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newbe.CQP.Framework;

namespace Newbe.CQP.Plugins.ProxyClient
{
    public class PluginMain : PluginBase
    {
        public override string AppId => "Newbe.CQP.Plugins.ProxyClient";

        public override int ProcessPrivateMessage(int subType, int sendTime, long fromQQ, string msg, int font)
        {
            CoolQApi.SendPrivateMessage(fromQQ, "asduq");
            return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
        }
    }
}