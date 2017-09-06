using StarmileFx.Models.Enum;
using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 消息类
    /// </summary>
    [SugarTable("Information")]
    public class Information : ModelBase
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageTypeEnum MessageType { get; set; }
    }
}
