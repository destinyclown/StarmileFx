using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StarmileFx.Models.Redis;
using YoungoFx.Web.Server.IService;
using YoungoFx.Web.Server.IServices;

namespace StarmileFx.Wap.Services
{
    public class CacheService
    {
        private readonly IYoungoServer _IYoungoServer;

        public CacheService(IYoungoServer IYoungoServer)
        {
            _IYoungoServer = IYoungoServer;
        }

        // Lock对象，线程安全所用
        private static readonly object syncRoot = new object();

        private static readonly object syncRoot2 = new object();

        /// <summary>
        /// 是否开启了线程
        /// </summary>
        public static bool IsStarted;

        /// <summary>
        /// 线程
        /// </summary>
        private static Thread _Thread;


        /// <summary>
        /// 线程方法
        /// </summary>
        /// <param name="param"></param>
        private void Run(object param)
        {
            while (IsStarted)
            {
                _IYoungoServer.GetCacheProductList();
                Thread.Sleep(1800000);
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
                _Thread.Start(_IYoungoServer);
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
