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
        #region 商品管理
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

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="product">购物车</param>
        /// <returns></returns>
        [HttpPost]
        public string AddProduct([FromForm]Product product)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.AddProduct(product);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="product">购物车</param>
        /// <returns></returns>
        [HttpPost]
        public string ModifyProduct([FromForm]Product product)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.ModifyProduct(product);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ValidateParmeterTypeOf("Id", typeof(int))]
        public string GetProduct(int Id)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetProduct(Id);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateParmeterTypeOf("Id", typeof(int))]
        public string DeleteProduct(int Id)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.DeleteProduct(Id);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 批量删除商品
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateParmeterTypeOf("Ids", typeof(int[]))]
        public string BatchDeleteProduct(int[] ProductIds)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.BatchDeleteProduct(ProductIds);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
        #endregion

        #region 商品类型管理
        /// <summary>
        /// 查询商品类型
        /// </summary>
        /// <returns></returns>
        public string GetProductTypeList()
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                int total = 0;
                responseModel.Content = _YoungoServer.GetProductTypeList(out total);
                responseModel.total = total;
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 添加商品类型
        /// </summary>
        /// <param name="productType">购物车</param>
        /// <returns></returns>
        [HttpPost]
        public string AddProductType([FromForm]ProductType productType)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.AddProductType(productType);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 修改商品类型
        /// </summary>
        /// <param name="productType">购物车</param>
        /// <returns></returns>
        [HttpPost]
        public string ModifyProductType([FromForm]ProductType productType)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.ModifyProductType(productType);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 获取商品类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ValidateParmeterTypeOf("Id", typeof(int))]
        public string GetProductType(int Id)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.GetProductType(Id);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateParmeterTypeOf("Id", typeof(int))]
        public string DeleteProductType(int Id)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.DeleteProductType(Id);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 批量删除商品
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateParmeterTypeOf("Ids", typeof(int[]))]
        public string BatchDeleteProductType(int[] Ids)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.BatchDeleteProductType(Ids);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
        #endregion

        #region 网站资源管理
        /// <summary>
        /// 获取网站资源列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ValidateParmeterNull("ProductId")]
        [ValidateParmeterTypeOf("Type", typeof(ResourcesEnum))]
        public string GetResourcesList(string ProductId, ResourcesEnum Type)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                int total = 0;
                responseModel.Content = _YoungoServer.GetResourcesList(ProductId, Type, out total);
                responseModel.total = total;
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 获取网站资源列表
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="Type"></param>
        /// <param name="Addresses"></param>
        /// <param name="Sorts"></param>
        /// <returns></returns>
        [ValidateParmeterNull("ProductId")]
        [ValidateParmeterTypeOf("Type", typeof(ResourcesEnum))]
        [ValidateParmeterTypeOf("Addresses", typeof(string[]))]
        [ValidateParmeterTypeOf("Sorts", typeof(int[]))]
        public string AddResources(string ProductId, ResourcesEnum Type, string[] Addresses, int[] Sorts)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                int total = 0;
                responseModel.Content = _YoungoServer.AddResources(ProductId, Type, Addresses, Sorts);
                responseModel.total = total;
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 删除网站资源
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateParmeterNull("ProductId")]
        [ValidateParmeterTypeOf("Type", typeof(ResourcesEnum))]
        public string BatchDeleteResources(string ProductId, ResourcesEnum Type)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.BatchDeleteResources(ProductId, Type);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 删除网站资源
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ValidateParmeterTypeOf("Id", typeof(int))]
        public string DeleteResources(int Id)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _YoungoServer.DeleteResources(Id);
                responseModel.IsSuccess = true;
                responseModel.ErrorMsg = "";
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
        #endregion

        #endregion 网站后台
    }
}
