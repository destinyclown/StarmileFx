using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Enum;

namespace StarmileFx.Wap.Services
{
    /// <summary>
    /// VIP服务
    /// </summary>
    public class VipService
    {
        /// <summary>
        /// 根据积分获取VIP类型
        /// </summary>
        /// <param name="Integral"></param>
        /// <returns></returns>
        public static string GetVip(int Integral)
        {
            switch (Integral)
            {
                case (int)VipTypeEnum.V1:
                    return "黄金会员V1";
                case (int)VipTypeEnum.V2:
                    return "黄金会员V2";
                case (int)VipTypeEnum.V3:
                    return "黄金会员V3";
                case (int)VipTypeEnum.V4:
                    return "黄金会员V4";
                case (int)VipTypeEnum.V5:
                    return "黄金会员V5";
                default:
                    return "普通会员";
            }
        }
    }
}
