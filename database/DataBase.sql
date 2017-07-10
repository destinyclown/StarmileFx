IF EXISTS (SELECT * FROM MASTER.sys.sysdatabases WHERE NAME = 'DataBase')
    PRINT '数据库DataBase已存在' 
else
begin 
Create database [DataBase] 
on  primary  -- 默认就属于primary文件组,可省略
(
/*--数据文件的具体描述--*/
    name='DataBase_data',  -- 主数据文件的逻辑名称
    filename='D:\Starmile\svn\StarmileFx\database\DataBase_data.mdf', -- 主数据文件的物理名称
    size=5mb, --主数据文件的初始大小
    maxsize=100mb, -- 主数据文件增长的最大值
    filegrowth=15%--主数据文件的增长率
)
--
--log on
--(
--/*--日志文件的具体描述,各参数含义同上--*/
--    name='DataBase_log',
--    filename='D:\CRZproject\CRZproject\BuildProcessTemplates\CRZproject\PinGouMall\Database\DataBase_log.ldf',
--    size=2mb,
--    filegrowth=1mb
--)
PRINT '成功创建数据库DataBase' 
end

GO


USE [DataBase]
GO

if object_id(N'SysEmailLogs',N'U') is null
begin
/*邮件日志*/
CREATE TABLE [SysEmailLogs](
	[SysEmailLogID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Email] [nvarchar](50) NOT NULL,
	[EmailLog] [nvarchar] NULL,
	[Status] [int] NOT NULL,
	[CreatDate] [datetime] DEFAULT GETDATE() NOT NULL,/*创建日期*/
)
PRINT '成功创建SysEmailLogs' 
END

if object_id(N'SysRolePermissions',N'U') is null
begin
/*角色等级*/
CREATE TABLE [SysRolePermissions](
	[PermissionsID] [int] IDENTITY(0,1) NOT NULL PRIMARY KEY,
	[Permissions] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Explain] [nvarchar](200) NULL,
	[Status] [int] NULL,
	[CreatDate] [datetime] DEFAULT GETDATE() NOT NULL,/*创建日期*/
)

INSERT INTO [SysRolePermissions](Permissions,Name,Explain,Status)
	VALUES(0,'超级管理员','超级管理员',1),
	(1,'管理员','普通管理员',1);
PRINT '成功创建SysRolePermissions' 
END

if object_id(N'SysRoles',N'U') is null
begin
/*角色列表*/
CREATE TABLE [SysRoles]
(
	[RoleID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[LoginName] [NVARCHAR](50)  NULL ,
	[Name] [NVARCHAR](50)  NULL ,
	[Pwd] [NVARCHAR](50)  NULL,
	[IsUsing] [int]  NULL,/*是否启用*/
	[Permissions] [int]  NULL,/*角色等级*/	
	[Url] [NVARCHAR](200)  NULL,/*头像*/	
	[CreatDate] [datetime] DEFAULT GETDATE() NULL,/*创建日期*/
)

INSERT INTO [SysRoles](RoleID,LoginName,Name,Pwd,IsUsing,Permissions)
	VALUES('00000000-0000-0000-0000-000000000000','starmile','超级管理员','21232F297A57A5A743894AE4A801FC3',1,0),
	('00000000-0000-0000-0000-000000000001','admin001','系统管理员','21232F297A57A5A743894AE4A801FC3',1,1);
PRINT '成功创建SysRoles' 
end

if object_id(N'SysMenus',N'U') is null
begin
/*菜单列表*/
CREATE TABLE [SysMenus]
(
	[MenuID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[Code] [NVARCHAR](50)  NULL ,
	[Icon] [NVARCHAR](100) NULL,
	[Name] [NVARCHAR](50)  NULL ,
	[Sort] [int] NULL,--排序
	[IsUsing] [int]  NULL,/*是否启用*/
	[CreatDate] [datetime] DEFAULT GETDATE() NULL,/*创建日期*/
)
INSERT INTO [SysMenus](MenuID,Code,Icon,Name,Sort,IsUsing)
	VALUES('00000000-0000-0000-0000-000000000001','SysModule','fa-cogs','系统管理',1,1);
PRINT '成功创建SysMenus'
end

if object_id(N'SysViews',N'U') is null
begin
/*视图列表*/
CREATE TABLE [SysViews]
(
	[WebID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[MenuID] [uniqueidentifier]  NULL FOREIGN KEY REFERENCES [SysMenus](MenuID),
	[Code] [NVARCHAR](50)  NULL ,
	[Icon] [NVARCHAR](100) NULL,
	[Name] [NVARCHAR](50)  NULL ,
	[Controllers] [NVARCHAR](50)  NULL ,
	[Views][NVARCHAR](50)  NULL ,
	[sort] [int] NULL,--排序
	[IsUsing] [int]  NULL,/*是否启用*/
	[PerId] [uniqueidentifier] NULL,
	[CreatDate] [datetime] DEFAULT GETDATE() NULL,/*创建日期*/
)
INSERT INTO [SysViews](WebID,Code,Controllers,Icon,Name,sort,Views,IsUsing,MenuID)
	VALUES(newid(),'SysRoles','Sys','fa-group','角色管理',0,'SysRoles',1,'00000000-0000-0000-0000-000000000001'),
	(newid(),'SysAuthorities','Sys','fa-key','权限管理',1,'SysAuthorities',1,'00000000-0000-0000-0000-000000000001'),
	(newid(),'SysViews','Sys','fa-laptop','页面管理',2,'SysViews',1,'00000000-0000-0000-0000-000000000001'),
	(newid(),'SysSetting','Sys','fa-wrench','系统设置',3,'SysSetting',1,'00000000-0000-0000-0000-000000000001'),
	(newid(),'SysLogs','Sys','fa fa-book','系统日志',4,'SysLogs',1,'00000000-0000-0000-0000-000000000001')
PRINT '成功创建SysViews'
END



if object_id(N'SysAuthorities',N'U') is null
begin
/*权限列表*/
CREATE TABLE [SysAuthorities]
(
	[AuthorityID] [int] NOT NULL PRIMARY KEY IDENTITY(0,1) ,
	[PermissionsID] [int] NULL FOREIGN KEY REFERENCES [SysRolePermissions](PermissionsID),
	[Code] [NVARCHAR](50)  NULL ,
	[IsUsing] [int]  NULL,/*是否启用*/
	[CreatDate] [datetime] DEFAULT GETDATE() NULL,/*创建日期*/
)
INSERT INTO SysAuthorities(PermissionsID,Code,IsUsing)
	VALUES(1,'SysModule',1),
	(1,'SysAuthorities',1),
	(1,'SysSetting',1),
	(1,'SysViews',1),
	(1,'SysLogs',1);

PRINT '成功创建SysAuthorities'
END

if object_id(N'SysRoleLogs',N'U') is null
begin
/*登录日志*/
CREATE TABLE [SysRoleLogs](
	[SysRoleLogID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[RoleID] [uniqueidentifier] NOT NULL FOREIGN KEY REFERENCES [SysRoles](RoleID),
	[LoginIP] [nvarchar](50) NOT NULL,
	[CreatDate] [datetime] DEFAULT GETDATE() NOT NULL,/*创建日期*/
)
PRINT '成功创建SysRoleLogs'
end

if object_id(N'SysMessage',N'U') is null
begin
/*消息表*/
CREATE TABLE [SysMessage](
	[MessageID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Sender] [uniqueidentifier] NOT NULL FOREIGN KEY REFERENCES [SysRoles](RoleID),
	[Receiver] [uniqueidentifier] NOT NULL FOREIGN KEY REFERENCES [SysRoles](RoleID),
	[SendDate] [datetime] DEFAULT GETDATE() NOT NULL,/*发送时间*/
	[ReceiveDate] [datetime] NULL,/*接收时间*/
	[Title] [nvarchar](50) NULL,
	[Content] [varchar](2000) NULL,
	[State] [int] NULL,
	[Type] [int] NULL
)
PRINT '成功创建SysMessage'
END

if object_id(N'SysRoleData',N'U') is null
begin
/*角色资料*/
CREATE TABLE [SysRoleData]
(
	[RoleDataID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[RoleID] [uniqueidentifier] NULL FOREIGN KEY REFERENCES [SysRoles](RoleID),
	[RoleNo] [NVARCHAR](50)  NULL ,
	[RealName] [NVARCHAR](10)  NULL ,/*真实姓名*/
	[Sex] [bit] NULL,
	[Origin] [NVARCHAR](20) NULL ,/*籍贯*/
	[Marry] [bit] NULL,
	[IDcard] [NVARCHAR](20) NULL ,/*身份证*/
	[Birthday] [datetime] NULL,
	[School] [NVARCHAR](20) NULL ,
	[Party] [NVARCHAR](20) NULL ,
	[Major] [NVARCHAR](20) NULL ,/*专业*/
	[Education] [NVARCHAR](10) NULL ,/*学历*/
	[Area] [NVARCHAR](20) NULL,
	[Mobile] [NVARCHAR](18) NULL ,/*电话号码*/
	[Email] [NVARCHAR](30)  NULL ,
	[QQ] [NVARCHAR](30)  NULL , 
	[WeChat] [NVARCHAR](30)  NULL ,
	[Company] [NVARCHAR](20)  NULL ,/*单位*/
	[Job] [bit] NULL,
	[CreatDate] [datetime] DEFAULT GETDATE() NULL,/*创建日期*/
)

PRINT '成功创建SysRoleData'
END



