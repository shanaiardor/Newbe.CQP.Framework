using System;
using Quartz;
using Quartz.Impl;

namespace Newbe.CQP.Plugins.TimedTask
{
    public class TimedMessageJob : ITimedMessageJob
    {
        private readonly IScheduler _scheduler;

        public TimedMessageJob()
        {
            _scheduler = new StdSchedulerFactory().GetScheduler();
        }

        void IDisposable.Dispose()
        {
            _scheduler?.Shutdown();
        }

        void ITimedMessageJob.AddJob(long qq, string msg)
        {
            _scheduler.ScheduleJob(JobBuilder.Create<MsgJob>()
                    .WithIdentity(qq.ToString())
                    .SetJobData(new JobDataMap
                    {
                        ["qq"] = qq,
                        ["msg"] = msg,
                    })
                    .UsingJobData("qq", qq)
                    .UsingJobData("msg", msg)
                    .Build(),
                TriggerBuilder.Create().StartNow()
                    .WithDailyTimeIntervalSchedule(config => config.WithIntervalInSeconds(3)).Build());
            _scheduler.Start();
        }

        void ITimedMessageJob.RemoveJob(long qq)
        {
            _scheduler.DeleteJob(new JobKey(qq.ToString()));
        }
    }
}