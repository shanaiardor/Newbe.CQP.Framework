using Newbe.CQP.Framework;
using Quartz;

namespace Newbe.CQP.Plugins.TimedTask
{
    public class MsgJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            var qq = (long) context.JobDetail.JobDataMap["qq"];
            var msg = (string) context.JobDetail.JobDataMap["msg"];
            NewbeInstanceManager.CoolQApi.SendPrivateMessage(qq, msg);
        }
    }
}