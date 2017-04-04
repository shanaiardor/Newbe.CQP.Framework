namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 插件基类
    /// </summary>
    public abstract class PluginBase : IPluginBase
    {
        /// <summary>
        /// 酷QApi
        /// </summary>
        protected readonly ICoolQApi CoolQApi;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="coolQApi"></param>
        protected PluginBase(ICoolQApi coolQApi)
        {
            CoolQApi = coolQApi;
        }


        /// <inheritdoc />
        public abstract string AppId { get; }


        /// <inheritdoc />
        public string ApiVersion { get; } = "9";


        /// <inheritdoc />
        public virtual int Enabled() => 0;

        /// <inheritdoc />
        public virtual int Disabled() => 0;


        /// <inheritdoc />
        public string AppInfo() => (ApiVersion + "," + AppId).ToLower();


        /// <inheritdoc />
        public int Initialize(int authcode)
        {
            //请勿更改此函数
            CoolQApi.SetAuthCode(authcode);
            return 0;
            //固定返回0
        }

        /// <inheritdoc />
        public virtual int CoolQExited()
        {
            return 0;
        }


        /// <inheritdoc />
        public virtual int ProcessPrivateMessage(int subType, int sendTime, long fromQq, string msg, int font) => 0;


        /// <inheritdoc />
        public virtual int ProcessGroupMessage(int subType, int sendTime, long fromGroup, long fromQq,
            string fromAnonymous,
            string msg, int font) => 0;

        /// <inheritdoc />
        public virtual int ProcessDiscussGroupMessage(int subType, int sendTime, long fromDiscuss, long fromQq,
            string msg,
            int font) => 0;

        /// <inheritdoc />
        public virtual int ProcessGroupUpload(int subType, int sendTime, long fromGroup, long fromQq, string file) => 0;


        /// <inheritdoc />
        public virtual int ProcessGroupAdminChange(int subType, int sendTime, long fromGroup, long target) => 0;


        /// <inheritdoc />
        public virtual int ProcessGroupMemberDecrease(int subType, int sendTime, long fromGroup, long fromQq,
            long target) => 0;


        /// <inheritdoc />
        public virtual int ProcessGroupMemberIncrease(int subType, int sendTime, long fromGroup, long fromQq,
            long target) => 0;


        /// <inheritdoc />
        public virtual int ProcessFriendsAdded(int subType, int sendTime, long fromQq) => 0;


        /// <inheritdoc />
        public virtual int ProcessAddFriendRequest(int subType, int sendTime, long fromQq, string msg, int font) => 0;

        /// <inheritdoc />
        public virtual int ProcessJoinGroupRequest(int subType, int sendTime, long fromGroup, long fromQq, string msg,
            string responseMark) => 0;
    }
}