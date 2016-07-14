## 背景 ##
> 平时开发一些小软件，需要实现简单的用户授权，每个小软件都需要单独去写个用户授权的后台，费事还不能集中管理，于是萌生自己写一个通用的软件用户授权系统(网络验证)。
ps:主要是为了学习下[abp](http://www.aspnetboilerplate.com)框架

## 涉及技术(主要) ##
- .net 4.5.2
- [abp](http://www.aspnetboilerplate.com)
- EF 6.0
- AutoMapper
- Mysql

## 使用方法 ##
1. clone 源码
2. 修改Web.config中的connectionString，填写你的数据库连接字符串(默认支持mysql)
3. 使用vs2015打开项目(`SNAS.sln`),打开`工具`->`NuGet包管理器`->`程序包管理器控制台`,在下方打开的控制台中，选择默认项目`SNAS.EntityFramework`,执行命令:`Update-Database`
4. 运行项目,使用账户:admin/123qwe(默认密码如需修改，请修改SNAS.Core.Users.User中的DefaultPassword)登录系统
5. 完成.

## 未实现功能 ##
- 用户管理
- 角色权限控制


## 系统界面截图 ##
![截图1](http://git.oschina.net/milosoft/SNAS/raw/master/document/%E7%B3%BB%E7%BB%9F%E6%88%AA%E5%9B%BE/%E6%88%AA%E5%9B%BE1.png)

![截图2](http://git.oschina.net/milosoft/SNAS/raw/master/document/%E7%B3%BB%E7%BB%9F%E6%88%AA%E5%9B%BE/%E6%88%AA%E5%9B%BE2.png)

![截图3](http://git.oschina.net/milosoft/SNAS/raw/master/document/%E7%B3%BB%E7%BB%9F%E6%88%AA%E5%9B%BE/%E6%88%AA%E5%9B%BE3.png)

![截图4](http://git.oschina.net/milosoft/SNAS/raw/master/document/%E7%B3%BB%E7%BB%9F%E6%88%AA%E5%9B%BE/%E6%88%AA%E5%9B%BE4.png)

## 数据库结构(核心部分) ##
![数据库结构](http://git.oschina.net/milosoft/SNAS/raw/master/document/%E7%B3%BB%E7%BB%9F%E6%88%AA%E5%9B%BE/%E6%95%B0%E6%8D%AE%E5%BA%93%E7%BB%93%E6%9E%84.png)

