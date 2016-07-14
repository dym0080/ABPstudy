using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNet.Identity;
using SNAS.Application.Softs.Dto;
using SNAS.Application.SoftUsers.Dto;
using SNAS.Application.Utils;
using SNAS.Core.SoftAuthorize;
using SNAS.Core.SoftLicenses;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;
using SNAS.Utils.Extension;
using SNAS.Utils.Helpers;

namespace SNAS.Application.SoftUsers
{
    public class SoftUserAppService : SNASAppServiceBase, ISoftUserAppService
    {
        private readonly IRepository<Soft, long> _softRepository;
        private readonly IRepository<SoftUser, long> _softUserRepository;
        private readonly IRepository<SoftLicense, long> _softLicenseRepository;
        private readonly IRepository<SoftUserLogin, long> _softUserLoginRepository;
        private readonly IRepository<MCodeChangeLog, long> _mCodeChangeLogRepository;
        private readonly IRepository<SoftBindOption, long> _softBindOptionRepository;
        private readonly IRepository<SoftUserLicense, long> _softUserLicenseRepository;
        private readonly IRepository<SoftUserLicenseMcode, long> _softUserLicenseMcodeRepository;


        private readonly SoftAuthorizeManager _softAuthorizeManager;

        public SoftUserAppService(
            IRepository<Soft, long> softRepository,
            IRepository<SoftUser, long> softUserRepository,
            IRepository<SoftLicense, long> softLicenseRepository,
            IRepository<SoftUserLogin, long> softUserLoginRepository,
             IRepository<MCodeChangeLog, long> mCodeChangeLogRepository,
            IRepository<SoftBindOption, long> softBindOptionRepository,
            IRepository<SoftUserLicense, long> softUserLicenseRepository,
            IRepository<SoftUserLicenseMcode, long> softUserLicenseMcodeRepository,

        SoftAuthorizeManager softAuthorizeManager)
        {
            _softRepository = softRepository;
            _softUserRepository = softUserRepository;
            _softLicenseRepository = softLicenseRepository;
            _softUserLoginRepository = softUserLoginRepository;
            _mCodeChangeLogRepository = mCodeChangeLogRepository;
            _softAuthorizeManager = softAuthorizeManager;
            _softUserLicenseRepository = softUserLicenseRepository;
            _softUserLicenseMcodeRepository = softUserLicenseMcodeRepository;
            _softBindOptionRepository = softBindOptionRepository;

        }

        public virtual async Task Create(SoftUserCreateInput input)
        {
            if (await _softUserRepository.CountAsync(t => t.LoginName == input.LoginName) > 0)
            {
                throw new UserFriendlyException("登录名称已存在!");
            }

            var item = input.MapTo<SoftUser>();
            item.IsActive = true;//默认启用
            item.Source = SoftUserSource.System;
            item.Password = new PasswordHasher().HashPassword(input.Password);
            var entity = await _softUserRepository.InsertAsync(item);
        }

        public virtual async Task Edit(SoftUserEditInput input)
        {
            if (await _softUserRepository.CountAsync(t => t.LoginName == input.LoginName && t.Id != input.Id) > 0)
            {
                throw new UserFriendlyException("登录名称已存在!");
            }

            var entity = await _softUserRepository.GetAsync(input.Id);
            entity.LoginName = input.LoginName;
            entity.QQ = input.QQ;
            entity.Mobile = input.Mobile;
            entity.Remark = input.Remark;
            entity.IsActive = input.IsActive;
            await _softUserRepository.UpdateAsync(entity);
        }

        public virtual async Task<AppPagedResultOutput<SoftUserListDto>> GetPageList(GetPageListInput input)
        {
            var query = _softUserRepository.GetAll();
            var where = FilterExpression.FindByGroup<SoftUser>(input.Filter);
            var count = await query.Where(where).CountAsync();
            var list = await query.Where(where)
                .OrderByDescending(t => t.CreationTime)
                .PageBy(input)
                .ToListAsync();

            var pageList = list.MapTo<List<SoftUserListDto>>();
            return new AppPagedResultOutput<SoftUserListDto>(count, pageList, input.CurrentPage, input.PageSize);
        }

        public virtual async Task<SoftUserItemDto> Get(long id)
        {
            var entity = await _softUserRepository.GetAsync(id);
            return entity.MapTo<SoftUserItemDto>();
        }

        public virtual async Task Delete(long id)
        {
            //用户已经授权不能删除
            if (await _softLicenseRepository.CountAsync(t => t.SoftUserId == id) > 0)
            {
                throw new UserFriendlyException("用户已经授权不能删除!");
            }

            //用户有登录数据不能删除
            if (await _softUserLoginRepository.CountAsync(t => t.SoftUserId == id) > 0)
            {
                throw new UserFriendlyException("用户有登录数据不能删除!");
            }

            await _softUserRepository.DeleteAsync(id);
        }

        public virtual async Task UseLicense(SoftUserUseLicenseInput input)
        {
            var licenseInfo = await _softLicenseRepository.FirstOrDefaultAsync(t => t.LicenseNo == input.LicenseNo);
            var userInfo = await _softUserRepository.GetAsync(input.SoftUserId);
            await _softAuthorizeManager.Authorize(licenseInfo, userInfo);
        }

        public virtual async Task ChangePassword(ChangePasswordDto dto)
        {
            if (dto.NewPassword.Length < 6)
            {
                throw new UserFriendlyException("密码不能为空!");
            }

            if (!dto.NewPassword.Equals(dto.NewPassword2))
            {
                throw new UserFriendlyException("2次输入的密码不一致!");
            }

            var userInfo = await _softUserRepository.GetAsync(dto.SoftUserId);
            userInfo.Password = new PasswordHasher().HashPassword(dto.NewPassword);

            await _softUserRepository.UpdateAsync(userInfo);
        }

        public virtual async Task<AppPagedResultOutput<SoftUserLicenseListDto>> GetLicensePageList(GetPageListInput input)
        {
            var query = _softUserLicenseRepository.GetAll();
            var where = FilterExpression.FindByGroup<SoftUserLicense>(input.Filter);


            var queryCount = query.Where(where)
                .GroupJoin(_softUserRepository.GetAll(), left => left.SoftUserId, right => right.Id,
                    (left, right) => new
                    {
                        UserLicense = left,
                        SoftUser = right
                    })
                .GroupJoin(_softRepository.GetAll(), left => left.UserLicense.SoftId, right => right.Id,
                    (left, right) => new
                    {
                        SoftUserLicense = left.UserLicense,
                        SoftUser = left.SoftUser,
                        Soft = right
                    });

            if (FilterExpression.HasValue(input.Filter, "loginName"))
            {
                string value = FilterExpression.GetValue(input.Filter, "loginName");
                queryCount = queryCount.Where(t => t.SoftUser.FirstOrDefault().LoginName.Contains(value));
            }

            if (FilterExpression.HasValue(input.Filter, "softName"))
            {
                string value = FilterExpression.GetValue(input.Filter, "softName");
                queryCount = queryCount.Where(t => t.Soft.FirstOrDefault().Name.Contains(value));
            }

            int count = await queryCount.CountAsync();


            var queryList = query.Where(where)
                 .GroupJoin(_softUserRepository.GetAll(), left => left.SoftUserId, right => right.Id,
                    (left, right) => new
                    {
                        UserLicense = left,
                        SoftUser = right
                    })
                .GroupJoin(_softRepository.GetAll(), left => left.UserLicense.SoftId, right => right.Id,
                    (left, right) => new
                    {
                        SoftUserLicense = left.UserLicense,
                        SoftUser = left.SoftUser,
                        Soft = right
                    });

            if (FilterExpression.HasValue(input.Filter, "loginName"))
            {
                string value = FilterExpression.GetValue(input.Filter, "loginName");
                queryList = queryList.Where(t => t.SoftUser.FirstOrDefault().LoginName.Contains(value));
            }

            if (FilterExpression.HasValue(input.Filter, "softName"))
            {
                string value = FilterExpression.GetValue(input.Filter, "softName");
                queryList = queryList.Where(t => t.Soft.FirstOrDefault().Name.Contains(value));
            }


            queryList = queryList.OrderByDescending(t => t.SoftUserLicense.CreationTime)
                .PageBy(input);
            var list = await queryList.Select(t => new SoftUserLicenseListDto
            {
                Id = t.SoftUserLicense.Id,
                AuthorizeTime = t.SoftUserLicense.AuthorizeTime,
                ExpireTime = t.SoftUserLicense.ExpireTime,
                IsActive = t.SoftUserLicense.IsActive,
                SoftName = t.Soft.FirstOrDefault().Name,
                Type = t.SoftUserLicense.Type,
                LoginName = t.SoftUser.FirstOrDefault().LoginName
            }).ToListAsync();
            var pageList = list;
            return new AppPagedResultOutput<SoftUserLicenseListDto>(count, pageList, input.CurrentPage, input.PageSize);
        }

        public virtual async Task EnableLicense(long id)
        {
            var info = await _softUserLicenseRepository.GetAsync(id);
            if (info == null)
            {
                throw new UserFriendlyException("数据不存在!");
            }
            info.IsActive = true;
            await _softUserLicenseRepository.UpdateAsync(info);
        }

        public virtual async Task DisableLicense(long id)
        {
            var info = await _softUserLicenseRepository.GetAsync(id);
            if (info == null)
            {
                throw new UserFriendlyException("数据不存在!");
            }
            info.IsActive = false;
            await _softUserLicenseRepository.UpdateAsync(info);
        }

        public virtual async Task<AppPagedResultOutput<SoftUserLicenseMcodeListDto>> GetLicenseMcodePageList(long softUserLicenseId, GetPageListInput input)
        {
            var query = _softUserLicenseMcodeRepository.GetAll();
            var where = FilterExpression.FindByGroup<SoftUserLicenseMcode>(input.Filter);
            where = where.And(t => t.SoftUserLicenseId == softUserLicenseId);
            var count = await query.Where(where).CountAsync();
            var list = await query.Where(where)
                .OrderByDescending(t => t.CreationTime)
                .PageBy(input)
                .ToListAsync();
            var pageList = list.MapTo<List<SoftUserLicenseMcodeListDto>>();
            return new AppPagedResultOutput<SoftUserLicenseMcodeListDto>(count, pageList, input.CurrentPage, input.PageSize);
        }

        public virtual async Task EnableLicenseMcode(long id)
        {
            var info = await _softUserLicenseMcodeRepository.GetAsync(id);
            if (info == null)
            {
                throw new UserFriendlyException("数据不存在!");
            }
            info.IsActive = true;
            await _softUserLicenseMcodeRepository.UpdateAsync(info);
        }

        public virtual async Task DisableLicenseMcode(long id)
        {
            var info = await _softUserLicenseMcodeRepository.GetAsync(id);
            if (info == null)
            {
                throw new UserFriendlyException("数据不存在!");
            }
            info.IsActive = false;
            await _softUserLicenseMcodeRepository.UpdateAsync(info);
        }

        public virtual async Task DeleteLicenseMcode(long id)
        {
            await _softUserLicenseMcodeRepository.DeleteAsync(id);
        }

        public virtual async Task ChangeMcode(MCodeChangeLogCreateInput input)
        {
            if (input.NewMCode.Length != 32)
            {
                throw new UserFriendlyException("新机器码格式不正确(32位)!");
            }
            var softUserLicenseMcode = await _softUserLicenseMcodeRepository.GetAsync(input.SoftUserLicenseMcodeId);
            if (softUserLicenseMcode == null)
            {
                throw new UserFriendlyException("旧机器码数据不存在!");
            }
            if (!softUserLicenseMcode.IsActive)
            {
                throw new UserFriendlyException("旧机器码被封禁，不能换绑!");
            }

            if (softUserLicenseMcode.Mcode == input.NewMCode)
            {
                throw new UserFriendlyException("新旧机器码是一样的，不需要换绑!");
            }

            if (await _softUserLicenseMcodeRepository.CountAsync(t => t.SoftUserLicenseId == softUserLicenseMcode.SoftUserLicenseId && t.Mcode == input.NewMCode) > 0)
            {
                throw new UserFriendlyException("新旧机器码已存在，可以正常使用，不需要换绑!");
            }

            long softId = softUserLicenseMcode.SoftUserLicense.SoftId;

            var softBindOption = await _softBindOptionRepository.FirstOrDefaultAsync(t => t.SoftId == softId);
            if (softBindOption != null)
            {
                if (await _mCodeChangeLogRepository.CountAsync(t => t.SoftUserLicenseId == softUserLicenseMcode.SoftUserLicenseId) >=
                    softBindOption.AllowChangeBindCount)
                {
                    throw new UserFriendlyException("换绑次数已经超过限制!");
                }
            }

            MCodeChangeLog mCodeChangeLog = new MCodeChangeLog();
            mCodeChangeLog.SoftUserLicenseId = softUserLicenseMcode.SoftUserLicenseId;
            mCodeChangeLog.SoftUserId = softUserLicenseMcode.SoftUserLicense.SoftUserId;
            mCodeChangeLog.SoftId = softUserLicenseMcode.SoftUserLicense.SoftId;
            mCodeChangeLog.OldMCode = softUserLicenseMcode.Mcode;
            mCodeChangeLog.NewMCode = input.NewMCode;
            mCodeChangeLog.Source = MCodeChangeSource.System;
            await _mCodeChangeLogRepository.InsertAsync(mCodeChangeLog);

            softUserLicenseMcode.Mcode = input.NewMCode;
            await _softUserLicenseMcodeRepository.UpdateAsync(softUserLicenseMcode);


        }

        public virtual async Task<AppPagedResultOutput<MCodeChangeLogListDto>> GetMCodeChangeLogPageList(long softUserLicenseId, GetPageListInput input)
        {
            var query = _mCodeChangeLogRepository.GetAll();
            var where = FilterExpression.FindByGroup<MCodeChangeLog>(input.Filter);
            where = where.And(t => t.SoftUserLicenseId == softUserLicenseId);
            var count = await query.Where(where).CountAsync();
            var list = await query.Where(where)
                .OrderByDescending(t => t.CreationTime)
                .PageBy(input)
                .ToListAsync();
            var pageList = list.MapTo<List<MCodeChangeLogListDto>>();
            return new AppPagedResultOutput<MCodeChangeLogListDto>(count, pageList, input.CurrentPage, input.PageSize);
        }

        public virtual async Task<AppPagedResultOutput<SoftUserLoginListDto>> GetLoginPageList(GetPageListInput input)
        {
            var query = _softUserLoginRepository.GetAll();
            var where = FilterExpression.FindByGroup<SoftUserLogin>(input.Filter);


            var queryCount = query.Where(where)
                .GroupJoin(_softUserRepository.GetAll(), left => left.SoftUserId, right => right.Id,
                    (left, right) => new
                    {
                        UserLicense = left,
                        SoftUser = right
                    })
                .GroupJoin(_softRepository.GetAll(), left => left.UserLicense.SoftId, right => right.Id,
                    (left, right) => new
                    {
                        SoftUserLicense = left.UserLicense,
                        SoftUser = left.SoftUser,
                        Soft = right
                    });

            if (FilterExpression.HasValue(input.Filter, "loginName"))
            {
                string value = FilterExpression.GetValue(input.Filter, "loginName");
                queryCount = queryCount.Where(t => t.SoftUser.FirstOrDefault().LoginName.Contains(value));
            }

            if (FilterExpression.HasValue(input.Filter, "softName"))
            {
                string value = FilterExpression.GetValue(input.Filter, "softName");
                queryCount = queryCount.Where(t => t.Soft.FirstOrDefault().Name.Contains(value));
            }

            int count = await queryCount.CountAsync();


            var queryList = query.Where(where)
                 .GroupJoin(_softUserRepository.GetAll(), left => left.SoftUserId, right => right.Id,
                    (left, right) => new
                    {
                        SoftUserLogin = left,
                        SoftUser = right
                    })
                .GroupJoin(_softRepository.GetAll(), left => left.SoftUserLogin.SoftId, right => right.Id,
                    (left, right) => new
                    {
                        SoftUserLogin = left.SoftUserLogin,
                        SoftUser = left.SoftUser,
                        Soft = right
                    });

            if (FilterExpression.HasValue(input.Filter, "loginName"))
            {
                string value = FilterExpression.GetValue(input.Filter, "loginName");
                queryList = queryList.Where(t => t.SoftUser.FirstOrDefault().LoginName.Contains(value));
            }

            if (FilterExpression.HasValue(input.Filter, "softName"))
            {
                string value = FilterExpression.GetValue(input.Filter, "softName");
                queryList = queryList.Where(t => t.Soft.FirstOrDefault().Name.Contains(value));
            }


            queryList = queryList.OrderByDescending(t => t.SoftUserLogin.CreationTime)
                .PageBy(input);
            var list = await queryList.Select(t => new SoftUserLoginListDto
            {
                Id = t.SoftUserLogin.Id,
                Mcode = t.SoftUserLogin.Mcode,
                Ip = t.SoftUserLogin.Ip,
                CreationTime = t.SoftUserLogin.CreationTime,
                SoftName = t.Soft.FirstOrDefault().Name,
                LoginName = t.SoftUser.FirstOrDefault().LoginName
            }).ToListAsync();
            var pageList = list;
            return new AppPagedResultOutput<SoftUserLoginListDto>(count, pageList, input.CurrentPage, input.PageSize);
        }
    }
}
