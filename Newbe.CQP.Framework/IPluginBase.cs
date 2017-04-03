namespace Newbe.CQP.Framework
{
    public interface IPluginBase
    {
        string ApiVersion { get; }
        string AppId { get; }

        string AppInfo();
        int CoolQExited();
        int Disabled();
        int Enabled();
        int Initialize(int authcode);
        int ProcessAddFriendRequest(int subType, int sendTime, long fromQQ, string msg, int font);
        int ProcessDiscussGroupMessage(int subType, int sendTime, long fromDiscuss, long fromQQ, string msg, int font);
        int ProcessFriendsAdded(int subType, int sendTime, long fromQQ);
        int ProcessGroupAdminChange(int subType, int sendTime, long fromGroup, long target);
        int ProcessGroupMemberDecrease(int subType, int sendTime, long fromGroup, long fromQQ, long target);
        int ProcessGroupMemberIncrease(int subType, int sendTime, long fromGroup, long fromQQ, long target);
        int ProcessGroupMessage(int subType, int sendTime, long fromGroup, long fromQQ, string fromAnonymous, string msg, int font);
        int ProcessGroupUpload(int subType, int sendTime, long fromGroup, long fromQQ, string file);
        int ProcessJoinGroupRequest(int subType, int sendTime, long fromGroup, long fromQQ, string msg, string responseMark);
        int ProcessPrivateMessage(int subType, int sendTime, long fromQQ, string msg, int font);
    }
}