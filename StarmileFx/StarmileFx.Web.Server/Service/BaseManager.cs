using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StarmileFx.Common;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using StarmileFx.Models.Json;
using StarmileFx.Web.Server.IServices;
using Microsoft.Extensions.Options;
using StarmileFx.Models.Web;

namespace StarmileFx.Web.Server.Services
{
    /// <summary>
    /// 接口实现
    /// </summary>
    public class BaseManager : IBaseServer
    {
        private string Api_Host;
        HttpHelper httpHelper = new HttpHelper();
        private readonly IOptions<WebConfig> _WebConfig;
        public BaseManager(IOptions<WebConfig> WebConfig)
        {
            //Api_Host = "http://localhost:8001/";//测试使用
            //Api_Host = "http://api.starmile.com.cn/";//线上测试
            //Api_Host = "https://api.starmile.com.cn/";//线上接口
            _WebConfig = WebConfig;
            Api_Host = _WebConfig.Value.IsTest ? _WebConfig.Value.TestApiHost : _WebConfig.Value.ApiHost;
        }

        public Task<ResponseResult<bool>> RefreshToken(string Token)
        {
            return Task.Run(() =>
            {
                return RefreshTokenAsync(Token);
            });
        }

        public async Task<ResponseResult<bool>> RefreshTokenAsync(string Token)
        {
            string Action = "Api";
            string Function = "/RefreshToken";
            string Parameters = string.Format(@"Token={0}", Token);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<bool>>(result);
            });
        }

        public Task<ResponseResult<Result>> Login(LoginFrom fromData)
        {
            return Task.Run(() =>
            {
                return LoginAsync(fromData);
            });
        }

        public async Task<ResponseResult<Result>> LoginAsync(LoginFrom fromData)
        {
            string Action = "Api";
            string Function = "/Login";
            string Parameters = string.Empty;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select, fromData);
            return await Task.Run(() =>
           {
               return JsonConvert.DeserializeObject<ResponseResult<Result>>(result);
           });
        }

        public Task<ResponseResult<Result>> Logout(string Token)
        {
            return Task.Run(() =>
            {
                return LogoutAsync(Token);
            });
        }

        public async Task<ResponseResult<Result>> LogoutAsync(string Token)
        {
            string Action = "Api";
            string Function = "/Logout";
            string Parameters = string.Format(@"Token={0}", Token);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<Result>>(result);
            });
        }

        public Task<ResponseResult<List<WebMenus>>> GetMenuJson(string Token)
        {
            return Task.Run(() =>
            {
                return GetMenuJsonAsync(Token);
            });
        }

        public async Task<ResponseResult<List<WebMenus>>> GetMenuJsonAsync(string Token)
        {
            string Action = "Api";
            string Function = "/LoadMenuByRole";
            string Parameters = string.Format(@"Token={0}", Token);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<List<WebMenus>>>(result);
            });
        }

        public Task<ResponseResult<List<SysCollection>>> GetCollectionList(string Token)
        {
            return Task.Run(() =>
            {
                return GetCollectionListAsync(Token);
            });
        }

        public async Task<ResponseResult<List<SysCollection>>> GetCollectionListAsync(string Token)
        {
            string Action = "Api";
            string Function = "/GetCollectionList";
            string Parameters = string.Format(@"Token={0}", Token);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<List<SysCollection>>>(result);
            });
        }

        public Task<ResponseResult<Result>> ConfirmCollection(WebCollection fromData)
        {
            return Task.Run(() =>
            {
                return ConfirmCollectionAsync(fromData);
            });
        }

        public async Task<ResponseResult<Result>> ConfirmCollectionAsync(WebCollection fromData)
        {
            string Action = "Api";
            string Function = "/ConfirmCollection";
            string Parameters = string.Empty;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select, fromData);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<Result>>(result);
            });
        }

        public Task<ResponseResult<Result>> CancelCollection(WebCollection fromData)
        {
            return Task.Run(() =>
            {
                return CancelCollectionAsync(fromData);
            });
        }

        public async Task<ResponseResult<Result>> CancelCollectionAsync(WebCollection fromData)
        {
            string Action = "Api";
            string Function = "/CancelCollection";
            string Parameters = string.Empty;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select, fromData);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<Result>>(result);
            });
        }
    }
}
