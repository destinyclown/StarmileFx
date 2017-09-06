CREATE DATABASE
IF NOT EXISTS `DataBase`;
USE `DataBase`;
/*Email日志*/
CREATE TABLE
IF NOT EXISTS `SysEmailLogs` (
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`Email` VARCHAR (50) NOT NULL COMMENT '邮件',
	`EmailLog` VARCHAR (200) NULL COMMENT '邮件日志',
	`State` boolean DEFAULT 0 COMMENT '状态',
	CHECK (`State` = 0 OR `State` = 1),
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);
/*角色列表*/
CREATE TABLE 
IF NOT EXISTS `SysRolePermissions`(
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`Permissions` INT NULL COMMENT '角色等级',
	`Name` varchar(50) NULL COMMENT '名称',
	`Explain` varchar(200) NULL COMMENT '备注',
	`State` boolean DEFAULT 0 COMMENT '状态',
	CHECK (`State` = 0 OR `State` = 1),
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);
/*用户列表*/
CREATE TABLE 
IF NOT EXISTS `SysRoles`
(
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`LoginName` VARCHAR(50)  NULL COMMENT '登录名称',
	`Name` VARCHAR(50)  NULL COMMENT '名称',
	`Pwd` VARCHAR(50)  NULL COMMENT '密码',
	`State` boolean DEFAULT 0 COMMENT '状态',
	CHECK (`State` = 0 OR `State` = 1),
	`Permissions` VARCHAR(100)  NULL COMMENT '角色等级',
	`Url` VARCHAR(200)  NULL COMMENT '头像',
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);
/*权限列表*/
CREATE TABLE 
IF NOT EXISTS `SysAuthorities`
(
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`PermissionsID` INT NULL,
	`Code` varchar(50)  NULL COMMENT '标识',
	`State` boolean DEFAULT 0 COMMENT '状态',
	CHECK (`State` = 0 OR `State` = 1),
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);
/*登录日志*/
CREATE TABLE 
IF NOT EXISTS `SysRoleLogs`(
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`RoleID` INT NOT NULL COMMENT '用户ID',
	`LoginIP` varchar(50) NOT NULL COMMENT 'IP',
	`State` boolean DEFAULT 0 COMMENT '状态',
	CHECK (`State` = 0 OR `State` = 1),
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间' 
);
/*消息表*/
CREATE TABLE 
IF NOT EXISTS `SysMessage`(
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`Sender` INT NOT NULL COMMENT '发送者',
	`Receiver` INT NOT NULL COMMENT '接收者',
	`Title` varchar(50) NULL COMMENT '标题',
	`Content` varchar(2000) NULL COMMENT '内容',
	`State` boolean DEFAULT 0 COMMENT '状态',
	CHECK (`State` = 0 OR `State` = 1),
	`Type` INT NULL COMMENT '类型',
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`SendDate` TIMESTAMP COMMENT '发送时间',
	`ReceiveDate` TIMESTAMP COMMENT '接收时间',
	`CreatTime` TIMESTAMP COMMENT '创建时间'
);

/*菜单列表*/
CREATE TABLE 
IF NOT EXISTS `SysMenus`(
	`ID` INT (11) NOT NULL auto_increment PRIMARY KEY,
	`Name` varchar(30) NOT NULL COMMENT '名称',
	`Icon` varchar(20) NULL COMMENT '图标',
	`Url` varchar(20) NULL COMMENT '地址',
	`Code` varchar(20) NOT NULL COMMENT '标识',
	`PId` INT (11) NULL COMMENT '父类ID',
	`State` boolean DEFAULT 0 COMMENT '状态',
	CHECK (`State` = 0 OR `State` = 1),
	`UpdateTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
	`CreatTime` TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间' 
);