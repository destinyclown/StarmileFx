using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StarmileFx.Api.Server.Data;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Models;
using StarmileFx.Models.H5;
using StarmileFx.Models.Youngo;
using StarmileFx.Models.Enum;

namespace StarmileFx.Api.Server.Services
{
    /// <summary>
    /// 接口实现
    /// </summary>
    public class YoungoManager : IYoungoServer
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        private YoungoContext _DataContext;

        public YoungoManager(YoungoContext DataContext)
        {
            _DataContext = DataContext;
        }

        #region 基本数据库处理
        #region 实体处理

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        private bool Commit()
        {
            return _DataContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Lambda获取实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : ModelBase
        {
            return _DataContext.Get<TEntity>(whereLambda);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsCommit">是否提交</param>
        /// <returns></returns>
        public bool Update<TEntity>(TEntity entity, bool IsCommit = true) where TEntity : ModelBase
        {
            _DataContext.Update<TEntity>(entity);
            if (IsCommit)
            {
                return Commit();
            }
            return false;
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsCommit">是否提交</param>
        /// <returns></returns>
        public bool Add<TEntity>(TEntity entity, bool IsCommit = true) where TEntity : ModelBase
        {
            _DataContext.Add<TEntity>(entity);
            if (IsCommit)
            {
                return Commit();
            }
            return false;
        }

        #endregion 实体处理

        #region 批处理

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool AddRange(IEnumerable<object> entities)
        {
            _DataContext.AddRange(entities);
            return Commit();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool UpdateRange(IEnumerable<object> entities)
        {
            _DataContext.UpdateRange(entities);
            return Commit();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool RemoveRange(IEnumerable<object> entities)
        {
            _DataContext.RemoveRange(entities);
            return Commit();
        }

        #endregion

        #region 获取列表

        /// <summary>
        /// 获取整个列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderbyLambda"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<TEntity> List<TEntity>(Expression<Func<TEntity, bool>> whereLambda,
            out int total) where TEntity : ModelBase
        {
            return _DataContext.List<TEntity>(whereLambda, out total);
        }

        /// <summary>
        /// 获取整个列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderbyLambda"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<TEntity> List<TEntity>(
            Expression<Func<TEntity, bool>> whereLambda, 
            Func<TEntity, object> orderbyLambda,
            out int total) where TEntity : ModelBase
        {
            return _DataContext.List<TEntity>(whereLambda, orderbyLambda, out total);
        }

        /// <summary>  
        /// 分页查询 + 条件查询 + 排序  
        /// </summary>  
        /// <typeparam name="TEntity">泛型</typeparam>  
        /// <param name="pageData">分页实体</param>
        /// <param name="whereLambda">查询条件</param>  
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="total">总数量</param>  
        /// <returns>IQueryable 泛型集合</returns>  
        public IQueryable<TEntity> PageData<TEntity>(
            PageData pageData, 
            Expression<Func<TEntity, bool>> whereLambda,
            Func<TEntity, object> orderbyLambda, 
            out int total
            ) where TEntity : ModelBase
        {
            return _DataContext.PageData<TEntity>(pageData, whereLambda, orderbyLambda, out total);
        }
        #endregion 获取列表
        #endregion

        #region 手机商城
        /// <summary>
        /// 首页商品列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<IndexProduct> IndexProductList(PageData page)
        {
            List<IndexProduct> indexProductList = new List<IndexProduct>();
            int total = 0;
            indexProductList = PageData<IndexProduct>(page, null, null, out total).ToList();
            return indexProductList;
        }

        /// <summary>
        /// 搜索页商品列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<SearchProduct> SearchProductList(string keyword, PageData page)
        {
            List<SearchProduct> searchProductList = new List<SearchProduct>();
            return searchProductList;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="WeCharKey"></param>
        /// <returns></returns>
        public Customer GetCustomer(string WeCharKey)
        {
            Customer customer = new Customer();
            return customer;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public List<OrderParent> GetOrderParentcsList(OrderStateEnum OrderState, int CustomerId)
        {
            List<OrderParent> orderParentList = new List<OrderParent>();
            return orderParentList;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="OrderParent"></param>
        /// <returns></returns>
        public bool CreateOrderParent(OrderParent OrderParent)
        {
            return false;
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public bool DeleteOrderParent(string OrderId)
        {
            return false;
        }

        /// <summary>
        /// 会员签到
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public bool CustomerSign(int CustomerId)
        {
            return false;
        }
        #endregion

        #region 网站后台

        #region Customer
        public Customer GetCustomer(int Id)
        {
            Customer Customer = new Customer();
            return Customer;
        }

        public bool AddCustomer(Customer Customer)
        {
            return false;
        }

        public bool ModifyCustomer(Customer Customer)
        {
            return false;
        }

        public bool DeleteCustomer(int Id)
        {
            return false;
        }
        #endregion

        #region CustomerComment
        public CustomerComment GetCustomerComment(int Id)
        {
            CustomerComment CustomerComment = new CustomerComment();
            return CustomerComment;
        }

        public bool AddCustomerComment(CustomerComment CustomerComment)
        {
            return false;
        }

        public bool ModifyCustomerComment(CustomerComment CustomerComment)
        {
            return false;
        }

        public bool DeleteCustomerComment(int Id)
        {
            return false;
        }
        #endregion

        #region CustomerSign
        public CustomerSign GetCustomerSign(int Id)
        {
            CustomerSign CustomerSign = new CustomerSign();
            return CustomerSign;
        }

        public bool AddCustomerSign(CustomerSign CustomerSign)
        {
            return false;
        }

        public bool ModifyCustomerSign(CustomerSign CustomerSign)
        {
            return false;
        }

        public bool DeleteCustomerSign(int Id)
        {
            return false;
        }
        #endregion

        #region DeliveryAddress
        public DeliveryAddress GetDeliveryAddress(int Id)
        {
            DeliveryAddress deliveryAddress = new DeliveryAddress();
            return deliveryAddress;
        }

        public bool AddDeliveryAddress(DeliveryAddress DeliveryAddress)
        {
            return false;
        }

        public bool ModifyDeliveryAddress(DeliveryAddress DeliveryAddress)
        {
            return false;
        }

        public bool DeleteDeliveryAddress(int Id)
        {
            return false;
        }
        #endregion

        #region OffLineOrderDetail
        public OffLineOrderDetail GetOffLineOrderDetail(int Id)
        {
            OffLineOrderDetail OffLineOrderDetail = new OffLineOrderDetail();
            return OffLineOrderDetail;
        }

        public bool AddOffLineOrderDetail(OffLineOrderDetail OffLineOrderDetail)
        {
            return false;
        }

        public bool ModifyOffLineOrderDetail(OffLineOrderDetail OffLineOrderDetail)
        {
            return false;
        }

        public bool DeleteOffLineOrderDetail(int Id)
        {
            return false;
        }
        #endregion

        #region OffLineOrderParent
        public OffLineOrderParent GetOffLineOrderParent(string OrderId)
        {
            OffLineOrderParent OffLineOrderParent = new OffLineOrderParent();
            return OffLineOrderParent;
        }

        public bool AddOffLineOrderParent(OffLineOrderParent OffLineOrderParent)
        {
            return false;
        }

        public bool ModifyOffLineOrderParent(OffLineOrderParent OffLineOrderParent)
        {
            return false;
        }

        public bool DeleteOffLineOrderParent(string OrderId)
        {
            return false;
        }
        #endregion

        #region OnLineOrderDetail
        public OnLineOrderDetail GetOnLineOrderDetail(int Id)
        {
            OnLineOrderDetail OnLineOrderDetail = new OnLineOrderDetail();
            return OnLineOrderDetail;
        }

        public bool AddOnLineOrderDetail(OnLineOrderDetail OnLineOrderDetail)
        {
            return false;
        }

        public bool ModifyOnLineOrderDetail(OnLineOrderDetail OnLineOrderDetail)
        {
            return false;
        }

        public bool DeleteOnLineOrderDetail(int Id)
        {
            return false;
        }
        #endregion

        #region OnLineOrderParent
        public OnLineOrderParent GetOnLineOrderParent(string OrderId)
        {
            OnLineOrderParent OnLineOrderParent = new OnLineOrderParent();
            return OnLineOrderParent;
        }

        public bool AddOnLineOrderParent(OnLineOrderParent OnLineOrderParent)
        {
            return false;
        }

        public bool ModifyOnLineOrderParent(OnLineOrderParent OnLineOrderParent)
        {
            return false;
        }

        public bool DeleteOnLineOrderParent(string OrderId)
        {
            return false;
        }
        #endregion

        #region OrderEstablish
        public OrderEstablish GetOrderEstablish(int Id)
        {
            OrderEstablish OrderEstablish = new OrderEstablish();
            return OrderEstablish;
        }

        public bool AddOrderEstablish(OrderEstablish OrderEstablish)
        {
            return false;
        }

        public bool ModifyOrderEstablish(OrderEstablish OrderEstablish)
        {
            return false;
        }

        public bool DeleteOrderEstablish(int Id)
        {
            return false;
        }
        #endregion

        #region Post
        public Post GetPost(int Id)
        {
            Post Post = new Post();
            return Post;
        }

        public bool AddPost(Post Post)
        {
            return false;
        }

        public bool ModifyPost(Post Post)
        {
            return false;
        }

        public bool DeletePost(int Id)
        {
            return false;
        }
        #endregion

        #region Product
        public Product GetProduct(string SKUProductCode)
        {
            Product Product = new Product();
            return Product;
        }

        public bool AddProduct(Product Product)
        {
            return false;
        }

        public bool ModifyProduct(Product Product)
        {
            return false;
        }

        public bool DeleteProduct(string SKUProductCode)
        {
            return false;
        }
        #endregion

        #region ProductType
        public ProductType GetProductType(int Id)
        {
            ProductType ProductType = new ProductType();
            return ProductType;
        }

        public bool AddProductType(ProductType ProductType)
        {
            return false;
        }

        public bool ModifyProductType(ProductType ProductType)
        {
            return false;
        }

        public bool DeleteProductType(int Id)
        {
            return false;
        }
        #endregion

        #region SKUEstablish
        public SKUEstablish GetSKUEstablish(int Id)
        {
            SKUEstablish SKUEstablish = new SKUEstablish();
            return SKUEstablish;
        }

        public bool AddSKUEstablish(SKUEstablish SKUEstablish)
        {
            return false;
        }

        public bool ModifySKUEstablish(SKUEstablish SKUEstablish)
        {
            return false;
        }

        public bool DeleteSKUEstablish(int Id)
        {
            return false;
        }
        #endregion

        #endregion
    }
}
