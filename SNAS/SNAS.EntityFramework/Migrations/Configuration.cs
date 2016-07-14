using System.Data.Entity.Migrations;
using SNAS.Migrations.SeedData;
using System.Data.Entity.Migrations.History;
using System.Data.Entity;
using System.Data.Common;

namespace SNAS.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SNAS.EntityFramework.SNASDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SNAS";
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());//设置Sql生成器为Mysql的            
        }

        protected override void Seed(SNAS.EntityFramework.SNASDbContext context)
        {
            new InitialDataBuilder(context).Build();
        }
        
    }


    /// <summary>
    /// 数据库迁移mysql补丁
    /// </summary>
    public class MySqlHistoryContext : HistoryContext
    {
        public MySqlHistoryContext(
          DbConnection existingConnection,
          string defaultSchema)
        : base(existingConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().Property(h => h.MigrationId).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<HistoryRow>().Property(h => h.ContextKey).HasMaxLength(200).IsRequired();
        }
    }

    public class MySqlConfiguration : DbConfiguration
    {
        public MySqlConfiguration()
        {
            SetHistoryContext("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }
    }    
}
