namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 指示酷Q加群请求类型。
    /// </summary>
    public enum CoolQAddGroupRequestType
    {
        /// <summary>
        /// 表示请求类型未指定。
        /// </summary>
        None = 0,

        /// <summary>
        /// 表示一般的加群请求。
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 表示此请求是邀请加群请求。
        /// </summary>
        Invtiation = 2
    }
}