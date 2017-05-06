using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Newbe.CQP.Framework
{
    /// <summary>
    /// AutofacContainerBuilder 用于自定义容器的注入
    /// </summary>
    public interface IAutofacContainerBuilder
    {
        /// <summary>
        /// 注册Container
        /// </summary>
        /// <param name="buildAction"></param>
        /// <returns></returns>
        void Register(ContainerBuilder buildAction);
    }
}