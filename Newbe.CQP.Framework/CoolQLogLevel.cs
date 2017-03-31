namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 表示酷Q日志等级。
    /// </summary>
    public enum CoolQLogLevel
    {
        /// <summary>
        /// 表示没有指定等级。
        /// </summary>
        None = 0,

        /// <summary>
        /// 表示酷Q日志等级为调试。
        /// </summary>
        Debug = 0,

        /// <summary>
        /// 表示酷Q日志等级为信息。
        /// </summary>
        Info = 10,

        /// <summary>
        /// 表示酷Q日志等级为信息（成功）。
        /// </summary>
        InfoSuccess = 11,

        /// <summary>
        /// 表示酷Q日志等级为信息（接收）。
        /// </summary>
        InfoReceive = 12,

        /// <summary>
        /// 表示酷Q日志等级为信息（发送）。
        /// </summary>
        InfoSend = 13,

        /// <summary>
        /// 表示酷Q日志等级为警告。
        /// </summary>
        Warning = 20,

        /// <summary>
        /// 表示酷Q日志等级为错误。
        /// </summary>
        Error = 30,

        /// <summary>
        /// 表示酷Q日志等级为致命错误。
        /// </summary>
        Fatal = 40
    }
}