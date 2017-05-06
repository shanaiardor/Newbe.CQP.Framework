namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 表示酷Q请求处理结果。
    /// </summary>
    public enum CoolQRequestResult
    {
        /// <summary>
        /// 此请求暂未处理。
        /// </summary>
        None = 0,
        /// <summary>
        /// 通过请求。
        /// </summary>
        Allow = 1,
        /// <summary>
        /// 拒绝请求。
        /// </summary>
        Deny = 2
    }
}
