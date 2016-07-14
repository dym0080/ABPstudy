﻿using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Abp.Collections;
using Abp.Modules;
using Abp.TestBase;
using Abp.Zero.SampleApp.MultiTenancy;
using Abp.Zero.SampleApp.NHibernate.TestDatas;
using Castle.MicroKernel.Registration;
using NHibernate;
using NHibernate.Linq;

namespace Abp.Zero.SampleApp.NHibernate
{
    public abstract class NHibernateTestBase : AbpIntegratedTestBase
    {
        private SQLiteConnection _connection;

        protected NHibernateTestBase()
        {
            UsingSession(session => new InitialTestDataBuilder(session).Build());            
        }

        protected override void PreInitialize()
        {
            _connection = new SQLiteConnection("data source=:memory:");
            _connection.Open();

            LocalIocManager.IocContainer.Register(
                Component.For<IDbConnection>().UsingFactoryMethod(() => _connection).LifestyleSingleton()
                );
        }

        protected override void AddModules(ITypeList<AbpModule> modules)
        {
            base.AddModules(modules);
            modules.Add<SampleAppNHibernateModule>();
        }

        public void UsingSession(Action<ISession> action)
        {
            using (var session = LocalIocManager.Resolve<ISessionFactory>().OpenSession(_connection))
            {
                using (var transaction = session.BeginTransaction())
                {
                    action(session);
                    session.Flush();
                    transaction.Commit();
                }
            }
        }

        public T UsingSession<T>(Func<ISession, T> func)
        {
            T result;

            using (var session = LocalIocManager.Resolve<ISessionFactory>().OpenSession(_connection))
            {
                using (var transaction = session.BeginTransaction())
                {
                    result = func(session);
                    session.Flush();
                    transaction.Commit();
                }
            }

            return result;
        }

        protected Tenant GetDefaultTenant()
        {
            return UsingSession(
                session =>
                {
                    return session.Query<Tenant>().Single(t => t.TenancyName == Tenant.DefaultTenantName);
                });
        }

        public override void Dispose()
        {
            _connection.Dispose();
            base.Dispose();
        }
    }
}