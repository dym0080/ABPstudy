using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Abp.Web.Models;
using Abp.Web.Mvc.Models;
using Abp.WebApi.Controllers;
using SNAS.Core.SoftAuthorize;

namespace SNAS.WebApi.Api.Controllers
{

    [Route("client/soft")]
    public class SoftController : ApiController
    {
        private readonly SoftAuthorizeManager _softAuthorizeManager;

        public SoftController(SoftAuthorizeManager softAuthorizeManager)
        {
            _softAuthorizeManager = softAuthorizeManager;
        }

        /// <summary>
        /// 用户登录软件
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="mcode">机器码(限制32位)</param>
        /// <param name="processNo">软件启动进程代号(如果软件控制了同一台电脑不允许多开可以为空)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("client/Soft/Login")]
        public Task<SoftAuthorizeResult> Login(string appId, string loginName, string password, string mcode, string processNo = "NONE")
        {
            return _softAuthorizeManager.Login(appId, loginName, password, mcode, GetIP(), processNo);
        }

        private string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="loginName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="mobile">手机号码(可为空)</param>
        /// <param name="qq">qq(可为空)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("client/Soft/Register")]
        public Task<SoftAuthorizeResult> Register(string appId, string loginName, string password, string mobile = "", string qq = "")
        {
            return _softAuthorizeManager.Register(appId, loginName, password, mobile, qq);
        }


        /// <summary>
        /// 卡密授权
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="loginName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="licenseNo">卡密</param>
        /// <returns></returns>
        [HttpPost]
        [Route("client/Soft/Authorize")]
        public Task<SoftAuthorizeResult> Authorize(string appId, string loginName, string password, string licenseNo)
        {
            return _softAuthorizeManager.Authorize(appId, loginName, password, licenseNo);
        }

        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="loginName">用户名</param>
        /// <param name="oldpassword">旧密码</param>
        /// <param name="newpassword1">新密码</param>
        /// <param name="newpassword2">确认新密码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("client/Soft/ChangePassword")]
        public Task<SoftAuthorizeResult> ChangePassword(string appId, string loginName, string oldpassword, string newpassword1, string newpassword2)
        {
            return _softAuthorizeManager.ChangePassword(appId, loginName, oldpassword, newpassword1, newpassword2);
        }


        /// <summary>
        /// 自助换绑
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="loginName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="oldmcode">原机器码</param>
        /// <param name="newmcode">新机器码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("client/Soft/ChangeMcode")]
        public Task<SoftAuthorizeResult> ChangeMcode(string appId, string loginName, string password, string oldmcode, string newmcode)
        {
            return _softAuthorizeManager.ChangeMcode(appId, loginName, password, oldmcode, newmcode);
        }

        /// <summary>
        /// 检测状态(轮询检测,轮询间隔(1-3分钟)是否需要强制下线)
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="mcode">机器码(限制32位)</param>
        /// <param name="processNo">软件启动进程代号(登录时使用的进程代号)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("client/Soft/CheckStatus")]
        public Task<SoftAuthorizeResult> CheckStatus(string appId, string loginName, string password, string mcode, string processNo = "NONE")
        {
            /*
            检测到强制下线的原因:
            1.没有上线记录
            2.长时间没有检测状态,后台任务自动将用户登录踢下线
            3.设置了多开配置，超过了多开的限制,踢掉了前面登录记录
            4.软件授权时间到            
            */
            return _softAuthorizeManager.CheckStatus(appId, loginName, password, mcode, GetIP(), processNo);
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="mcode">机器码(限制32位)</param>
        /// <param name="processNo">软件启动进程代号(登录时使用的进程代号)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("client/Soft/Logout")]
        public Task<SoftAuthorizeResult> Logout(string appId, string loginName, string password, string mcode, string processNo = "NONE")
        {
            return _softAuthorizeManager.Logout(appId, loginName, password, mcode, GetIP(), processNo);
        }
    }
}
