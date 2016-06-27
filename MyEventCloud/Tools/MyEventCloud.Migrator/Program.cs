﻿using System;
using Abp;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Castle.Core.Logging;
using Castle.Facilities.Logging;

namespace MyEventCloud.Migrator
{
    public class Program
    {
        private static bool _skipConnVerification = false;

        public static void Main(string[] args)
        {
            ParseArgs(args);

            using (var bootstrapper = new AbpBootstrapper())
            {
                bootstrapper.IocManager.IocContainer
                    .AddFacility<LoggingFacility>(f => f.UseLog4Net()
                        .WithConfig("log4net.config")
                    );

                bootstrapper.Initialize();

                using (var migrateExecuter = bootstrapper.IocManager.ResolveAsDisposable<MultiTenantMigrateExecuter>())
                {
                    migrateExecuter.Object.Run(_skipConnVerification);
                }

                Console.WriteLine("Press ENTER to exit...");
                Console.ReadLine();
            }
        }

        private static void ParseArgs(string[] args)
        {
            if (args.IsNullOrEmpty())
            {
                return;
            }

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                switch (arg)
                {
                    case "-s":
                        _skipConnVerification = true;
                        break;
                }
            }
        }
    }
}
