using System;

namespace Newbe.CQP.Plugins.TimedTask
{
    public interface ITimedMessageJob : IDisposable
    {
        void AddJob(long qq, string msg);
        void RemoveJob(long qq);
    }
}