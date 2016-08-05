using System.Data.Common;
using Abp.Zero.EntityFramework;
using SNAS.Core.Authorization.Roles;
using SNAS.Core.MultiTenancy;
using SNAS.Core.Users;
using System.Data.Entity;
using SNAS.Core.SoftLicenses;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;
using SNAS.Core.Finances;
using SNAS.Core.Dictionarys;

namespace SNAS.EntityFramework
{
    public class SNASDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...   

        /// <summary>
        /// 软件
        /// </summary>
        public virtual IDbSet<Soft> Softs { get; set; }

        /// <summary>
        /// 字典
        /// </summary>
        public virtual IDbSet<Dictionary> Dictionarys { get; set; }

        /// <summary>
        /// 软件注册配置
        /// </summary>
        public virtual IDbSet<SoftRegisterOption> SoftRegisterOptions { get; set; }

        /// <summary>
        /// 软件绑定配置
        /// </summary>
        public virtual IDbSet<SoftBindOption> SoftBindOptions { get; set; }

        /// <summary>
        /// 软件多开配置
        /// </summary>
        public virtual IDbSet<SoftMoreOpenOption> SoftMoreOpenOptions { get; set; }

        /// <summary>
        /// 软件卡密配置
        /// </summary>
        public virtual IDbSet<SoftLicenseOption> SoftLicenseOptions { get; set; }

        /// <summary>
        /// 软件用户
        /// </summary>
        public virtual IDbSet<SoftUser> SoftUser { get; set; }

        /// <summary>
        /// 用户登录记录
        /// </summary>
        public virtual IDbSet<SoftUserLogin> SoftUserLogins { get; set; }

        /// <summary>
        /// 卡密
        /// </summary>
        public virtual IDbSet<SoftLicense> SoftLicenses { get; set; }

        /// <summary>
        /// 卡密绑定机器码
        /// </summary>
        public virtual IDbSet<SoftUserLicenseMcode> SoftUserLicenseMcodes { get; set; }

        /// <summary>
        /// 财务记录
        /// </summary>
        public virtual IDbSet<Finance> Finances { get; set; }

        /// <summary>
        /// 用户软件授权记录
        /// </summary>
        public virtual IDbSet<SoftUserLicense> SoftUserLicenses { get; set; }

        /// <summary>
        /// 机器码换绑记录
        /// </summary>
        public virtual IDbSet<MCodeChangeLog> MCodeChangeLogs { get; set; }

        /// <summary>
        /// 软件在线记录
        /// </summary>
        public virtual IDbSet<SoftOnlineLog> SoftOnlineLogs { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public SNASDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in SNASDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of SNASDbContext since ABP automatically handles it.
         */
        public SNASDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public SNASDbContext(DbConnection connection)
            : base(connection, true)
        {

        }



    }
}
