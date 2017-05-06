using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Newbe.CQP.Framework;

namespace Newbe.CQP.Plugins.TimedTask
{
    public class ContainerRegister : IAutofacContainerBuilder
    {
        void IAutofacContainerBuilder.Register(ContainerBuilder buildAction)
        {
            buildAction.RegisterType<TimedMessageJob>().AsImplementedInterfaces().SingleInstance();
        }
    }
}