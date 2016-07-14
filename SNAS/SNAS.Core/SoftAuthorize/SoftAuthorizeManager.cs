using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.UI;
using Castle.MicroKernel.SubSystems.Conversion;
using Microsoft.AspNet.Identity;
using SNAS.Core.Finances;
using SNAS.Core.SoftLicenses;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;

namespace SNAS.Core.SoftAuthorize
{
    public class SoftAuthorizeManager : IDomainService
    {
        private readonly IRepository<Soft, long> _softRepository;
        private readonly IRepository<SoftLicense, long> _softLicenseRepository;
        private readonly IRepository<SoftUserLicense, long> _softUserLicenseRepository;
        private readonly IRepository<Finance, long> _financeRepository;
        private readonly IRepository<SoftUser, long> _softUserRepository;
        private readonly IRepository<SoftUserLicenseMcode, long> _softUserLicenseMcodeRepository;
        private readonly IRepository<SoftUserLogin, long> _softUserLoginRepository;
        private readonly IRepository<SoftOnlineLog, long> _softOnlineRepository;
        private readonly IRepository<SoftBindOption, long> _softBindOptionRepository;
        private readonly IRepository<MCodeChangeLog, long> _mCodeChangeLogRepository;
        private readonly ICacheManager _cacheManager;

        public SoftAuthorizeManager(
            IRepository<Soft, long> softRepository,
            IRepository<SoftLicense, long> softLicenseRepository,
            IRepository<SoftUserLicense, long> softUserLicenseRepository,
            IRepository<SoftUser, long> softUserRepository,
            IRepository<Finance, long> financeRepository,
            IRepository<SoftUserLicenseMcode, long> softUserLicenseMcodeRepository,
            IRepository<SoftUserLogin, long> softUserLoginRepository,
            IRepository<SoftOnlineLog, long> softOnlineRepository,
            IRepository<SoftBindOption, long> softBindOptionRepository,
            IRepository<MCodeChangeLog, long> mCodeChangeLogRepository,
            ICacheManager cacheManager

            )
        {
            _softRepository = softRepository;
            _softLicenseRepository = softLicenseRepository;
            _softUserLicenseRepository = softUserLicenseRepository;
            _financeRepository = financeRepository;
            _softUserRepository = softUserRepository;
            _softUserLicenseMcodeRepository = softUserLicenseMcodeRepository;
            _softUserLoginRepository = softUserLoginRepository;
            _softOnlineRepository = softOnlineRepository;
            _softBindOptionRepository = softBindOptionRepository;
            _mCodeChangeLogRepository = mCodeChangeLogRepository;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 将卡密授权给用户
        /// </summary>
        /// <param name="softLicense"></param>
        /// <param name="softUser"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task Authorize(SoftLicense softLicense, SoftUser softUser)
        {
            #region 检查参数是否有效

            if (softLicense == null)
            {
                throw new UserFriendlyException("卡密数据不存在!");
            }
            if (softUser == null)
            {
                throw new UserFriendlyException("用户不存在!");
            }
            if (softLicense.Status != SoftLicenseStatus.Sell)
            {
                throw new UserFriendlyException("卡密不存在或已被使用!");
            }
            if (!softUser.IsActive)
            {
                throw new UserFriendlyException("用户账号被停用,无法授权!");
            }

            //判断用户是否已经授权本软件至永久
            var userLicenseInfo = await _softUserLicenseRepository.FirstOrDefaultAsync(t => t.SoftId == softLicense.SoftId && t.SoftUserId == softUser.Id);
            if (userLicenseInfo != null && !userLicenseInfo.ExpireTime.HasValue)
            {
                throw new UserFriendlyException("软件已经授权到永久,无需授权!");
            }

            #endregion

            #region 授权

            if (userLicenseInfo == null)
            {
                userLicenseInfo = new SoftUserLicense();
                userLicenseInfo.SoftId = softLicense.SoftId;
                userLicenseInfo.SoftUserId = softUser.Id;
                userLicenseInfo.AuthorizeTime = DateTime.Now;
                userLicenseInfo.IsActive = true;
                userLicenseInfo.Type = SoftUserLicenseType.Fee;
                switch (softLicense.LicenseType)
                {
                    case SoftLicenseType.Day:
                        userLicenseInfo.ExpireTime = DateTime.Now.AddDays(1);
                        break;
                    case SoftLicenseType.Forever:
                        userLicenseInfo.ExpireTime = null;
                        break;
                    case SoftLicenseType.Hour:
                        userLicenseInfo.ExpireTime = DateTime.Now.AddHours(1);
                        break;
                    case SoftLicenseType.Month:
                        userLicenseInfo.ExpireTime = DateTime.Now.AddMonths(1);
                        break;
                    case SoftLicenseType.Week:
                        userLicenseInfo.ExpireTime = DateTime.Now.AddDays(7);
                        break;
                    case SoftLicenseType.Year:
                        userLicenseInfo.ExpireTime = DateTime.Now.AddYears(1);
                        break;
                }
                await _softUserLicenseRepository.InsertAsync(userLicenseInfo);
            }
            else
            {
                userLicenseInfo.Type = SoftUserLicenseType.Fee;
                switch (softLicense.LicenseType)
                {
                    case SoftLicenseType.Day:
                        userLicenseInfo.ExpireTime = userLicenseInfo.ExpireTime.Value.AddDays(1);
                        break;
                    case SoftLicenseType.Forever:
                        userLicenseInfo.ExpireTime = null;
                        break;
                    case SoftLicenseType.Hour:
                        userLicenseInfo.ExpireTime = userLicenseInfo.ExpireTime.Value.AddHours(1);
                        break;
                    case SoftLicenseType.Month:
                        userLicenseInfo.ExpireTime = userLicenseInfo.ExpireTime.Value.AddMonths(1);
                        break;
                    case SoftLicenseType.Week:
                        userLicenseInfo.ExpireTime = userLicenseInfo.ExpireTime.Value.AddDays(7);
                        break;
                    case SoftLicenseType.Year:
                        userLicenseInfo.ExpireTime = userLicenseInfo.ExpireTime.Value.AddYears(1);
                        break;
                }
                await _softUserLicenseRepository.UpdateAsync(userLicenseInfo);
            }

            #endregion

            softLicense.Status = SoftLicenseStatus.HasUse;
            softLicense.SoftUserId = softUser.Id;
            softLicense.UseTime = DateTime.Now;
            await _softLicenseRepository.UpdateAsync(softLicense);

        }

        [UnitOfWork]
        public virtual async Task<SoftAuthorizeResult> Login(string appId, string loginName, string password, string mcode, string ip, string processNo)
        {
            #region 检查参数是否正确

            if (string.IsNullOrEmpty(appId))
            {
                return new SoftAuthorizeResult() { errormsg = "appId不允许为空!" };
            }
            if (string.IsNullOrEmpty(loginName))
            {
                return new SoftAuthorizeResult() { errormsg = "用户名不允许为空!" };
            }
            if (string.IsNullOrEmpty(password))
            {
                return new SoftAuthorizeResult() { errormsg = "密码不允许为空!" };
            }
            if (string.IsNullOrEmpty(mcode))
            {
                return new SoftAuthorizeResult() { errormsg = "机器码不允许为空!" };
            }
            if (mcode.Length != 32)
            {
                return new SoftAuthorizeResult() { errormsg = "机器码格式不正确(正确的格式为32位英文字符和数字)!" };
            }

            var soft = await _softRepository.FirstOrDefaultAsync(t => t.AppId == appId);
            if (soft == null)
            {
                return new SoftAuthorizeResult() { errormsg = "软件不存在!" };
            }
            if (!soft.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "软件已停用,有问题请联系客服!" };
            }

            var softUser = await _softUserRepository.FirstOrDefaultAsync(t => t.LoginName == loginName);
            if (softUser == null)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名或密码不正确!" };
            }
            if (new PasswordHasher().VerifyHashedPassword(softUser.Password, password) != PasswordVerificationResult.Success)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名或密码不正确!" };
            }
            if (!softUser.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "该用户已被禁用,如有疑问请联系客服!" };
            }

            var softUserLicense = await _softUserLicenseRepository.FirstOrDefaultAsync(t => t.SoftUserId == softUser.Id && t.SoftId == soft.Id);
            if (softUserLicense == null)
            {
                return new SoftAuthorizeResult() { errormsg = "软件未授权,请先使用卡密授权该软件!" };
            }

            if (!softUserLicense.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "该软件授权已被禁用,如有疑问请联系客服!" };
            }
            if (softUserLicense.ExpireTime.HasValue && DateTime.Now >= softUserLicense.ExpireTime.Value)
            {
                return new SoftAuthorizeResult() { errormsg = "该软件授权已到期!" };
            }
            #endregion


            #region 机器码操作

            //如果软件绑定模式为 绑定机器,就必须验证机器码是否存在,
            if (soft.BindMode == SoftBindMode.BindMachine)
            {
                if (soft.SoftBindOption != null)
                {
                    //有配置绑定的设置
                    int count = await _softUserLicenseMcodeRepository.CountAsync(t => t.SoftUserLicenseId == softUserLicense.Id && t.Mcode != mcode);
                    if (count >= soft.SoftBindOption.AllowBindCount)
                    {
                        return new SoftAuthorizeResult() { errormsg = "软件绑定的机器码已经超出限制!" };
                    }
                }
            }
            else
            {
                //不绑定时，不需要针对验证码做任何操作
            }

            //不管是否需要绑定机器，机器码都保存记录好
            var softUserLicenseMcode = await _softUserLicenseMcodeRepository.FirstOrDefaultAsync(t => t.SoftUserLicenseId == softUserLicense.Id && t.Mcode == mcode);
            if (softUserLicenseMcode != null && !softUserLicenseMcode.IsActive && soft.BindMode == SoftBindMode.BindMachine)
            {
                return new SoftAuthorizeResult() { errormsg = "该机器码已被封禁,禁止登录换绑等操作!" };
            }
            if (softUserLicenseMcode == null)
            {
                //机器码不存在就保存，绑定
                softUserLicenseMcode = new SoftUserLicenseMcode();
                softUserLicenseMcode.SoftUserLicenseId = softUserLicense.Id;
                softUserLicenseMcode.Mcode = mcode;
                softUserLicenseMcode.IsActive = true;
                await _softUserLicenseMcodeRepository.InsertAsync(softUserLicenseMcode);
            }
            else
            {

                await _softUserLicenseMcodeRepository.UpdateAsync(softUserLicenseMcode);
            }


            #endregion

            #region 插入登录记录

            SoftUserLogin softUserLogin = new SoftUserLogin();
            softUserLogin.SoftUserId = softUser.Id;
            softUserLogin.SoftId = soft.Id;
            softUserLogin.Ip = ip;
            softUserLogin.Mcode = mcode;
            await _softUserLoginRepository.InsertAsync(softUserLogin);

            #endregion


            #region 处理在线多开

            //如果限制了多开数量,随机踢掉一个在线用户
            if (soft.SoftMoreOpenOption != null)
            {
                switch (soft.SoftMoreOpenOption.MoreOpenRange)
                {
                    case SoftMoreOpenRange.All:
                        {
                            int count = await _softOnlineRepository.CountAsync(t => t.SoftId == soft.Id && t.SoftUserId == softUser.Id && t.IsOnline == true);
                            if (count >= soft.SoftMoreOpenOption.LimitCount)
                            {
                                var kickItem = await _softOnlineRepository.FirstOrDefaultAsync(t => t.SoftId == soft.Id && t.SoftUserId == softUser.Id && t.IsOnline == true);
                                if (kickItem != null)
                                {
                                    kickItem.IsOnline = false;
                                    kickItem.OfflineReason = SoftOfflineReason.KicksOff;
                                    kickItem.OfflineTime = DateTime.Now;
                                    await _softOnlineRepository.UpdateAsync(kickItem);
                                }
                            }
                        }
                        break;
                    case SoftMoreOpenRange.Ip:
                        {
                            int count = await _softOnlineRepository.CountAsync(t => t.SoftId == soft.Id && t.SoftUserId == softUser.Id && t.Ip == ip && t.IsOnline == true);
                            if (count >= soft.SoftMoreOpenOption.LimitCount)
                            {
                                var kickItem = await _softOnlineRepository.FirstOrDefaultAsync(t => t.SoftId == soft.Id && t.SoftUserId == softUser.Id && t.Ip == ip && t.IsOnline == true);
                                if (kickItem != null)
                                {
                                    kickItem.IsOnline = false;
                                    kickItem.OfflineReason = SoftOfflineReason.KicksOff;
                                    kickItem.OfflineTime = DateTime.Now;
                                    await _softOnlineRepository.UpdateAsync(kickItem);
                                }
                            }
                        }
                        break;
                    case SoftMoreOpenRange.Machine:
                        {
                            int count = await _softOnlineRepository.CountAsync(t => t.SoftId == soft.Id && t.SoftUserId == softUser.Id && t.Mcode == mcode && t.IsOnline == true);
                            if (count >= soft.SoftMoreOpenOption.LimitCount)
                            {
                                var kickItem = await _softOnlineRepository.FirstOrDefaultAsync(t => t.SoftId == soft.Id && t.SoftUserId == softUser.Id && t.Mcode == mcode && t.IsOnline == true);
                                if (kickItem != null)
                                {
                                    kickItem.IsOnline = false;
                                    kickItem.OfflineReason = SoftOfflineReason.KicksOff;
                                    kickItem.OfflineTime = DateTime.Now;
                                    await _softOnlineRepository.UpdateAsync(kickItem);
                                }
                            }
                        }
                        break;
                }

            }

            //插入在线记录
            SoftOnlineLog softOnlineLog = new SoftOnlineLog();
            softOnlineLog.SoftId = soft.Id;
            softOnlineLog.SoftUserId = softUser.Id;
            softOnlineLog.IsOnline = true;
            softOnlineLog.Mcode = mcode;
            softOnlineLog.Ip = ip;
            softOnlineLog.ProcessNo = processNo;
            softOnlineLog.OfflineReason = SoftOfflineReason.None;
            softOnlineLog.LastCheckTime = DateTime.Now;

            await _softOnlineRepository.InsertAsync(softOnlineLog);


            #endregion

            return new SoftAuthorizeResult() { success = true };
        }


        [UnitOfWork]
        public virtual async Task<SoftAuthorizeResult> Register(string appId, string loginName, string password, string mobile = "", string qq = "")
        {
            #region 检查参数

            if (string.IsNullOrEmpty(appId))
            {
                return new SoftAuthorizeResult() { errormsg = "appId不允许为空!" };
            }
            if (string.IsNullOrEmpty(loginName))
            {
                return new SoftAuthorizeResult() { errormsg = "用户名不允许为空!" };
            }
            if (string.IsNullOrEmpty(password))
            {
                return new SoftAuthorizeResult() { errormsg = "密码不允许为空!" };
            }
            if (loginName.Length < 4)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名长度必须在4位以上!" };
            }
            if (password.Length < 6)
            {
                return new SoftAuthorizeResult() { errormsg = "密码长度必须在6位以上!" };
            }

            var soft = await _softRepository.FirstOrDefaultAsync(t => t.AppId == appId);
            if (soft == null)
            {
                return new SoftAuthorizeResult() { errormsg = "软件不存在!" };
            }
            if (!soft.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "软件已停用,有问题请联系客服!" };
            }


            #endregion

            #region 用户注册

            var softRegisterOption = soft.SoftRegisterOption;
            if (softRegisterOption != null)
            {
                if (!softRegisterOption.AllowRegister)
                {
                    return new SoftAuthorizeResult() { errormsg = "软件不允许注册,有需要请联系客服!" };
                }
            }

            SoftUser softUser = await _softUserRepository.FirstOrDefaultAsync(t => t.LoginName == loginName);
            if (softUser != null)
            {
                if (new PasswordHasher().VerifyHashedPassword(softUser.Password, password) == PasswordVerificationResult.Success && softUser.IsActive)
                {
                    //如果，注册的账号已经存在，则直接返回成功
                    softUser.Mobile = mobile;
                    softUser.QQ = qq;
                    await _softUserRepository.UpdateAsync(softUser);
                }
                else
                {
                    return new SoftAuthorizeResult() { errormsg = "用户名已存在!" };
                }
            }
            else
            {
                softUser = new SoftUser();
                softUser.IsActive = true;//默认启用
                softUser.LoginName = loginName;
                softUser.Source = SoftUserSource.Register;
                softUser.Password = new PasswordHasher().HashPassword(password);
                var entity = await _softUserRepository.InsertAsync(softUser);
            }

            #endregion

            #region 写入免费和试用授权

            if (await _softUserLicenseRepository.CountAsync(t => t.SoftId == soft.Id && t.SoftUserId == softUser.Id) == 0)
            {
                if (soft.RunMode == SoftRunMode.Free)
                {
                    //免费用户，直接写入授权到永久
                    SoftUserLicense softUserLicense = new SoftUserLicense();
                    softUserLicense.SoftId = soft.Id;
                    softUserLicense.SoftUserId = softUser.Id;
                    softUserLicense.AuthorizeTime = DateTime.Now;
                    softUserLicense.IsActive = true;
                    softUserLicense.ExpireTime = null;
                    softUserLicense.Type = SoftUserLicenseType.Free;
                    await _softUserLicenseRepository.InsertAsync(softUserLicense);
                }
                else
                {
                    if (softRegisterOption != null && softRegisterOption.TrialTime > 0)
                    {
                        //试用用户，直接写入授权到 系统时间+试用分钟
                        SoftUserLicense softUserLicense = new SoftUserLicense();
                        softUserLicense.SoftId = soft.Id;
                        softUserLicense.SoftUserId = softUser.Id;
                        softUserLicense.AuthorizeTime = DateTime.Now;
                        softUserLicense.IsActive = true;
                        softUserLicense.ExpireTime = softUserLicense.AuthorizeTime.AddMinutes(softRegisterOption.TrialTime);
                        softUserLicense.Type = SoftUserLicenseType.Trial;
                        await _softUserLicenseRepository.InsertAsync(softUserLicense);
                    }
                }
            }

            #endregion

            return new SoftAuthorizeResult() { success = true };
        }


        public virtual async Task<SoftAuthorizeResult> Authorize(string appId, string loginName, string password, string licenseNo)
        {
            #region 检查参数

            if (string.IsNullOrEmpty(appId))
            {
                return new SoftAuthorizeResult() { errormsg = "appId不允许为空!" };
            }
            if (string.IsNullOrEmpty(loginName))
            {
                return new SoftAuthorizeResult() { errormsg = "用户名不允许为空!" };
            }
            if (string.IsNullOrEmpty(password))
            {
                return new SoftAuthorizeResult() { errormsg = "密码不允许为空!" };
            }
            if (string.IsNullOrEmpty(licenseNo))
            {
                return new SoftAuthorizeResult() { errormsg = "卡密不允许为空!" };
            }

            var soft = await _softRepository.FirstOrDefaultAsync(t => t.AppId == appId);
            if (soft == null)
            {
                return new SoftAuthorizeResult() { errormsg = "软件不存在!" };
            }
            if (!soft.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "软件已停用,有问题请联系客服!" };
            }

            if (soft.RunMode == SoftRunMode.Free)
            {
                return new SoftAuthorizeResult() { errormsg = "软件为免费软件不需要授权!" };
            }

            var softUser = await _softUserRepository.FirstOrDefaultAsync(t => t.LoginName == loginName);
            if (softUser == null)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名或密码不正确!" };
            }
            if (new PasswordHasher().VerifyHashedPassword(softUser.Password, password) != PasswordVerificationResult.Success)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名或密码不正确!" };
            }
            if (!softUser.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "账号被封禁，有疑问请联系客服!" };
            }

            var softLicense = await _softLicenseRepository.FirstOrDefaultAsync(t => t.SoftId == soft.Id && t.LicenseNo == licenseNo);
            if (softLicense == null)
            {
                return new SoftAuthorizeResult() { errormsg = "卡密不正确!" };
            }

            #endregion

            try
            {
                await Authorize(softLicense, softUser);
            }
            catch (UserFriendlyException ex)
            {
                return new SoftAuthorizeResult() { errormsg = ex.Message };
            }
            catch (Exception ex)
            {
                return new SoftAuthorizeResult() { errormsg = "授权失败" };
            }

            return new SoftAuthorizeResult() { success = true };
        }


        public virtual async Task<SoftAuthorizeResult> ChangePassword(string appId, string loginName, string oldpassword,
            string newpassword1, string newpassword2)
        {
            #region 检查参数

            if (string.IsNullOrEmpty(appId))
            {
                return new SoftAuthorizeResult() { errormsg = "appId不允许为空!" };
            }
            if (string.IsNullOrEmpty(loginName))
            {
                return new SoftAuthorizeResult() { errormsg = "用户名不允许为空!" };
            }
            if (string.IsNullOrEmpty(oldpassword))
            {
                return new SoftAuthorizeResult() { errormsg = "密码不允许为空!" };
            }
            if (string.IsNullOrEmpty(newpassword1))
            {
                return new SoftAuthorizeResult() { errormsg = "新密码不允许为空!" };
            }
            if (newpassword1.Length < 6)
            {
                return new SoftAuthorizeResult() { errormsg = "新密码长度不能少于6位!" };
            }
            if (!newpassword1.Equals(newpassword2))
            {
                return new SoftAuthorizeResult() { errormsg = "2次输入的密码不一致!" };
            }

            var soft = await _softRepository.FirstOrDefaultAsync(t => t.AppId == appId);
            if (soft == null)
            {
                return new SoftAuthorizeResult() { errormsg = "软件不存在!" };
            }

            if (!soft.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "软件已停用,有问题请联系客服!" };
            }


            var softUser = await _softUserRepository.FirstOrDefaultAsync(t => t.LoginName == loginName);
            if (softUser == null)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名或密码不正确!" };
            }
            if (new PasswordHasher().VerifyHashedPassword(softUser.Password, oldpassword) != PasswordVerificationResult.Success)
            {
                return new SoftAuthorizeResult() { errormsg = "旧密码不正确!" };
            }
            if (!softUser.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "账号被封禁，有疑问请联系客服!" };
            }

            #endregion

            softUser.Password = new PasswordHasher().HashPassword(newpassword1);
            await _softUserRepository.UpdateAsync(softUser);
            return new SoftAuthorizeResult() { success = true };

        }


        public virtual async Task<SoftAuthorizeResult> ChangeMcode(string appId, string loginName, string password,
            string oldmcode, string newmcode)
        {
            #region 检查参数是否正确

            if (string.IsNullOrEmpty(appId))
            {
                return new SoftAuthorizeResult() { errormsg = "appId不允许为空!" };
            }
            if (string.IsNullOrEmpty(loginName))
            {
                return new SoftAuthorizeResult() { errormsg = "用户名不允许为空!" };
            }
            if (string.IsNullOrEmpty(password))
            {
                return new SoftAuthorizeResult() { errormsg = "密码不允许为空!" };
            }
            if (string.IsNullOrEmpty(oldmcode))
            {
                return new SoftAuthorizeResult() { errormsg = "原机器码不允许为空!" };
            }
            if (string.IsNullOrEmpty(newmcode))
            {
                return new SoftAuthorizeResult() { errormsg = "新机器码不允许为空!" };
            }
            if (newmcode.Length != 32)
            {
                return new SoftAuthorizeResult() { errormsg = "机器码格式不正确(正确的格式为32位英文字符和数字)!" };
            }
            if (oldmcode.Equals(newmcode))
            {
                return new SoftAuthorizeResult() { errormsg = "新旧机器码是一样的，不需要换绑!" };
            }

            var soft = await _softRepository.FirstOrDefaultAsync(t => t.AppId == appId);
            if (soft == null)
            {
                return new SoftAuthorizeResult() { errormsg = "软件不存在!" };
            }
            if (!soft.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "软件已停用,有问题请联系客服!" };
            }

            var softUser = await _softUserRepository.FirstOrDefaultAsync(t => t.LoginName == loginName);
            if (softUser == null)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名或密码不正确!" };
            }
            if (new PasswordHasher().VerifyHashedPassword(softUser.Password, password) != PasswordVerificationResult.Success)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名或密码不正确!" };
            }
            if (!softUser.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "该用户已被禁用,如有疑问请联系客服!" };
            }

            var softUserLicense = await _softUserLicenseRepository.FirstOrDefaultAsync(t => t.SoftUserId == softUser.Id && t.SoftId == soft.Id);
            if (softUserLicense == null)
            {
                return new SoftAuthorizeResult() { errormsg = "软件未授权,请先使用卡密授权该软件!" };
            }

            if (!softUserLicense.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "该软件授权已被禁用,如有疑问请联系客服!" };
            }

            if (softUserLicense.ExpireTime.HasValue && DateTime.Now >= softUserLicense.ExpireTime.Value)
            {
                return new SoftAuthorizeResult() { errormsg = "该软件授权已到期!" };
            }


            var userLicenseMcode = await _softUserLicenseMcodeRepository.FirstOrDefaultAsync(t => t.SoftUserLicenseId == softUserLicense.Id && t.Mcode == oldmcode);
            if (userLicenseMcode == null)
            {
                return new SoftAuthorizeResult() { errormsg = "原机器码没有绑定，无法换绑!" };
            }
            if (!userLicenseMcode.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "原机器码已被禁用,无法换绑,如有疑问请联系客服!" };
            }
            if (await _softUserLicenseMcodeRepository.CountAsync(t => t.SoftUserLicenseId == softUserLicense.Id && t.Mcode == newmcode) > 0)
            {
                return new SoftAuthorizeResult() { errormsg = "新旧机器码已存在，可以正常使用，不需要换绑!" };
            }

            long softId = softUserLicense.SoftId;

            var softBindOption = await _softBindOptionRepository.FirstOrDefaultAsync(t => t.SoftId == softId);
            if (softBindOption != null)
            {
                if (await _mCodeChangeLogRepository.CountAsync(t => t.SoftUserLicenseId == softUserLicense.Id) >=
                    softBindOption.AllowChangeBindCount)
                {
                    return new SoftAuthorizeResult() { errormsg = "换绑次数已经超过限制!" };
                }
            }

            #endregion

            MCodeChangeLog mCodeChangeLog = new MCodeChangeLog();
            mCodeChangeLog.SoftUserLicenseId = softUserLicense.Id;
            mCodeChangeLog.SoftUserId = softUserLicense.SoftUserId;
            mCodeChangeLog.SoftId = softUserLicense.SoftId;
            mCodeChangeLog.OldMCode = userLicenseMcode.Mcode;
            mCodeChangeLog.NewMCode = newmcode;
            mCodeChangeLog.Source = MCodeChangeSource.User;
            await _mCodeChangeLogRepository.InsertAsync(mCodeChangeLog);

            userLicenseMcode.Mcode = newmcode;
            await _softUserLicenseMcodeRepository.UpdateAsync(userLicenseMcode);

            string okMsg = "";
            if (softBindOption != null)
            {
                int hasChgCount = await _mCodeChangeLogRepository.CountAsync(t => t.SoftUserLicenseId == softUserLicense.Id);
                okMsg = string.Format("软件允许换绑次数:{0},您已换绑:{1}次,还剩:{2}次换绑!", softBindOption.AllowChangeBindCount, hasChgCount, softBindOption.AllowChangeBindCount - hasChgCount);
            }
            return new SoftAuthorizeResult() { success = true, msg = okMsg };
        }


        public virtual async Task<SoftAuthorizeResult> Logout(string appId, string loginName, string password, string mcode, string ip, string processNo)
        {
            #region 检查参数是否正确

            if (string.IsNullOrEmpty(appId))
            {
                return new SoftAuthorizeResult() { errormsg = "appId不允许为空!" };
            }
            if (string.IsNullOrEmpty(loginName))
            {
                return new SoftAuthorizeResult() { errormsg = "用户名不允许为空!" };
            }
            if (string.IsNullOrEmpty(password))
            {
                return new SoftAuthorizeResult() { errormsg = "密码不允许为空!" };
            }
            if (string.IsNullOrEmpty(mcode))
            {
                return new SoftAuthorizeResult() { errormsg = "机器码不允许为空!" };
            }
            if (mcode.Length != 32)
            {
                return new SoftAuthorizeResult() { errormsg = "机器码格式不正确(正确的格式为32位英文字符和数字)!" };
            }

            var soft = await _softRepository.FirstOrDefaultAsync(t => t.AppId == appId);
            if (soft == null)
            {
                return new SoftAuthorizeResult() { errormsg = "软件不存在!" };
            }

            var softUser = await _softUserRepository.FirstOrDefaultAsync(t => t.LoginName == loginName);
            if (softUser == null)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名或密码不正确!" };
            }
            if (new PasswordHasher().VerifyHashedPassword(softUser.Password, password) != PasswordVerificationResult.Success)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名或密码不正确!" };
            }


            var onlineInfo = await _softOnlineRepository.FirstOrDefaultAsync(t => t.SoftId == soft.Id && t.SoftUserId == softUser.Id && t.Mcode == mcode && t.ProcessNo == processNo && t.IsOnline);
            if (onlineInfo == null)
            {
                return new SoftAuthorizeResult() { success = true, msg = "登录记录不存在!" };
            }


            #endregion


            onlineInfo.IsOnline = false;
            onlineInfo.OfflineReason = SoftOfflineReason.Logout;
            onlineInfo.OfflineTime = DateTime.Now;

            await _softOnlineRepository.UpdateAsync(onlineInfo);

            return new SoftAuthorizeResult() { success = true };
        }

        public virtual async Task<SoftAuthorizeResult> CheckStatus(string appId, string loginName, string password, string mcode, string ip, string processNo)
        {

            #region 轮询时间小于1分钟，直接返回错误

            string token = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", appId, loginName, password, mcode, ip, processNo);
            DateTime lastCheckTime = _cacheManager.GetCache("CheckStatus").Get(token, () => { return DateTime.Now.AddMinutes(-2); });//如果缓存中没有数据,默认给2分钟前
            if ((DateTime.Now - lastCheckTime).TotalMinutes < 1)
            {
                return new SoftAuthorizeResult() { errormsg = "检测时间周期不能少于1分钟!" };
            }
            _cacheManager.GetCache("CheckStatus").Set(token, DateTime.Now, new TimeSpan(0, 3, 0));

            #endregion

            #region 检查参数是否正确

            if (string.IsNullOrEmpty(appId))
            {
                return new SoftAuthorizeResult() { errormsg = "appId不允许为空!" };
            }
            if (string.IsNullOrEmpty(loginName))
            {
                return new SoftAuthorizeResult() { errormsg = "用户名不允许为空!" };
            }
            if (string.IsNullOrEmpty(password))
            {
                return new SoftAuthorizeResult() { errormsg = "密码不允许为空!" };
            }
            if (string.IsNullOrEmpty(mcode))
            {
                return new SoftAuthorizeResult() { errormsg = "机器码不允许为空!" };
            }
            if (mcode.Length != 32)
            {
                return new SoftAuthorizeResult() { errormsg = "机器码格式不正确(正确的格式为32位英文字符和数字)!" };
            }

            var soft = await _softRepository.FirstOrDefaultAsync(t => t.AppId == appId);
            if (soft == null)
            {
                return new SoftAuthorizeResult() { errormsg = "软件不存在!" };
            }
            if (!soft.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "软件已停用,有问题请联系客服!" };
            }

            var softUser = await _softUserRepository.FirstOrDefaultAsync(t => t.LoginName == loginName);
            if (softUser == null)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名或密码不正确!" };
            }
            if (new PasswordHasher().VerifyHashedPassword(softUser.Password, password) != PasswordVerificationResult.Success)
            {
                return new SoftAuthorizeResult() { errormsg = "用户名或密码不正确!" };
            }
            if (!softUser.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "该用户已被禁用,如有疑问请联系客服!" };
            }

            var softUserLicense = await _softUserLicenseRepository.FirstOrDefaultAsync(t => t.SoftUserId == softUser.Id && t.SoftId == soft.Id);
            if (softUserLicense == null)
            {
                return new SoftAuthorizeResult() { errormsg = "软件未授权,请先使用卡密授权该软件!" };
            }

            if (!softUserLicense.IsActive)
            {
                return new SoftAuthorizeResult() { errormsg = "该软件授权已被禁用,如有疑问请联系客服!" };
            }
            if (softUserLicense.ExpireTime.HasValue && DateTime.Now >= softUserLicense.ExpireTime.Value)
            {
                return new SoftAuthorizeResult() { errormsg = "该软件授权已到期!" };
            }


            var onlineInfo = await _softOnlineRepository.FirstOrDefaultAsync(t => t.SoftId == soft.Id && t.SoftUserId == softUser.Id && t.Mcode == mcode && t.ProcessNo == processNo && t.IsOnline);
            if (onlineInfo == null)
            {
                return new SoftAuthorizeResult() { success = true, msg = "登录记录不存在!" };
            }


            #endregion

            onlineInfo.LastCheckTime = DateTime.Now;
            onlineInfo.OnlineTime += (int)(DateTime.Now - (onlineInfo.LastCheckTime.HasValue ? onlineInfo.LastCheckTime.Value : onlineInfo.CreationTime)).TotalMinutes;

            await _softOnlineRepository.UpdateAsync(onlineInfo);

            return new SoftAuthorizeResult() { success = true };
        }
    }
 
}
