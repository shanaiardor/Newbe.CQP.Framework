using System;
using System.IO;
using LiteDB;
using Newbe.CQP.Framework.Logging;

namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 酷Q.NET测试插件。
    /// </summary>
    public sealed class TestPlugin
    {
        //Api版本号，若酷Q官方SDK没有更新此版本号，请勿改动此值
        private const int ApiVersion = 9;
        //AppID
        private const string AppId = "newbe.cqp.framework";
        private static ILog Logger = LogProvider.For<TestPlugin>();

        private TestPlugin()
        {
            PluginHelper.CQ.SendPrivateMessage(472158246, 44.ToString());
            Logger.Debug("heheda");
        }

        /// <summary>
        /// 此函数会在插件被开启时发生。
        /// </summary>
        /// <returns><see cref="Integer"/> 返回处理过程是否成功的值。</returns>
        [DllExport("_eventEnable")]
        public static int Enabled()
        {
            return 0;
        }

        /// <summary>
        /// 此函数会在插件被禁用时发生。
        /// </summary>
        /// <returns><see cref="Integer"/> 返回处理过程是否成功的值。</returns>
        [DllExport("_eventDisable")]
        public static int Disabled()
        {
            return 0;
        }

        /// <summary>
        /// 向酷Q提供插件信息。
        /// </summary>
        /// <returns><see cref="String"/> 一个固定格式字符串。</returns>
        [DllExport("AppInfo")]
        public static string AppInfo()
        {
            //请勿修改此函数
            return (ApiVersion.ToString() + "," + AppId);
        }

        /// <summary>
        /// 获取此插件的AuthCode。
        /// </summary>
        /// <param name="authcode">由酷Q提供的AuthCode。</param>
        /// <returns><see cref="Integer"/> </returns>
        [DllExport("Initialize", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int Initialize(int authcode)
        {
            //请勿更改此函数
            PluginHelper.CQ.SetAuthCode(authcode);
            return 0;
            //固定返回0
        }

        /// <summary>
        /// 此函数会在酷Q退出时被调用。
        /// </summary>
        /// <returns><see cref="Integer"/> </returns>
        [DllExport("_eventExit", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int CoolQExited()
        {
            return 0;
            //固定返回0
        }


        /// <summary>
        /// 处理私聊消息。
        /// </summary>
        /// <param name="subType">私聊消息类型。11代表消息来自好友；1代表消息来自在线状态；2代表消息来自群；3代表消息来自讨论组。</param>
        /// <param name="sendTime">消息发送时间的时间戳。</param>
        /// <param name="fromQQ">发送此消息的QQ号码。</param>
        /// <param name="msg">消息的内容。</param>
        /// <param name="font">消息所使用的字体。</param>
        /// <returns><see cref="Integer"/> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        [DllExport("_eventPrivateMsg", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int ProcessPrivateMessage(int subType, int sendTime, long fromQQ, string msg, int font)
        {
            //0为忽略 1为拦截
            PluginHelper.CQ.SendPrivateMessage(fromQQ, 1.ToString());
            LogProvider.For<TestPlugin>().Debug(1.ToString);
            var myClass = new MyClass {Name = "hehe"};
            using (var fileStream = File.Create("1.txt"))
            {
                using (var db = new LiteDatabase("my.db"))
                {
                    db.GetCollection<MyClass>("hehe").Insert(myClass);
                }
            }
            PluginHelper.CQ.SendPrivateMessage(fromQQ, 3.ToString());
            LogProvider.For<TestPlugin>().Debug(3.ToString());

            return 0;
        }

        private class MyClass
        {
            public string Name { get; set; }
        }

        /// <summary>
        /// 处理群聊消息。
        /// </summary>
        /// <param name="subType">消息类型，目前固定为1。</param>
        /// <param name="sendTime">消息发送时间的时间戳。</param>
        /// <param name="fromGroup">消息来源群号。</param>
        /// <param name="fromQQ">发送此消息的QQ号码。</param>
        /// <param name="fromAnonymous">发送此消息的匿名用户。</param>
        /// <param name="msg">消息内容。</param>
        /// <param name="font">消息所使用字体。</param>
        /// <returns><see cref="Integer"/> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        [DllExport("_eventGroupMsg", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int ProcessGroupMessage(int subType, int sendTime, long fromGroup, long fromQQ,
            string fromAnonymous,
            string msg, int font)
        {
            //0为忽略 1为拦截

            return 0;
        }

        /// <summary>
        /// 处理讨论组消息。
        /// </summary>
        /// <param name="subType">消息类型，目前固定为1。</param>
        /// <param name="sendTime">消息发送时间的时间戳。</param>
        /// <param name="fromDiscuss">消息来源讨论组号。</param>
        /// <param name="fromQQ">发送此消息的QQ号码。</param>
        /// <param name="msg">消息内容。</param>
        /// <param name="font">消息所使用字体。</param>
        /// <returns><see cref="Integer"/> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        [DllExport("_eventDiscussMsg", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int ProcessDiscussGroupMessage(int subType, int sendTime, long fromDiscuss, long fromQQ,
            string msg,
            int font)
        {
            //0为忽略 1为拦截

            return 0;
        }

        /// <summary>
        /// 处理群文件上传事件。
        /// </summary>
        /// <param name="subType">消息类型，目前固定为1。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromGroup">事件来源群号。</param>
        /// <param name="fromQQ">上传此文件的QQ号码。</param>
        /// <param name="file">上传的文件的信息。</param>
        /// <returns><see cref="Integer"/> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        [DllExport("_eventGroupUpload", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int ProcessGroupUpload(int subType, int sendTime, long fromGroup, long fromQQ, string file)
        {
            return 0;
        }

        /// <summary>
        /// 处理群管理员变动事件。
        /// </summary>
        /// <param name="subType">事件类型。1为被取消管理员，2为被设置管理员。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromGroup">事件来源群号。</param>
        /// <param name="target">被操作的QQ。</param>
        /// <returns><see cref="Integer"/> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        [DllExport("_eventSystem_GroupAdmin", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int ProcessGroupAdminChange(int subType, int sendTime, long fromGroup, long target)
        {
            return 0;
        }

        /// <summary>
        /// 处理群成员数量减少事件。
        /// </summary>
        /// <param name="subType">事件类型。1为群员离开；2为群员被踢为；3为自己(即登录号)被踢。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromGroup">事件来源群号。</param>
        /// <param name="fromQQ">事件来源QQ。</param>
        /// <param name="target">被操作的QQ。</param>
        /// <returns><see cref="Integer"/> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        [DllExport("_eventSystem_GroupMemberDecrease", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int ProcessGroupMemberDecrease(int subType, int sendTime, long fromGroup, long fromQQ, long target)
        {
            return 0;
        }

        /// <summary>
        /// 处理群成员添加事件。
        /// </summary>
        /// <param name="subType">事件类型。1为管理员已同意；2为管理员邀请。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromGroup">事件来源群号。</param>
        /// <param name="fromQQ">事件来源QQ。</param>
        /// <param name="target">被操作的QQ。</param>
        /// <returns><see cref="Integer"/> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        [DllExport("_eventSystem_GroupMemberIncrease", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int ProcessGroupMemberIncrease(int subType, int sendTime, long fromGroup, long fromQQ, long target)
        {
            return 0;
        }

        /// <summary>
        /// 处理好友已添加事件。
        /// </summary>
        /// <param name="subType">事件类型。固定为1。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromQQ">事件来源QQ。</param>
        /// <returns><see cref="Integer"/> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        [DllExport("_eventFriend_Add", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int ProcessFriendsAdded(int subType, int sendTime, long fromQQ)
        {
            return 0;
        }

        /// <summary>
        /// 处理好友添加请求。
        /// </summary>
        /// <param name="subType">事件类型。固定为1。</param>
        /// <param name="sendTime">事件发生时间的时间戳。</param>
        /// <param name="fromQQ">事件来源QQ。</param>
        /// <param name="msg">附言内容。</param>
        /// <param name="font">消息所使用字体。</param>
        /// <returns><see cref="Integer"/> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        [DllExport("_eventRequest_AddFriend", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int ProcessAddFriendRequest(int subType, int sendTime, long fromQQ, string msg, int font)
        {
            return 0;
        }

        /// <summary>
        /// 处理加群请求。
        /// </summary>
        /// <param name="subType">请求类型。1为他人申请入群；2为自己(即登录号)受邀入群。</param>
        /// <param name="sendTime">请求发送时间戳。</param>
        /// <param name="fromGroup">要加入的群的群号。</param>
        /// <param name="fromQQ">发送此请求的QQ号码。</param>
        /// <param name="msg">附言内容。</param>
        /// <param name="responseMark">用于处理请求的标识。</param>
        /// <returns><see cref="Integer"/> 是否拦截消息的值，0为忽略消息，1为拦截消息。</returns>
        [DllExport("_eventRequest_AddGroup", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static int ProcessJoinGroupRequest(int subType, int sendTime, long fromGroup, long fromQQ, string msg,
            string responseMark)
        {
            return 0;
        }

        //菜单示例
        //假设菜单中在json文件中的设置如下
        //{
        //        "name""设置A",			//菜单名称
        //       "function":"_menuA"		//菜单对应函数
        //}
        //则
        //<DllExport("菜单对应函数")>
        //Public Shared Function <执行过程名称>() As Integer
        //    Return 0 '固定返回0
        //End Function
    }
}