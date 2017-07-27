using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Options;
using StarmileFx.Common;
using StarmileFx.Models.Enum;
using StarmileFx.Models.Json;
using static StarmileFx.Models.Enum.BaseEnum;

namespace StarmileFx.Api.Services
{
    public class EmailService
    {
        // Lock对象，线程安全所用
        private static readonly object syncRoot = new object();

        private static EmailService _EmailService = new EmailService();

        /// <summary>
        /// 配置
        /// </summary>
        public static EmailModel _EmailConfig = new EmailModel();

        /// <summary>
        /// 日志队列
        /// </summary>
        private List<Email> _MessageList = new List<Email>();
        /// <summary>
        /// 是否开启了线程
        /// </summary>
        public static bool IsStarted;

        /// <summary>
        /// 线程
        /// </summary>
        private static Thread _Thread;

        /// <summary>
        /// 管理员邮箱
        /// </summary>
        public static string AdminEmail = "757950169@qq.com";

        /// <summary>
        /// 管理员邮箱
        /// </summary>
        public static string YoungoEmail = "757950169@qq.com";
        
        /// <summary>
        /// 发送订单邮件
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="OrederId"></param>
        /// <param name="MessageType"></param>
        public static void SendOrderEmail(int CustomerId, string OrederId, MessageTypeEnum MessageType)
        {
            string message = "";
            switch (MessageType)
            {
                case MessageTypeEnum.Cancel:
                    message = string.Format("您好，ID为：【{0}】的客户申请【取消订单】，订单号为：【{0}】，请及时处理！", CustomerId, OrederId);
                    break;
                case MessageTypeEnum.ApplyRefund:
                    message = string.Format("您好，ID为：【{0}】的客户申请【申请退款】，订单号为：【{0}】，请及时处理！", CustomerId, OrederId);
                    break;
                case MessageTypeEnum.ApplyReturns:
                    message = string.Format("您好，ID为：【{0}】的客户申请【申请退货】，订单号为：【{0}】，请及时处理！", CustomerId, OrederId);
                    break;
                case MessageTypeEnum.ApplyExchange:
                    message = string.Format("您好，ID为：【{0}】的客户申请【申请换货】，订单号为：【{0}】，请及时处理！", CustomerId, OrederId);
                    break;
            }
            //发送信息给系统管理员
            MailHelper.Send(_EmailConfig, AdminEmail, MessageType.ToString(), message);
            //发送信息给Youngo管理员
            MailHelper.Send(_EmailConfig, YoungoEmail, MessageType.ToString(), message);
        }

        /// <summary>
        /// 错误日志邮件
        /// </summary>
        /// <param name="Controller"></param>
        /// <param name="Action"></param>
        /// <param name="ErrorMsg"></param>
        /// <param name="Ip"></param>
        public static void SendErrorEmail(string Controller, string Action, string ErrorMsg, string Ip)
        {
            string message = string.Format("StarmileFx.Api系统出错\r\n客户端IP地址：{0}\r\nController：{1}\r\nAction：{2}\r\n错误信息：{3}", Ip, Controller, Action, ErrorMsg);
            //发送信息给系统管理员
            MailHelper.Send(_EmailConfig, AdminEmail, "StarmileFx.Api系统出错", message);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="elist"></param>
        private void Send(List<Email> elist)
        {
            foreach (Email email in elist)
            {
                switch (email.type)
                {
                    case EmailTypeEnum.Youngo:
                        MailHelper.Send(_EmailConfig, _EmailConfig.YoungoEamil, email.Subject, email.Message);
                        MailHelper.Send(_EmailConfig, _EmailConfig.AdminEamil, email.Subject, email.Message);
                        break;
                    case EmailTypeEnum.Error:
                        MailHelper.Send(_EmailConfig, _EmailConfig.YoungoEamil, email.Subject, email.Message);
                        break;
                }
            }
        }

        /// <summary>
        /// 添加邮件
        /// </summary>
        /// <param name="message"></param>
        public static void Add(Email email)
        {
            if (IsStarted)
            {
                lock (syncRoot)
                {
                    _EmailService._MessageList.Add(email);
                }
            }
        }

        /// <summary>
        /// 线程方法
        /// </summary>
        /// <param name="param"></param>
        private static void Run(object param)
        {
            while (IsStarted)
            {
                if (_EmailService._MessageList.Count > 0)
                {
                    List<Email> wList = _EmailService._MessageList;
                    lock (syncRoot)
                    {
                        _EmailService._MessageList = new List<Email>();
                        _EmailService.Send(wList);
                        Thread.Sleep(10000);
                    }
                    
                }
                if (!IsStarted) return;
                
            }
            
        }

        /// <summary>
        /// 线程开启
        /// </summary>
        /// <returns></returns>
        public static bool Start(EmailModel EmailModel)
        {
            bool ret = false;
            _EmailConfig = EmailModel;
            while (!IsStarted)
            {
                ParameterizedThreadStart start = new ParameterizedThreadStart(Run);
                _Thread = new Thread(start);
                _Thread.Start(_EmailService);
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

    /// <summary>
    /// 邮寄
    /// </summary>
    public class Email
    {
        /// <summary>
        /// 邮寄类型
        /// </summary>
        public EmailTypeEnum type { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Message { get; set; }
    }
}
