namespace SNAS.Core.SoftLicenses
{
    /// <summary>
    /// 软件卡密状态
    /// </summary>
    public enum SoftLicenseStatus
    {

        /// <summary>
        /// 正常
        /// </summary>
        Normal=0,

        /// <summary>
        /// 已售出
        /// </summary>
        Sell = 1,
        
        /// <summary>
        /// 已使用
        /// </summary>
        HasUse = 2,

        /// <summary>
        /// 退卡
        /// </summary>
        Retuurn = 3,

        
    }
}
