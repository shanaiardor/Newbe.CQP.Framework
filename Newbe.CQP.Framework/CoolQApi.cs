using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Newbe.CQP.Framework
{
    /// <summary>
    ///     向酷Q.NET插件提供酷Q Api
    /// </summary>
    public class CoolQApi
    {
        private int _cqauthcode;

        /// <summary>
        ///     获取酷Q当前登录用户的Cookies。
        /// </summary>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public string Cookies => NativeMethods.CQ_getCookies(_cqauthcode);

        /// <summary>
        ///     获取加密后的Cookies。
        /// </summary>
        /// <returns><see cref="SecureString" /> 保存了Cookies的<see cref="SecureString" />对象。</returns>
        /// <remarks>
        ///     使用方法详见：
        ///     https://msdn.microsoft.com/zh-cn/library/system.security.securestring.aspx
        ///     此Api是酷Q非官方Api。
        /// </remarks>
        public SecureString SafelyCookies
        {
            get
            {
                var sc = new SecureString();
                foreach (var c in NativeMethods.CQ_getCookies(_cqauthcode))
                    sc.AppendChar(c);
                sc.MakeReadOnly();
                return sc;
            }
        }

        /// <summary>
        ///     获取酷Q当前登录用户的CsrfToken。
        /// </summary>
        /// <returns>
        ///     <see cref="Integer" />
        /// </returns>
        public int CsrfToken => NativeMethods.CQ_getCsrfToken(_cqauthcode);

        /// <summary>
        ///     获取当前插件的数据存储目录。
        /// </summary>
        /// <returns>
        ///     <See cref="string" />
        /// </returns>
        public string AppDirectory => NativeMethods.CQ_getAppDirectory(_cqauthcode);

        /// <summary>
        ///     获取酷Q当前登录账户的昵称。
        /// </summary>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public string NickName => NativeMethods.CQ_getLoginNick(_cqauthcode);

        /// <summary>
        ///     获取酷Q当前登录账户的QQ号码。
        /// </summary>
        /// <returns>
        ///     <see cref="Long" />
        /// </returns>
        public long Number => NativeMethods.CQ_getLoginQQ(_cqauthcode);

        /// <summary>
        ///     设置插件权限代码。
        /// </summary>
        /// <param name="authCode">从酷Q处获得的权限代码。</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetAuthCode(int authCode)
        {
            _cqauthcode = authCode;
        }

        /// <summary>
        ///     向指定的QQ发送私聊消息。
        /// </summary>
        /// <param name="qq">接收此消息的QQ。</param>
        /// <param name="message">私聊消息内容。</param>
        public void SendPrivateMessage(long qq, string message)
        {
            NativeMethods.CQ_sendPrivateMsg(_cqauthcode, qq, message);
        }

        /// <summary>
        ///     向指定的群发送群消息。
        /// </summary>
        /// <param name="groupId">接收此消息的群号。</param>
        /// <param name="message">消息内容。</param>
        public void SendGroupMessage(long groupId, string message)
        {
            NativeMethods.CQ_sendGroupMsg(_cqauthcode, groupId, message);
        }

        /// <summary>
        ///     向指定的讨论组发送讨论组消息。
        /// </summary>
        /// <param name="dicussGroupId">接收此消息的讨论组号。</param>
        /// <param name="message">消息内容。</param>
        public void SendDiscussGroupMessage(long dicussGroupId, string message)
        {
            NativeMethods.CQ_sendDiscussMsg(_cqauthcode, dicussGroupId, message);
        }

        /// <summary>
        ///     把日志写入到酷Q运行日志中。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        public void Log(CoolQLogLevel level, string message)
        {
            NativeMethods.CQ_addLog(_cqauthcode, (int) level, level.ToString(), message);
        }

        /// <summary>
        ///     把日志写入到酷Q运行日志中。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        /// <param name="category">日志类别。</param>
        public void Log(CoolQLogLevel level, string message, string category)
        {
            NativeMethods.CQ_addLog(_cqauthcode, (int) level, category, message);
        }

        /// <summary>
        ///     获取缓存的群成员信息。
        /// </summary>
        /// <param name="groupId">要获取信息的群成员的所在群。</param>
        /// <param name="qq">要获取信息的群成员QQ。</param>
        /// <returns>
        ///     <see cref="GroupMemberInfo" />
        /// </returns>
        /// <remarks>此函数采用Flexlive的处理方法。</remarks>
        public GroupMemberInfo GetGroupMemberInfo(long groupId, long qq)
        {
            try
            {
                var data = NativeMethods.CQ_getGroupMemberInfoV2(_cqauthcode, groupId, qq, 0);
                var memberBytes = Convert.FromBase64String(data);
                var info = new GroupMemberInfo();
                var groupNumberBytes = new byte[8];

                Array.Copy(memberBytes, 0, groupNumberBytes, 0, 8);
                Array.Reverse(groupNumberBytes);
                info.GroupId = BitConverter.ToInt64(groupNumberBytes, 0);

                var qqNumberBytes = new byte[8];
                Array.Copy(memberBytes, 8, qqNumberBytes, 0, 8);
                Array.Reverse(qqNumberBytes);
                info.Number = BitConverter.ToInt64(qqNumberBytes, 0);

                var nameLengthBytes = new byte[2];
                Array.Copy(memberBytes, 16, nameLengthBytes, 0, 2);
                Array.Reverse(nameLengthBytes);
                var nameLength = BitConverter.ToInt16(nameLengthBytes, 0);

                var nameBytes = new byte[nameLength];
                Array.Copy(memberBytes, 18, nameBytes, 0, nameLength);
                info.NickName = Encoding.Default.GetString(nameBytes);

                var cardLengthBytes = new byte[2];
                Array.Copy(memberBytes, 18 + nameLength, cardLengthBytes, 0, 2);
                Array.Reverse(cardLengthBytes);
                var cardLength = BitConverter.ToInt16(cardLengthBytes, 0);

                var cardBytes = new byte[cardLength];
                Array.Copy(memberBytes, 20 + nameLength, cardBytes, 0, cardLength);
                info.InGroupName = Encoding.Default.GetString(cardBytes);

                var genderBytes = new byte[4];
                Array.Copy(memberBytes, 20 + nameLength + cardLength, genderBytes, 0, 4);
                Array.Reverse(genderBytes);
                info.Gender = BitConverter.ToInt32(genderBytes, 0) == 0 ? "男" : " 女";

                var ageBytes = new byte[4];
                Array.Copy(memberBytes, 24 + nameLength + cardLength, ageBytes, 0, 4);
                Array.Reverse(ageBytes);
                info.Age = BitConverter.ToInt32(ageBytes, 0);

                var areaLengthBytes = new byte[2];
                Array.Copy(memberBytes, 28 + nameLength + cardLength, areaLengthBytes, 0, 2);
                Array.Reverse(areaLengthBytes);
                var areaLength = BitConverter.ToInt16(areaLengthBytes, 0);

                var areaBytes = new byte[areaLength];
                Array.Copy(memberBytes, 30 + nameLength + cardLength, areaBytes, 0, areaLength);
                info.Area = Encoding.Default.GetString(areaBytes);

                var addGroupTimesBytes = new byte[4];
                Array.Copy(memberBytes, 30 + nameLength + cardLength + areaLength, addGroupTimesBytes, 0, 4);
                Array.Reverse(addGroupTimesBytes);
                info.JoinTime =
                    new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime()
                        .AddSeconds(BitConverter.ToInt32(addGroupTimesBytes, 0));

                var lastSpeakTimesBytes = new byte[4];
                Array.Copy(memberBytes, 34 + nameLength + cardLength + areaLength, lastSpeakTimesBytes, 0, 4);
                Array.Reverse(lastSpeakTimesBytes);
                info.LastSpeakingTime =
                    new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime()
                        .AddSeconds(BitConverter.ToInt32(lastSpeakTimesBytes, 0));

                var levelNameLengthBytes = new byte[2];
                Array.Copy(memberBytes, 38 + nameLength + cardLength + areaLength, levelNameLengthBytes, 0, 2);
                Array.Reverse(levelNameLengthBytes);
                var levelNameLength = BitConverter.ToInt16(levelNameLengthBytes, 0);

                var levelNameBytes = new byte[levelNameLength];
                Array.Copy(memberBytes, 40 + nameLength + cardLength + areaLength, levelNameBytes, 0, levelNameLength);
                info.Level = Encoding.Default.GetString(levelNameBytes);

                var authorBytes = new byte[4];
                Array.Copy(memberBytes, 40 + nameLength + cardLength + areaLength + levelNameLength, authorBytes, 0, 4);
                Array.Reverse(authorBytes);
                var authority = BitConverter.ToInt32(authorBytes, 0);
                info.Authority = authority == 3 ? "群主" : (authority == 2 ? "管理员" : "成员");

                var badBytes = new byte[4];
                Array.Copy(memberBytes, 44 + nameLength + cardLength + areaLength + levelNameLength, badBytes, 0, 4);
                Array.Reverse(badBytes);
                info.HasBadRecord = BitConverter.ToInt32(badBytes, 0) == 1;

                var titleLengthBytes = new byte[2];
                Array.Copy(memberBytes, 48 + nameLength + cardLength + areaLength + levelNameLength, titleLengthBytes, 0,
                    2);
                Array.Reverse(titleLengthBytes);
                var titleLength = BitConverter.ToInt16(titleLengthBytes, 0);

                var titleBytes = new byte[titleLength];
                Array.Copy(memberBytes, 50 + nameLength + cardLength + areaLength + levelNameLength, titleBytes, 0,
                    titleLength);
                info.Title = Encoding.Default.GetString(titleBytes);

                var titleExpireBytes = new byte[4];
                Array.Copy(memberBytes, 50 + nameLength + cardLength + areaLength + levelNameLength + titleLength,
                    titleExpireBytes, 0, 4);
                Array.Reverse(titleExpireBytes);
                info.TitleExpirationTime = BitConverter.ToInt32(titleExpireBytes, 0);

                var modifyCardBytes = new byte[4];
                Array.Copy(memberBytes, 54 + nameLength + cardLength + areaLength + levelNameLength + titleLength,
                    titleExpireBytes, 0, 4);
                Array.Reverse(titleExpireBytes);
                info.CanModifyInGroupName = BitConverter.ToInt32(modifyCardBytes, 0) == 1;
                return info;
            }
            catch (Exception e)
            {
                Log(CoolQLogLevel.Error, e.Message, "获取群成员信息");
            }
            throw new Exception();
        }

        /// <summary>
        ///     给指定QQ点赞。
        /// </summary>
        /// <param name="qq">要点赞的QQ的号码。</param>
        public void SendGood(long qq)
        {
            NativeMethods.CQ_sendLike(_cqauthcode, qq);
        }

        /// <summary>
        ///     处理好友请求。
        /// </summary>
        /// <param name="responseMark">用于处理此好友请求的反馈标识。</param>
        /// <param name="result">是否通过此请求。</param>
        public void ProcessAddFriendRequest(string responseMark, CoolQRequestResult result)
        {
            NativeMethods.CQ_setFriendAddRequest(_cqauthcode, responseMark, (int) result, "");
        }

        /// <summary>
        ///     处理好友请求。
        /// </summary>
        /// <param name="responseMark">用于处理此好友请求的反馈标识。</param>
        /// <param name="result">是否通过此请求。</param>
        /// <param name="remark">对此好友的备注名。</param>
        public void ProcessAddFriendRequest(string responseMark, CoolQRequestResult result, string remark)
        {
            NativeMethods.CQ_setFriendAddRequest(_cqauthcode, responseMark, (int) result, remark);
        }

        /// <summary>
        ///     把指定QQ从指定群中踢出。
        /// </summary>
        /// <param name="groupId">要踢出QQ的群。</param>
        /// <param name="qq">要踢出的QQ。</param>
        /// <param name="rejectAddGroupRequest">指示是否不再处理此人的加群申请。</param>
        public void KickFromGroup(long groupId, long qq, bool rejectAddGroupRequest)
        {
            NativeMethods.CQ_setGroupKick(_cqauthcode, groupId, qq, rejectAddGroupRequest ? 1 : 0);
        }

        /// <summary>
        ///     对在指定群里的指定QQ进行禁言。
        /// </summary>
        /// <param name="groupId">要进行禁言操作的群。</param>
        /// <param name="qq">要禁言的QQ。</param>
        /// <param name="duration">禁言持续时长。</param>
        public void Ban(long groupId, long qq, TimeSpan duration)
        {
            var totalsec = (long) Math.Floor(duration.TotalSeconds);
            NativeMethods.CQ_setGroupBan(_cqauthcode, groupId, qq, totalsec);
        }

        /// <summary>
        ///     对在指定群里的指定QQ进行禁言。
        /// </summary>
        /// <param name="groupId">要进行禁言操作的群。</param>
        /// <param name="qq">要禁言的QQ。</param>
        /// <param name="duration">禁言持续秒数。</param>
        public void Ban(long groupId, long qq, int duration)
        {
            NativeMethods.CQ_setGroupBan(_cqauthcode, groupId, qq, duration);
        }

        /// <summary>
        ///     在指定群内对被禁言的QQ执行解除禁言操作。
        /// </summary>
        /// <param name="groupId">要进行解除禁言操作的群。</param>
        /// <param name="qq">要解除禁言的QQ。</param>
        public void RemoveBanned(long groupId, long qq)
        {
            Ban(groupId, qq, 0);
        }

        /// <summary>
        ///     把指定QQ设定为指定群的管理员。
        /// </summary>
        /// <param name="groupId">要进行操作的群。</param>
        /// <param name="qq">要被提升为管理员的QQ。</param>
        public void SetAsAdmin(long groupId, long qq)
        {
            NativeMethods.CQ_setGroupAdmin(_cqauthcode, groupId, qq, 1);
        }

        /// <summary>
        ///     把指定群指定QQ的群管理员身份取消。
        /// </summary>
        /// <param name="groupId">要执行操作的群。</param>
        /// <param name="qq">要取消管理员的QQ。</param>
        public void RevokeAdmin(long groupId, long qq)
        {
            NativeMethods.CQ_setGroupAdmin(_cqauthcode, groupId, qq, 0);
        }

        /// <summary>
        ///     对指定群执行全体禁言操作。
        /// </summary>
        /// <param name="groupId">要进行全体禁言的群。</param>
        public void BanAll(long groupId)
        {
            NativeMethods.CQ_setGroupWholeBan(_cqauthcode, groupId, 1);
        }

        /// <summary>
        ///     解除指定群的全体禁言状态。
        /// </summary>
        /// <param name="groupId">要解除全体禁言状态的群。</param>
        public void RemoveBannedAll(long groupId)
        {
            NativeMethods.CQ_setGroupWholeBan(_cqauthcode, groupId, 0);
        }

        /// <summary>
        ///     对在指定群里的指定匿名用户进行禁言。在对指定的匿名用户进行禁言后，不能解除匿名用户的禁言状态。
        /// </summary>
        /// <param name="groupId">要进行禁言操作的群。</param>
        /// <param name="anonymous">要禁言的匿名用户。</param>
        /// <param name="duration">禁言持续时长。</param>
        public void BanAnonymous(long groupId, string anonymous, TimeSpan duration)
        {
            NativeMethods.CQ_setGroupAnonymousBan(_cqauthcode, groupId, anonymous,
                (long) Math.Floor(duration.TotalSeconds));
        }

        /// <summary>
        ///     对在指定群里的指定匿名用户进行禁言。在对指定的匿名用户进行禁言后，不能解除匿名用户的禁言状态。
        /// </summary>
        /// <param name="groupId">要进行禁言操作的群。</param>
        /// <param name="anonymous">要禁言的匿名用户。</param>
        /// <param name="duration">禁言持续秒数。</param>
        public void BanAnonymous(long groupId, string anonymous, long duration)
        {
            NativeMethods.CQ_setGroupAnonymousBan(_cqauthcode, groupId, anonymous, duration);
        }

        /// <summary>
        ///     打开指定的群的匿名聊天。
        /// </summary>
        /// <param name="groupId">要打开匿名聊天的群。</param>
        public void EnableAnonymousChat(long groupId)
        {
            NativeMethods.CQ_setGroupAnonymous(_cqauthcode, groupId, 1);
        }

        /// <summary>
        ///     关闭指定的群的匿名聊天。
        /// </summary>
        /// <param name="groupId">要关闭匿名聊天的群。</param>
        public void DisableAnonymousChat(long groupId)
        {
            NativeMethods.CQ_setGroupAnonymous(_cqauthcode, groupId, 0);
        }

        /// <summary>
        ///     修改在指定群内指定QQ的群名片。
        /// </summary>
        /// <param name="groupId">要执行操作的群。</param>
        /// <param name="qq">要修改名片的QQ。</param>
        /// <param name="newCard">新名片内容。</param>
        public void SetGroupCard(long groupId, long qq, string newCard)
        {
            NativeMethods.CQ_setGroupCard(_cqauthcode, groupId, qq, newCard);
        }

        /// <summary>
        ///     退出指定群。
        /// </summary>
        /// <param name="groupId">要退出的群的群号。</param>
        public void QuitTheGroup(long groupId)
        {
            NativeMethods.CQ_setGroupLeave(_cqauthcode, groupId, 0);
        }

        /// <summary>
        ///     退出并解散指定群。
        /// </summary>
        /// <param name="groupId">要退出并解散的群的群号。</param>
        public void QuitAndDismissTheGroup(long groupId)
        {
            NativeMethods.CQ_setGroupLeave(_cqauthcode, groupId, 1);
        }

        /// <summary>
        ///     赋予指定群内指定成员永久群成员专属头衔。
        /// </summary>
        /// <param name="groupId">要执行操作的群。</param>
        /// <param name="qq">要赋予头衔的QQ。</param>
        /// <param name="newTitle">头衔内容。</param>
        public void SetGroupTitle(long groupId, long qq, string newTitle)
        {
            NativeMethods.CQ_setGroupSpecialTitle(_cqauthcode, groupId, qq, newTitle, -1);
        }

        /// <summary>
        ///     赋予指定群内指定成员会过期的群成员专属头衔。
        /// </summary>
        /// <param name="groupId">要执行操作的群。</param>
        /// <param name="qq">要赋予头衔的QQ。</param>
        /// <param name="newTitle">头衔内容。</param>
        /// <param name="duration">头衔生效时间。（单位：秒）</param>
        public void SetGroupTitle(long groupId, long qq, long newTitle, long duration)
        {
            NativeMethods.CQ_setGroupSpecialTitle(_cqauthcode, groupId, qq, newTitle.ToString(), duration);
        }

        /// <summary>
        ///     退出指定的讨论组。
        /// </summary>
        /// <param name="discussGroupId">要退出的讨论组的ID。</param>
        public void QuitTheDiscussGroup(long discussGroupId)
        {
            NativeMethods.CQ_setDiscussLeave(_cqauthcode, discussGroupId);
        }

        /// <summary>
        ///     处理加群请求。
        /// </summary>
        /// <param name="responseMark">用于处理请求的反馈标识。</param>
        /// <param name="resquestType">请求类型。</param>
        /// <param name="operation">请求处理结果。</param>
        /// <param name="reason">附言。</param>
        public void ProcessAddGroupRequest(string responseMark, CoolQAddGroupRequestType resquestType,
            CoolQRequestResult operation, string reason)
        {
            NativeMethods.CQ_setGroupAddRequestV2(_cqauthcode, responseMark, (int) resquestType, (int) operation, reason);
        }

        private abstract class NativeMethods
        {
            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_sendPrivateMsg(int authCode, long QQid,
                [MarshalAs(UnmanagedType.LPStr)] [In] string message);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_sendGroupMsg(int authCode, long groupId,
                [MarshalAs(UnmanagedType.LPStr)] [In] string message);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_sendDiscussMsg(int authCode, long discussId,
                [MarshalAs(UnmanagedType.LPStr)] [In] string message);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_sendLike(int authCode, long QQid);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setGroupKick(int authCode, long groupId, long QQid, int rejectAddRequest);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setGroupBan(int authCode, long groupId, long QQid, long duration);

            //
            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setGroupAdmin(int authCode, long groupId, long QQid, int setAdmin);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setGroupWholeBan(int authCode, long groupId, int enableban);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setGroupAnonymousBan(int authCode, long groupId,
                [MarshalAs(UnmanagedType.LPStr)] [In] string anonymous, long duration);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setGroupAnonymous(int authCode, long groupId, int enableAnomymous);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setGroupCard(int authCode, long groupId, long QQid,
                [MarshalAs(UnmanagedType.LPStr)] [In] string newCard);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setGroupLeave(int authCode, long groupId, int isdismissed);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setGroupSpecialTitle(int authCode, long groupId, long QQid,
                [MarshalAs(UnmanagedType.LPStr)] [In] string newTitle, long duration);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setDiscussLeave(int authCode, long discussGroupId);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setFriendAddRequest(int authCode,
                [MarshalAs(UnmanagedType.LPStr)] [In] string responseFlag, int responseOperation,
                [MarshalAs(UnmanagedType.LPStr)] [In] string remark);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_setGroupAddRequestV2(int authCode,
                [MarshalAs(UnmanagedType.LPStr)] [In] string responseFlag, int requestType, int responseOperation,
                [MarshalAs(UnmanagedType.LPStr)] [In] string reason);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern string CQ_getGroupMemberInfoV2(int authCode, long groupId, long QQid, int nocache);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_addLog(int authCode, int priority,
                [MarshalAs(UnmanagedType.LPStr)] [In] string category,
                [MarshalAs(UnmanagedType.LPStr)] [In] string content);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern string CQ_getCookies(int authCode);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern int CQ_getCsrfToken(int authCode);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern long CQ_getLoginQQ(int authCode);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern string CQ_getLoginNick(int authCode);

            [DllImport("CQP.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = false)]
            public static extern string CQ_getAppDirectory(int authCode);
        }
    }
}