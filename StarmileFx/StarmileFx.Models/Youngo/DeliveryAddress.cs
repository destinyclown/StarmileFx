using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 收货地址
    /// </summary>
    public class DeliveryAddress : ModelBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 接收人名称
        /// </summary>
        public string ReceiveName { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
