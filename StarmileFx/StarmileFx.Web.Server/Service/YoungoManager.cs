using Newtonsoft.Json;
using StarmileFx.Common;
using StarmileFx.Models;
using StarmileFx.Models.Web;
using StarmileFx.Models.Youngo;
using StarmileFx.Web.Server.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarmileFx.Web.Server.Service
{
    public class YoungoManager: IYoungoServer
    {
        private string Api_Host;
        HttpHelper httpHelper = new HttpHelper();
        public YoungoManager()
        {
            Api_Host = "http://localhost:8001/";//测试使用
            //Api_Host = "http://api.starmile.com.cn/";//线上测试
            //Api_Host = "https://api.starmile.com.cn/";//线上接口
        }

        public Task<ResponseResult<List<ProductWeb>>> GetProductList(ProductSearch search)
        {
            return Task.Run(() =>
            {
                return GetProductListAsync(search);
            });
        }

        public async Task<ResponseResult<List<ProductWeb>>> GetProductListAsync(ProductSearch search)
        {
            string Action = "Youngo";
            string Function = "/GetProductList";
            string Parameters = string.Empty;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select, search);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<List<ProductWeb>>>(result);
            });
        }
    }
}
