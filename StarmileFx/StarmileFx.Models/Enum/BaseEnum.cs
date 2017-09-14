using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Enum
{
    public class BaseEnum
    {
        public enum EmailTypeEnum : int
        {
            Error = 1,
            Youngo = 2,
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public class ErrorCode
        {
            /// <summary>
            /// 数据错误
            /// </summary>
            public const string DataError = "0x100000";

            /// <summary>
            /// 参数错误
            /// </summary>
            public const string ParameterError = "0x100001";

            /// <summary>
            /// 令牌错误
            /// </summary>
            public const string TokenError = "0x100002";

            /// <summary>
            /// 认证错误
            /// </summary>
            public const string AuthorizationError = "0x100003";

            /// <summary>
            /// 签名错误
            /// </summary>
            public const string SignError = "0x100004";

            /// <summary>
            /// 系统错误
            /// </summary>
            public const string SystemError = "0xFFF000";
        }
    }
}
