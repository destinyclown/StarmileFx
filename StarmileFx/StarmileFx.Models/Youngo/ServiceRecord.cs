﻿using StarmileFx.Models.Enum;
using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 售后记录
    /// </summary>
    [SugarTable("ServiceRecord")]
    public class ServiceRecord : ModelBase
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 售后类型
        /// </summary>
        public ServiceTypeEnum Type { get; set; }
        /// <summary>
        /// 内容说明
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 是否处理
        /// </summary>
        public bool IsHandle { get; set; }
    }
}
