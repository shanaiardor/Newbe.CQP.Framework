using Newbe.CQP.Framework;

namespace Newbe.CQP.Plugins.Parrot
{
    public class ParrotPlugin : PluginBase
    {
        public ParrotPlugin(ICoolQApi coolQApi) : base(coolQApi)
        {
        }

        public override string AppId => "Newbe.CQP.Plugins.Parrot";

        public override int ProcessPrivateMessage(int subType, int sendTime, long fromQQ, string msg, int font)
        {
            CoolQApi.SendPrivateMessage(fromQQ, msg);
            return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
        }
    }
}