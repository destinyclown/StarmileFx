﻿using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StarmileFx.Common;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using StarmileFx.Models.Json;
using YoungoFx.Web.Server.IServices;
using static StarmileFx.Models.Web.HomeFromModel;

namespace YoungoFx.Web.Server.Services
{
    /// <summary>
    /// 接口实现
    /// </summary>
    public class BaseManager : IBaseServer
    {
        private string Api_Host;
        HttpHelper httpHelper = new HttpHelper();
        public BaseManager()
        {
            Api_Host = "http://localhost:8001/";//测试使用
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
            string Function = "/LoginAsync";
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
            string Function = "/LogoutAsync";
            string Parameters = string.Format(@"Token={0}", Token);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<Result>>(result);
            });
        }

        public Task<ResponseResult<SysMenusModel>> LoadMenuByRole(string Token)
        {
            return Task.Run(() =>
            {
                return LoadMenuByRoleAsync(Token);
            });
        }

        public async Task<ResponseResult<SysMenusModel>> LoadMenuByRoleAsync(string Token)
        {
            string Action = "Api";
            string Function = "/LoadMenuByRoleAsync";
            string Parameters = string.Format(@"Token={0}", Token);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<SysMenusModel>>(result);
            });
        }
    }
}
