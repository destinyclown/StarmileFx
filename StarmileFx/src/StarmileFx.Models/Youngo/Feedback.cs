﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Enum;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 意见反馈
    /// </summary>
    public class Feedback : ModelBase
    {
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
