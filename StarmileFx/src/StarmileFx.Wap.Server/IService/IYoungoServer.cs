using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models;
using StarmileFx.Models.Enum;
using StarmileFx.Models.Redis;
using StarmileFx.Models.Wap;
using StarmileFx.Models.Youngo;

namespace StarmileFx.Wap.Server.IService
{
    public interface IYoungoServer
    {
        /// <summary>
        /// 创建订单编号
        /// </summary>
        /// <returns></returns>
        string CreateOrderID();
        /// <summary>
        /// 判断是否存在购物车
        /// </summary>
        /// <param name="custmoerId"></param>
        /// <returns></returns>
        bool IsExistenceCart(int custmoerId);
        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        ShopCart GetShopCart(int CustomerID);
        /// <summary>
        /// 创建购物车
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        bool CreateShopCart(ShopCart shopCart);
        /// <summary>
        /// 修改购物车
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        bool ModifyShopCart(ShopCart shopCart);
        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        bool ClearShopCart(int CustomerID);
        /// <summary>
        /// 获取缓存中的商品列表
        /// </summary>
        /// <returns></returns>
        Task<CacheProductList> GetCacheProductList();
        /// <summary>
        /// api获取商品列表
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<CacheProductList>> GetProductList();
        /// <summary>
        /// 获取默认地址
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        Task<ResponseResult<DeliveryAddress>> GetDefaultAddress(int CustomerId);
        /// <summary>
        /// 获取默认地址
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        Task<ResponseResult<List<DeliveryAddress>>> GetDeliveryAddressList(int CustomerId);
        /// <summary>
        /// 确认订单（付款）
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        Task<ResponseResult<bool>> OrderPay(string orderId, string TransactionId);
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        Task<ResponseResult<bool>> OrderCreate(ShopCart shopCart);
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<ResponseResult<bool>> OrderCancel(string orderId);
        /// <summary>
        /// 删除订单（非物理删除）
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        Task<ResponseResult<bool>> OrderDelete(string orderId);
        /// <summary>
        /// 完成订单（确认收货）
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        Task<ResponseResult<bool>> OrderComplete(string orderId);
        /// <summary>
        /// 创建临时购物车
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        bool CreateTemporaryShopCart(ShopCart shopCart);
        /// <summary>
        /// 获取临时购物车
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        ShopCart GetTemporaryShopCart(int CustomerID);
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="OrderState"></param>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        Task<ResponseResult<List<OrderParent>>> GetOrderParentcsList(OrderStateEnum OrderState, int CustomerId, int PageSize, int PageIndex);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="WeCharKey"></param>
        /// <returns></returns>
        Task<ResponseResult<Customer>> GetCustomer(string WeCharKey);
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        Task<ResponseResult<List<Information>>> GetMessageList(int CustomerId, int PageSize, int PageIndex);
    }
}
