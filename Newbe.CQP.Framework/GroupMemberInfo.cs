using System;

namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 表示一个群成员的信息。
    /// </summary>
    public sealed class GroupMemberInfo
    {
        /// <summary>
        /// 此群成员在其个人资料上所填写的年龄。
        /// </summary>
        /// <returns> </returns>
        public int Age { get; set; }

        /// <summary>
        /// 此群成员在其个人资料上所填写的区域。
        /// </summary>
        /// <returns></returns>
        public string Area { get; set; }

        /// <summary>
        /// 此群成员的身份。
        /// </summary>
        /// <returns><see cref="String"/></returns>
        public string Authority { get; set; }

        /// <summary>
        /// 指示此群成员是否能够修改所有群成员名片的值。
        /// </summary>
        /// <returns><see cref="Boolean"/> </returns>
        public bool CanModifyInGroupName { get; set; }

        /// <summary>
        /// 此群成员在其个人资料上所填写的性别。
        /// </summary>
        /// <returns></returns>
        public string Gender { get; set; }

        /// <summary>
        /// 此群成员的群名片。
        /// </summary>
        /// <returns></returns>
        public string InGroupName { get; set; }

        /// <summary>
        /// 此群成员的头衔。
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }

        /// <summary>
        /// 此群成员所在群号。
        /// </summary>
        /// <returns> </returns>
        public long GroupId { get; set; }

        /// <summary>
        /// 指示此群成员是否有不良记录的值。
        /// </summary>
        /// <returns><see cref="Boolean"/> </returns>
        public bool HasBadRecord { get; set; }

        /// <summary>
        /// 头衔过期时间。
        /// </summary>
        /// <returns> </returns>
        public int TitleExpirationTime { get; set; }

        /// <summary>
        /// 此群成员的入群时间。
        /// </summary>
        /// <returns> </returns>
        public System.DateTime JoinTime { get; set; }

        /// <summary>
        /// 此群成员最后发言日期。
        /// </summary>
        /// <returns></returns>
        public System.DateTime LastSpeakingTime { get; set; }

        /// <summary>
        /// 此群成员的群内等级。
        /// </summary>
        /// <returns></returns>
        public string Level { get; set; }

        /// <summary>
        /// 此群成员的昵称。
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }

        /// <summary>
        /// 此群成员的QQ号码。
        /// </summary>
        /// <returns></returns>
        public long Number { get; set; }
    }
}