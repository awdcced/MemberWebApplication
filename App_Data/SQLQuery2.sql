INSERT INTO Shops (S_Name, S_Category, S_ContactName, S_ContactTel, S_Address, S_Remark, S_IsHasSetAdmin, S_CreateTime)  
VALUES  
('科技之光', 1, '张三', '13800138000', '北京市海淀区', '主营电子产品', 1, '2023-04-01 08:00:00'),  
('时尚前沿', 2, '李四', '13900139000', '上海市黄浦区', '主营服装服饰', 0, '2023-04-02 10:00:00'),  
('书香门第', 3, '王五', '13700137000', '广州市天河区', '主营图书', 1, '2023-04-03 12:00:00'),  
('家居乐园', 4, '赵六', '13600136000', '深圳市南山区', '主营家居用品', 1, '2023-04-04 14:00:00');

INSERT INTO Users (S_ID, U_LoginName, U_Password, U_RealName, U_Sex, U_Telephone, U_Role, U_CanDelete)  
VALUES  
(1, 'zhangsan', 'password123', '张三', '男', '13800138000', 1, 1),  
(1, 'zhangsan_admin', 'adminpass', '张三管理员', '男', '13800138001', 2, 1),  
(2, 'lisi', 'lisipass', '李四', '女', '13900139000', 1, 0),  
(3, 'wangwu', 'wangwupass', '王五', '男', '13700137000', 3, 1),  
(4, 'zhaoliu', 'zhaoliupass', '赵六', '女', '13600136000', 2, 1);