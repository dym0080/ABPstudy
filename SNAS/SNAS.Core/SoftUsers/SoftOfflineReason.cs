namespace SNAS.Core.SoftUsers
{
    /// <summary>
    /// 软件用户下线原因
    /// </summary>
    public enum SoftOfflineReason
    {

        /// <summary>
        /// 未登出
        /// </summary>
        None=0,

        /// <summary>
        /// 用户登出
        /// </summary>
        Logout =1,

        /// <summary>
        /// 多开超出自动踢掉
        /// </summary>
        KicksOff = 2,

        /// <summary>
        /// 后台强制踢掉
        /// </summary>
        Kill = 3

    }
}
