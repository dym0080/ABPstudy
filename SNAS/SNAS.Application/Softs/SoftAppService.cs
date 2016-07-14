using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SNAS.Core.Softs;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using Abp.UI;
using SNAS.Application.Softs.Dto;
using SNAS.Application.Utils;
using SNAS.Core.Finances;
using SNAS.Core.SoftLicenses;
using SNAS.Core.SoftUsers;
using SNAS.Utils.Extension;
using SNAS.Utils.Helpers;

namespace SNAS.Application.Softs
{
    public class SoftAppService : SNASAppServiceBase, ISoftAppService
    {
        private readonly IRepository<Soft, long> _softRepository;
        private readonly IRepository<SoftLicenseOption, long> _softLicenseOptionRepository;
        private readonly IRepository<SoftMoreOpenOption, long> _softMoreOpenOptionRepository;
        private readonly IRepository<SoftRegisterOption, long> _softRegisterOptionRepository;
        private readonly IRepository<SoftBindOption, long> _softBindOptionRepository;
        private readonly IRepository<SoftLicense, long> _softLicenseRepository;
        private readonly IRepository<SoftUser, long> _softUserRepository;
        private readonly IRepository<Finance, long> _financeRepository;

        public SoftAppService(
            IRepository<Soft, long> softRepository,
            IRepository<SoftLicenseOption, long> softLicenseOptionRepository,
            IRepository<SoftMoreOpenOption, long> softMoreOpenOptionRepository,
            IRepository<SoftRegisterOption, long> softRegisterOptionRepository,
            IRepository<SoftBindOption, long> softBindOptionRepository,
            IRepository<SoftLicense, long> softLicenseRepository,
            IRepository<SoftUser, long> softUserRepository,
            IRepository<Finance, long> financeRepository
            )
        {
            _softRepository = softRepository;
            _softLicenseOptionRepository = softLicenseOptionRepository;
            _softMoreOpenOptionRepository = softMoreOpenOptionRepository;
            _softRegisterOptionRepository = softRegisterOptionRepository;
            _softBindOptionRepository = softBindOptionRepository;
            _softLicenseRepository = softLicenseRepository;
            _softUserRepository = softUserRepository;
            _financeRepository = financeRepository;
        }

        public virtual async Task Create(SoftCreateInput input)
        {
            var item = input.MapTo<Soft>();
            //自动生成appid,appsecret
            item.AppId = "ns" + MD5Helper.EnCode16Bit(DateTime.Now.ToString()).ToLower();
            item.AppSecret = MD5Helper.EnCode(RandExtension.GetRandomNum()).ToLower();
            item.IsActive = true;//默认启用
            var entity = await _softRepository.InsertAsync(item);
        }

        public virtual async Task Edit(SoftEditInput input)
        {
            var entity = await _softRepository.GetAsync(input.Id);
            entity.Name = input.Name;
            entity.BindMode = input.BindMode;
            entity.RunMode = input.RunMode;
            entity.Remark = input.Remark;
            entity.IsActive = input.IsActive;
            //entity = input.MapTo<Soft>();
            await _softRepository.UpdateAsync(entity);
        }

        public virtual async Task<AppPagedResultOutput<SoftListDto>> GetPageList(GetPageListInput input)
        {
            var query = _softRepository.GetAll();
            var where = FilterExpression.FindByGroup<Soft>(input.Filter);
            var count = await query.Where(where).CountAsync();
            var list = await query.Where(where)
                .OrderByDescending(t => t.CreationTime)
                .PageBy(input)
                .ToListAsync();

            var pageList = list.MapTo<List<SoftListDto>>();
            return new AppPagedResultOutput<SoftListDto>(count, pageList, input.CurrentPage, input.PageSize);
        }

        public virtual async Task<SoftItemDto> Get(long id)
        {
            var entity = await _softRepository.GetAsync(id);
            return entity.MapTo<SoftItemDto>();
        }

        public virtual async Task Delete(long id)
        {
            //检测是否有卡密数据，有的话，不允许删除
            if (await _softLicenseRepository.CountAsync(t => t.SoftId == id) > 0)
            {
                throw new UserFriendlyException("软件已经有卡密数据,不能删除!");
            }

            //删除软件相关数据
            await _softLicenseOptionRepository.DeleteAsync(t => t.SoftId == id);
            await _softMoreOpenOptionRepository.DeleteAsync(t => t.SoftId == id);
            await _softRegisterOptionRepository.DeleteAsync(t => t.SoftId == id);

            //删除软件
            await _softRepository.DeleteAsync(id);
        }

        public virtual async Task CreateLicenseOption(SoftLicenseOptionCreateInput input)
        {
            var item = input.MapTo<SoftLicenseOption>();
            //判断一个类型只能添加一次
            if (await _softLicenseOptionRepository.CountAsync(t => t.LicenseType == item.LicenseType && t.SoftId == item.SoftId) > 0)
            {
                throw new UserFriendlyException("该类型已经设置价格，不能重复设置!");
            }

            var entity = await _softLicenseOptionRepository.InsertAsync(item);
        }

        public virtual async Task EditLicenseOption(SoftLicenseOptionEditInput input)
        {
            var entity = await _softLicenseOptionRepository.GetAsync(input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("价格信息不存在!");
            }

            entity.LicenseType = input.LicenseType;
            entity.Price = input.Price;
            //判断一个类型只能添加一次
            if (await _softLicenseOptionRepository.CountAsync(t => t.LicenseType == input.LicenseType && t.SoftId == entity.SoftId && t.Id != entity.Id) > 0)
            {
                throw new UserFriendlyException("该类型已经设置价格，不能重复设置!");
            }
            await _softLicenseOptionRepository.UpdateAsync(entity);
        }

        public virtual async Task<ListResultDto<SoftLicenseOptionListDto>> GetLicenseOptionPageList(long softId)
        {
            var query = _softLicenseOptionRepository.GetAll();
            var list = await query.Where(t => t.SoftId == softId)
                .OrderBy(t => t.LicenseType)
                .ToListAsync();
            var pageList = list.MapTo<List<SoftLicenseOptionListDto>>();
            return new ListResultDto<SoftLicenseOptionListDto>(pageList);
        }

        public virtual async Task<SoftLicenseOptionItemDto> GetLicenseOption(long id)
        {
            var entity = await _softLicenseOptionRepository.GetAsync(id);
            return entity.MapTo<SoftLicenseOptionItemDto>();

        }

        public virtual async Task DeleteLicenseOption(long id)
        {
            await _softLicenseOptionRepository.DeleteAsync(id);
        }

        public virtual async Task<SoftMoreOpenOptionItemDto> GetMoreOpenOption(long softId)
        {
            var entity = await _softMoreOpenOptionRepository.FirstOrDefaultAsync(t => t.SoftId == softId);
            return entity.MapTo<SoftMoreOpenOptionItemDto>();
        }

        public virtual async Task SaveMoreOpenOption(SoftMoreOpenOptionItemDto input)
        {
            var item = input.MapTo<SoftMoreOpenOption>();
            if (item.VerifyCycle < 10)
            {
                throw new UserFriendlyException("校验间隔不能少于10秒!");
            }
            if (await _softMoreOpenOptionRepository.CountAsync(t => t.SoftId == input.SoftId) > 0)
            {
                await _softMoreOpenOptionRepository.UpdateAsync(item);
            }
            else
            {
                await _softMoreOpenOptionRepository.InsertAsync(item);
            }
        }

        public virtual async Task<SoftRegisterOptionItemDto> GetRegisterOption(long softId)
        {
            var entity = await _softRegisterOptionRepository.FirstOrDefaultAsync(t => t.SoftId == softId);
            return entity.MapTo<SoftRegisterOptionItemDto>();
        }

        public virtual async Task SaveRegisterOption(SoftRegisterOptionItemDto input)
        {
            var item = input.MapTo<SoftRegisterOption>();

            if (await _softRegisterOptionRepository.CountAsync(t => t.SoftId == input.SoftId) > 0)
            {
                await _softRegisterOptionRepository.UpdateAsync(item);
            }
            else
            {
                await _softRegisterOptionRepository.InsertAsync(item);
            }
        }

        public virtual async Task<SoftBindOptionItemDto> GetBindOption(long softId)
        {
            var entity = await _softBindOptionRepository.FirstOrDefaultAsync(t => t.SoftId == softId);
            return entity.MapTo<SoftBindOptionItemDto>();
        }

        public virtual async Task SaveBindOption(SoftBindOptionItemDto input)
        {
            var item = input.MapTo<SoftBindOption>();

            if (await _softBindOptionRepository.CountAsync(t => t.SoftId == input.SoftId) > 0)
            {
                await _softBindOptionRepository.UpdateAsync(item);
            }
            else
            {
                await _softBindOptionRepository.InsertAsync(item);
            }
        }

        public virtual async Task<ListResultDto<ComboboxItemDto>> GetLicenseOptionComboList(long softId)
        {
            var query = _softLicenseOptionRepository.GetAll();
            var list = await query.Where(t => t.SoftId == softId)
                .OrderBy(t => t.LicenseType)
                .ToListAsync();
            var pageList = list.MapTo<List<ComboboxItemDto>>();
            return new ListResultDto<ComboboxItemDto>(pageList);
        }

        public virtual async Task GenerateLicenses(SoftLicenseGenerateItemInput input)
        {
            if (input.Count < 1)
            {
                throw new UserFriendlyException("生成卡密数量不能小于1!");
            }

            var licenseOption = await _softLicenseOptionRepository.FirstOrDefaultAsync(t => t.Id == input.SoftLicenseOptionId && t.SoftId == input.SoftId);
            if (licenseOption == null)
            {
                throw new UserFriendlyException("价格类型不存在!");
            }

            for (int i = 0; i < input.Count; i++)
            {
                SoftLicense license = new SoftLicense();
                license.SoftId = licenseOption.SoftId;
                license.LicenseType = licenseOption.LicenseType;
                license.LicenseNo = MD5Helper.EnCode16Bit(Guid.NewGuid().ToString());
                license.ApplyTime = DateTime.Now;
                license.Status = SoftLicenseStatus.Normal;
                license.SellType = SoftLicenseSellType.None;
                license.Price = licenseOption.Price;

                await _softLicenseRepository.InsertAsync(license);
            }
        }

        public virtual async Task<AppPagedResultOutput<SoftLicenseListDto>> GetLicensePageList(long softId, GetPageListInput input)
        {

            var query = _softLicenseRepository.GetAll();
            var where = FilterExpression.FindByGroup<SoftLicense>(input.Filter);
            where = where.And(t => t.SoftId == softId);


            var queryCount = query.Where(where)
                    .GroupJoin(_softUserRepository.GetAll(), softLicense => softLicense.SoftUserId, softUser => softUser.Id,
                    (softLicense, softUser) => new
                    {
                        SoftLicense = softLicense,
                        SoftUser = softUser
                    });
            if (FilterExpression.HasValue(input.Filter, "loginName"))
            {
                string value = FilterExpression.GetValue(input.Filter, "loginName");
                queryCount = queryCount.Where(t => t.SoftUser.FirstOrDefault().LoginName.Contains(value));
            }

            int count = await queryCount.CountAsync();


            var queryList = query.Where(where)
                .GroupJoin(_softUserRepository.GetAll(), softLicense => softLicense.SoftUserId, softUser => softUser.Id,
                    (softLicense, softUser) => new
                    {
                        SoftLicense = softLicense,
                        SoftUser = softUser
                    });


            //queryList = queryList.Where(t => t.SoftUser.FirstOrDefault().LoginName.Contains("001"));
            if (FilterExpression.HasValue(input.Filter, "loginName"))
            {
                string value = FilterExpression.GetValue(input.Filter, "loginName");
                queryList = queryList.Where(t => t.SoftUser.FirstOrDefault().LoginName.Contains(value));
            }


            queryList = queryList.OrderByDescending(t => t.SoftLicense.CreationTime)
                .PageBy(input);
            var list = await queryList.Select(t => new SoftLicenseListDto
            {
                Id = t.SoftLicense.Id,
                ApplyTime = t.SoftLicense.ApplyTime,
                CreationTime = t.SoftLicense.CreationTime,
                LicenseNo = t.SoftLicense.LicenseNo,
                LicenseType = t.SoftLicense.LicenseType,
                Price = t.SoftLicense.Price,
                Status = t.SoftLicense.Status,
                UseTime = t.SoftLicense.UseTime,
                SoftUser_LoginName = t.SoftUser.FirstOrDefault().LoginName
            }).ToListAsync();
            var pageList = list;
            return new AppPagedResultOutput<SoftLicenseListDto>(count, pageList, input.CurrentPage, input.PageSize);
        }

        public virtual async Task DeleteLicense(long id)
        {
            var item = await _softLicenseRepository.FirstOrDefaultAsync(t => t.Id == id);
            if (item == null)
            {
                throw new UserFriendlyException("卡密数据不存在!");
            }
            if (item.Status != SoftLicenseStatus.Normal)
            {
                throw new UserFriendlyException("卡密已售出/已使用不能删除!");
            }
            await _softLicenseRepository.DeleteAsync(item.Id);
        }

        public virtual async Task SellLicense(long id)
        {
            var item = await _softLicenseRepository.FirstOrDefaultAsync(t => t.Id == id);
            if (item == null)
            {
                throw new UserFriendlyException("卡密数据不存在!");
            }
            if (item.Status != SoftLicenseStatus.Normal)
            {
                throw new UserFriendlyException("卡密当前状态不能出售!");
            }
            item.SellType = SoftLicenseSellType.Manually;
            item.Status = SoftLicenseStatus.Sell;
            item.SellerTime = DateTime.Now;

            await _softLicenseRepository.UpdateAsync(item);

            //写入财务记录
            Finance finance = new Finance();
            finance.Type = FinanceType.InCome;
            finance.Money = item.Price;
            finance.Remark = string.Format("出售卡密:{0}", item.LicenseNo);

            await _financeRepository.InsertAsync(finance);

        }

        public virtual async Task ReturnLicense(long id)
        {
            var item = await _softLicenseRepository.FirstOrDefaultAsync(t => t.Id == id);
            if (item == null)
            {
                throw new UserFriendlyException("卡密数据不存在!");
            }
            if (item.Status != SoftLicenseStatus.Sell && item.Status != SoftLicenseStatus.HasUse)
            {
                throw new UserFriendlyException("卡密当前状态不能退货!");
            }
            item.SellType = SoftLicenseSellType.Manually;
            item.Status = SoftLicenseStatus.Retuurn;

            await _softLicenseRepository.UpdateAsync(item);

            //写入财务记录
            Finance finance = new Finance();
            finance.Type = FinanceType.Expenses;
            finance.Money = item.Price;
            finance.Remark = string.Format("退货卡密:{0}", item.LicenseNo);

            await _financeRepository.InsertAsync(finance);
        }
    }


}
