namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 静态实例管理器
    /// </summary>
    public static class NewbeInstanceManager
    {
        /// <summary>
        /// 获取CoolApi实例
        /// </summary>
        public static ICoolQApi CoolQApi { get; internal set; }
    }
}