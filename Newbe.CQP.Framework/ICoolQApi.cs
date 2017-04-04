using System;
using System.ComponentModel;
using System.Security;

namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 向酷Q.NET插件提供酷Q Api
    /// </summary>
    public interface ICoolQApi
    {
        /// <summary>
        ///     获取当前插件的数据存储目录。
        /// </summary>
        /// <returns>
        ///
        /// </returns>
        string AppDirectory { get; }

        /// <summary>
        ///     获取酷Q当前登录用户的Cookies。
        /// </summary>
        string Cookies { get; }

        /// <summary>
        ///     获取酷Q当前登录用户的CsrfToken。
        /// </summary>
        /// <returns>
        ///
        /// </returns>
        int CsrfToken { get; }

        /// <summary>
        ///     获取酷Q当前登录账户的昵称。
        /// </summary>
        /// <returns>
        ///
        /// </returns>
        string NickName { get; }

        /// <summary>
        ///     获取酷Q当前登录账户的QQ号码。
        /// </summary>
        /// <returns>
        ///
        /// </returns>
        long Number { get; }

        /// <summary>
        ///     获取加密后的Cookies。
        /// </summary>
        /// <returns><see cref="SecureString" /> 保存了Cookies的<see cref="SecureString" />对象。</returns>
        /// <remarks>
        ///     使用方法详见：
        ///     https://msdn.microsoft.com/zh-cn/library/system.security.securestring.aspx
        ///     此Api是酷Q非官方Api。
        /// </remarks>
        SecureString SafelyCookies { get; }

        /// <summary>
        ///     对在指定群里的指定QQ进行禁言。
        /// </summary>
        /// <param name="groupId">要进行禁言操作的群。</param>
        /// <param name="qq">要禁言的QQ。</param>
        /// <param name="duration">禁言持续秒数。</param>
        void Ban(long groupId, long qq, int duration);

        /// <summary>
        ///     对在指定群里的指定QQ进行禁言。
        /// </summary>
        /// <param name="groupId">要进行禁言操作的群。</param>
        /// <param name="qq">要禁言的QQ。</param>
        /// <param name="duration">禁言持续时长。</param>
        void Ban(long groupId, long qq, TimeSpan duration);

        /// <summary>
        ///     对指定群执行全体禁言操作。
        /// </summary>
        /// <param name="groupId">要进行全体禁言的群。</param>
        void BanAll(long groupId);

        /// <summary>
        ///     对在指定群里的指定匿名用户进行禁言。在对指定的匿名用户进行禁言后，不能解除匿名用户的禁言状态。
        /// </summary>
        /// <param name="groupId">要进行禁言操作的群。</param>
        /// <param name="anonymous">要禁言的匿名用户。</param>
        /// <param name="duration">禁言持续秒数。</param>
        void BanAnonymous(long groupId, string anonymous, long duration);

        /// <summary>
        ///     对在指定群里的指定匿名用户进行禁言。在对指定的匿名用户进行禁言后，不能解除匿名用户的禁言状态。
        /// </summary>
        /// <param name="groupId">要进行禁言操作的群。</param>
        /// <param name="anonymous">要禁言的匿名用户。</param>
        /// <param name="duration">禁言持续时长。</param>
        void BanAnonymous(long groupId, string anonymous, TimeSpan duration);

        /// <summary>
        ///     关闭指定的群的匿名聊天。
        /// </summary>
        /// <param name="groupId">要关闭匿名聊天的群。</param>
        void DisableAnonymousChat(long groupId);

        /// <summary>
        ///     打开指定的群的匿名聊天。
        /// </summary>
        /// <param name="groupId">要打开匿名聊天的群。</param>
        void EnableAnonymousChat(long groupId);

        /// <summary>
        ///     获取缓存的群成员信息。
        /// </summary>
        /// <param name="groupId">要获取信息的群成员的所在群。</param>
        /// <param name="qq">要获取信息的群成员QQ。</param>
        /// <returns>
        ///     <see cref="GroupMemberInfo" />
        /// </returns>
        /// <remarks>此函数采用Flexlive的处理方法。</remarks>
        GroupMemberInfo GetGroupMemberInfo(long groupId, long qq);

        /// <summary>
        ///     把指定QQ从指定群中踢出。
        /// </summary>
        /// <param name="groupId">要踢出QQ的群。</param>
        /// <param name="qq">要踢出的QQ。</param>
        /// <param name="rejectAddGroupRequest">指示是否不再处理此人的加群申请。</param>
        void KickFromGroup(long groupId, long qq, bool rejectAddGroupRequest);

        /// <summary>
        ///     把日志写入到酷Q运行日志中。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        void Log(CoolQLogLevel level, string message);

        /// <summary>
        ///     把日志写入到酷Q运行日志中。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        /// <param name="category">日志类别。</param>
        void Log(CoolQLogLevel level, string message, string category);

        /// <summary>
        ///     处理好友请求。
        /// </summary>
        /// <param name="responseMark">用于处理此好友请求的反馈标识。</param>
        /// <param name="result">是否通过此请求。</param>
        void ProcessAddFriendRequest(string responseMark, CoolQRequestResult result);

        /// <summary>
        ///     处理好友请求。
        /// </summary>
        /// <param name="responseMark">用于处理此好友请求的反馈标识。</param>
        /// <param name="result">是否通过此请求。</param>
        /// <param name="remark">对此好友的备注名。</param>
        void ProcessAddFriendRequest(string responseMark, CoolQRequestResult result, string remark);

        /// <summary>
        ///     处理加群请求。
        /// </summary>
        /// <param name="responseMark">用于处理请求的反馈标识。</param>
        /// <param name="resquestType">请求类型。</param>
        /// <param name="operation">请求处理结果。</param>
        /// <param name="reason">附言。</param>
        void ProcessAddGroupRequest(string responseMark, CoolQAddGroupRequestType resquestType,
            CoolQRequestResult operation, string reason);

        /// <summary>
        ///     退出并解散指定群。
        /// </summary>
        /// <param name="groupId">要退出并解散的群的群号。</param>
        void QuitAndDismissTheGroup(long groupId);

        /// <summary>
        ///     退出指定的讨论组。
        /// </summary>
        /// <param name="discussGroupId">要退出的讨论组的ID。</param>
        void QuitTheDiscussGroup(long discussGroupId);

        /// <summary>
        ///     退出指定群。
        /// </summary>
        /// <param name="groupId">要退出的群的群号。</param>
        void QuitTheGroup(long groupId);

        /// <summary>
        ///     在指定群内对被禁言的QQ执行解除禁言操作。
        /// </summary>
        /// <param name="groupId">要进行解除禁言操作的群。</param>
        /// <param name="qq">要解除禁言的QQ。</param>
        void RemoveBanned(long groupId, long qq);


        /// <summary>
        ///     解除指定群的全体禁言状态。
        /// </summary>
        /// <param name="groupId">要解除全体禁言状态的群。</param>
        void RemoveBannedAll(long groupId);

        /// <summary>
        ///     把指定群指定QQ的群管理员身份取消。
        /// </summary>
        /// <param name="groupId">要执行操作的群。</param>
        /// <param name="qq">要取消管理员的QQ。</param>
        void RevokeAdmin(long groupId, long qq);

        /// <summary>
        ///     向指定的讨论组发送讨论组消息。
        /// </summary>
        /// <param name="dicussGroupId">接收此消息的讨论组号。</param>
        /// <param name="message">消息内容。</param>
        void SendDiscussGroupMessage(long dicussGroupId, string message);

        /// <summary>
        ///     给指定QQ点赞。
        /// </summary>
        /// <param name="qq">要点赞的QQ的号码。</param>
        void SendGood(long qq);

        /// <summary>
        ///     向指定的群发送群消息。
        /// </summary>
        /// <param name="groupId">接收此消息的群号。</param>
        /// <param name="message">消息内容。</param>
        void SendGroupMessage(long groupId, string message);

        /// <summary>
        ///     向指定的QQ发送私聊消息。
        /// </summary>
        /// <param name="qq">接收此消息的QQ。</param>
        /// <param name="message">私聊消息内容。</param>
        void SendPrivateMessage(long qq, string message);

        /// <summary>
        ///     把指定QQ设定为指定群的管理员。
        /// </summary>
        /// <param name="groupId">要进行操作的群。</param>
        /// <param name="qq">要被提升为管理员的QQ。</param>
        void SetAsAdmin(long groupId, long qq);

        /// <summary>
        ///     设置插件权限代码。
        /// </summary>
        /// <param name="authCode">从酷Q处获得的权限代码。</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void SetAuthCode(int authCode);


        /// <summary>
        ///     修改在指定群内指定QQ的群名片。
        /// </summary>
        /// <param name="groupId">要执行操作的群。</param>
        /// <param name="qq">要修改名片的QQ。</param>
        /// <param name="newCard">新名片内容。</param>
        void SetGroupCard(long groupId, long qq, string newCard);


        /// <summary>
        ///     赋予指定群内指定成员会过期的群成员专属头衔。
        /// </summary>
        /// <param name="groupId">要执行操作的群。</param>
        /// <param name="qq">要赋予头衔的QQ。</param>
        /// <param name="newTitle">头衔内容。</param>
        /// <param name="duration">头衔生效时间。（单位：秒）</param>
        void SetGroupTitle(long groupId, long qq, long newTitle, long duration);

        /// <summary>
        ///     赋予指定群内指定成员永久群成员专属头衔。
        /// </summary>
        /// <param name="groupId">要执行操作的群。</param>
        /// <param name="qq">要赋予头衔的QQ。</param>
        /// <param name="newTitle">头衔内容。</param>
        void SetGroupTitle(long groupId, long qq, string newTitle);
    }
}