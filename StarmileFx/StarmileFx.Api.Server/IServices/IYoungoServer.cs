﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StarmileFx.Models;
using StarmileFx.Models.Enum;
using StarmileFx.Models.Redis;
using StarmileFx.Models.Wap;
using StarmileFx.Models.Youngo;
using static StarmileFx.Models.Wap.WapFrom;
using StarmileFx.Models.Web;

namespace StarmileFx.Api.Server.IServices
{
    /// <summary>
    /// Youngo数据库接口
    /// </summary>
    public interface IYoungoServer
    {
        #region 手机商城
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        CacheProductList GetCacheProductList();

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="WeCharKey"></param>
        /// <returns></returns>
        Customer GetCustomer(string WeCharKey);

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="OrderState"></param>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        List<OrderParent> GetOrderParentcsList(OrderStateEnum OrderState, int CustomerId, int PageSize, int PageIndex);

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        List<OrderParent> GetOrderParent(string orderId);

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        bool OrderCreate(ShopCart shopCart);

        /// <summary>
        /// 取消订单（非物理删除）
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="IsDelet">是否删除</param>
        /// <returns></returns>
        bool OrderCancel(string OrderId, bool IsDelet = false);

        /// <summary>
        /// 订单确认（支付）
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="TransactionId">交易编号</param>
        /// <returns></returns>
        bool OrderPay(string OrderId, string TransactionId);

        /// <summary>
        /// 完成订单（确认收货）
        /// </summary>
        /// <param name="OrederId"></param>
        /// <returns></returns>
        bool OrderComplete(string OrederId);

        /// <summary>
        /// 会员积分变动记录
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="signEnum"></param>
        /// <returns></returns>
        bool ChangeCustomerSign(int CustomerId, SignEnum signEnum);

        /// <summary>
        /// 提交评论
        /// </summary>
        /// <param name="CommentFrom"></param>
        /// <returns></returns>
        bool SubmitComment(CommentFrom CommentFrom);

        /// <summary>
        /// 提交地址
        /// </summary>
        /// <param name="DeliveryAddressFrom"></param>
        /// <returns></returns>
        bool SubmitDeliveryAddress(DeliveryAddressFrom DeliveryAddressFrom);

        /// <summary>
        /// 获取地址列表
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        List<DeliveryAddress> GetDeliveryAddressList(int CustomerId);

        /// <summary>
        /// 获取默认地址
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        DeliveryAddress GetDefaultAddress(int CustomerId);

        /// <summary>
        /// 提交反馈意见
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        bool SubmitFeedback(FeedbackFrom from);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="OrederId"></param>
        /// <param name="MessageType"></param>
        /// <returns></returns>
        bool SendMessage(int CustomerId, string OrederId, MessageTypeEnum MessageType);

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        List<Information> GetMessageList(int CustomerId, int PageSize, int PageIndex);

        /// <summary>
        /// 订单申请售后
        /// </summary>
        /// <param name="OrederId"></param>
        /// <param name="Type"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        bool OrderServiceApply(string OrederId, ServiceTypeEnum Type, string Content);

        /// <summary>
        /// 获取商品资源
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductResources GetProductResources(string productId);

        /// <summary>
        /// 添加图片资源
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool SubmitResources(Resources list);
        #endregion

        #region 网站后台

        #region 商品管理
        /// <summary>
        /// 查询商品
        /// </summary>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<ProductWeb> GetProductList(ProductSearch search, out int total);

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProduct(int id);

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        bool AddProduct(Product product);

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        bool ModifyProduct(Product product);

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteProduct(int id);

        /// <summary>
        /// 批量删除商品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDeleteProduct(int[] ids);
        #endregion

        #region 商品类型管理
        /// <summary>
        /// 获取商品类型列表
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        List<ProductType> GetProductTypeList();

        /// <summary>
        /// 获取商品类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductType GetProductType(int id);

        /// <summary>
        /// 添加商品类型
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        bool AddProductType(ProductType productType);

        /// <summary>
        /// 修改商品类型
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        bool ModifyProductType(ProductType productType);

        /// <summary>
        /// 删除商品类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteProductType(int id);

        /// <summary>
        /// 批量删除商品类型
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDeleteProductType(int[] ids);
        #endregion

        #region 网站资源管理
        /// <summary>
        /// 获取网站资源列表
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="Type"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<Resources> GetResourcesList(string ProductId, ResourcesEnum Type);

        /// <summary>
        /// 添加网站资源
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="Type"></param>
        /// <param name="Addresses"></param>
        /// <param name="Sorts"></param>
        /// <returns></returns>
        bool AddResources(string ProductId, ResourcesEnum Type, string[] Addresses, int[] Sorts);

        /// <summary>
        /// 删除网站资源
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteResources(int Id);

        /// <summary>
        /// 批量删除网站资源
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        bool BatchDeleteResources(string ProductId, ResourcesEnum Type);
        #endregion
        #endregion
    }
}
