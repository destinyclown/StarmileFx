CREATE DATABASE
IF NOT EXISTS `Youngo`;
USE `Youngo`;

/*产品类型*/
CREATE TABLE
IF NOT EXISTS `ProductType` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`TypeName` varchar(50) NULL COMMENT '类型名称',
	`Code` varchar(50) NULL COMMENT '标识',
	`Introduce` varchar(50) NULL COMMENT '介绍',
	`State` boolean DEFAULT 0 COMMENT '状态',
	CHECK (`State` = 0 OR `State` = 1),
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);


/*产品表*/
CREATE TABLE
IF NOT EXISTS `Product` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`ProductID` varchar(50)  NULL UNIQUE COMMENT '标识SKU',
	`Label` varchar(50)  NULL COMMENT '产品标签',
	`CnName` varchar(50)  NULL COMMENT '中文名称',
	`EnName` varchar(50)  NULL COMMENT '英文名称',
	`Weight` FLOAT(18,2) NULL COMMENT '重量',
	`CostPrice` FLOAT(18,2)  NULL COMMENT '出售价格',
	`PurchasePrice` FLOAT(18,2)  NULL COMMENT '进货价格',
	`Introduce` varchar(50)  NULL COMMENT '产品介绍',
	`Type` INT NULL COMMENT '类型',
	`Remarks` varchar(500)  NULL COMMENT '备注',
	`Stock` INT(11)  NULL COMMENT '库存',
	`IsTop`boolean DEFAULT 0 COMMENT '是否置顶',
	CHECK (`IsTop` = 0 OR `IsTop` = 1),
	`IsOutOfStock`boolean DEFAULT 0 COMMENT '是否缺货',
	CHECK (`IsOutOfStock` = 0 OR `IsOutOfStock` = 1),
	`IsClearStock` boolean DEFAULT 0 COMMENT '是否清理库存',
	CHECK (`IsClearStock` = 0 OR `IsClearStock` = 1),
	`IsDelete` boolean DEFAULT 0 COMMENT '是否删除',
	CHECK (`IsDelete` = 0 OR `IsDelete` = 1),
	`State` boolean DEFAULT 0 COMMENT '状态',
	CHECK (`State` = 0 OR `State` = 1),
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`OnlineTime` TIMESTAMP NULL COMMENT '上线时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);


/*订单表*/
CREATE TABLE
IF NOT EXISTS `OnLineOrderParent` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`OrderID` varchar(50)  NULL UNIQUE COMMENT '订单ID',
	`TraceID` varchar(50)  NULL UNIQUE COMMENT '跟踪号',
	`BatchProcessingID` varchar(50)  NULL COMMENT '批次号',
	`DeliveryUser` varchar(50) NULL COMMENT '发货人',
	`CustomerID` INT  NULL COMMENT '客户ID',
	`DeliveryAddressID` INT  NULL COMMENT '收货地址ID',
	`OrderState` INT DEFAULT 1 COMMENT '订单状态1、待付款2、待发货3、待收货',
	`PaymentType` INT DEFAULT 1 COMMENT '付款类型1、微信支付2、支付宝支付3、货到付款（暂不支持）',
	`PostID` INT NULL COMMENT '邮寄ID',
	`Weight` FLOAT(18,2) NULL COMMENT '重量',
	`TotalPrice` FLOAT(18,2)  NULL COMMENT '总价格',
	`PackPrice` FLOAT(18,2)  NULL COMMENT '打包价格',
  `ExpressPrice` FLOAT(18,2)  NULL COMMENT '快递价格',
	`OrderType` INT NULL COMMENT '订单类型',
	`CustomerRemarks` varchar(500)  NULL COMMENT '客户备注',
	`IsDelivery` boolean DEFAULT 0 COMMENT '是否发货',
	CHECK (`IsDelivery` = 0 OR `IsDelivery` = 1),
	`IsDelay` boolean DEFAULT 0 COMMENT '是否延迟发货',
	CHECK (`IsDelay` = 0 OR `IsDelay` = 1),
	`IsRemoteArea` boolean DEFAULT 0 COMMENT '是否偏远地区',
	CHECK (`IsRemoteArea` = 0 OR `IsRemoteArea` = 1),
	`IsDelete` boolean DEFAULT 0 COMMENT '是否删除',
	CHECK (`IsDelete` = 0 OR `IsDelete` = 1),
	`IsFragile` boolean DEFAULT 0 COMMENT '是否发货',
	CHECK (`IsFragile` = 0 OR `IsFragile` = 1),
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`DeliveryTime` TIMESTAMP NULL COMMENT '发货时间',
	`PayTime` TIMESTAMP NULL COMMENT '付款时间',
	`FinishTime` TIMESTAMP NULL COMMENT '完成时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*订单详细表*/
CREATE TABLE
IF NOT EXISTS `OnLineOrderDetail` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`OrderID` varchar(50)  NULL COMMENT '订单ID',
	`ProductID` varchar(50)  NULL COMMENT '商品ID',
	`Number` INT DEFAULT 1 COMMENT '数量',
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*邮寄方式表*/
CREATE TABLE
IF NOT EXISTS `Post` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`PostName` varchar(50) UNIQUE NULL COMMENT '名称',
	`PostCode` varchar(50) UNIQUE NULL COMMENT '邮寄标签',
	`IsStop` boolean DEFAULT 1 COMMENT '是否停止',
	CHECK (`IsStop` = 0 OR `IsStop` = 1),
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*订单创建表*/
CREATE TABLE
IF NOT EXISTS `OrderEstablish` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`OriginalOrderID` INT(11)  NULL COMMENT '原始订单号',
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*订单创建表*/
CREATE TABLE
IF NOT EXISTS `SKUEstablish` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`OriginalSKU` INT(11)  NULL COMMENT '原始SKU',
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*订单表*/
CREATE TABLE
IF NOT EXISTS `OffLineOrderParent` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`OrderID` varchar(50) NULL UNIQUE COMMENT '订单ID',
	`OrderState` INT DEFAULT 0 COMMENT '订单状态',
	`PaymentType` INT DEFAULT 0 COMMENT '付款类型',
	`TotalPrice` FLOAT(18,2)  NULL COMMENT '总价格',
	`DiscountPrice` FLOAT(18,2)  NULL COMMENT '折扣价格',
	`CollectPrice` FLOAT(18,2)  NULL COMMENT '收取价格',
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`PayTime` TIMESTAMP COMMENT '付款时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*订单详细表*/
CREATE TABLE
IF NOT EXISTS `OffLineOrderDetail` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`OrderID` varchar(50)  NULL COMMENT '订单ID',
	`ProductID` varchar(50) NULL COMMENT '商品ID',
	`Number` INT DEFAULT 1 COMMENT '数量',
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*用户*/
CREATE TABLE
IF NOT EXISTS `Customer` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`UserName` varchar(50)  NULL COMMENT '用户名',
	`WeCharKey` varchar(100) NULL COMMENT '微信Key',
	`Integral` INT DEFAULT 1 COMMENT '积分',
	`CustomerType` INT DEFAULT 1 COMMENT '用户类型（1、普通用户2、白金会员3、黄金会员）',
	`State` boolean DEFAULT 0 COMMENT '状态',
	CHECK (`State` = 0 OR `State` = 1),
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*订单（商品）评论*/
CREATE TABLE
IF NOT EXISTS `CustomerComment` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`OrderID` varchar(50)  NULL COMMENT '订单ID',
	`CustomerID` INT  NULL COMMENT '用户ID',
	`ProductID` varchar(50) NULL COMMENT '商品ID',
	`Comment` varchar(50) NULL COMMENT '评论',
	`Reply` INT NULL COMMENT '回复ID',
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*积分记录*/
CREATE TABLE
IF NOT EXISTS `CustomerSign` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`CustomerID` INT  NULL COMMENT '用户ID',
	`Integral` INT DEFAULT 1 COMMENT '数量',
	`Mode` varchar(50) NULL COMMENT '方式',
	`CreatTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间'
);

/*收货地址*/
CREATE TABLE
IF NOT EXISTS `DeliveryAddress` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`CustomerID` INT  NULL COMMENT '用户ID',
	`ReceiveName` varchar(50)  NULL COMMENT '收件人姓名',
	`Zip` varchar(50)  NULL COMMENT '邮编',
	`Province` varchar(50)  NULL COMMENT '省',
	`City` varchar(50)  NULL COMMENT '城市',
	`Area` varchar(50)  NULL COMMENT '地区',
	`Street` varchar(50)  NULL COMMENT '街道',
	`Address` varchar(50)  NULL COMMENT '详细地址',
	`Phone` varchar(50)  NULL COMMENT '电话',
	`IsDefault` boolean DEFAULT 0 COMMENT '是否默认',
	CHECK (`IsDefault` = 0 OR `IsDefault` = 1),
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*意见反馈*/
CREATE TABLE
IF NOT EXISTS `Feedback` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`Content` varchar(2000)  NULL COMMENT '反馈内容',
	`Phone` varchar(50)  NULL COMMENT '电话',
	`Email` varchar(50)  NULL COMMENT '邮箱',
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*浏览历史*/
CREATE TABLE
IF NOT EXISTS `ViewHistory` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`CustomerID` INT  NULL COMMENT '用户ID',
	`ProductID` varchar(50) NULL COMMENT '商品ID',
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*搜索历史*/
CREATE TABLE
IF NOT EXISTS `SreachHistory` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`CustomerID` INT  NULL COMMENT '用户ID',
	`Keyword` varchar(50) NULL COMMENT '关键字',
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);