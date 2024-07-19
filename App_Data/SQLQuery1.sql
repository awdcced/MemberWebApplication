-- 检查数据库是否存在，如果不存在则创建  
use master

go

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'MemberManagementSystemDB')  

BEGIN  

    Create DATABASE MemberManagementSystemDB;

END  

GO  

USE MemberManagementSystemDB;  

GO  

-- 创建基础数据类别表  

CREATE TABLE Categories (  

    C_Category VARCHAR(20) PRIMARY KEY,  

    C_Illustration VARCHAR(20),  

    C_IsShow BIT  

);  

-- 创建基础数据项表  

CREATE TABLE CategoryItems (  
    C_ID INT IDENTITY(1,1) PRIMARY KEY,

    C_Category VARCHAR(20),

    CI_ID INT ,

    CI_Name VARCHAR(20),  

    FOREIGN KEY (C_Category) REFERENCES Categories(C_Category)  

);  

-- 创建店铺表  

CREATE TABLE Shops (  

    S_ID INT IDENTITY(1,1) PRIMARY KEY,  

    S_Name VARCHAR(20),  

    S_Category INT, -- 注意：这里可能需要一个外键指向另一个表，但你没有提供该表  

    S_ContactName VARCHAR(20),  

    S_ContactTel VARCHAR(20),  

    S_Address VARCHAR(50),  

    S_Remark VARCHAR(100),  

    S_IsHasSetAdmin BIT,  

    S_CreateTime DATETIME  

);  

-- 创建用户表  

CREATE TABLE Users (  

    U_ID INT IDENTITY(1,1) PRIMARY KEY,  

    S_ID INT,  

    U_LoginName NVARCHAR(20),  

    U_Password NVARCHAR(50),  

    U_RealName NVARCHAR(20),  

    U_Sex NVARCHAR(10),  

    U_Telephone NVARCHAR(20),  

    U_Role INT,  

    U_CanDelete BIT,  

    FOREIGN KEY (S_ID) REFERENCES Shops(S_ID)  

);  

-- 创建会员等级表  

CREATE TABLE CardLevels (  

    CL_ID INT IDENTITY(1,1) PRIMARY KEY,  

    CL_LevelName NVARCHAR(20),  

    CL_NeedPoint NVARCHAR(50), -- 注意：这里使用NVARCHAR可能不是最佳选择，如果存储数字则使用INT或BIGINT  

    CL_Point FLOAT,  

    CL_Percent FLOAT  

);  

-- 创建会员表  

CREATE TABLE MemCards (  

    MC_ID INT IDENTITY(1,1) PRIMARY KEY,  

    CL_ID INT,  

    S_ID INT,  

    MC_CardID NVARCHAR(50) UNIQUE,  

    MC_Password NVARCHAR(20),  

    MC_Name NVARCHAR(20),  

    MC_Sex INT, -- 注意：这里使用INT可能不是最佳选择，如果只有男女则使用BIT或CHAR(1)  

    MC_Mobile NVARCHAR(50),  

    MC_Photo VARCHAR(200),  

    MC_Birthday_Month INT,  

    MC_Birthday_Day INT,  

    MC_BirthdayType TINYINT,  

    MC_IsPast TINYINT,  

    MC_PastTime DATETIME,  

    MC_Point INT,  

    MC_Money REAL,  

    MC_TotalMoney REAL,  

    MC_TotalCount INT,  

    MC_State INT,  

    MC_IsPointAuto TINYINT,  

    MC_RefererID INT,  

    MC_RefererCard NVARCHAR(50),  

    MC_RefererName NVARCHAR(20),  

    MC_CreateTime DATETIME,  

    FOREIGN KEY (CL_ID) REFERENCES CardLevels(CL_ID),  

    FOREIGN KEY (S_ID) REFERENCES Shops(S_ID)  

);  

-- 创建礼品表  

CREATE TABLE ExchangGifts (  

    EG_ID INT IDENTITY(1,1) PRIMARY KEY,  

    S_ID INT,  

    EG_GiftCode VARCHAR(255),  

    EG_GiftName VARCHAR(255),  

    EG_Photo VARCHAR(255),  

    EG_Point INT,  

    EG_Number INT,  

    EG_ExchangNum INT,  

    EG_Remark VARCHAR(255),  

    FOREIGN KEY (S_ID) REFERENCES Shops(S_ID)  

); 

-- 创建消费订单表  

CREATE TABLE ConsumptionOrders (  
    CO_ID INT IDENTITY(1,1) PRIMARY KEY,  
    MC_ID INT,  
    S_ID INT,  
    CO_TotalPrice DECIMAL(10, 2),  
    CO_CreateTime DATETIME,  
    CO_Status TINYINT, -- 订单状态，例如：0=待支付，1=已支付，2=已取消等  
    FOREIGN KEY (MC_ID) REFERENCES MemCards(MC_ID),  
    FOREIGN KEY (S_ID) REFERENCES Shops(S_ID)  
);  

CREATE TABLE TransferLogs (  

    TL_ID INT IDENTITY(1,1) PRIMARY KEY,  

    S_ID INT NOT NULL,  

    U_ID INT NOT NULL,  

    TL_FromMC_ID INT,  

    TL_FromMC_CardID NVARCHAR(50),  

    TL_ToMC_ID INT,  

    TL_ToMC_CardID NVARCHAR(50),  

    TL_TransferMoney MONEY NOT NULL,  

    TL_Remark VARCHAR(200),  

    TL_CreateTime DATETIME NOT NULL DEFAULT GETDATE(),  

    FOREIGN KEY (S_ID) REFERENCES Shops(S_ID), -- Shops表存在且S_ID是主键  

    FOREIGN KEY (U_ID) REFERENCES Users(U_ID), -- Users表U_ID是主键  
);

CREATE TABLE ExchangLogs (  

    EL_ID INT IDENTITY(1,1) PRIMARY KEY,  

    S_ID INT NOT NULL,  

    U_ID INT NOT NULL,  

    MC_ID INT,  

    MC_CardID NVARCHAR(50),  

    MC_Name NVARCHAR(20),  

    EG_ID INT,  

    EG_GiftCode NVARCHAR(50),  

    EG_GiftName NVARCHAR(50),  

    EL_Number INT NOT NULL,  

    EL_Point INT NOT NULL,  

    EL_CreateTime DATETIME NOT NULL DEFAULT GETDATE(),  

    FOREIGN KEY (S_ID) REFERENCES Shops(S_ID), -- Shops表S_ID是主键  

    FOREIGN KEY (U_ID) REFERENCES Users(U_ID), -- Users表U_ID是主键  

);
CREATE TABLE Categories (  

    C_Category VARCHAR(20) PRIMARY KEY,  

    C_Illustration VARCHAR(20),  

    C_IsShow BIT  

);  
INSERT INTO Categories VALUES('S_Category','店铺类型',1)
INSERT INTO Categories VALUES('U_Role','身份',1)
INSERT INTO Categories VALUES('MC_State','会员状态',1)
INSERT INTO Categories VALUES('CO_OrderType','积分操作类型',1)
INSERT INTO CategoryItems VALUES('S_Category',1,'总部')
INSERT INTO CategoryItems VALUES('S_Category',2,'加盟店')
INSERT INTO CategoryItems VALUES('S_Category',3,'直营店')
INSERT INTO CategoryItems VALUES('U_Role',1,'系统管理员')
INSERT INTO CategoryItems VALUES('U_Role',2,'店长')
INSERT INTO CategoryItems VALUES('U_Role',3,'业务员')
INSERT INTO CategoryItems VALUES('MC_State',1,'正常')
INSERT INTO CategoryItems VALUES('MC_State',2,'挂失')
INSERT INTO CategoryItems VALUES('MC_State',3,'锁定')
INSERT INTO CategoryItems VALUES('CO_OrderType',1,'兑换积分')
INSERT INTO CategoryItems VALUES('CO_OrderType',2,'积分返现')
INSERT INTO CategoryItems VALUES('CO_OrderType',3,'减积分')
INSERT INTO CategoryItems VALUES('CO_OrderType',4,'转介绍积分')
INSERT INTO CategoryItems VALUES('CO_OrderType',5,'快速消费')

INSERT INTO Shops (S_Name, S_Category, S_ContactName, S_ContactTel, S_Address, S_Remark, S_IsHasSetAdmin, S_CreateTime)  
VALUES ('光谷步行街店', 1, '张三', '13800138000', '武汉市洪山区光谷步行街', '新店开业，优惠多多！', 1, '2023-04-01 08:00:00');  
  
INSERT INTO Shops (S_Name, S_Category, S_ContactName, S_ContactTel, S_Address, S_Remark, S_IsHasSetAdmin, S_CreateTime)  
VALUES ('江汉路店', 1, '李四', '13900139000', '武汉市江汉区江汉路步行街', '老店新开，品质保证！', 1, '2023-04-05 10:00:00');  
  
INSERT INTO Shops (S_Name, S_Category, S_ContactName, S_ContactTel, S_Address, S_Remark, S_IsHasSetAdmin, S_CreateTime)  
VALUES ('徐东大街店', 1, '王五', '13700137000', '武汉市武昌区徐东大街', '进口零食，应有尽有！', 1, '2023-04-10 12:00:00');  
  
INSERT INTO Shops (S_Name, S_Category, S_ContactName, S_ContactTel, S_Address, S_Remark, S_IsHasSetAdmin, S_CreateTime)  
VALUES ('汉街店', 1, '赵六', '13600136000', '武汉市武昌区楚河汉街', '网红打卡地，零食也疯狂！', 1, '2023-04-15 14:00:00');  
  
INSERT INTO Shops (S_Name, S_Category, S_ContactName, S_ContactTel, S_Address, S_Remark, S_IsHasSetAdmin, S_CreateTime)  
VALUES ('王家湾店', 1, '孙七', '13500135000', '武汉市汉阳区王家湾', '家庭聚会，零食必备！', 1, '2023-04-20 16:00:00');  
  
INSERT INTO Shops (S_Name, S_Category, S_ContactName, S_ContactTel, S_Address, S_Remark, S_IsHasSetAdmin, S_CreateTime)  
VALUES ('青山和平大道店', 1, '周八', '13400134000', '武汉市青山区和平大道', '青山区首店，欢迎光临！', 1, '2023-04-25 18:00:00');  
  
INSERT INTO Shops (S_Name, S_Category, S_ContactName, S_ContactTel, S_Address, S_Remark, S_IsHasSetAdmin, S_CreateTime)  
VALUES ('街道口店', 1, '吴九', '13300133000', '武汉市洪山区街道口', '学生最爱，价格亲民！', 1, '2023-05-01 20:00:00');  
  
INSERT INTO Shops (S_Name, S_Category, S_ContactName, S_ContactTel, S_Address, S_Remark, S_IsHasSetAdmin, S_CreateTime)  
VALUES ('光谷广场店', 1, '郑十', '13200132000', '武汉市洪山区光谷广场', '购物天堂，零食满满！', 1, '2023-05-05 22:00:00');  

-- 插入光谷步行街店的管理员用户  
INSERT INTO Users  
VALUES (1, 'zhangsan_admin', 'Password123', '张三', '1', '13800138000', 2, 1);  
  
-- 插入江汉路店的管理员用户  
INSERT INTO Users 
VALUES (2, 'lisi_admin', 'Password123', '李四', '1', '13900139000', 2, 1);  
  
-- 插入徐东大街店的管理员用户  
INSERT INTO Users
VALUES (3, 'wangwu_admin', 'Password123', '王五', '1', '13800137000', 2, 1);  
  
-- 插入汉街店的管理员用户  
INSERT INTO Users
VALUES (4, 'zhaoliu_admin', 'Password123', '赵六', '1', '13800136000', 2, 1);  
  
-- 插入王家湾店的管理员用户  
INSERT INTO Users
VALUES (5, 'sunqi_admin', 'Password123', '孙七',  '1', '13800135000', 2, 1);  
  
-- 插入青山和平大道店的管理员用户  
INSERT INTO Users 
VALUES (6, 'zhouba_admin', 'Password123', '周八', '1', '13800134000', 2, 1);  
  
-- 插入街道口店的管理员用户  
INSERT INTO Users  
VALUES (7, 'wujiu_admin', 'Password123', '吴九',  '1', '13800133000', 2, 1);  
  
-- 插入光谷广场店的管理员用户  
INSERT INTO Users
VALUES (8, 'zhengshi_admin', 'Password123', '郑十',  '1', '13800132000', 2, 1);
select * from Shops
select * from Users
  