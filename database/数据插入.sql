USE `DataBase`;
INSERT INTO SysRolePermissions(`Permissions`,`Name`,`Explain`,`State`)
	VALUES(0,'超级管理员','超级管理员',1),
	(1,'管理员','普通管理员',1);

INSERT INTO SysRoles(`LoginName`,`Name`,`Pwd`,`State`,`Permissions`)
	VALUES('starmile','超级管理员','3F8BAD3587361F4EFF9EAF6BF7E7DA9',1,0),
	('admin001','系统管理员','3F8BAD3587361F4EFF9EAF6BF7E7DA9',1,1);

INSERT INTO SysAuthorities(`PermissionsID`,`Code`,`State`)
	VALUES(1,'SysModule',1),
	(1,'SysAuthorities',1),
	(1,'SysSetting',1),
	(1,'SysViews',1),
	(1,'SysLogs',1);