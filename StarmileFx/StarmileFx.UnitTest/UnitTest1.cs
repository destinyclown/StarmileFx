using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarmileFx.Web.Server.IServices;
using StarmileFx.Common.Encryption;
using StarmileFx.Models;
using StarmileFx.Models.Web;
using System.Threading.Tasks;

namespace StarmileFx.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IBaseServer _BaseServer;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="IBaseServer"></param>
        public UnitTest1(IBaseServer IBaseServer)
        {
            _BaseServer = IBaseServer;
        }
        [TestMethod]
        public void TestMethod1()
        {
            string str = "erewr";
            str = str.Length > 200 ? str.Substring(0, 200) : str;
        }

        [TestMethod]
        public async Task LoginTestAsync()
        {
            LoginFrom FromData = new LoginFrom
            {
                Email = "starmilefx@163.com",
                Password = "123456",
                Ip = ""
            };
            FromData.Password = Encryption.ToMd5(FromData.Password);
            ResponseResult responseResult = await _BaseServer.Login(FromData);

        }
    }
}
