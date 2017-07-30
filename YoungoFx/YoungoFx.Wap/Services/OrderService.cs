using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models;
using StarmileFx.Models.Youngo;

namespace StarmileFx.Wap.Services
{
    public class OrderService
    {
        private static Stack<OnLineOrderParent> _Orders { get; set; }
        private static ConcurrentStack<OnLineOrderParent> _ConcurrenOrders { get; set; }
        private static Stopwatch swTask { get; set; }
        /// <summary>
        /// 是否开启了线程
        /// </summary>
        public static bool IsStarted;

        public OrderService()
        {
            _ConcurrenOrders = new ConcurrentStack<OnLineOrderParent>();
            Stopwatch swTask = new Stopwatch();
            swTask.Start();

            /*创建任务 tk1  tk1 执行 数据集合添加操作*/
            Task tk1 = Task.Factory.StartNew(() =>
            {
            });
            

            Task.WaitAll(tk1);
            swTask.Stop();
        }

        public static async Task<Result> AddOrder(OnLineOrderParent OrderParent)
        {
            return await Task.Run(() =>
            {
                return  AddConcurrenOrder(OrderParent);
            });
        }

        /// <summary>
        /// 线程开启
        /// </summary>
        /// <returns></returns>
        public static bool Start()
        {
            bool ret = false;
            while (!IsStarted)
            {
                ret = true;
            }
            return ret;
        }

        static Result AddConcurrenOrder(OnLineOrderParent OrderParent)
        {
            var result = new Result();
            if (_ConcurrenOrders.Count >= 300)
            {
                result.IsSuccessful = false;
                result.ReasonDescription = "已超过最大并发数";
                return result;
            }
            else
            {
                _ConcurrenOrders.Push(OrderParent);
            }
            return result;
        }
    }
}
