using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Web
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginFrom
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [Description("邮箱")]
        [Required(ErrorMessage = "请输入登录邮箱！")]
        [EmailAddress(ErrorMessage = "请输入正确的邮箱！")]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        [Required(ErrorMessage = "请输入登录密码！")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// 记住用户
        /// </summary>
        [Description("记住用户")]
        [Display(Name = "记住密码")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        [Description("Ip地址")]
        public string Ip { get; set; }
    }
}
