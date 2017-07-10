IF EXISTS (SELECT * FROM MASTER.sys.sysdatabases WHERE NAME = 'DataBase')
    PRINT '���ݿ�DataBase�Ѵ���' 
else
begin 
Create database [DataBase] 
on  primary  -- Ĭ�Ͼ�����primary�ļ���,��ʡ��
(
/*--�����ļ��ľ�������--*/
    name='DataBase_data',  -- �������ļ����߼�����
    filename='D:\Starmile\svn\StarmileFx\database\DataBase_data.mdf', -- �������ļ�����������
    size=5mb, --�������ļ��ĳ�ʼ��С
    maxsize=100mb, -- �������ļ����������ֵ
    filegrowth=15%--�������ļ���������
)
--
--log on
--(
--/*--��־�ļ��ľ�������,����������ͬ��--*/
--    name='DataBase_log',
--    filename='D:\CRZproject\CRZproject\BuildProcessTemplates\CRZproject\PinGouMall\Database\DataBase_log.ldf',
--    size=2mb,
--    filegrowth=1mb
--)
PRINT '�ɹ��������ݿ�DataBase' 
end

GO


USE [DataBase]
GO

if object_id(N'SysEmailLogs',N'U') is null
begin
/*�ʼ���־*/
CREATE TABLE [SysEmailLogs](
	[SysEmailLogID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Email] [nvarchar](50) NOT NULL,
	[EmailLog] [nvarchar] NULL,
	[Status] [int] NOT NULL,
	[CreatDate] [datetime] DEFAULT GETDATE() NOT NULL,/*��������*/
)
PRINT '�ɹ�����SysEmailLogs' 
END

if object_id(N'SysRolePermissions',N'U') is null
begin
/*��ɫ�ȼ�*/
CREATE TABLE [SysRolePermissions](
	[PermissionsID] [int] IDENTITY(0,1) NOT NULL PRIMARY KEY,
	[Permissions] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Explain] [nvarchar](200) NULL,
	[Status] [int] NULL,
	[CreatDate] [datetime] DEFAULT GETDATE() NOT NULL,/*��������*/
)

INSERT INTO [SysRolePermissions](Permissions,Name,Explain,Status)
	VALUES(0,'��������Ա','��������Ա',1),
	(1,'����Ա','��ͨ����Ա',1);
PRINT '�ɹ�����SysRolePermissions' 
END

if object_id(N'SysRoles',N'U') is null
begin
/*��ɫ�б�*/
CREATE TABLE [SysRoles]
(
	[RoleID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[LoginName] [NVARCHAR](50)  NULL ,
	[Name] [NVARCHAR](50)  NULL ,
	[Pwd] [NVARCHAR](50)  NULL,
	[IsUsing] [int]  NULL,/*�Ƿ�����*/
	[Permissions] [int]  NULL,/*��ɫ�ȼ�*/	
	[Url] [NVARCHAR](200)  NULL,/*ͷ��*/	
	[CreatDate] [datetime] DEFAULT GETDATE() NULL,/*��������*/
)

INSERT INTO [SysRoles](RoleID,LoginName,Name,Pwd,IsUsing,Permissions)
	VALUES('00000000-0000-0000-0000-000000000000','starmile','��������Ա','21232F297A57A5A743894AE4A801FC3',1,0),
	('00000000-0000-0000-0000-000000000001','admin001','ϵͳ����Ա','21232F297A57A5A743894AE4A801FC3',1,1);
PRINT '�ɹ�����SysRoles' 
end

if object_id(N'SysMenus',N'U') is null
begin
/*�˵��б�*/
CREATE TABLE [SysMenus]
(
	[MenuID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[Code] [NVARCHAR](50)  NULL ,
	[Icon] [NVARCHAR](100) NULL,
	[Name] [NVARCHAR](50)  NULL ,
	[Sort] [int] NULL,--����
	[IsUsing] [int]  NULL,/*�Ƿ�����*/
	[CreatDate] [datetime] DEFAULT GETDATE() NULL,/*��������*/
)
INSERT INTO [SysMenus](MenuID,Code,Icon,Name,Sort,IsUsing)
	VALUES('00000000-0000-0000-0000-000000000001','SysModule','fa-cogs','ϵͳ����',1,1);
PRINT '�ɹ�����SysMenus'
end

if object_id(N'SysViews',N'U') is null
begin
/*��ͼ�б�*/
CREATE TABLE [SysViews]
(
	[WebID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[MenuID] [uniqueidentifier]  NULL FOREIGN KEY REFERENCES [SysMenus](MenuID),
	[Code] [NVARCHAR](50)  NULL ,
	[Icon] [NVARCHAR](100) NULL,
	[Name] [NVARCHAR](50)  NULL ,
	[Controllers] [NVARCHAR](50)  NULL ,
	[Views][NVARCHAR](50)  NULL ,
	[sort] [int] NULL,--����
	[IsUsing] [int]  NULL,/*�Ƿ�����*/
	[PerId] [uniqueidentifier] NULL,
	[CreatDate] [datetime] DEFAULT GETDATE() NULL,/*��������*/
)
INSERT INTO [SysViews](WebID,Code,Controllers,Icon,Name,sort,Views,IsUsing,MenuID)
	VALUES(newid(),'SysRoles','Sys','fa-group','��ɫ����',0,'SysRoles',1,'00000000-0000-0000-0000-000000000001'),
	(newid(),'SysAuthorities','Sys','fa-key','Ȩ�޹���',1,'SysAuthorities',1,'00000000-0000-0000-0000-000000000001'),
	(newid(),'SysViews','Sys','fa-laptop','ҳ�����',2,'SysViews',1,'00000000-0000-0000-0000-000000000001'),
	(newid(),'SysSetting','Sys','fa-wrench','ϵͳ����',3,'SysSetting',1,'00000000-0000-0000-0000-000000000001'),
	(newid(),'SysLogs','Sys','fa fa-book','ϵͳ��־',4,'SysLogs',1,'00000000-0000-0000-0000-000000000001')
PRINT '�ɹ�����SysViews'
END



if object_id(N'SysAuthorities',N'U') is null
begin
/*Ȩ���б�*/
CREATE TABLE [SysAuthorities]
(
	[AuthorityID] [int] NOT NULL PRIMARY KEY IDENTITY(0,1) ,
	[PermissionsID] [int] NULL FOREIGN KEY REFERENCES [SysRolePermissions](PermissionsID),
	[Code] [NVARCHAR](50)  NULL ,
	[IsUsing] [int]  NULL,/*�Ƿ�����*/
	[CreatDate] [datetime] DEFAULT GETDATE() NULL,/*��������*/
)
INSERT INTO SysAuthorities(PermissionsID,Code,IsUsing)
	VALUES(1,'SysModule',1),
	(1,'SysAuthorities',1),
	(1,'SysSetting',1),
	(1,'SysViews',1),
	(1,'SysLogs',1);

PRINT '�ɹ�����SysAuthorities'
END

if object_id(N'SysRoleLogs',N'U') is null
begin
/*��¼��־*/
CREATE TABLE [SysRoleLogs](
	[SysRoleLogID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[RoleID] [uniqueidentifier] NOT NULL FOREIGN KEY REFERENCES [SysRoles](RoleID),
	[LoginIP] [nvarchar](50) NOT NULL,
	[CreatDate] [datetime] DEFAULT GETDATE() NOT NULL,/*��������*/
)
PRINT '�ɹ�����SysRoleLogs'
end

if object_id(N'SysMessage',N'U') is null
begin
/*��Ϣ��*/
CREATE TABLE [SysMessage](
	[MessageID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Sender] [uniqueidentifier] NOT NULL FOREIGN KEY REFERENCES [SysRoles](RoleID),
	[Receiver] [uniqueidentifier] NOT NULL FOREIGN KEY REFERENCES [SysRoles](RoleID),
	[SendDate] [datetime] DEFAULT GETDATE() NOT NULL,/*����ʱ��*/
	[ReceiveDate] [datetime] NULL,/*����ʱ��*/
	[Title] [nvarchar](50) NULL,
	[Content] [varchar](2000) NULL,
	[State] [int] NULL,
	[Type] [int] NULL
)
PRINT '�ɹ�����SysMessage'
END

if object_id(N'SysRoleData',N'U') is null
begin
/*��ɫ����*/
CREATE TABLE [SysRoleData]
(
	[RoleDataID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[RoleID] [uniqueidentifier] NULL FOREIGN KEY REFERENCES [SysRoles](RoleID),
	[RoleNo] [NVARCHAR](50)  NULL ,
	[RealName] [NVARCHAR](10)  NULL ,/*��ʵ����*/
	[Sex] [bit] NULL,
	[Origin] [NVARCHAR](20) NULL ,/*����*/
	[Marry] [bit] NULL,
	[IDcard] [NVARCHAR](20) NULL ,/*���֤*/
	[Birthday] [datetime] NULL,
	[School] [NVARCHAR](20) NULL ,
	[Party] [NVARCHAR](20) NULL ,
	[Major] [NVARCHAR](20) NULL ,/*רҵ*/
	[Education] [NVARCHAR](10) NULL ,/*ѧ��*/
	[Area] [NVARCHAR](20) NULL,
	[Mobile] [NVARCHAR](18) NULL ,/*�绰����*/
	[Email] [NVARCHAR](30)  NULL ,
	[QQ] [NVARCHAR](30)  NULL , 
	[WeChat] [NVARCHAR](30)  NULL ,
	[Company] [NVARCHAR](20)  NULL ,/*��λ*/
	[Job] [bit] NULL,
	[CreatDate] [datetime] DEFAULT GETDATE() NULL,/*��������*/
)

PRINT '�ɹ�����SysRoleData'
END



