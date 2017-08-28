using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Models;
using StarmileFx.Models.Wap;
using StarmileFx.Models.Youngo;
using StarmileFx.Models.Enum;
using StarmileFx.Models.Redis;
using static StarmileFx.Models.Wap.WapFrom;
using StarmileFx.Models.Web;
using SqlSugar;
using StarmileFx.Models.Json;
using Microsoft.Extensions.Options;
using StarmileFx.Api.Server.BaseData;

namespace StarmileFx.Api.Server.Services
{
    /// <summary>
    /// 接口实现
    /// </summary>
    public class YoungoManager : IYoungoServer
    {
        /// <summary>
        /// 
        /// </summary>
        private SqlSugarClient _db;
        private IOptions<ConnectionStrings> _ConnectionStrings;

        public YoungoManager(IOptions<ConnectionStrings> ConnectionStrings)
        {
            _ConnectionStrings = ConnectionStrings;
            _db = BaseClient.GetInstance(_ConnectionStrings.Value.YoungoConnection);
        }

        #region 手机商城

        #region 商品相关
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public CacheProductList GetCacheProductList()
        {
            CacheProductList _CacheProductList = new CacheProductList
            {
                ProductTypeList = _db.Queryable<ProductType>().Where(a => a.State).ToList()
            };
            string sql = @"SELECT
	                        p.Id,
	                        p.ProductID,
	                        p.Label,
	                        p.CnName,
	                        p.EnName,
	                        p.ExpressCode,
	                        p.Weight,
	                        p.CostPrice,
	                        p.PurchasePrice,
	                        p.Introduce,
	                        p.Type,
	                        p.Remarks,
	                        p.SalesVolume,
	                        p.Stock,
	                        p.IsTop,
	                        p.IsOutOfStock,
	                        p.IsClearStock,
	                        p.State,
	                        p.IsDelete,
	                        p.OnlineTime,
	                        p.Brand,
	                        p.BrandIntroduce,
	                        p.CreatTime,
                            r.Address as Picture
                        FROM
	                        Product AS p
                        LEFT JOIN Resources AS r ON p.ProductID = r.ProductID AND r.Type = 1 
                        WHERE p.State";

            _CacheProductList.ProductList = _db.Ado.SqlQuery<ProductModel>(sql).ToList();
            _CacheProductList.ExpressList = _db.Queryable<Express>().Where(a => !a.IsStop & a.IsDefault).ToList();
            return _CacheProductList;
        }

        /// <summary>
        /// 获取商品资源
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductResources GetProductResources(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new Exception("商品SKU不能为空！");
            }
            ProductResources rp = NewMethod();
            rp.ProductID = productId;
            string sql = @"SELECT
	                        cc.Id,
	                        c.UserName,
	                        cc.OrderID,
	                        cc.ProductID,
	                        cc.`Comment`,
	                        cc.Reply,
                            cc.Star,
	                        cc.CreatTime
                        FROM
	                        CustomerComment AS cc
                        INNER JOIN Customer AS c ON c.Id = cc.CustomerID
                        WHERE cc.ProductID=@productId";
            SugarParameter[] parameters = new SugarParameter[] 
            {
                new SugarParameter("@productId", productId)
            };
            parameters[0].DbType = System.Data.DbType.String;

            rp.CommentList = _db.Ado.SqlQuery<ProductComment>(sql, parameters).ToList();
            rp.ResourcesList = _db.Queryable<Resources>().Where(a => a.ProductID == productId && (a.Type == ResourcesEnum.Comment || a.Type == ResourcesEnum.Product)).ToList();
            return rp;
        }

        private static ProductResources NewMethod()
        {
            return new ProductResources();
        }

        /// <summary>
        /// 添加图片资源
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public bool SubmitResources(Resources from)
        {
            return _db.Insertable(from).ExecuteCommand() > 0;
        }
        #endregion 商品相关

        #region 订单相关
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="OrderState"></param>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public List<OrderParent> GetOrderParentcsList(OrderStateEnum OrderState, int CustomerId, int PageSize, int PageIndex)
        {
            //sql语句
            string sql = @"SELECT
	                        od.Id,
	                        op.CustomerID,
	                        od.ProductID,
	                        od.OrderID,
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
	                        p.PurchasePrice,
	                        p.CnName AS ProductName,
	                        op.TotalPrice,
	                        op.CustomerRemarks,
	                        op.DeliveryTime,
	                        op.PayTime,
	                        op.FinishTime,
	                        op.UpdateTime,
	                        op.CreatTime,
	                        r.Address AS Picture
                        FROM
	                        OnLineOrderDetail AS od
                        INNER JOIN 	OnLineOrderParent AS op ON op.OrderID = od.OrderID
                        INNER JOIN Product AS p ON p.ProductID = od.ProductID
                        INNER JOIN DeliveryAddress AS da ON op.DeliveryAddressID = da.Id
                        LEFT JOIN Resources AS r ON p.ProductID = r.ProductID
                        AND r.Type = 1 and Sort =0
                        WHERE op.IsDelete = 0 AND op.CustomerID = @customerId ";
            if (OrderState != OrderStateEnum.All) {
                sql += "AND op.OrderState = @orderState ";
            }
            sql +="LIMIT @pageIndex,@page";

            SugarParameter[] parameters = new SugarParameter[]
            {
                new SugarParameter("@orderState", OrderState),
                new SugarParameter("@customerId", CustomerId),
                new SugarParameter("@pageIndex", (PageIndex - 1) * PageSize),
                new SugarParameter("@page", PageSize)  
            };
            parameters[0].DbType = System.Data.DbType.Int32;
            parameters[1].DbType = System.Data.DbType.Int32;
            parameters[2].DbType = System.Data.DbType.Int32;
            parameters[3].DbType = System.Data.DbType.Int32;

            List<OrderParent> orderParentList = _db.Ado.SqlQuery<OrderParent>(sql, parameters).ToList();
            return orderParentList;
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderParent> GetOrderParent(string orderId)
        {
            //sql语句
            string sql = @"SELECT
	                        od.Id,
	                        op.CustomerID,
	                        od.ProductID,
	                        od.OrderID,
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
	                        p.PurchasePrice,
	                        p.CnName AS ProductName,
	                        op.TotalPrice,
	                        op.CustomerRemarks,
	                        op.DeliveryTime,
	                        op.PayTime,
	                        op.FinishTime,
	                        op.UpdateTime,
	                        op.CreatTime,
	                        r.Address AS Picture
                        FROM
	                        OnLineOrderDetail AS od
                        INNER JOIN 	OnLineOrderParent AS op ON op.OrderID = od.OrderID
                        INNER JOIN Product AS p ON p.ProductID = od.ProductID
                        INNER JOIN DeliveryAddress AS da ON op.DeliveryAddressID = da.Id
                        LEFT JOIN Resources AS r ON p.ProductID = r.ProductID
                        AND r.Type = 1 and Sort = 0
                        WHERE op.IsDelete = 0 AND op.OrderID = @orderId ";

            SugarParameter[] parameters = new SugarParameter[]
            {
                new SugarParameter("@orderId", orderId)
            };
            parameters[0].DbType = System.Data.DbType.String;

            List<OrderParent> orderParentList = _db.Ado.SqlQuery<OrderParent>(sql, parameters).ToList();
            return orderParentList;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public bool OrderCreate(ShopCart shopCart)
        {
            if (shopCart == null)
            {
                throw new Exception("购物车为空！");
            }
            int detailCount = 0;

            //主表数据
            OnLineOrderParent order = new OnLineOrderParent
            {
                DeliveryAddressID = shopCart.Address.Id,
                OrderID = shopCart.OrderId,
                OrderState = shopCart.OrderState,
                PayTime = shopCart.PayTime,
                PaymentType = shopCart.PaymentType,
                CustomerRemarks = shopCart.CustomerRemarks,
                CustomerID = shopCart.CustomerID,
                TotalPrice = shopCart.TotalPrice
            };
            if (shopCart.ProductList.Count == 1)
            {
                if (shopCart.ProductList.Find(a => 1 == 1).Number == 1)
                {
                    order.OrderType = OrderTypeEnum.单条单数;
                }
                else
                {
                    order.OrderType = OrderTypeEnum.单条多件;
                }
            }
            else
            {
                order.OrderType = OrderTypeEnum.多条;
            }
            order.IsDelete = false;
            try
            {
                //开启事务
                _db.Ado.BeginTran();

                if (_db.Insertable(order).ExecuteCommand() > 0)
                {
                    //明细数据
                    foreach (var Product in shopCart.ProductList)
                    {
                        OnLineOrderDetail orderDetail = new OnLineOrderDetail
                        {
                            OrderID = shopCart.OrderId,
                            ProductID = Product.ProductID,
                            Number = Product.Number
                        };
                        if (_db.Insertable(orderDetail).ExecuteCommand() > 0)
                        {
                            detailCount++;
                        }
                    }
                    //判断是否全部提交成功
                    if (detailCount == shopCart.ProductList.Count)
                    {
                        //提交事务
                        _db.Ado.CommitTran();
                        return true;
                    }
                    else
                    {
                        //回滚事务
                        _db.Ado.RollbackTran();
                        throw new Exception("创建订单异常！");
                    }
                }
                else
                {
                    //回滚事务
                    _db.Ado.RollbackTran();
                    throw new Exception("创建订单异常！");
                }
                
            }
            catch (Exception ex)
            {
                //回滚事务
                _db.Ado.RollbackTran();
                throw ex;
            } 
        }

        /// <summary>
        /// 取消订单（非物理删除）
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="IsDelete">是否删除</param>
        /// <returns></returns>
        public bool OrderCancel(string OrderId, bool IsDelete = false)
        {
            if (string.IsNullOrWhiteSpace(OrderId))
            {
                throw new Exception("订单号为空！");
            }
            OrderId = OrderId.ToUpper();
            OnLineOrderParent order = _db.Queryable<OnLineOrderParent>().First(a => a.OrderID.Equals(OrderId) & a.IsDelete == false);
            if (order == null)
            {
                throw new Exception("订单信息为空，请检查！");
            }
            var type = order.OrderState;
            order.OrderState = OrderStateEnum.Canceled;
            order.IsDelete = IsDelete;

            try
            {
                _db.Ado.BeginTran();
                if (_db.Updateable(order).ExecuteCommand() > 0)
                {
                    //发送通知
                    if (SendMessage(order.CustomerID, order.OrderID, MessageTypeEnum.Cancel))
                    {
                        _db.Ado.CommitTran();
                        return true;
                    }
                    _db.Ado.RollbackTran();
                    return false;
                }
                else
                {
                    _db.Ado.RollbackTran();
                    throw new Exception("删除订单异常！");
                }
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
                throw;
            }
        }

        /// <summary>
        /// 订单确认（支付）
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="TransactionId">交易编号</param>
        /// <returns></returns>
        public bool OrderPay(string OrderId, string TransactionId)
        {
            if (string.IsNullOrEmpty(OrderId) || string.IsNullOrEmpty(TransactionId))
            {
                throw new Exception("订单编号或交易编号不能为空！");
            }
            OnLineOrderParent order = _db.Queryable<OnLineOrderParent>().First(a => a.OrderID == OrderId);
            if (order != null && order.OrderState == OrderStateEnum.WaitPayment)
            {
                order.OrderState = OrderStateEnum.WaitShipment;
                order.PayTime = DateTime.Now;
                try
                {
                    if (_db.Updateable(order).ExecuteCommand() > 0)
                    {
                        TransactionRecord tr = new TransactionRecord
                        {
                            OrderID = OrderId,
                            TotalPrice = order.TotalPrice,
                            TransactionID = TransactionId,
                            Type = PaymentTypeEnum.WeChatPayment
                        };
                        if (_db.Insertable(tr).ExecuteCommand() > 0)
                        {
                            ChangeCustomerSign(order.CustomerID, SignEnum.购买商品50点积分);
                            return SendMessage(order.CustomerID, order.OrderID, MessageTypeEnum.Confirm);
                        }
                        else
                        {
                            throw new Exception("添加交易日志异常！");
                        }
                    }
                    else
                    {
                        throw new Exception("更新订单异常！");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                throw new Exception("查无订单或订单状态异常！");
            }
        }

        /// <summary>
        /// 完成订单（确认收货）
        /// </summary>
        /// <param name="OrederId"></param>
        /// <returns></returns>
        public bool OrderComplete(string OrederId)
        {
            OnLineOrderParent order = _db.Queryable<OnLineOrderParent>().First(a => a.OrderID == OrederId);
            if (order != null && order.OrderState == OrderStateEnum.WaitDelivery)
            {
                order.OrderState = OrderStateEnum.WaitShipment;
                order.PayTime = DateTime.Now;
                if (_db.Updateable(order).ExecuteCommand() > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("更新订单异常！");
                }
            }
            else
            {
                throw new Exception("查无订单或订单状态异常！");
            }
        }

        /// <summary>
        /// 订单申请售后
        /// </summary>
        /// <param name="OrederId"></param>
        /// <param name="Type"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public bool OrderServiceApply(string OrederId, ServiceTypeEnum Type, string Content)
        {
            OnLineOrderParent order = _db.Queryable<OnLineOrderParent>().First(a => a.OrderID == OrederId);
            if (order != null && order.OrderState == OrderStateEnum.Completed)
            {
                ServiceRecord sr = new ServiceRecord
                {
                    Content = Content,
                    CustomerID = order.CustomerID,
                    Type = Type,
                    OrderID = OrederId
                };
                if (_db.Insertable(sr).ExecuteCommand() > 0)
                {
                    order.IsDelete = true;
                    switch (Type)
                    {
                        case ServiceTypeEnum.Exchange:
                            order.OrderState = OrderStateEnum.ApplyExchange;
                            break;
                        case ServiceTypeEnum.Refund:
                            order.OrderState = OrderStateEnum.ApplyRefund;
                            break;
                        default:
                            order.OrderState = OrderStateEnum.ApplyReturns;
                            break;
                    }
                    if (_db.Updateable(order).ExecuteCommand() > 0)
                    {
                        //发送通知
                        switch (Type)
                        {
                            case ServiceTypeEnum.Exchange:
                                return SendMessage(order.CustomerID, order.OrderID, MessageTypeEnum.ApplyExchange);
                            case ServiceTypeEnum.Refund:
                                return SendMessage(order.CustomerID, order.OrderID, MessageTypeEnum.ApplyRefund);
                            default:
                                return SendMessage(order.CustomerID, order.OrderID, MessageTypeEnum.ApplyReturns);
                        }
                    }
                    else
                    {
                        throw new Exception("删除订单异常！");
                    }
                }
                else
                {
                    throw new Exception("添加售后记录订单异常！");
                }
            }
            else
            {
                throw new Exception("查无订单或订单状态异常！");
            }
        }


        #endregion 订单相关

        #region 会员相关
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="WeCharKey"></param>
        /// <returns></returns>
        public Customer GetCustomer(string WeCharKey)
        {
            Customer customer = _db.Queryable<Customer>().First(a => a.State & a.WeCharKey == WeCharKey);
            return customer;
        }

        /// <summary>
        /// 提交评论
        /// </summary>
        /// <param name="CommentFrom"></param>
        /// <returns></returns>
        public bool SubmitComment(CommentFrom CommentFrom)
        {
            CustomerComment comment = new CustomerComment
            {
                CustomerID = CommentFrom.CustomerID,
                OrderID = CommentFrom.OrderID,
                ProductID = CommentFrom.ProductID,
                Reply = CommentFrom.Reply
            };
            if (_db.Insertable(comment).ExecuteCommand() > 0)
            {
                return ChangeCustomerSign(CommentFrom.CustomerID, SignEnum.添加评论10点积分);
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
                DeliveryAddress model = _db.Queryable<DeliveryAddress>().First(a => a.Id == DeliveryAddressFrom.Id);
                model.Address = DeliveryAddressFrom.Address;
                model.Area = DeliveryAddressFrom.Area;
                model.City = DeliveryAddressFrom.City;
                model.Phone = DeliveryAddressFrom.Phone;
                model.Province = DeliveryAddressFrom.Province;
                model.ReceiveName = DeliveryAddressFrom.ReceiveName;
                model.IsDefault = DeliveryAddressFrom.IsDefault;
                if (_db.Updateable(model).ExecuteCommand() > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("提交地址异常！");
                }
            }
            else
            {
                DeliveryAddress model = new DeliveryAddress
                {
                    Address = DeliveryAddressFrom.Address,
                    Area = DeliveryAddressFrom.Area,
                    City = DeliveryAddressFrom.City,
                    Phone = DeliveryAddressFrom.Phone,
                    Province = DeliveryAddressFrom.Province,
                    ReceiveName = DeliveryAddressFrom.ReceiveName,
                    IsDefault = DeliveryAddressFrom.IsDefault
                };
                if (_db.Insertable(model).ExecuteCommand() > 0)
                {
                    return true;
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
            List<DeliveryAddress> DeliveryAddressList = _db.Queryable<DeliveryAddress>().Where(a => a.CustomerID == CustomerId).ToList();
            return DeliveryAddressList;
        }

        /// <summary>
        /// 获取默认地址
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DeliveryAddress GetDefaultAddress(int CustomerId)
        {
            return _db.Queryable<DeliveryAddress>().First(a => a.CustomerID == CustomerId && a.IsDefault);
        }

        /// <summary>
        /// 会员积分变动记录
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="signEnum"></param>
        /// <returns></returns>
        public bool ChangeCustomerSign(int CustomerId, SignEnum signEnum)
        {
            Customer customer = _db.Queryable<Customer>().First(a => a.State & a.Id == CustomerId);
            customer.Integral += (int)signEnum;
            if (_db.Updateable(customer).ExecuteCommand() > 0)
            {
                CustomerSign sign = NewMethod1();
                sign.CustomerID = CustomerId;
                sign.Integral = (int)signEnum;
                sign.Mode = signEnum.ToString();
                if (_db.Insertable(sign).ExecuteCommand() > 0)
                {
                    //提交事务
                    return true;
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

        private static CustomerSign NewMethod1()
        {
            return new CustomerSign();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="signEnum"></param>
        /// <returns></returns>
        public bool SendMessage(int CustomerId, string OrederId, MessageTypeEnum MessageType)
        {
            Information Message = new Information
            {
                CustomerID = CustomerId,
                OrderID = OrederId
            };
            string message = string.Empty;
            switch (MessageType)
            {
                case MessageTypeEnum.Confirm:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已经确认付款，请耐心等候发货！", OrederId);
                    break;
                case MessageTypeEnum.Shipment:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已经发货，请耐心等候商品送达！", OrederId);
                    break;
                case MessageTypeEnum.Cancel:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已经取消！", OrederId);
                    break;
                case MessageTypeEnum.ApplyRefund:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已申请退款，请耐心等候售后处理！", OrederId);
                    break;
                case MessageTypeEnum.ApplyReturns:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已申请退货，请耐心等候售后处理！", OrederId);
                    break;
                case MessageTypeEnum.ApplyExchange:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已申请换货，请耐心等候售后处理！", OrederId);
                    break;
                case MessageTypeEnum.Refunded:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已退款完成！", OrederId);
                    break;
                case MessageTypeEnum.Returned:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已退货完成！", OrederId);
                    break;
                case MessageTypeEnum.Exchanged:
                    message = string.Format("尊敬的用户，您好：您的订单{0}已换货完成！", OrederId);
                    break;
            }
            if (_db.Insertable(Message).ExecuteCommand() > 0)
            {
                return true;
            }
            else
            {
                _db.Ado.RollbackTran();
                throw new Exception("发送消息异常！");
            }
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public List<Information> GetMessageList(int CustomerId, int PageSize, int PageIndex)
        {
            PageData page = new PageData { PageIndex = PageIndex, PageSize = PageSize };
            int total = 0;
            List<Information> list = _db.Queryable<Information>().Where(a => a.CustomerID == CustomerId).OrderBy(a => a.CreatTime).ToPageList(page.PageIndex, page.PageSize, ref total).ToList();
            return list;
        }

        /// <summary>
        /// 提交反馈意见
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public bool SubmitFeedback(FeedbackFrom from)
        {
            Feedback model = new Feedback
            {
                Content = from.Content,
                Type = from.Type,
                Phone = from.Phone
            };
            if (_db.Insertable(model).ExecuteCommand() > 0)
            {
                return true;
            }
            else
            {
                throw new Exception("发送消息异常！");
            }
        }
        #endregion 会员相关

        #endregion

        #region 网站后台

        #region 商品管理
        /// <summary>
        /// 查询商品
        /// </summary>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ProductWeb> GetProductList(ProductSearch search, out int total)
        {
            string sql = @"SELECT
	                        p.Id,
	                        p.ProductID,
	                        p.Label,
	                        p.CnName,
	                        p.EnName,
	                        p.ExpressCode,
	                        p.Weight,
	                        p.CostPrice,
	                        p.PurchasePrice,
	                        p.Introduce,
	                        pt.TypeName,
	                        p.Remarks,
	                        p.SalesVolume,
	                        p.Stock,
	                        p.IsTop,
	                        p.IsOutOfStock,
	                        p.IsClearStock,
	                        p.State,
	                        p.IsDelete,
	                        p.OnlineTime,
	                        p.Brand,
	                        p.BrandIntroduce,
	                        p.CreatTime,
                            r.Address as Picture
                        FROM
	                        Product AS p
                        LEFT JOIN ProductType AS pt ON p.Type = pt.Id
                        LEFT JOIN Resources AS r ON p.ProductID = r.ProductID AND r.Type = 1 AND r.Sort = 0
                        WHERE p.State = @state AND p.IsDelete = 0 ";
            string sqlcount = @"SELECT COUNT(0) AS Count
                        FROM
	                        Product AS p
                        LEFT JOIN ProductType AS pt ON p.Type = pt.Id 
                        LEFT JOIN Resources AS r ON p.ProductID = r.ProductID AND r.Type = 1 AND r.Sort = 0 
                        WHERE p.State = @state AND p.IsDelete = 0 ";
            if (!string.IsNullOrEmpty(search.CnName))
            {
                sql += "AND locate(@name,p.CnName)>0 ";
                sqlcount += "AND locate(@name,p.CnName)>0 ";
            }
            if (!string.IsNullOrEmpty(search.ProductID))
            {
                sql += "AND p.ProductID = @productId ";
                sqlcount += "AND p.ProductID = @productId ";
            }
            if (search.Type != null)
            {
                sql += "AND p.Type = @type ";
                sqlcount += "AND p.Type = @type ";
            }
            if (search.dateType == 0)
            {
                if (!string.IsNullOrEmpty(search.startDate))
                {
                    sql += "AND p.CreatTime > @startDate ";
                    sqlcount += "AND p.CreatTime > @startDate ";
                }
                if (!string.IsNullOrEmpty(search.endDate))
                {
                    sql += "AND p.CreatTime > @endDate ";
                    sqlcount += "AND p.CreatTime > @endDate ";
                }
            }
            else if (search.dateType == 1)
            {
                if (!string.IsNullOrEmpty(search.startDate))
                {
                    sql += "AND p.UpdateTime > @startDate ";
                    sqlcount += "AND p.UpdateTime > @startDate ";
                }
                if (!string.IsNullOrEmpty(search.endDate))
                {
                    sql += "AND p.UpdateTime > @endDate ";
                    sqlcount += "AND p.UpdateTime > @endDate ";
                }
            }
            sql += "LIMIT @pageIndex,@page";
            search.Evaluate();
            SugarParameter[] parameters = new SugarParameter[]
            {
                new SugarParameter("@name", search.CnName),
                new SugarParameter("@productId", search.ProductID),
                new SugarParameter("@type", search.Type),
                new SugarParameter("@startDate", search.startDate.ToString()),
                new SugarParameter("@endDate", search.endDate),
                new SugarParameter("@pageIndex", (search.PageIndex - 1) * search.PageSize),
                new SugarParameter("@page", search.PageSize),
                new SugarParameter("@state", search.State)
            };
            parameters[0].DbType = System.Data.DbType.String;
            parameters[1].DbType = System.Data.DbType.String;
            parameters[2].DbType = System.Data.DbType.Int32;
            parameters[3].DbType = System.Data.DbType.String;
            parameters[4].DbType = System.Data.DbType.String;
            parameters[5].DbType = System.Data.DbType.Int32;
            parameters[6].DbType = System.Data.DbType.Int32;
            parameters[7].DbType = System.Data.DbType.Boolean;

            //总数
            total = _db.Ado.GetInt(sqlcount, parameters);
            List<ProductWeb> list = _db.Ado.SqlQuery<ProductWeb>(sql, parameters).ToList();
            return list;
        }

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {
            return _db.Queryable<Product>().InSingle(id);
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="Product"></param>
        /// <returns></returns>
        public bool AddProduct(Product Product)
        {
            if(_db.Insertable(Product).ExecuteCommand() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="Product"></param>
        /// <returns></returns>
        public bool ModifyProduct(Product Product)
        {
            if (_db.Updateable(Product).ExecuteCommand() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteProduct(int Id)
        {
            Product product = GetProduct(Id);
            product.IsDelete = true;
            if (_db.Updateable(product).ExecuteCommand() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 批量删除商品
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public bool BatchDeleteProduct(int[] Ids)
        {
            int count = 0;
            foreach (int Id in Ids)
            {
                Product product = GetProduct(Id);
                product.IsDelete = true;
                if (_db.Updateable(product).ExecuteCommand() > 0)
                {
                    count++;
                }
                else
                {
                    return false;
                }
            }
            if (count == Ids.Count())
            {
                return true;
            }
            return false;
        }
        #endregion 商品管理

        #region 商品类型管理
        /// <summary>
        /// 获取商品类型列表
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ProductType> GetProductTypeList()
        {
            return _db.Queryable<ProductType>().Where(a => a.State).ToList();
        }

        /// <summary>
        /// 获取商品类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ProductType GetProductType(int Id)
        {
            ProductType ProductType = new ProductType();
            return ProductType;
        }

        /// <summary>
        /// 添加商品类型
        /// </summary>
        /// <param name="ProductType"></param>
        /// <returns></returns>
        public bool AddProductType(ProductType ProductType)
        {
            if (_db.Insertable(ProductType).ExecuteCommand() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改商品类型
        /// </summary>
        /// <param name="ProductType"></param>
        /// <returns></returns>
        public bool ModifyProductType(ProductType ProductType)
        {
            if (_db.Updateable(ProductType).ExecuteCommand() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除商品类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteProductType(int Id)
        {
            if (_db.Deleteable<ProductType>().In(Id).ExecuteCommand() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 批量删除商品类型
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public bool BatchDeleteProductType(int[] Ids)
        {
            int count = _db.Deleteable<ProductType>().In(Ids).ExecuteCommand();
            if (count == Ids.Count())
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 网站资源管理
        /// <summary>
        /// 获取网站资源列表
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="Type"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<Resources> GetResourcesList(string ProductId, ResourcesEnum Type)
        {
            return _db.Queryable<Resources>().Where(a => a.ProductID == ProductId && a.Type == Type).ToList();
        }

        /// <summary>
        /// 删除网站资源
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="Type"></param>
        /// <param name="Addresses"></param>
        /// <param name="Sorts"></param>
        /// <returns></returns>
        public bool AddResources(string ProductId, ResourcesEnum Type, string[] Addresses, int[] Sorts)
        {
            int count = 0;
            int i = 0;
            foreach (string Address in Addresses)
            {
                Resources resources = new Resources
                {
                    ProductID = ProductId,
                    Type = Type,
                    Address = Address,
                    Sort = Sorts[i]
                };
                i++;
                if (_db.Insertable(resources).ExecuteCommand() > 0)
                {
                    count++;
                }
                else
                {
                    return false;
                }
            }
            if (count == Addresses.Count())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除网站资源
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteResources(int Id)
        {
            if (_db.Deleteable<Resources>().In(Id).ExecuteCommand() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 批量删除网站资源
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool BatchDeleteResources(string ProductId, ResourcesEnum Type)
        {
            if (_db.Deleteable<Resources>().Where(a => a.ProductID == ProductId && a.Type == Type).ExecuteCommand() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #endregion
    }
}
