USE [Automanage]
GO
/****** 对象:  Table [dbo].[T_Ecard_Receive_Details]    脚本日期: 05/20/2013 15:10:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Ecard_Receive_Details](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[on_duty_id] [int] NOT NULL,
	[work_no] [varchar](10) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[account] [int] NOT NULL CONSTRAINT [DF_T_Ecard_Receive_Details_account]  DEFAULT (0),
	[position] [int] NOT NULL,
	[isReceived] [int] NOT NULL CONSTRAINT [DF_T_Ecard_Receive_Details_isReceived]  DEFAULT (0),
	[addTime] [datetime] NULL CONSTRAINT [DF_T_Ecard_Receive_Details_addTime]  DEFAULT (getdate()),
	[receiveTime] [datetime] NULL,
	[IpAddress] [varchar](15) COLLATE Chinese_PRC_CI_AS NULL,
	[oper] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[flag] [int] NULL,
	[ryCount] [int] NULL CONSTRAINT [DF_T_Ecard_Receive_Details_ryCount]  DEFAULT (2),
 CONSTRAINT [PK_T_Ecard_Receive_Details] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[T_ECARD_Black](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[work_no] [varchar](10) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[addtime] [datetime] NOT NULL CONSTRAINT [DF_T_ECARD_Black_addtime]  DEFAULT (getdate()),
	[transfered] [int] NOT NULL CONSTRAINT [DF_T_ECARD_Black_transfered]  DEFAULT (0),
 CONSTRAINT [PK_T_ECARD_Black] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[T_ECard_Other](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bat_id] [int] NULL,
	[work_no] [varchar](10) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[account] [float] NOT NULL CONSTRAINT [DF_T_ECard_Other_account]  DEFAULT (0),
	[type] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[isReceived] [int] NOT NULL CONSTRAINT [DF_T_ECard_Other_isReceived]  DEFAULT (0),
	[receiveTime] [datetime] NULL,
	[ipAddress] [varchar](15) COLLATE Chinese_PRC_CI_AS NULL,
	[oper] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[isValid] [int] NULL CONSTRAINT [DF_T_ECard_Other_isValid]  DEFAULT (1),
 CONSTRAINT [PK_T_ECard_Other] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[T_Ecard_Other_Bat](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[batname] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[addtime] [datetime] NOT NULL CONSTRAINT [DF_T_Ecard_Other_Bat_addtime]  DEFAULT (getdate()),
	[oper] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ipAddress] [varchar](15) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_T_Ecard_Other_Bat] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) ON [PRIMARY]
) ON [PRIMARY]



go


CREATE VIEW [dbo].[V_Ecard_Other]
AS
SELECT     dbo.T_ECard_Other.work_no, dbo.T_ECard_Other.account, dbo.T_ECard_Other.type, dbo.T_ECard_Other.isReceived, dbo.T_ECard_Other.ipAddress, 
                      dbo.T_ECard_Other.oper, dbo.T_ECard_Other.id, dbo.Person.Work_name, dbo.Person.Handset, dbo.T_ECard_Other.receiveTime, 
                      dbo.T_ECard_Other.bat_id, dbo.T_Ecard_Other_Bat.batname, dbo.T_Ecard_Other_Bat.oper AS addOPer, dbo.T_Ecard_Other_Bat.ipAddress AS addIP, 
                      dbo.T_Ecard_Other_Bat.addtime, dbo.T_ECard_Other.isValid
FROM         dbo.T_ECard_Other INNER JOIN
                      dbo.Person ON dbo.T_ECard_Other.work_no = dbo.Person.Work_no INNER JOIN
                      dbo.T_Ecard_Other_Bat ON dbo.T_ECard_Other.bat_id = dbo.T_Ecard_Other_Bat.id
WHERE     (dbo.T_ECard_Other.isValid = 1)

GO
CREATE VIEW [dbo].[V_Ecard_Plans]
AS
SELECT     dbo.Engi_Plan.Plan_id, dbo.Person_Plan.Person_plan_id, dbo.On_Duty.id AS on_duty_id, dbo.Engi_Plan.Engi_brand, dbo.Engi_Plan.Engi_no, 
                      CAST(CONVERT(varchar(10), dbo.Engi_Plan.Plan_Date, 120) + ' ' + dbo.Engi_Plan.Open_time AS datetime) AS open_time, dbo.Engi_Plan.Roadway, 
                      dbo.Engi_Plan.qduan, dbo.Person_Plan.Driver_1no, dbo.Person_Plan.Driver_1name, dbo.Person_Plan.driver_2no, dbo.Person_Plan.driver_2name, 
                      dbo.Person_Plan.driver_3NO, dbo.Person_Plan.driver_3Name, dbo.Person_Plan.driver_4NO, dbo.Person_Plan.driver_4Name, 
                      dbo.Person_Plan.driver_5no, dbo.Person_Plan.driver_5name, dbo.On_Duty.whole_time, dbo.On_Duty.Arrive_time, dbo.On_Duty.Return_time, 
                      dbo.On_Duty.Return_time2, dbo.Engi_Plan.TractionType, dbo.T_Engi_QYLX.lx_type, dbo.On_Duty.Arrive_time2, dbo.On_Duty.Return_roadway, 
                      dbo.On_Duty.Return_roadway2
FROM         dbo.Person_Plan INNER JOIN
                      dbo.On_Duty ON dbo.Person_Plan.Person_plan_id = dbo.On_Duty.person_plan_id INNER JOIN
                      dbo.Engi_Plan ON dbo.Person_Plan.Plan_id = dbo.Engi_Plan.Plan_id LEFT OUTER JOIN
                      dbo.T_Engi_QYLX ON dbo.Engi_Plan.TractionType = dbo.T_Engi_QYLX.lx_name

GO


CREATE VIEW [dbo].[V_Ecard_Details]
AS
SELECT     dbo.T_Ecard_Receive_Details.id, dbo.T_Ecard_Receive_Details.on_duty_id, dbo.T_Ecard_Receive_Details.work_no, 
                      dbo.T_Ecard_Receive_Details.account, dbo.T_Ecard_Receive_Details.isReceived, dbo.T_Ecard_Receive_Details.addTime, 
                      dbo.T_Ecard_Receive_Details.receiveTime, dbo.T_Ecard_Receive_Details.IpAddress, dbo.T_Ecard_Receive_Details.oper, dbo.Person.Work_name, 
                      dbo.Person.Department, dbo.Person.Handset, dbo.V_Ecard_Plans.Engi_brand, dbo.V_Ecard_Plans.Engi_no, dbo.V_Ecard_Plans.open_time, 
                      dbo.V_Ecard_Plans.Roadway, dbo.V_Ecard_Plans.qduan, dbo.V_Ecard_Plans.Return_time, dbo.V_Ecard_Plans.Return_time2, 
                      dbo.V_Ecard_Plans.Arrive_time, dbo.V_Ecard_Plans.whole_time, dbo.T_Ecard_Receive_Details.position, dbo.T_Ecard_Receive_Details.flag, 
                      dbo.V_Ecard_Plans.Arrive_time2, dbo.V_Ecard_Plans.Return_roadway2, dbo.V_Ecard_Plans.Return_roadway, 
                      dbo.T_Ecard_Receive_Details.ryCount
FROM         dbo.T_Ecard_Receive_Details INNER JOIN
                      dbo.Person ON dbo.T_Ecard_Receive_Details.work_no = dbo.Person.Work_no INNER JOIN
                      dbo.V_Ecard_Plans ON dbo.T_Ecard_Receive_Details.work_no = dbo.V_Ecard_Plans.Driver_1no AND dbo.T_Ecard_Receive_Details.position = 1 AND 
                      dbo.T_Ecard_Receive_Details.on_duty_id = dbo.V_Ecard_Plans.on_duty_id
UNION
SELECT     T_Ecard_Receive_Details_1.id, T_Ecard_Receive_Details_1.on_duty_id, T_Ecard_Receive_Details_1.work_no, T_Ecard_Receive_Details_1.account, 
                      T_Ecard_Receive_Details_1.isReceived, T_Ecard_Receive_Details_1.addTime, T_Ecard_Receive_Details_1.receiveTime, 
                      T_Ecard_Receive_Details_1.IpAddress, T_Ecard_Receive_Details_1.oper, Person_1.Work_name, Person_1.Department, Person_1.Handset, 
                      V_Ecard_Plans_1.Engi_brand, V_Ecard_Plans_1.Engi_no, V_Ecard_Plans_1.open_time, V_Ecard_Plans_1.Roadway, V_Ecard_Plans_1.qduan, 
                      V_Ecard_Plans_1.Return_time, V_Ecard_Plans_1.Return_time2, V_Ecard_Plans_1.Arrive_time, V_Ecard_Plans_1.whole_time, 
                      T_Ecard_Receive_Details_1.position, T_Ecard_Receive_Details_1.flag, V_Ecard_Plans_1.Arrive_time2, V_Ecard_Plans_1.Return_roadway2, 
                      V_Ecard_Plans_1.Return_roadway, T_Ecard_Receive_Details_1.ryCount
FROM         dbo.T_Ecard_Receive_Details AS T_Ecard_Receive_Details_1 INNER JOIN
                      dbo.Person AS Person_1 ON T_Ecard_Receive_Details_1.work_no = Person_1.Work_no INNER JOIN
                      dbo.V_Ecard_Plans AS V_Ecard_Plans_1 ON T_Ecard_Receive_Details_1.work_no = V_Ecard_Plans_1.driver_2no AND 
                      T_Ecard_Receive_Details_1.position = 2 AND T_Ecard_Receive_Details_1.on_duty_id = V_Ecard_Plans_1.on_duty_id
UNION
SELECT     T_Ecard_Receive_Details_1.id, T_Ecard_Receive_Details_1.on_duty_id, T_Ecard_Receive_Details_1.work_no, T_Ecard_Receive_Details_1.account, 
                      T_Ecard_Receive_Details_1.isReceived, T_Ecard_Receive_Details_1.addTime, T_Ecard_Receive_Details_1.receiveTime, 
                      T_Ecard_Receive_Details_1.IpAddress, T_Ecard_Receive_Details_1.oper, Person_1.Work_name, Person_1.Department, Person_1.Handset, 
                      V_Ecard_Plans_1.Engi_brand, V_Ecard_Plans_1.Engi_no, V_Ecard_Plans_1.open_time, V_Ecard_Plans_1.Roadway, V_Ecard_Plans_1.qduan, 
                      V_Ecard_Plans_1.Return_time, V_Ecard_Plans_1.Return_time2, V_Ecard_Plans_1.Arrive_time, V_Ecard_Plans_1.whole_time, 
                      T_Ecard_Receive_Details_1.position, T_Ecard_Receive_Details_1.flag, V_Ecard_Plans_1.Arrive_time2, V_Ecard_Plans_1.Return_roadway2, 
                      V_Ecard_Plans_1.Return_roadway, T_Ecard_Receive_Details_1.ryCount
FROM         dbo.T_Ecard_Receive_Details AS T_Ecard_Receive_Details_1 INNER JOIN
                      dbo.Person AS Person_1 ON T_Ecard_Receive_Details_1.work_no = Person_1.Work_no INNER JOIN
                      dbo.V_Ecard_Plans AS V_Ecard_Plans_1 ON T_Ecard_Receive_Details_1.work_no = V_Ecard_Plans_1.driver_3NO AND 
                      T_Ecard_Receive_Details_1.position = 3 AND T_Ecard_Receive_Details_1.on_duty_id = V_Ecard_Plans_1.on_duty_id
UNION
SELECT     T_Ecard_Receive_Details_1.id, T_Ecard_Receive_Details_1.on_duty_id, T_Ecard_Receive_Details_1.work_no, T_Ecard_Receive_Details_1.account, 
                      T_Ecard_Receive_Details_1.isReceived, T_Ecard_Receive_Details_1.addTime, T_Ecard_Receive_Details_1.receiveTime, 
                      T_Ecard_Receive_Details_1.IpAddress, T_Ecard_Receive_Details_1.oper, Person_1.Work_name, Person_1.Department, Person_1.Handset, 
                      V_Ecard_Plans_1.Engi_brand, V_Ecard_Plans_1.Engi_no, V_Ecard_Plans_1.open_time, V_Ecard_Plans_1.Roadway, V_Ecard_Plans_1.qduan, 
                      V_Ecard_Plans_1.Return_time, V_Ecard_Plans_1.Return_time2, V_Ecard_Plans_1.Arrive_time, V_Ecard_Plans_1.whole_time, 
                      T_Ecard_Receive_Details_1.position, T_Ecard_Receive_Details_1.flag, V_Ecard_Plans_1.Arrive_time2, V_Ecard_Plans_1.Return_roadway2, 
                      V_Ecard_Plans_1.Return_roadway, T_Ecard_Receive_Details_1.ryCount
FROM         dbo.T_Ecard_Receive_Details AS T_Ecard_Receive_Details_1 INNER JOIN
                      dbo.Person AS Person_1 ON T_Ecard_Receive_Details_1.work_no = Person_1.Work_no INNER JOIN
                      dbo.V_Ecard_Plans AS V_Ecard_Plans_1 ON T_Ecard_Receive_Details_1.work_no = V_Ecard_Plans_1.driver_4NO AND 
                      T_Ecard_Receive_Details_1.position = 4 AND T_Ecard_Receive_Details_1.on_duty_id = V_Ecard_Plans_1.on_duty_id
UNION
SELECT     T_Ecard_Receive_Details_1.id, T_Ecard_Receive_Details_1.on_duty_id, T_Ecard_Receive_Details_1.work_no, T_Ecard_Receive_Details_1.account, 
                      T_Ecard_Receive_Details_1.isReceived, T_Ecard_Receive_Details_1.addTime, T_Ecard_Receive_Details_1.receiveTime, 
                      T_Ecard_Receive_Details_1.IpAddress, T_Ecard_Receive_Details_1.oper, Person_1.Work_name, Person_1.Department, Person_1.Handset, 
                      V_Ecard_Plans_1.Engi_brand, V_Ecard_Plans_1.Engi_no, V_Ecard_Plans_1.open_time, V_Ecard_Plans_1.Roadway, V_Ecard_Plans_1.qduan, 
                      V_Ecard_Plans_1.Return_time, V_Ecard_Plans_1.Return_time2, V_Ecard_Plans_1.Arrive_time, V_Ecard_Plans_1.whole_time, 
                      T_Ecard_Receive_Details_1.position, T_Ecard_Receive_Details_1.flag, V_Ecard_Plans_1.Arrive_time2, V_Ecard_Plans_1.Return_roadway2, 
                      V_Ecard_Plans_1.Return_roadway, T_Ecard_Receive_Details_1.ryCount
FROM         dbo.T_Ecard_Receive_Details AS T_Ecard_Receive_Details_1 INNER JOIN
                      dbo.Person AS Person_1 ON T_Ecard_Receive_Details_1.work_no = Person_1.Work_no INNER JOIN
                      dbo.V_Ecard_Plans AS V_Ecard_Plans_1 ON T_Ecard_Receive_Details_1.work_no = V_Ecard_Plans_1.driver_5no AND 
                      T_Ecard_Receive_Details_1.position = 5 AND T_Ecard_Receive_Details_1.on_duty_id = V_Ecard_Plans_1.on_duty_id

GO

SET ANSI_PADDING OFF