using System;
using System.Collections.Generic;
using System.Linq;
using StarmileFx.Common.Enum;
using StarmileFx.Models.Base;
using Microsoft.AspNetCore.Http;

namespace StarmileFx.Api.Models
{
    #region SysRolesOnline单例

    /// <summary>
    /// 在线用户队列
    /// </summary>
    public sealed class SysRolesOnline
    {
        // 依然是静态自动hold实例
        private static volatile SysRolesOnline instance = null;
        // Lock对象，线程安全所用
        private static readonly object syncRoot = new object();

        private SysRolesOnline() { SysRolesList = new List<SysRoleOnline>(); }

        public List<SysRoleOnline> SysRolesList { get; set; }

        /// <summary>
        /// 在线用户队列单例
        /// </summary>
        public static SysRolesOnline Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SysRolesOnline();
                    }
                }

                return instance;
            }
        }



    }

    #endregion SysRolesOnline单例

    #region SysRoleOnline队列类

    /// <summary>
    /// 队列类
    /// </summary>
    public class SysRoleOnline
    {
        private HttpContext HttpContext { get; set; }
        public SysRoleOnline(HttpContext context)
        {
            HttpContext = context;
            Token = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            InitIp();
            ActiveTime = DateTime.Now;
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 实体
        /// </summary>
        public SysRoles SysRole { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; private set; }
        
        /// <summary>
        /// 用户IP
        /// </summary>
        public string Ip { get; private set; }

        /// <summary>
        /// 活跃时间
        /// </summary>
        public DateTime ActiveTime { get; set; }

        /// <summary>
        /// 将cookie保存到token中
        /// </summary>
        private void InitToken()
        {
            string token = this.HttpContext.Request.Cookies[SysConst.Token];
            if (string.IsNullOrWhiteSpace(token))
            {
                token = this.HttpContext.Request.Query["token"].FirstOrDefault();
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                token = this.HttpContext.Request.Headers["token"].FirstOrDefault();
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                token = Guid.NewGuid().ToString().Replace("-", "");
                this.HttpContext.Response.Cookies.Append(SysConst.Token, token, new CookieOptions { Expires = DateTime.Now.AddDays(7) });
            }
            this.Token = token;
        }

        /// <summary>
        /// 获取用户IP
        /// </summary>
        private void InitIp()
        {
            this.Ip = this.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
    #endregion SysRoleOnline队列类
}
