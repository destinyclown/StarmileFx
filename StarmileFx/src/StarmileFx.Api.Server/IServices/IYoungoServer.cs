using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StarmileFx.Models;
using StarmileFx.Models.Enum;
using StarmileFx.Models.Redis;
using StarmileFx.Models.Wap;
using StarmileFx.Models.Youngo;
using static StarmileFx.Models.Wap.WapFrom;

namespace StarmileFx.Api.Server.IServices
{
    /// <summary>
    /// Youngo数据库接口
    /// </summary>
    public interface IYoungoServer
    {
        #region 基本操作

        /// <summary>
        /// Lambda获取实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : ModelBase;

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsCommit">是否提交</param>
        /// <returns></returns>
        bool Update<TEntity>(TEntity entity, bool IsCommit = true) where TEntity : ModelBase;

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsCommit">是否提交</param>
        /// <returns></returns>
        bool Add<TEntity>(TEntity entity, bool IsCommit = true) where TEntity : ModelBase;

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        bool AddRange(IEnumerable<object> entities);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        bool UpdateRange(IEnumerable<object> entities);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        bool RemoveRange(IEnumerable<object> entities);

        /// <summary>
        /// 获取整个列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderbyLambda"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IQueryable<TEntity> List<TEntity>(Expression<Func<TEntity, bool>> whereLambda,
            out int total) where TEntity : ModelBase;

        /// <summary>
        /// 获取整个列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderbyLambda"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IQueryable<TEntity> List<TEntity>(
            Expression<Func<TEntity, bool>> whereLambda, 
            Func<TEntity, object> orderbyLambda,
            out int total) where TEntity : ModelBase;

        /// <summary>  
        /// 分页查询 + 条件查询 + 排序  
        /// </summary>  
        /// <typeparam name="TEntity">泛型</typeparam>  
        /// <param name="pageData">分页实体</param>
        /// <param name="whereLambda">查询条件</param>  
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="total">总数量</param>  
        /// <returns>IQueryable 泛型集合</returns> 
        IQueryable<TEntity> PageData<TEntity>(
            PageData pageData, 
            Expression<Func<TEntity, bool>> whereLambda,
            Func<TEntity, object> orderbyLambda, 
            out int total
            ) where TEntity : ModelBase;

        #endregion 基本操作

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
        /// <param name="page"></param>
        /// <returns></returns>
        List<OrderParent> GetOrderParentcsList(OrderStateEnum OrderState, int CustomerId, PageData page);

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        bool CreateOrderParent(ShopCart shopCart);

        /// <summary>
        /// 取消订单（非物理删除）
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        bool CancelOrderParent(string OrderId);

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
        #endregion

        #region 网站后台

        #region Customer
        Customer GetCustomer(int id);

        bool AddCustomer(Customer Customer);

        bool ModifyCustomer(Customer Customer);

        bool DeleteCustomer(int id);
        #endregion

        #region CustomerComment
        CustomerComment GetCustomerComment(int id);

        bool AddCustomerComment(CustomerComment CustomerComment);

        bool ModifyCustomerComment(CustomerComment CustomerComment);

        bool DeleteCustomerComment(int id);
        #endregion

        #region CustomerSign
        CustomerSign GetCustomerSign(int id);

        bool AddCustomerSign(CustomerSign CustomerSign);

        bool ModifyCustomerSign(CustomerSign CustomerSign);

        bool DeleteCustomerSign(int id);
        #endregion

        #region DeliveryAddress
        DeliveryAddress GetDeliveryAddress(int id);

        bool AddDeliveryAddress(DeliveryAddress DeliveryAddress);

        bool ModifyDeliveryAddress(DeliveryAddress DeliveryAddress);

        bool DeleteDeliveryAddress(int id);
        #endregion

        #region OnLineOrderDetail
        OnLineOrderDetail GetOnLineOrderDetail(int id);

        bool AddOnLineOrderDetail(OnLineOrderDetail OnLineOrderDetail);

        bool ModifyOnLineOrderDetail(OnLineOrderDetail OnLineOrderDetail);

        bool DeleteOnLineOrderDetail(int id);
        #endregion

        #region OnLineOrderParent
        OnLineOrderParent GetOnLineOrderParent(string orderId);

        bool AddOnLineOrderParent(OnLineOrderParent OnLineOrderParent);

        bool ModifyOnLineOrderParent(OnLineOrderParent OnLineOrderParent);

        bool DeleteOnLineOrderParent(string orderId);
        #endregion

        #region OffLineOrderDetail
        OffLineOrderDetail GetOffLineOrderDetail(int id);

        bool AddOffLineOrderDetail(OffLineOrderDetail OffLineOrderDetail);

        bool ModifyOffLineOrderDetail(OffLineOrderDetail OffLineOrderDetail);

        bool DeleteOffLineOrderDetail(int id);
        #endregion

        #region OffLineOrderParent
        OffLineOrderParent GetOffLineOrderParent(string orderId);

        bool AddOffLineOrderParent(OffLineOrderParent OffLineOrderParent);

        bool ModifyOffLineOrderParent(OffLineOrderParent OffLineOrderParent);

        bool DeleteOffLineOrderParent(string orderId);
        #endregion

        #region OrderEstablish
        OrderEstablish GetOrderEstablish(int id);

        bool AddOrderEstablish(OrderEstablish OrderEstablish);

        bool ModifyOrderEstablish(OrderEstablish OrderEstablish);

        bool DeleteOrderEstablish(int id);
        #endregion

        #region Post
        Post GetPost(int id);

        bool AddPost(Post Post);

        bool ModifyPost(Post Post);

        bool DeletePost(int id);
        #endregion

        #region SKUEstablish
        SKUEstablish GetSKUEstablish(int id);

        bool AddSKUEstablish(SKUEstablish SKUEstablish);

        bool ModifySKUEstablish(SKUEstablish SKUEstablish);

        bool DeleteSKUEstablish(int id);
        #endregion

        #region ProductType
        ProductType GetProductType(int id);

        bool AddProductType(ProductType productType);

        bool ModifyProductType(ProductType productType);

        bool DeleteProductType(int id);
        #endregion

        #region Product
        Product GetProduct(string SKUProductCode);

        bool AddProduct(Product product);

        bool ModifyProduct(Product product);

        bool DeleteProduct(string SKUProductCode);
        #endregion

        #endregion
    }
}
