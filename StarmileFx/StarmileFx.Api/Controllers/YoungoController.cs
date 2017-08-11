﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Models;
using StarmileFx.Models.Enum;
using StarmileFx.Models.Redis;
using static StarmileFx.Models.Wap.WapFrom;
using StarmileFx.Models.Youngo;
using StarmileFx.Models.Web;
using StarmileFx.Api.FilterAttributes;

namespace StarmileFx.Api.Controllers
{
    public class YoungoController : BaseController
    {
        //依赖注入
        private readonly IYoungoServer _YoungoServer;

        public YoungoController(IYoungoServer IYoungoServer)
        {
            _YoungoServer = IYoungoServer;
        }

        #region 手机商城
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public string GetCacheProductList()
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetCacheProductList();
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="OrderState"></param>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [ValidateParmeterTypeOf("OrderState", typeof(OrderStateEnum))]
        [ValidateParmeterTypeOf("CustomerId", typeof(int))]
        public string GetOrderParentcsList(OrderStateEnum OrderState, int CustomerId, int PageSize, int PageIndex)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetOrderParentcsList(OrderState, CustomerId, PageSize, PageIndex);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [ValidateParmeterNull("OrderId")]
        public string GetOrderParent(string OrderId)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetOrderParent(OrderId);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="shopCart">购物车</param>
        /// <returns></returns>
        [HttpPost]
        public string OrderCreate([FromForm]ShopCart shopCart)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.OrderCreate(shopCart);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 订单确认（支付）
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="TransactionId">交易编号</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateParmeterNull("OrderId,TransactionId")]
        public string OrderPay(string OrderId, string TransactionId)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.OrderPay(OrderId, TransactionId);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 取消订单（非物理删除）
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="IsDelete">是否删除</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateParmeterNull("OrderId")]
        public string OrderCancel(string OrderId, bool IsDelete = false)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.OrderCancel(OrderId, IsDelete);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 完成订单（确认收货）
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateParmeterNull("OrderId")]
        public string OrderComplete(string OrderId)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.OrderComplete(OrderId);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="WeCharKey"></param>
        /// <returns></returns>
        [ValidateParmeterNull("WeCharKey")]
        public string GetCustomer(string WeCharKey)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetCustomer(WeCharKey);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 获取默认地址
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [ValidateParmeterTypeOf("CustomerId", typeof(int))]
        public string GetDefaultAddress(int CustomerId)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetDefaultAddress(CustomerId);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 获取地址列表
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [ValidateParmeterTypeOf("CustomerId", typeof(int))]
        public string GetDeliveryAddressList(int CustomerId)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetDeliveryAddressList(CustomerId);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 获取商品资源
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [ValidateParmeterNull("ProductId")]
        public string GetProductResources(string ProductId)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetProductResources(ProductId);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 提交评论
        /// </summary>
        /// <param name="CommentFrom"></param>
        /// <returns></returns>
        [HttpPost]
        public string SubmitComment([FromForm]CommentFrom CommentFrom)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.SubmitComment(CommentFrom);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 提交图片资源
        /// </summary>
        /// <param name="CommentFrom"></param>
        /// <returns></returns>
        [HttpPost]
        public string SubmitResources([FromForm]Resources ResourcesFrom)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.SubmitResources(ResourcesFrom);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 提交地址
        /// </summary>
        /// <param name="DeliveryAddressFrom"></param>
        /// <returns></returns>
        [HttpPost]
        public string SubmitDeliveryAddress([FromForm]DeliveryAddressFrom DeliveryAddressFrom)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.SubmitDeliveryAddress(DeliveryAddressFrom);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [ValidateParmeterTypeOf("CustomerId", typeof(int))]
        [ValidateParmeterTypeOf("PageSize", typeof(int))]
        [ValidateParmeterTypeOf("PageIndex", typeof(int))]
        public string GetMessageList(int CustomerId, int PageSize, int PageIndex)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetMessageList(CustomerId, PageSize, PageIndex);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 提交反馈意见
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public string SubmitFeedback([FromForm]FeedbackFrom from)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.SubmitFeedback(from);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
        #endregion 手机商城

        #region 网站后台
        /// <summary>
        /// 查询商品
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public string GetProductList([FromForm]ProductSearch from)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                int total = 0;
                responseModel.Content = _YoungoServer.GetProductList(from, out total);
                responseModel.total = total;
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
        #endregion 网站后台
    }
}
