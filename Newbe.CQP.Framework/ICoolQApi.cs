using System;
using System.Security;

namespace Newbe.CQP.Framework
{
    public interface ICoolQApi
    {
        string AppDirectory { get; }
        string Cookies { get; }
        int CsrfToken { get; }
        string NickName { get; }
        long Number { get; }
        SecureString SafelyCookies { get; }

        void Ban(long groupId, long qq, int duration);
        void Ban(long groupId, long qq, TimeSpan duration);
        void BanAll(long groupId);
        void BanAnonymous(long groupId, string anonymous, long duration);
        void BanAnonymous(long groupId, string anonymous, TimeSpan duration);
        void DisableAnonymousChat(long groupId);
        void EnableAnonymousChat(long groupId);
        GroupMemberInfo GetGroupMemberInfo(long groupId, long qq);
        void KickFromGroup(long groupId, long qq, bool rejectAddGroupRequest);
        void Log(CoolQLogLevel level, string message);
        void Log(CoolQLogLevel level, string message, string category);
        void ProcessAddFriendRequest(string responseMark, CoolQRequestResult result);
        void ProcessAddFriendRequest(string responseMark, CoolQRequestResult result, string remark);
        void ProcessAddGroupRequest(string responseMark, CoolQAddGroupRequestType resquestType, CoolQRequestResult operation, string reason);
        void QuitAndDismissTheGroup(long groupId);
        void QuitTheDiscussGroup(long discussGroupId);
        void QuitTheGroup(long groupId);
        void RemoveBanned(long groupId, long qq);
        void RemoveBannedAll(long groupId);
        void RevokeAdmin(long groupId, long qq);
        void SendDiscussGroupMessage(long dicussGroupId, string message);
        void SendGood(long qq);
        void SendGroupMessage(long groupId, string message);
        void SendPrivateMessage(long qq, string message);
        void SetAsAdmin(long groupId, long qq);
        void SetAuthCode(int authCode);
        void SetGroupCard(long groupId, long qq, string newCard);
        void SetGroupTitle(long groupId, long qq, long newTitle, long duration);
        void SetGroupTitle(long groupId, long qq, string newTitle);
    }
}