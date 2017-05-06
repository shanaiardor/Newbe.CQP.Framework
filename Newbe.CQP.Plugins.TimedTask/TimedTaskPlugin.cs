using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newbe.CQP.Framework;

namespace Newbe.CQP.Plugins.TimedTask
{
    public class TimedTaskPlugin : PluginBase
    {
        private readonly ITimedMessageJob _timedMessageJob;

        public TimedTaskPlugin(ICoolQApi coolQApi, ITimedMessageJob timedMessageJob) : base(coolQApi)
        {
            _timedMessageJob = timedMessageJob;
        }

        public override int ProcessPrivateMessage(int subType, int sendTime, long fromQq, string msg, int font)
        {
            switch (msg)
            {
                case "1":
                    _timedMessageJob.AddJob(fromQq, "呵呵哒");
                    CoolQApi.SendPrivateMessage(fromQq, "接下来将会每个3秒发送一条消息，发送2可以中断发送");
                    break;
                case "2":
                    _timedMessageJob.RemoveJob(fromQq);
                    CoolQApi.SendPrivateMessage(fromQq, "消息停止发送");
                    break;
            }
            return base.ProcessPrivateMessage(subType, sendTime, fromQq, msg, font);
        }

        public override string AppId => "Newbe.CQP.Plugins.TimedTask";
    }
}