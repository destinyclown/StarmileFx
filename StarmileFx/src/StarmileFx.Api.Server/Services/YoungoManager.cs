using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StarmileFx.Api.Server.Data;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Models;
using StarmileFx.Models.Wap;
using StarmileFx.Models.Youngo;
using StarmileFx.Models.Enum;
using StarmileFx.Models.Redis;
using MySql.Data.MySqlClient;
using static StarmileFx.Models.Wap.WapFrom;

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
        private YoungoContext _YoungoContext;
        /// <summary>
        /// 开启事务(重要)
        /// </summary>
        private static bool Transaction = false;

        public YoungoManager(YoungoContext YoungoContext)
        {
            _YoungoContext = YoungoContext;
        }

        #region 基本数据库处理
        #region 实体处理

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        private bool Commit()
        {
            return _YoungoContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Lambda获取实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : ModelBase
        {
            return _YoungoContext.Get<TEntity>(whereLambda);
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
            _YoungoContext.Update<TEntity>(entity);
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
            _YoungoContext.Add<TEntity>(entity);
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
            _YoungoContext.AddRange(entities);
            return Commit();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool UpdateRange(IEnumerable<object> entities)
        {
            _YoungoContext.UpdateRange(entities);
            return Commit();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool RemoveRange(IEnumerable<object> entities)
        {
            _YoungoContext.RemoveRange(entities);
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
            return _YoungoContext.List<TEntity>(whereLambda, out total);
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
            return _YoungoContext.List<TEntity>(whereLambda, orderbyLambda, out total);
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
            return _YoungoContext.PageData<TEntity>(pageData, whereLambda, orderbyLambda, out total);
        }
        #endregion 获取列表
        #endregion

        #region 手机商城
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public CacheProductList GetCacheProductList()
        {
            CacheProductList _CacheProductList = new CacheProductList();
            int total = 0;
            _CacheProductList.ProductTypeList = List<ProductType>(a => a.State, out total).ToList();
            _CacheProductList.ProductList = List<Product>(a => a.State, out total).ToList();
            _CacheProductList.ResourcesList = List<Resources>(a => a.Type == ResourcesEnum.Product || a.Type == ResourcesEnum.Comment, out total).ToList();
            string sql = @"SELECT
	                        cc.ID,
	                        cc.`Comment`,
	                        c.UserName,
	                        cc.OrderID,
	                        cc.Reply,
	                        cc.ProductID,
	                        cc.UpdateTime,
	                        cc.CreatTime
                        FROM
	                        CustomerComment AS cc
                        INNER JOIN Product AS p ON cc.ProductID = p.ProductID AND p.State
                        INNER JOIN Customer AS c ON cc.CustomerID = c.ID";
            MySqlParameter[] parameters = new MySqlParameter[]{};
            _CacheProductList.CommentList = _YoungoContext.ExecuteSql<ProductComment>(sql, parameters).ToList();
            return _CacheProductList;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="WeCharKey"></param>
        /// <returns></returns>
        public Customer GetCustomer(string WeCharKey)
        {
            Customer customer = Get<Customer>(a => a.State & a.WeCharKey == WeCharKey);
            return customer;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="OrderState"></param>
        /// <param name="CustomerId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<OrderParent> GetOrderParentcsList(OrderStateEnum OrderState, int CustomerId, PageData page)
        {
            //sql语句
            string sql = @"SELECT
	                        op.ID,
	                        op.CustomerID,
	                        od.ProductID,
	                        op.OrderID,
	                        op.TraceID,
	                        op.PackPrice,
	                        op.ExpressPrice,
	                        op.OrderState,
	                        op.PaymentType,
	                        da.ReceiveName,
	                        da.Address,
	                        da.Province,
	                        da.City,
	                        da.Area,
	                        da.Phone,
	                        od.Number,
	                        op.TotalPrice,
	                        op.CustomerRemarks,
	                        op.DeliveryTime,
	                        op.PayTime,
	                        op.FinishTime,
	                        op.UpdateTime,
	                        op.CreatTime
                        FROM
	                        OnLineOrderParent AS op
                        INNER JOIN Onlineorderdetail AS od ON op.OrderID = od.OrderID
                        INNER JOIN DeliveryAddress AS da ON op.DeliveryAddressID = da.ID
                        WHERE op.IsDelet = 0 AND op.CustomerID = @customerId ";
            if (OrderState != OrderStateEnum.All) {
                sql += "AND op.OrderState = @orderState ";
            }
            sql +="LIMIT @pageIndex,@page";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@orderState", OrderState),
                new MySqlParameter("@customerId", CustomerId),
                new MySqlParameter("@pageIndex", (page.PageIndex - 1) * page.PageSize),
                new MySqlParameter("@page", page.PageSize)  
            };
            parameters[0].MySqlDbType = MySqlDbType.Int32;
            parameters[1].MySqlDbType = MySqlDbType.Int32;
            parameters[2].MySqlDbType = MySqlDbType.Int32;
            parameters[3].MySqlDbType = MySqlDbType.Int32;

            List<OrderParent> orderParentList = _YoungoContext.ExecuteSql<OrderParent>(sql, parameters).ToList();
            return orderParentList;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public bool CreateOrderParent(ShopCart shopCart)
        {
            if (shopCart == null)
            {
                throw new Exception("购物车为空！");
            }
            int detailCount = 0;

            //主表数据
            OnLineOrderParent order = new OnLineOrderParent();
            order.DeliveryAddressID = shopCart.Address.ID;
            order.OrderID = shopCart.OrderId;
            order.OrderState = shopCart.OrderState;
            order.OrderType = 1;
            order.PayTime = shopCart.PayTime;
            order.PaymentType = shopCart.PaymentType;
            order.CustomerRemarks = shopCart.CustomerRemarks;
            order.CustomerID = shopCart.CustomerID;
            order.TotalPrice = shopCart.TotalPrice;
            order.IsDelet = false;
            if (Add(order, Transaction))
            {
                //明细数据
                foreach (var Product in shopCart.ProductList)
                {
                    OnLineOrderDetail orderDetail = new OnLineOrderDetail();
                    orderDetail.OrderID = shopCart.OrderId;
                    orderDetail.ProductID = Product.ProductID;
                    orderDetail.Number = Product.Number;
                    if (Add(orderDetail, Transaction))
                    {
                        detailCount++;
                    }
                }
                //判断是否全部提交成功
                if (detailCount == shopCart.ProductList.Count)
                {
                    return Commit();
                    //ChangeCustomerSign(shopCart.CustomerID, SignEnum.购买商品20点积分);
                    //SendMessage(order.CustomerID, order.OrderID, MessageTypeEnum.订单确认通知);
                }
                else
                {
                    throw new Exception("创建订单异常！");
                }
            }
            else
            {
                throw new Exception("创建订单异常！");
            }
        }

        /// <summary>
        /// 取消订单（非物理删除）
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public bool CancelOrderParent(string OrderId)
        {
            if (string.IsNullOrWhiteSpace(OrderId))
            {
                throw new Exception("订单号为空！");
            }
            OrderId = OrderId.ToUpper();
            OnLineOrderParent order = Get<OnLineOrderParent>(a => a.OrderID.Equals(OrderId) & a.IsDelet == false);
            if (order == null)
            {
                throw new Exception("订单信息为空，请检查！");
            }
            order.IsDelet = true;
            if (Update(order, Transaction))
            {
                //发送通知
                return SendMessage(order.CustomerID, order.OrderID, MessageTypeEnum.订单取消通知);
            }
            else
            {
                throw new Exception("删除订单异常！");
            }
        }

        /// <summary>
        /// 提交评论
        /// </summary>
        /// <param name="CommentFrom"></param>
        /// <returns></returns>
        public bool SubmitComment(CommentFrom CommentFrom)
        {
            CustomerComment comment = new CustomerComment();
            comment.CustomerID = CommentFrom.CustomerID;
            comment.OrderID = CommentFrom.OrderID;
            comment.ProductID = CommentFrom.ProductID;
            comment.Reply = CommentFrom.Reply;
            if (Add(comment, Transaction))
            {
                return ChangeCustomerSign(CommentFrom.CustomerID, SignEnum.添加评论5点积分);
            }
            else
            {
                throw new Exception("提交评论异常！");
            }
        }

        /// <summary>
        /// 提交地址
        /// </summary>
        /// <param name="DeliveryAddressFrom"></param>
        /// <returns></returns>
        public bool SubmitDeliveryAddress(DeliveryAddressFrom DeliveryAddressFrom)
        {
            if (DeliveryAddressFrom.IsModify)
            {
                DeliveryAddress model = Get<DeliveryAddress>(a => a.ID == DeliveryAddressFrom.ID);
                model.Address = DeliveryAddressFrom.Address;
                model.Area = DeliveryAddressFrom.Area;
                model.City = DeliveryAddressFrom.City;
                model.Phone = DeliveryAddressFrom.Phone;
                model.Province = DeliveryAddressFrom.Province;
                model.ReceiveName = DeliveryAddressFrom.ReceiveName;
                model.IsDefault = DeliveryAddressFrom.IsDefault;
                if (Update(model, Transaction))
                {
                    //提交事务
                    return Commit();
                }
                else
                {
                    throw new Exception("提交地址异常！");
                }
            }
            else
            {
                DeliveryAddress model = new DeliveryAddress();
                model.Address = DeliveryAddressFrom.Address;
                model.Area = DeliveryAddressFrom.Area;
                model.City = DeliveryAddressFrom.City;
                model.Phone = DeliveryAddressFrom.Phone;
                model.Province = DeliveryAddressFrom.Province;
                model.ReceiveName = DeliveryAddressFrom.ReceiveName;
                model.IsDefault = DeliveryAddressFrom.IsDefault;
                if (Add(model, Transaction))
                {
                    //提交事务
                    return Commit();
                }
                else
                {
                    throw new Exception("提交地址异常！");
                }
            }
        }

        /// <summary>
        /// 获取地址列表
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public List<DeliveryAddress> GetDeliveryAddressList(int CustomerId)
        {
            int total = 0;
            List<DeliveryAddress> DeliveryAddressList = List<DeliveryAddress>(a => a.CustomerID == CustomerId, out total).ToList();
            return DeliveryAddressList;
        }

        /// <summary>
        /// 获取默认地址
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DeliveryAddress GetDefaultAddress(int CustomerId)
        {
            return Get<DeliveryAddress>(a => a.CustomerID == CustomerId && a.IsDefault);
        }

        /// <summary>
        /// 会员积分变动记录
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="signEnum"></param>
        /// <returns></returns>
        public bool ChangeCustomerSign(int CustomerId, SignEnum signEnum)
        {
            Customer customer = Get<Customer>(a => a.State & a.ID == CustomerId);
            customer.Integral += (int)signEnum;
            if (Update(customer, Transaction))
            {
                CustomerSign sign = new CustomerSign();
                sign.CustomerID = CustomerId;
                sign.Integral = (int)signEnum;
                sign.Mode = signEnum.ToString();
                if (Add(sign, Transaction))
                {
                    //提交事务
                    return Commit();
                }
                else
                {
                    throw new Exception("添加积分记录异常！");
                }
            }
            else
            {
                throw new Exception("会员签到异常！");
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="signEnum"></param>
        /// <returns></returns>
        public bool SendMessage(int CustomerId, string OrederId, MessageTypeEnum MessageType)
        {
            Information Message = new Information();
            Message.CustomerID = CustomerId;
            Message.OrderID = OrederId;
            string message = string.Empty;
            switch (MessageType)
            {
                case MessageTypeEnum.订单确认通知:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已经确认付款，请耐心等候发货！", OrederId);
                    break;
                case MessageTypeEnum.订单发货通知:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已经发货，请耐心等候商品送达！", OrederId);
                    break;
                case MessageTypeEnum.订单取消通知:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已经取消！", OrederId);
                    break;
                case MessageTypeEnum.订单申请退款通知:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已申请退款，请耐心等候售后处理！", OrederId);
                    break;
                case MessageTypeEnum.订单申请退货通知:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已申请退货，请耐心等候售后处理！", OrederId);
                    break;
                case MessageTypeEnum.订单退款完成通知:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已退款完成！", OrederId);
                    break;
                case MessageTypeEnum.订单退货完成通知:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已退货完成！", OrederId);
                    break;
            }
            if (Add(Message, Transaction))
            {
                return Commit();
            }
            else
            {
                throw new Exception("发送消息异常！");
            }
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<Information> GetMessageList(int CustomerId, PageData page)
        {
            int total = 0;
            List<Information> list = PageData<Information>(page, a => a.CustomerID == CustomerId, a => a.CreatTime, out total).ToList();
            return list;
        }

        /// <summary>
        /// 订单确认（支付）
        /// </summary>
        /// <param name="OrederId"></param>
        /// <returns></returns>
        public bool OrderPay(string OrederId)
        {
            OnLineOrderParent order = Get<OnLineOrderParent>(a => a.OrderID == OrederId);
            if (order != null)
            {
                order.OrderState = OrderStateEnum.WaitShipment;
                order.PayTime = DateTime.Now;
                if (Update(order, Transaction))
                {
                    return Commit();
                }
                else
                {
                    throw new Exception("更新订单异常！");
                }
            }
            else
            {
                throw new Exception("查无订单！");
            }
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
