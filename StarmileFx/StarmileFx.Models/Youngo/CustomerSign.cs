﻿using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 积分记录
    /// </summary>
    [SugarTable("CustomerSign")]
    public class CustomerSign : ModelBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }
        /// <summary>
        /// 来源方式
        /// </summary>
        public string Mode { get; set; }
    }
}
