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
            //Api_Host = "http://localhost:8001/";//测试使用
            //Api_Host = "http://api.starmile.com.cn/";//线上测试
            Api_Host = "https://api.starmile.com.cn/";//线上接口
        }

        #region 商品管理
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

        public Task<ResponseResult<bool>> AddProduct(Product product)
        {
            return Task.Run(() =>
            {
                return AddProductAsync(product);
            });
        }

        public async Task<ResponseResult<bool>> AddProductAsync(Product product)
        {
            string Action = "Youngo";
            string Function = "/AddProduct";
            string Parameters = string.Empty;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select, product);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<bool>>(result);
            });
        }

        public Task<ResponseResult<bool>> ModifyProduct(Product product)
        {
            return Task.Run(() =>
            {
                return ModifyProductAsync(product);
            });
        }

        public async Task<ResponseResult<bool>> ModifyProductAsync(Product product)
        {
            string Action = "Youngo";
            string Function = "/ModifyProduct";
            string Parameters = string.Empty;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select, product);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<bool>>(result);
            });
        }

        public Task<ResponseResult<Product>> GetProduct(int Id)
        {
            return Task.Run(() =>
            {
                return GetProductAsync(Id);
            });
        }

        public async Task<ResponseResult<Product>> GetProductAsync(int Id)
        {
            string Action = "Youngo";
            string Function = "/GetProduct";
            string Parameters = string.Format("Id={0}", Id);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<Product>>(result);
            });
        }

        public Task<ResponseResult<bool>> BatchDeleteProduct(int[] Ids)
        {
            return Task.Run(() =>
            {
                return BatchDeleteProductAsync(Ids);
            });
        }

        public async Task<ResponseResult<bool>> BatchDeleteProductAsync(int[] Ids)
        {
            string Action = "Youngo";
            string Function = "/BatchDeleteProduct";
            string Parameters = string.Format("Ids={0}", Ids);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<bool>>(result);
            });
        }
        public Task<ResponseResult<bool>> DeleteProduct(int Id)
        {
            return Task.Run(() =>
            {
                return DeleteProductAsync(Id);
            });
        }

        public async Task<ResponseResult<bool>> DeleteProductAsync(int Id)
        {
            string Action = "Youngo";
            string Function = "/DeleteProduct";
            string Parameters = string.Format("Id={0}", Id);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<bool>>(result);
            });
        }
        #endregion
    }
}
