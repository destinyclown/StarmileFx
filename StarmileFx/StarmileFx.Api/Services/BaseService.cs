using System;
using System.Threading;
using StarmileFx.Common.Enum;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using Microsoft.AspNetCore.Http;
using StarmileFx.Api.Models;

namespace StarmileFx.Api.Services
{
    public class BaseService
    {
        // Lock对象，线程安全所用
        private static readonly object syncRoot = new object();

        private static readonly object syncRoot2 = new object();

        private static readonly object syncRoot3 = new object();

        private static BaseService baseService = new BaseService();

        /// <summary>
        /// 是否开启了线程
        /// </summary>
        public static bool m_isStarted;

        /// <summary>
        /// 线程
        /// </summary>
        private static Thread m_thread;

        /// <summary>
        /// 添加队列项
        /// </summary>
        /// <param name="item"></param>
        private void AddItem(SysRoleOnline item)
        {
            if (m_isStarted)
            {
                SysRolesOnline.Instance.SysRolesList.Add(item);
            }
        }

        /// <summary>
        /// 获取用户Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetToken(int roleId)
        {
            var model = SysRolesOnline.Instance.SysRolesList.Find(a => a.RoleID == roleId);
            if (model != null)
            {
                return model.Token;
            }
            return string.Empty;
        }

        /// <summary>
        /// 根据Token(令牌)获取用户实体
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static SysRoles GetRoleByToken(string Token)
        {
            var model = SysRolesOnline.Instance.SysRolesList.Find(a => a.Token == Token);
            if (model != null)
            {
                return model.SysRole;
            }
            return null;
        }

        /// <summary>
        /// 根据Token(令牌)获取用户ID
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static int GetRoleIdByToken(string Token)
        {
            var model = SysRolesOnline.Instance.SysRolesList.Find(a => a.Token == Token);
            if (model != null)
            {
                return model.RoleID;
            }
            return 0;
        }

        /// <summary>
        /// 将用户插入队列返回Token
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string Insert(SysRoles model, HttpContext context)
        {
            if (m_isStarted)
            {
                lock (syncRoot)
                {
                    SysRoleOnline sysRoleOnline = SysRolesOnline.Instance.SysRolesList.Find(a => a.RoleID == model.Id);
                    if (sysRoleOnline == null)
                    {
                        sysRoleOnline = new SysRoleOnline(context) { RoleID = model.Id, SysRole = model };
                        baseService.AddItem(sysRoleOnline);
                        return sysRoleOnline.Token;
                    }
                    return sysRoleOnline.Token;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 根据Token获取在线用户列表
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<SysRoleOnline> GetSysRoleOnline(string Token)
        {
            var model = GetRoleByToken(Token);
            if (model != null)
            {
                if (model.Permissions == 0)
                {
                    return SysRolesOnline.Instance.SysRolesList;
                }
            }
            return null;
        }

        /// <summary>
        /// 刷新用户返回新Token(令牌)
        /// </summary>
        /// <param name="Token"></param>
        public static string Refresh(string Token, HttpContext context)
        {
            SysRoleOnline sysRoleOnline = SysRolesOnline.Instance.SysRolesList.Find(a => a.Token == Token);
            if (sysRoleOnline != null)
            {
                RemoveItem(sysRoleOnline);
                return Insert(sysRoleOnline.SysRole, context);
            }
            return string.Empty;
        }

        /// <summary>
        /// 清理过期用户
        /// </summary>
        public static void ClearTimeOut()
        {
            var list = SysRolesOnline.Instance.SysRolesList.FindAll(Predicate);
            foreach (var model in list)
            {
                RemoveItem(model);
            }
        }

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="model"></param>
        public static bool ClearRole(string Token)
        {
            if (Token != null)
            {
                SysRoleOnline sysRoleOnline = SysRolesOnline.Instance.SysRolesList.Find(a => a.Token == Token);
                if (RemoveItem(sysRoleOnline))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 移除列表用户
        /// </summary>
        /// <param name="sysRoleOnline"></param>
        private static bool RemoveItem(SysRoleOnline sysRoleOnline)
        {
            if (m_isStarted)
            {
                lock (syncRoot3)
                {
                    return SysRolesOnline.Instance.SysRolesList.Remove(sysRoleOnline);
                }
            }
            return false;
        }

        /// <summary>
        /// 判断用户活动时间是否小于指定值
        /// </summary>
        /// <param name="sysRoleOnline"></param>
        /// <returns></returns>
        private static bool Predicate(SysRoleOnline sysRoleOnline)
        {
            if (sysRoleOnline == null)
                return false;

            return sysRoleOnline.ActiveTime < DateTime.Now.AddDays(-7);
        }

        /// <summary>
        /// 线程方法
        /// </summary>
        /// <param name="param"></param>
        private static void Run(object param)
        {
            while (m_isStarted)
            {
                lock (syncRoot2)
                {
                    ClearTimeOut();     
                }
                Thread.Sleep(10000);
            }
        }

        /// <summary>
        /// 线程开启
        /// </summary>
        /// <returns></returns>
        public static bool Start()
        {
            bool ret = false;
            while (!m_isStarted)
            {
                ParameterizedThreadStart start = new ParameterizedThreadStart(Run);
                m_thread = new Thread(start);
                m_thread.Start(baseService);
                m_isStarted = true;
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// 线程结束
        /// </summary>
        /// <returns></returns>
        public static bool Stop()
        {
            bool ret = false;
            if (m_isStarted)
            {
                m_isStarted = false;
                m_thread.Join(200);
                ret = true;
            }
            return ret;
        }
    }
}
