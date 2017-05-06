namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 酷Q .net插件接口
    /// </summary>
    public interface IPluginBase
    {
        /// <summary>
        /// Api版本号，若酷Q官方SDK没有更新此版本号，请勿改动此值
        /// </summary>
        string ApiVersion { get; }

        /// <summary>
        /// AppID
        /// </summary>
        string AppId { get; }

        /// <summary>
        /// 向酷Q提供插件信息。
        /// </summary>
        /// <returns>一个固定格式字符串。</returns>
        string AppInfo();

        /// <summary>
        /// 此函数会在酷Q退出时被调用。
        /// </summary>
        /// <returns> </returns>
        int CoolQExited();

        /// <summary>
        /// 此函数会在插件被禁用时发生。
        /// </summary>
        /// <returns> 返回处理过程是否成功的值。</returns>
        int Disabled();

        /// <summary>
        /// 此函数会在插件被开启时发生。
        /// </summary>
        /// <returns> 返回处理过程是否成功的值。</returns>
        int Enabled();

        /// <summary>
        /// 获取此插件的AuthCode。
        /// </summary>
        /// <param name="authcode">由酷Q提供的AuthCode。</param>
        /// <returns> </returns>
        int Initialize(int authcode);

        /// <summary>
        /// 处理好友添加请求。
        /// </summary>
        /// <param name="subType">事件类型。固定为1。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromQq">事件来源QQ。</param>
        /// <param name="msg">附言内容。</param>
        /// <param name="font">消息所使用字体。</param>
        /// <returns> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        int ProcessAddFriendRequest(int subType, int sendTime, long fromQq, string msg, int font);

        /// <summary>
        /// 处理讨论组消息。
        /// </summary>
        /// <param name="subType">消息类型，目前固定为1。</param>
        /// <param name="sendTime">消息发送时间的时间戳。</param>
        /// <param name="fromDiscuss">消息来源讨论组号。</param>
        /// <param name="fromQq">发送此消息的QQ号码。</param>
        /// <param name="msg">消息内容。</param>
        /// <param name="font">消息所使用字体。</param>
        /// <returns> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        int ProcessDiscussGroupMessage(int subType, int sendTime, long fromDiscuss, long fromQq, string msg, int font);

        /// <summary>
        /// 处理好友已添加事件。
        /// </summary>
        /// <param name="subType">事件类型。固定为1。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromQq">事件来源QQ。</param>
        /// <returns> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        int ProcessFriendsAdded(int subType, int sendTime, long fromQq);

        /// <summary>
        /// 处理群管理员变动事件。
        /// </summary>
        /// <param name="subType">事件类型。1为被取消管理员，2为被设置管理员。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromGroup">事件来源群号。</param>
        /// <param name="target">被操作的QQ。</param>
        /// <returns> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        int ProcessGroupAdminChange(int subType, int sendTime, long fromGroup, long target);

        /// <summary>
        /// 处理群成员数量减少事件。
        /// </summary>
        /// <param name="subType">事件类型。1为群员离开；2为群员被踢为；3为自己(即登录号)被踢。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromGroup">事件来源群号。</param>
        /// <param name="fromQq">事件来源QQ。</param>
        /// <param name="target">被操作的QQ。</param>
        /// <returns> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        int ProcessGroupMemberDecrease(int subType, int sendTime, long fromGroup, long fromQq, long target);

        /// <summary>
        /// 处理群成员添加事件。
        /// </summary>
        /// <param name="subType">事件类型。1为管理员已同意；2为管理员邀请。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromGroup">事件来源群号。</param>
        /// <param name="fromQq">事件来源QQ。</param>
        /// <param name="target">被操作的QQ。</param>
        /// <returns> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        int ProcessGroupMemberIncrease(int subType, int sendTime, long fromGroup, long fromQq, long target);

        /// <summary>
        /// 处理群聊消息。
        /// </summary>
        /// <param name="subType">消息类型，目前固定为1。</param>
        /// <param name="sendTime">消息发送时间的时间戳。</param>
        /// <param name="fromGroup">消息来源群号。</param>
        /// <param name="fromQq">发送此消息的QQ号码。</param>
        /// <param name="fromAnonymous">发送此消息的匿名用户。</param>
        /// <param name="msg">消息内容。</param>
        /// <param name="font">消息所使用字体。</param>
        /// <returns> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        int ProcessGroupMessage(int subType, int sendTime, long fromGroup, long fromQq, string fromAnonymous,
            string msg,
            int font);

        /// <summary>
        /// 处理群文件上传事件。
        /// </summary>
        /// <param name="subType">消息类型，目前固定为1。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromGroup">事件来源群号。</param>
        /// <param name="fromQq">上传此文件的QQ号码。</param>
        /// <param name="file">上传的文件的信息。</param>
        /// <returns> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        int ProcessGroupUpload(int subType, int sendTime, long fromGroup, long fromQq, string file);


        /// <summary>
        /// 处理加群请求。
        /// </summary>
        /// <param name="subType">请求类型。1为他人申请入群；2为自己(即登录号)受邀入群。</param>
        /// <param name="sendTime">请求发送时间戳。</param>
        /// <param name="fromGroup">要加入的群的群号。</param>
        /// <param name="fromQq">发送此请求的QQ号码。</param>
        /// <param name="msg">附言内容。</param>
        /// <param name="responseMark">用于处理请求的标识。</param>
        /// <returns> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        int ProcessJoinGroupRequest(int subType, int sendTime, long fromGroup, long fromQq, string msg,
            string responseMark);

        /// <summary>
        /// 处理私聊消息。
        /// </summary>
        /// <param name="subType">私聊消息类型。11代表消息来自好友；1代表消息来自在线状态；2代表消息来自群；3代表消息来自讨论组。</param>
        /// <param name="sendTime">消息发送时间的时间戳。</param>
        /// <param name="fromQq">发送此消息的QQ号码。</param>
        /// <param name="msg">消息的内容。</param>
        /// <param name="font">消息所使用的字体。</param>
        /// <returns> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        int ProcessPrivateMessage(int subType, int sendTime, long fromQq, string msg, int font);

        #region 菜单

        /// <summary>
        /// 菜单A
        /// </summary>
        /// <returns></returns>
        int ProcessMenuClickA();

        /// <summary>
        /// 菜单B
        /// </summary>
        /// <returns></returns>
        int ProcessMenuClickB();

        /// <summary>
        /// 菜单C
        /// </summary>
        /// <returns></returns>
        int ProcessMenuClickC();

        /// <summary>
        /// 菜单D
        /// </summary>
        /// <returns></returns>
        int ProcessMenuClickD();

        /// <summary>
        /// 菜单E
        /// </summary>
        /// <returns></returns>
        int ProcessMenuClickE();

        /// <summary>
        /// 菜单F
        /// </summary>
        /// <returns></returns>
        int ProcessMenuClickF();

        /// <summary>
        /// 菜单G
        /// </summary>
        /// <returns></returns>
        int ProcessMenuClickG();

        /// <summary>
        /// 菜单H
        /// </summary>
        /// <returns></returns>
        int ProcessMenuClickH();

        /// <summary>
        /// 菜单I
        /// </summary>
        /// <returns></returns>
        int ProcessMenuClickI();

        #endregion
    }
}