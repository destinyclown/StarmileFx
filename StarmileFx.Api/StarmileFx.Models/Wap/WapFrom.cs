using StarmileFx.Models.Enum;

namespace StarmileFx.Models.Wap
{
    public class WapFrom
    {
        /// <summary>
        /// 评论提交类
        /// </summary>
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

        /// <summary>
        /// 地址提交类
        /// </summary>
        public class DeliveryAddressFrom
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
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

        /// <summary>
        /// 购物车提交类
        /// </summary>
        public class ShoppingCartFrom
        {
            /// <summary>
            /// 用户编号
            /// </summary>
            public int CustomerID { get; set; }
            /// <summary>
            /// 是否勾选
            /// </summary>
            public string CartCheck { get; set; }
            /// <summary>
            /// 商品ID（SKU）
            /// </summary>
            public string ProductID { get; set; }
            /// <summary>
            /// 商品数量
            /// </summary>
            public string Number { get; set; }
        }

        /// <summary>
        /// 意见反馈提交类
        /// </summary>
        public class FeedbackFrom
        {
            /// <summary>
            /// 验证码
            /// </summary>
            public string ValidCode { get; set; }
            /// <summary>
            /// 意见类型
            /// </summary>
            public FeedbackTypeEnum Type { get; set; }
            /// <summary>
            /// 反馈内容
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// 电话
            /// </summary>
            public string Phone { get; set; }
        }
    }
}
