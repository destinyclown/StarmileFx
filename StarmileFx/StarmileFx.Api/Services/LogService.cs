using StarmileFx.Api.Server.IServices;
using StarmileFx.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarmileFx.Api.Services
{
    /// <summary>
    /// 日志线程系统
    /// </summary>
    public class LogService
    {
        private readonly IBaseServer _BaseServer;

        public LogService(IBaseServer IBaseServer)
        {
            _BaseServer = IBaseServer;
        }

        // Lock对象，线程安全所用
        private static readonly object syncRoot = new object();

        private static readonly object syncRoot2 = new object();

        /// <summary>
        /// 日志队列
        /// </summary>
        private static List<SysLog> _Logs = new List<SysLog>();

        /// <summary>
        /// 是否开启了线程
        /// </summary>
        public static bool IsStarted;

        /// <summary>
        /// 线程
        /// </summary>
        private static Thread _Thread;

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="message"></param>
        public static void Add(SysLog log)
        {
            if (IsStarted)
            {
                lock (syncRoot)
                {
                     _Logs.Add(log);
                }
            }
        }

        /// <summary>
        /// 线程方法
        /// </summary>
        /// <param name="param"></param>
        private void Run(object param)
        {
            while (IsStarted)
            {
                if (_Logs.Count > 0)
                {
                    List<SysLog> wList = _Logs;
                    _Logs = new List<SysLog>();
                    lock (syncRoot2)
                    {
                        foreach (var log in wList)
                        {
                            try
                            {
                                _BaseServer.Logger(log);
                            }
                            catch (Exception ex)
                            {
                                Email email = new Email
                                {
                                    Type = StarmileFx.Models.Enum.BaseEnum.EmailTypeEnum.Error,
                                    Subject = "StarmileFx.Api系统错误",
                                    Message = ex.Message
                                };
                                EmailService.Add(email);
                            }
                        }
                    }
                    Thread.Sleep(60000);
                }
                if (!IsStarted) return;
            }
        }

        /// <summary>
        /// 线程开启
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            bool ret = false;
            while (!IsStarted)
            {
                ParameterizedThreadStart start = new ParameterizedThreadStart(Run);
                _Thread = new Thread(start);
                _Thread.Start(_BaseServer);
                IsStarted = true;
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
            if (IsStarted)
            {
                IsStarted = false;
                _Thread.Join(200);
                ret = true;
            }
            return ret;
        }
    }
}
