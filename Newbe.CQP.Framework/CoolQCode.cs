using System;

namespace Newbe.CQP.Framework
{
    /// <summary>
    /// 表示酷Q代码合集。
    /// </summary>
    public static class CoolQCode
    {
        /// <summary>
        /// 获取酷Q匿名代码。
        /// </summary>
        /// <param name="ignore">是否要强制匿名。</param>
        /// <returns></returns>
        public static string Anonymous(bool ignore)
        {
            return $"[CQ:anonymous{(ignore ? "ignore=true" : "")}]";
        }

        /// <summary>
        /// 获取用于@指定QQ的代码。
        /// </summary>
        /// <param name="number">要被@的QQ的号码。如果为@全体成员，则为-1。</param>
        /// <returns></returns>
        public static string At(long number)
        {
            return $"[CQ:at,qq={(number == -1 ? "all" : number.ToString())}]";
        }

        /// <summary>
        /// 获取用于发送特定Emoji表情的代码。
        /// </summary>
        /// <param name="id">Emoji表情ID。 </param>
        /// <returns></returns>
        public static string Emoji(int id)
        {
            return "[CQ:emoji,id=" + id + "]";
        }

        /// <summary>
        /// 获取用于发送表情的代码。
        /// </summary>
        /// <param name="id">表情ID。 </param>
        /// <returns></returns>
        public static string Expression(int id)
        {
            return "[CQ:face,id=" + id + "]";
        }

        /// <summary>
        /// 获取用于发送自定义图片的代码。
        /// </summary>
        /// <param name="imagePath">自定义图片路径。</param>
        /// <returns></returns>
        public static string Image(string imagePath)
        {
            return "[CQ:image,file=" + imagePath + "]";
        }

        /// <summary>
        /// 获取用于分享音乐的代码。
        /// </summary>
        /// <param name="id">音乐索引。</param>
        /// <returns></returns>
        public static string ShareMusic(int id)
        {
            return "[CQ:music,id" + id + "]";
        }


        /// <summary>
        /// 获取用于分享自定义音乐的代码。
        /// </summary>
        /// <param name="descriptionUri">描述自定义音乐的Url。</param>
        /// <param name="audioUri">音乐文件的Url</param>
        /// <param name="title">分享标题，建议12字以内。</param>
        /// <param name="content">分享内容，建议30字以内。</param>
        /// <param name="imageUri">分享链接的图片的Url。</param>
        /// <returns></returns>
        public static string ShareMusic(Uri descriptionUri, Uri audioUri, string title, string content, Uri imageUri)
        {
            if (descriptionUri == null || audioUri == null || imageUri == null)
            {
                return "";
            }
            return "[CQ:music,type=custom,url=" + descriptionUri.ToString() + ",audio=" + audioUri.ToString() +
                   ",title=" +
                   title + ",content=" + content + ",image=" + imageUri.ToString() + "]";
        }

        /// <summary>
        /// 获取用于分享本地音频文件的代码。
        /// </summary>
        /// <param name="recordFileName">音频文件的完整路径。</param>
        /// <returns></returns>
        public static string ShareRecord(string recordFileName)
        {
            return "[CQ:record,file=" + recordFileName + "]";
        }

        /// <summary>
        /// 获取戳了一下（即窗口抖动）的代码。
        /// </summary>
        /// <returns></returns>
        public static string Shake()
        {
            return "[CQ:shake]";
        }


        /// <summary>
        /// 获取分享自定义链接的代码。
        /// </summary>
        /// <param name="uri">自定义链接的Url。</param>
        /// <param name="title">分享内容标题，建议12字以内。</param>
        /// <param name="content">分享内容，建议30字以内。</param>
        /// <param name="imageUri">分享内容的附带图片。</param>
        /// <returns></returns>
        public static string ShareLink(Uri uri, string title, string content, Uri imageUri)
        {
            if (uri == null || imageUri == null)
            {
                return "";
            }
            return "[CQ:share,url=" + uri.ToString() + ",title=" + title + ",content=" + content + ",image=" +
                   imageUri.ToString() + "]";
        }
    }
}