CREATE TABLE `PersonInfo` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` varchar(30)  NOT NULL COMMENT '姓名',
  `Tel` varchar(30)  NOT NULL COMMENT '電話',
  `CreateTime` datetime COMMENT '新增時間',
  `EditTime` datetime COMMENT '編輯時間',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='練習用人員資料表';


-- 新增資料
INSERT INTO PersonInfo 
(Name, Tel, CreateTime, EditTime)
values 
('測試1', '0912345678', '2022-09-14 00:00:00', null),
('測試2', '0912345671', '2022-09-14 00:00:00', null),
('測試3', '0912345672', '2022-09-14 00:00:00', null)