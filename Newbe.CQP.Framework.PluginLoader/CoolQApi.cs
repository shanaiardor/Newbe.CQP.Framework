using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Newbe.CQP.Framework.PluginLoader
{
    internal class CoolQApi : ICoolQApi
    {
        private int _cqauthcode;

        /// <inheritdoc />
        public string Cookies => NativeMethods.CQ_getCookies(_cqauthcode);


        /// <inheritdoc />
        public SecureString SafelyCookies
        {
            get
            {
                var sc = new SecureString();
                foreach (var c in NativeMethods.CQ_getCookies(_cqauthcode))
                {
                    sc.AppendChar(c);
                }
                sc.MakeReadOnly();
                return sc;
            }
        }


        /// <inheritdoc />
        public int CsrfToken => NativeMethods.CQ_getCsrfToken(_cqauthcode);


        /// <inheritdoc />
        public string AppDirectory => NativeMethods.CQ_getAppDirectory(_cqauthcode);


        /// <inheritdoc />
        public string NickName => NativeMethods.CQ_getLoginNick(_cqauthcode);


        /// <inheritdoc />
        public long Number => NativeMethods.CQ_getLoginQQ(_cqauthcode);


        /// <inheritdoc />
        public void SetAuthCode(int authCode)
        {
            _cqauthcode = authCode;
        }


        /// <inheritdoc />
        public void SendPrivateMessage(long qq, string message)
        {
            NativeMethods.CQ_sendPrivateMsg(_cqauthcode, qq, message);
        }


        /// <inheritdoc />
        public void SendGroupMessage(long groupId, string message)
        {
            NativeMethods.CQ_sendGroupMsg(_cqauthcode, groupId, message);
        }


        /// <inheritdoc />
        public void SendDiscussGroupMessage(long dicussGroupId, string message)
        {
            NativeMethods.CQ_sendDiscussMsg(_cqauthcode, dicussGroupId, message);
        }


        /// <inheritdoc />
        public void Log(CoolQLogLevel level, string message)
        {
            NativeMethods.CQ_addLog(_cqauthcode, (int) level, level.ToString(), message);
        }


        /// <inheritdoc />
        public void Log(CoolQLogLevel level, string message, string category)
        {
            NativeMethods.CQ_addLog(_cqauthcode, (int) level, category, message);
        }


        /// <inheritdoc />
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
            catch (Exception)
            {
                //无法获取到群成员信息，直接返回null
                return null;
            }
        }


        /// <inheritdoc />
        public void SendGood(long qq)
        {
            NativeMethods.CQ_sendLike(_cqauthcode, qq);
        }


        /// <inheritdoc />
        public void ProcessAddFriendRequest(string responseMark, CoolQRequestResult result)
        {
            NativeMethods.CQ_setFriendAddRequest(_cqauthcode, responseMark, (int) result, "");
        }


        /// <inheritdoc />
        public void ProcessAddFriendRequest(string responseMark, CoolQRequestResult result, string remark)
        {
            NativeMethods.CQ_setFriendAddRequest(_cqauthcode, responseMark, (int) result, remark);
        }


        /// <inheritdoc />
        public void KickFromGroup(long groupId, long qq, bool rejectAddGroupRequest)
        {
            NativeMethods.CQ_setGroupKick(_cqauthcode, groupId, qq, rejectAddGroupRequest ? 1 : 0);
        }


        /// <inheritdoc />
        public void Ban(long groupId, long qq, TimeSpan duration)
        {
            var totalsec = (long) Math.Floor(duration.TotalSeconds);
            NativeMethods.CQ_setGroupBan(_cqauthcode, groupId, qq, totalsec);
        }

        /// <inheritdoc />
        public void Ban(long groupId, long qq, int duration)
        {
            NativeMethods.CQ_setGroupBan(_cqauthcode, groupId, qq, duration);
        }


        /// <inheritdoc />
        public void RemoveBanned(long groupId, long qq)
        {
            Ban(groupId, qq, 0);
        }

        /// <inheritdoc />
        public void SetAsAdmin(long groupId, long qq)
        {
            NativeMethods.CQ_setGroupAdmin(_cqauthcode, groupId, qq, 1);
        }


        /// <inheritdoc />
        public void RevokeAdmin(long groupId, long qq)
        {
            NativeMethods.CQ_setGroupAdmin(_cqauthcode, groupId, qq, 0);
        }

        /// <inheritdoc />
        public void BanAll(long groupId)
        {
            NativeMethods.CQ_setGroupWholeBan(_cqauthcode, groupId, 1);
        }

        /// <inheritdoc />
        public void RemoveBannedAll(long groupId)
        {
            NativeMethods.CQ_setGroupWholeBan(_cqauthcode, groupId, 0);
        }

        /// <inheritdoc />
        public void BanAnonymous(long groupId, string anonymous, TimeSpan duration)
        {
            NativeMethods.CQ_setGroupAnonymousBan(_cqauthcode, groupId, anonymous,
                (long) Math.Floor(duration.TotalSeconds));
        }


        /// <inheritdoc />
        public void BanAnonymous(long groupId, string anonymous, long duration)
        {
            NativeMethods.CQ_setGroupAnonymousBan(_cqauthcode, groupId, anonymous, duration);
        }

        /// <inheritdoc />
        public void EnableAnonymousChat(long groupId)
        {
            NativeMethods.CQ_setGroupAnonymous(_cqauthcode, groupId, 1);
        }

        /// <inheritdoc />
        public void DisableAnonymousChat(long groupId)
        {
            NativeMethods.CQ_setGroupAnonymous(_cqauthcode, groupId, 0);
        }

        /// <inheritdoc />
        public void SetGroupCard(long groupId, long qq, string newCard)
        {
            NativeMethods.CQ_setGroupCard(_cqauthcode, groupId, qq, newCard);
        }

        /// <inheritdoc />
        public void QuitTheGroup(long groupId)
        {
            NativeMethods.CQ_setGroupLeave(_cqauthcode, groupId, 0);
        }


        /// <inheritdoc />
        public void QuitAndDismissTheGroup(long groupId)
        {
            NativeMethods.CQ_setGroupLeave(_cqauthcode, groupId, 1);
        }


        /// <inheritdoc />
        public void SetGroupTitle(long groupId, long qq, string newTitle)
        {
            NativeMethods.CQ_setGroupSpecialTitle(_cqauthcode, groupId, qq, newTitle, -1);
        }

        /// <inheritdoc />
        public void SetGroupTitle(long groupId, long qq, long newTitle, long duration)
        {
            NativeMethods.CQ_setGroupSpecialTitle(_cqauthcode, groupId, qq, newTitle.ToString(), duration);
        }


        /// <inheritdoc />
        public void QuitTheDiscussGroup(long discussGroupId)
        {
            NativeMethods.CQ_setDiscussLeave(_cqauthcode, discussGroupId);
        }


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