using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Wap
{
    public class WapFrom
    {
        public class CommentFrom
        {
            /// <summary>
            /// 用户ID
            /// </summary>
            public int CustomerID { get; set; }
            /// <summary>
            /// 订单编号
            /// </summary>
            public string OrderID { get; set; }
            /// <summary>
            /// 商品ID（SKU）
            /// </summary>
            public string ProductID { get; set; }
            /// <summary>
            /// 评论
            /// </summary>
            public string Comment { get; set; }
            /// <summary>
            /// 回复ID
            /// </summary>
            public int? Reply { get; set; }
        }

        public class DeliveryAddressFrom
        {
            /// <summary>
            /// 
            /// </summary>
            public int ID { get; set; }
            /// <summary>
            /// 用户ID
            /// </summary>
            public int CustomerID { get; set; }
            /// <summary>
            /// 接收人名称
            /// </summary>
            public string ReceiveName { get; set; }
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
            /// <summary>
            /// 是否修改
            /// </summary>
            public bool IsModify { get; set; }
        }
    }
}
