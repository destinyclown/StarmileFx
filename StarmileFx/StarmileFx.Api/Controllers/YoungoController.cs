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
        //[Route("")]
        [ResponseCache(VaryByHeader = "Accept-Encoding", Location = ResponseCacheLocation.Client, Duration = 10)]
        public string GetCacheProductList()
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetCacheProductList(),
                    IsSuccess = true, 
                };
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
        //[Route("GetOrderParentcsList/{CustomerId:int}")]
        public string GetOrderParentcsList(OrderStateEnum OrderState, int CustomerId, int PageSize, int PageIndex)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetOrderParentcsList(OrderState, CustomerId, PageSize, PageIndex),
                    IsSuccess = true, 
                };
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
        //[Route("GetOrderParent/OrderId={OrderId}")]
        public string GetOrderParent(string OrderId)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetOrderParent(OrderId),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.OrderCreate(shopCart),
                    IsSuccess = true,
                    
                };
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
        //[Route("OrderPay/OrderId={OrderId}&TransactionId={TransactionId}")]
        public string OrderPay(string OrderId, string TransactionId)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.OrderPay(OrderId, TransactionId),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.OrderCancel(OrderId, IsDelete),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.OrderComplete(OrderId),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetCustomer(WeCharKey),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetDefaultAddress(CustomerId),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetDeliveryAddressList(CustomerId),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetProductResources(ProductId),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.SubmitComment(CommentFrom),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.SubmitResources(ResourcesFrom),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.SubmitDeliveryAddress(DeliveryAddressFrom),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetMessageList(CustomerId, PageSize, PageIndex),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.SubmitFeedback(from),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = new PageResult<List<ProductWeb>>
                    {
                        Data = _YoungoServer.GetProductList(from, out int total),
                        Total = total
                    },
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.AddProduct(product),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.ModifyProduct(product),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetProduct(Id),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.DeleteProduct(Id),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.BatchDeleteProduct(ProductIds),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetProductTypeList(),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.AddProductType(productType),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.ModifyProductType(productType),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetProductType(Id),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.DeleteProductType(Id),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.BatchDeleteProductType(Ids),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.GetResourcesList(ProductId, Type),
                    IsSuccess = true,
                    
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.AddResources(ProductId, Type, Addresses, Sorts),
                    IsSuccess = true
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.BatchDeleteResources(ProductId, Type),
                    IsSuccess = true,
                };
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
                var responseModel = new ResponseResult
                {
                    Data = _YoungoServer.DeleteResources(Id),
                    IsSuccess = true,
                };
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
        #endregion

        #endregion 网站后台
    }
}
