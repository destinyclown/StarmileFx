using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Models;
using StarmileFx.Models.Enum;
using StarmileFx.Models.Redis;
using static StarmileFx.Models.Wap.WapFrom;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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
        /// <param name="page"></param>
        /// <returns></returns>
        public string GetOrderParentcsList(OrderStateEnum OrderState, int CustomerId, PageData page)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetOrderParentcsList(OrderState, CustomerId, page);
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
        /// <param name="orderId">订单编号</param>
        /// <param name="IsDelet">是否删除</param>
        /// <returns></returns>
        [HttpPost]
        public string OrderCancel(string orderId, bool IsDelet = false)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.OrderCancel(orderId, IsDelet);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 完成订单（确认收货）
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        [HttpPost]
        public string OrderComplete(string orderId)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.OrderComplete(orderId);
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
        /// 获取默认地址
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetMessageList(int CustomerId, [FromForm]PageData page)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetMessageList(CustomerId, page);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
    }
}
