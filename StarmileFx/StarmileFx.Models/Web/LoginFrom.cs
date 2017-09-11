using System;
using System.Collections.Generic;
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
        [Required(ErrorMessage = "请输入登录邮箱！")]
        [EmailAddress(ErrorMessage = "请输入正确的邮箱！")]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入登录密码！")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        /// <summary>
        /// 记住用户
        /// </summary>
        [Display(Name = "记住密码")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        public string Ip { get; set; }
    }
}
