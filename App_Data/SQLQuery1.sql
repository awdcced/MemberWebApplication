-- 检查数据库是否存在，如果不存在则创建  
use master

go

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'MemberManagementSystem')  

BEGIN  

    CREATE DATABASE MemberManagementSystem;  

END  

GO  

USE MemberManagementSystem;  

GO  

-- 创建基础数据类别表  

CREATE TABLE Categories (  

    C_Category VARCHAR(20) PRIMARY KEY,  

    C_Illustration VARCHAR(20),  

    C_IsShow BIT  

);  

-- 创建基础数据项表  

CREATE TABLE CategoryItems (  

    C_Category VARCHAR(20),  

    CI_ID INT IDENTITY(1,1) PRIMARY KEY,  

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