using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.AutoMapper
{
    public static class AutoMapperWebConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<SoftProfile>();
                //cfg.AddProfile<SoftUserProfile>();
            });
            Mapper.AssertConfigurationIsValid();//验证所有的映射配置是否都正常
        }
    }
}
