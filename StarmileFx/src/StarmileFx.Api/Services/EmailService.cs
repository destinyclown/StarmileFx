using StarmileFx.Common;
using StarmileFx.Models.Enum;

namespace StarmileFx.Api.Services
{
    public static class EmailService
    {
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
            MailHelper.Send(AdminEmail, MessageType.ToString(), message);
            //发送信息给Youngo管理员
            MailHelper.Send(YoungoEmail, MessageType.ToString(), message);
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
            MailHelper.Send(AdminEmail, "StarmileFx.Api系统出错", message);
        }
    }
}
