USE [PetSupplies_8517]
GO
/****** Object:  Table [dbo].[AdMonth]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdMonth](
	[AdMonthID] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NULL,
	[Month] [int] NULL,
	[LockOutEndDate] [datetime] NULL,
	[LockOutStartDate] [datetime] NULL,
	[InHomeStart] [datetime] NULL,
	[InHomeEnd] [datetime] NULL,
	[PetpartnerInfo] [nvarchar](50) NULL,
	[CorpInHomeDate] [datetime] NULL,
	[AdOptionID] [int] NULL,
	[CorpPlanText] [nvarchar](max) NULL,
 CONSTRAINT [PK_AdMonth] PRIMARY KEY CLUSTERED 
(
	[AdMonthID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ADOption]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADOption](
	[ADOptionID] [int] IDENTITY(1,1) NOT NULL,
	[ADOptionName] [nvarchar](50) NULL,
	[MinimumCirculation] [nvarchar](50) NULL,
	[Vendorname] [nvarchar](50) NULL,
 CONSTRAINT [PK_ADOption] PRIMARY KEY CLUSTERED 
(
	[ADOptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AllowedStoreOption]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllowedStoreOption](
	[AllowedStoreOptionID] [int] IDENTITY(1,1) NOT NULL,
	[StoreID] [int] NULL,
	[AdOptionID] [int] NULL,
 CONSTRAINT [PK_AllowedStoreOption] PRIMARY KEY CLUSTERED 
(
	[AllowedStoreOptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Coupon]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon](
	[CouponID] [int] IDENTITY(1,1) NOT NULL,
	[CouponText] [nvarchar](100) NULL,
 CONSTRAINT [PK_Coupon] PRIMARY KEY CLUSTERED 
(
	[CouponID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CouponAdMonth]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CouponAdMonth](
	[AdMonthID] [int] NOT NULL,
	[CouponId] [int] NOT NULL,
 CONSTRAINT [PK_CouponAdMonth] PRIMARY KEY CLUSTERED 
(
	[AdMonthID] ASC,
	[CouponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventLog]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](max) NULL,
	[StackTrace] [varchar](max) NULL,
	[Source] [varchar](max) NULL,
	[Url] [varchar](255) NULL,
	[Datetime] [datetime] NULL,
	[IpAddress] [varchar](20) NULL,
 CONSTRAINT [PK_EventLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistory](
	[LoginHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[TimeStamp] [datetime] NULL CONSTRAINT [DF_LoginHistory_TimeStamp]  DEFAULT (getdate()),
	[IPaddress] [nvarchar](50) NULL,
	[Device] [nvarchar](50) NULL,
	[Browser] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
(
	[LoginHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Market]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Market](
	[MarketID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Market] PRIMARY KEY CLUSTERED 
(
	[MarketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Store]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[StoreID] [int] IDENTITY(1,1) NOT NULL,
	[Ownergroup] [nvarchar](50) NULL,
	[Circulation] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Storename] [nvarchar](50) NULL,
	[MarketID] [int] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[StoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreAdChoice]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreAdChoice](
	[ChoiceID] [int] NULL,
	[AdMonthID] [int] IDENTITY(1,1) NOT NULL,
	[StoreID] [int] NULL,
	[AdOptionID] [int] NULL,
	[TimeStamp] [time](7) NULL,
	[UserID] [int] NULL,
	[IPAddress] [nvarchar](50) NULL,
	[Device] [nvarchar](50) NULL,
	[Browser] [nvarchar](50) NULL,
	[InHomeDate] [datetime] NULL,
	[ChoiceInitials] [nvarchar](50) NULL,
	[FollowedCorporate] [bit] NULL,
 CONSTRAINT [PK_StoreAdChoice] PRIMARY KEY CLUSTERED 
(
	[AdMonthID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreAdChoiceHistory]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreAdChoiceHistory](
	[ChoiceID] [int] IDENTITY(1,1) NOT NULL,
	[AdMonthID] [int] NULL,
	[StoreID] [int] NULL,
	[AdOptionID] [int] NULL,
	[TimeStamp] [time](7) NULL,
	[UserID] [int] NULL,
	[IPAddress] [nvarchar](50) NULL,
	[Device] [nvarchar](50) NULL,
	[Browser] [nvarchar](50) NULL,
	[InHomeDate] [datetime] NULL,
	[ChoiceInitials] [nvarchar](50) NULL,
	[CouponID] [int] NULL,
 CONSTRAINT [PK_StoreAdChoiceHistory] PRIMARY KEY CLUSTERED 
(
	[ChoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Ownername] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[IsAdmin] [bit] NOT NULL CONSTRAINT [DF_User_IsAdmin]  DEFAULT ((0)),
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_User_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserStore]    Script Date: 6/4/2016 6:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStore](
	[UserStoreID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[StoreID] [int] NULL,
 CONSTRAINT [PK_UserStore] PRIMARY KEY CLUSTERED 
(
	[UserStoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[AdMonth] ON 

GO
INSERT [dbo].[AdMonth] ([AdMonthID], [Year], [Month], [LockOutEndDate], [LockOutStartDate], [InHomeStart], [InHomeEnd], [PetpartnerInfo], [CorpInHomeDate], [AdOptionID], [CorpPlanText]) VALUES (1, 2017, 3, CAST(N'2016-06-07 00:00:00.000' AS DateTime), CAST(N'2016-06-09 00:00:00.000' AS DateTime), CAST(N'2016-06-02 00:00:00.000' AS DateTime), CAST(N'2016-06-09 00:00:00.000' AS DateTime), N'sdf', CAST(N'2016-06-15 00:00:00.000' AS DateTime), 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[AdMonth] OFF
GO
SET IDENTITY_INSERT [dbo].[ADOption] ON 

GO
INSERT [dbo].[ADOption] ([ADOptionID], [ADOptionName], [MinimumCirculation], [Vendorname]) VALUES (1, N'Mega', NULL, N'Mega')
GO
INSERT [dbo].[ADOption] ([ADOptionID], [ADOptionName], [MinimumCirculation], [Vendorname]) VALUES (2, N'4-Page', NULL, N'4-Page')
GO
INSERT [dbo].[ADOption] ([ADOptionID], [ADOptionName], [MinimumCirculation], [Vendorname]) VALUES (3, N'Value', NULL, N'Value')
GO
INSERT [dbo].[ADOption] ([ADOptionID], [ADOptionName], [MinimumCirculation], [Vendorname]) VALUES (4, N'Post Card', NULL, N'Post Card')
GO
SET IDENTITY_INSERT [dbo].[ADOption] OFF
GO
SET IDENTITY_INSERT [dbo].[AllowedStoreOption] ON 

GO
INSERT [dbo].[AllowedStoreOption] ([AllowedStoreOptionID], [StoreID], [AdOptionID]) VALUES (1, 2, 1)
GO
INSERT [dbo].[AllowedStoreOption] ([AllowedStoreOptionID], [StoreID], [AdOptionID]) VALUES (3, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[AllowedStoreOption] OFF
GO
SET IDENTITY_INSERT [dbo].[Coupon] ON 

GO
INSERT [dbo].[Coupon] ([CouponID], [CouponText]) VALUES (1, N'This Test Coupon')
GO
INSERT [dbo].[Coupon] ([CouponID], [CouponText]) VALUES (3, N'Next')
GO
SET IDENTITY_INSERT [dbo].[Coupon] OFF
GO
SET IDENTITY_INSERT [dbo].[EventLog] ON 

GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (1, N'Object reference not set to an instance of an object.', N'   at PetSuppliesPlus.Repository.Service.StoreService.Save(StoreModel model) in E:\Svn\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\StoreService.cs:line 214', N'PetSuppliesPlus.Repository', N'http://localhost:60867/Admin/ManageStore/SaveStore', CAST(N'2016-05-27 09:55:52.777' AS DateTime), N'::1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (2, N'Object reference not set to an instance of an object.', N'   at PetSuppliesPlus.Repository.Service.StoreService.Save(StoreModel model) in E:\Svn\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\StoreService.cs:line 214', N'PetSuppliesPlus.Repository', N'http://localhost:60867/Admin/ManageStore/SaveStore', CAST(N'2016-05-27 09:58:55.743' AS DateTime), N'::1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (3, N'Object reference not set to an instance of an object.', N'   at PetSuppliesPlus.Repository.Service.StoreService.Save(StoreModel model) in E:\Svn\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\StoreService.cs:line 214', N'PetSuppliesPlus.Repository', N'http://localhost:60867/Admin/ManageStore/SaveStore', CAST(N'2016-05-27 09:58:55.743' AS DateTime), N'::1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (4, N'Object reference not set to an instance of an object.', N'   at PetSuppliesPlus.Repository.Service.StoreService.Save(StoreModel model) in E:\Svn\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\StoreService.cs:line 214', N'PetSuppliesPlus.Repository', N'http://localhost:60867/Admin/ManageStore/SaveStore', CAST(N'2016-05-27 11:57:02.557' AS DateTime), N'::1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (5, N'Object reference not set to an instance of an object.', N'   at PetSuppliesPlus.Repository.Service.StoreService.Save(StoreModel model) in E:\Svn\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\StoreService.cs:line 214', N'PetSuppliesPlus.Repository', N'http://localhost:60867/Admin/ManageStore/SaveStore', CAST(N'2016-05-27 11:58:16.590' AS DateTime), N'::1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (6, N'Object reference not set to an instance of an object.', N'   at PetSuppliesPlus.Repository.Service.StoreService.Save(StoreModel model) in E:\Svn\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\StoreService.cs:line 214', N'PetSuppliesPlus.Repository', N'http://localhost:60867/Admin/ManageStore/SaveStore', CAST(N'2016-05-27 12:00:31.887' AS DateTime), N'::1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (7, N'Object reference not set to an instance of an object.', N'   at PetSuppliesPlus.Framework.utilityHelper.ReadGlobalMessage(String root, String node) in E:\Svn\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Framework\utilityHelper.cs:line 86
   at PetSuppliesPlus.Repository.Service.StoreService.Save(StoreModel model) in E:\Svn\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\StoreService.cs:line 218', N'PetSuppliesPlus.Framework', N'http://localhost:60867/Admin/ManageStore/SaveStore', CAST(N'2016-05-27 12:02:47.833' AS DateTime), N'::1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (8, N'Object reference not set to an instance of an object.', N'   at PetSuppliesPlus.Framework.utilityHelper.ReadGlobalMessage(String root, String node) in E:\Svn\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Framework\utilityHelper.cs:line 86
   at PetSuppliesPlus.Repository.Service.StoreService.Save(StoreModel model) in E:\Svn\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\StoreService.cs:line 218', N'PetSuppliesPlus.Framework', N'http://localhost:60867/Admin/ManageStore/SaveStore', CAST(N'2016-05-27 12:03:27.830' AS DateTime), N'::1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (9, N'Object reference not set to an instance of an object.', N'   at PetSuppliesPlus.Repository.Service.StoreService.Save(StoreModel model) in E:\Svn\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\StoreService.cs:line 234', N'PetSuppliesPlus.Repository', N'http://localhost:60867/Admin/ManageStore/SaveStore', CAST(N'2016-05-27 12:09:21.120' AS DateTime), N'::1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (10002, N'Object reference not set to an instance of an object.', N'   at PetSuppliesPlus.Repository.Service.UserService.Save(UsersModel model) in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\UserService.cs:line 237', N'PetSuppliesPlus.Repository', N'http://localhost:60867/Admin/ManageUser/SaveUser', CAST(N'2016-06-01 06:36:49.497' AS DateTime), N'127.0.0.1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (10003, N'An error occurred while updating the entries. See the inner exception for details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 72
   at PetSuppliesPlus.Web.Areas.Admin.Controllers.AccountController.DoLogin(LoginModel model) in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Web\Areas\Admin\Controllers\AccountController.cs:line 73', N'EntityFramework', N'http://localhost:60867/Admin/Account/DoLogin', CAST(N'2016-06-01 07:29:57.413' AS DateTime), N'127.0.0.1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (10004, N'An error occurred while updating the entries. See the inner exception for details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 72
   at PetSuppliesPlus.Web.Areas.Admin.Controllers.AccountController.DoLogin(LoginModel model) in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Web\Areas\Admin\Controllers\AccountController.cs:line 73', N'EntityFramework', N'http://localhost:60867/Admin/Account/DoLogin', CAST(N'2016-06-01 08:16:10.517' AS DateTime), N'127.0.0.1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (10005, N'An error occurred while updating the entries. See the inner exception for details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 72
   at PetSuppliesPlus.Web.Areas.Admin.Controllers.AccountController.DoLogin(LoginModel model) in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Web\Areas\Admin\Controllers\AccountController.cs:line 73', N'EntityFramework', N'http://localhost:60867/Admin/Account/DoLogin', CAST(N'2016-06-01 08:20:36.137' AS DateTime), N'127.0.0.1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (10006, N'Conflicting changes detected. This may happen when trying to insert multiple entities with the same key.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 72
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 310', N'EntityFramework', N'http://localhost:60867/Admin/ManageAdMonth/Save', CAST(N'2016-06-01 13:34:54.810' AS DateTime), N'127.0.0.1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (10007, N'An error occurred while updating the entries. See the inner exception for details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 72
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 310', N'EntityFramework', N'http://localhost:60867/Admin/ManageAdMonth/Save', CAST(N'2016-06-01 13:35:02.950' AS DateTime), N'127.0.0.1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (10008, N'An error occurred while updating the entries. See the inner exception for details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 72
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 310', N'EntityFramework', N'http://localhost:60867/Admin/ManageAdMonth/Save', CAST(N'2016-06-01 13:35:14.943' AS DateTime), N'127.0.0.1')
GO
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (10009, N'An error occurred while updating the entries. See the inner exception for details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 72
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in E:\Gaurav Shared\Projects\Austin\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 310', N'EntityFramework', N'http://localhost:60867/Admin/ManageAdMonth/Save', CAST(N'2016-06-01 13:46:53.557' AS DateTime), N'127.0.0.1')
GO
SET IDENTITY_INSERT [dbo].[EventLog] OFF
GO
SET IDENTITY_INSERT [dbo].[LoginHistory] ON 

GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1, 1, CAST(N'2016-06-01 14:01:23.780' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (2, 1, CAST(N'2016-06-01 14:33:15.320' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (3, 1, CAST(N'2016-06-02 11:54:12.077' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (4, 1, CAST(N'2016-06-02 14:43:35.503' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (5, 1, CAST(N'2016-06-02 14:45:51.597' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (6, 1, CAST(N'2016-06-02 15:13:15.917' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (7, 1, CAST(N'2016-06-02 15:37:55.803' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (8, 1, CAST(N'2016-06-02 17:48:50.210' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (9, 1, CAST(N'2016-06-02 17:53:43.797' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (10, 1, CAST(N'2016-06-03 10:36:58.270' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (11, 1, CAST(N'2016-06-03 11:20:33.337' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (12, 1, CAST(N'2016-06-03 11:27:10.950' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (13, 1008, CAST(N'2016-06-03 11:35:15.107' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (14, 1, CAST(N'2016-06-03 12:56:31.920' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (15, 1, CAST(N'2016-06-03 14:45:06.317' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (16, 1, CAST(N'2016-06-03 15:23:11.933' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (17, 1008, CAST(N'2016-06-03 15:28:39.417' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (18, 1, CAST(N'2016-06-03 15:40:06.273' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (19, 1, CAST(N'2016-06-03 15:45:06.163' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (20, 1008, CAST(N'2016-06-03 15:51:28.167' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (21, 1, CAST(N'2016-06-03 15:57:56.427' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (22, 1, CAST(N'2016-06-03 16:07:44.510' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (23, 1, CAST(N'2016-06-03 16:16:00.203' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (24, 1, CAST(N'2016-06-03 16:29:20.687' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (25, 1, CAST(N'2016-06-03 16:30:07.017' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (26, 1, CAST(N'2016-06-03 16:30:38.200' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (27, 1, CAST(N'2016-06-03 16:31:08.763' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (28, 1, CAST(N'2016-06-03 16:31:54.467' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (29, 1, CAST(N'2016-06-03 16:32:38.153' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (30, 1, CAST(N'2016-06-03 16:58:46.660' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (31, 1, CAST(N'2016-06-03 17:42:33.993' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (32, 1008, CAST(N'2016-06-03 17:45:36.060' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (33, 1008, CAST(N'2016-06-03 17:46:28.963' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (34, 1008, CAST(N'2016-06-03 17:46:57.390' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (35, 1, CAST(N'2016-06-03 17:47:02.220' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (36, 1, CAST(N'2016-06-03 17:54:16.227' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (37, 1, CAST(N'2016-06-03 17:57:04.717' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (38, 1, CAST(N'2016-06-03 18:07:22.117' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (39, 1, CAST(N'2016-06-03 18:09:59.847' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (40, 1, CAST(N'2016-06-03 18:11:05.597' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (41, 1, CAST(N'2016-06-03 18:12:24.587' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (42, 1, CAST(N'2016-06-03 18:12:28.107' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (43, 1, CAST(N'2016-06-03 18:14:19.417' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (44, 1, CAST(N'2016-06-03 18:34:52.290' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (45, 1008, CAST(N'2016-06-03 18:37:24.030' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (46, 1, CAST(N'2016-06-03 18:38:16.753' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (47, 1, CAST(N'2016-06-03 18:44:30.913' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (48, 1, CAST(N'2016-06-03 18:49:48.440' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (49, 1, CAST(N'2016-06-04 09:35:13.937' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (50, 1008, CAST(N'2016-06-04 09:43:04.600' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (51, 1, CAST(N'2016-06-04 10:01:54.363' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (52, 1008, CAST(N'2016-06-04 10:20:12.527' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (53, 1, CAST(N'2016-06-04 10:24:11.887' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (54, 1, CAST(N'2016-06-04 10:32:52.633' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (55, 1, CAST(N'2016-06-04 11:01:55.700' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (56, 1008, CAST(N'2016-06-04 11:14:46.027' AS DateTime), N'::1', N'Web', N'Chrome')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (57, 1, CAST(N'2016-06-04 11:52:47.807' AS DateTime), N'::1', N'Web', N'Chrome')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (58, 1, CAST(N'2016-06-04 12:02:16.043' AS DateTime), N'::1', N'Web', N'Chrome')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (59, 1008, CAST(N'2016-06-04 12:04:16.397' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (60, 1, CAST(N'2016-06-04 12:20:08.703' AS DateTime), N'::1', N'Web', N'Chrome')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (61, 1008, CAST(N'2016-06-04 12:21:09.377' AS DateTime), N'::1', N'Web', N'Chrome')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (62, 1, CAST(N'2016-06-04 12:25:42.253' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (63, 1, CAST(N'2016-06-04 12:32:59.800' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (64, 1, CAST(N'2016-06-04 12:33:35.650' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (65, 1, CAST(N'2016-06-04 12:50:16.520' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (66, 1, CAST(N'2016-06-04 12:59:07.620' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (67, 1, CAST(N'2016-06-04 13:03:39.173' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (68, 1, CAST(N'2016-06-04 13:14:50.833' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (69, 1, CAST(N'2016-06-04 13:44:00.303' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (70, 1, CAST(N'2016-06-04 14:32:56.347' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (71, 1, CAST(N'2016-06-04 14:36:11.470' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (72, 1008, CAST(N'2016-06-04 15:59:31.617' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (73, 1, CAST(N'2016-06-04 18:41:45.310' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
SET IDENTITY_INSERT [dbo].[LoginHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[Market] ON 

GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1, N'Mail')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (2, N'Newspaper')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1002, N'Atta')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1003, N'test maar3')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1005, N'test')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1006, N'zasdf')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1007, N'zasdfed')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1008, N'zasdfedss')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1009, N'Sunday Market1')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1010, N'Atta1')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1011, N'Atta Marketf')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1012, N'Check')
GO
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1013, N'check2')
GO
SET IDENTITY_INSERT [dbo].[Market] OFF
GO
SET IDENTITY_INSERT [dbo].[Store] ON 

GO
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [Circulation], [City], [State], [Storename], [MarketID]) VALUES (1, N'new owner', N'circulation2', N'patna', N'bihar', N'storename', 1)
GO
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [Circulation], [City], [State], [Storename], [MarketID]) VALUES (2, N'new group', N'noida', N'dlehi', N'delhi', N'sotre1', 1)
GO
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [Circulation], [City], [State], [Storename], [MarketID]) VALUES (3, N'sdfdsf', N'fsdfsdaf', N'sdfsd', N'fsadfsd', N'fasdfsdafs', 1)
GO
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [Circulation], [City], [State], [Storename], [MarketID]) VALUES (4, N'sdfdsf', N'fsdfsdaf', N'sdfsd', N'fsadfsd', N'fasdfsdafs', 1)
GO
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [Circulation], [City], [State], [Storename], [MarketID]) VALUES (1002, N'Jack', N'Main Market', N'Delhi', N'NCR', N'Jack Store', 1)
GO
SET IDENTITY_INSERT [dbo].[Store] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1, N'Admin', N'admin@gmail.com', N'TWZd8ohZNpN7Wz9X0Ba0gA==', 1, 1)
GO
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1007, N'Test', N'test@gmail.com', N'TWZd8ohZNpN7Wz9X0Ba0gA==', 0, 0)
GO
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1008, N'Jack', N'jack@gmail.com', N'TWZd8ohZNpN7Wz9X0Ba0gA==', 0, 1)
GO
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1009, N'test', N'test@test.com', N'TWZd8ohZNpN7Wz9X0Ba0gA==', 0, 1)
GO
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1010, N'test', N'test@t.com', N'TWZd8ohZNpN7Wz9X0Ba0gA==', 0, 0)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserStore] ON 

GO
INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (1, 1007, 1)
GO
INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (3, 1009, 3)
GO
INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (4, 1010, 3)
GO
INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (6, 1008, 1)
GO
INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (7, 1008, 1)
GO
INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (9, 1008, 1)
GO
INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (10, 1008, 1002)
GO
INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (11, 1008, 4)
GO
SET IDENTITY_INSERT [dbo].[UserStore] OFF
GO
ALTER TABLE [dbo].[AdMonth]  WITH CHECK ADD  CONSTRAINT [FK_AdMonth_ADOption] FOREIGN KEY([AdOptionID])
REFERENCES [dbo].[ADOption] ([ADOptionID])
GO
ALTER TABLE [dbo].[AdMonth] CHECK CONSTRAINT [FK_AdMonth_ADOption]
GO
ALTER TABLE [dbo].[AllowedStoreOption]  WITH CHECK ADD  CONSTRAINT [FK_AllowedStoreOption_ADOption] FOREIGN KEY([AdOptionID])
REFERENCES [dbo].[ADOption] ([ADOptionID])
GO
ALTER TABLE [dbo].[AllowedStoreOption] CHECK CONSTRAINT [FK_AllowedStoreOption_ADOption]
GO
ALTER TABLE [dbo].[AllowedStoreOption]  WITH CHECK ADD  CONSTRAINT [FK_AllowedStoreOption_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([StoreID])
GO
ALTER TABLE [dbo].[AllowedStoreOption] CHECK CONSTRAINT [FK_AllowedStoreOption_Store]
GO
ALTER TABLE [dbo].[CouponAdMonth]  WITH CHECK ADD  CONSTRAINT [FK_CouponAdMonth_AdMonth] FOREIGN KEY([AdMonthID])
REFERENCES [dbo].[AdMonth] ([AdMonthID])
GO
ALTER TABLE [dbo].[CouponAdMonth] CHECK CONSTRAINT [FK_CouponAdMonth_AdMonth]
GO
ALTER TABLE [dbo].[CouponAdMonth]  WITH CHECK ADD  CONSTRAINT [FK_CouponAdMonth_Coupon] FOREIGN KEY([CouponId])
REFERENCES [dbo].[Coupon] ([CouponID])
GO
ALTER TABLE [dbo].[CouponAdMonth] CHECK CONSTRAINT [FK_CouponAdMonth_Coupon]
GO
ALTER TABLE [dbo].[LoginHistory]  WITH CHECK ADD  CONSTRAINT [FK_LoginHistory_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[LoginHistory] CHECK CONSTRAINT [FK_LoginHistory_User]
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_Market] FOREIGN KEY([MarketID])
REFERENCES [dbo].[Market] ([MarketID])
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [FK_Store_Market]
GO
ALTER TABLE [dbo].[StoreAdChoice]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoice_ADOption] FOREIGN KEY([AdOptionID])
REFERENCES [dbo].[ADOption] ([ADOptionID])
GO
ALTER TABLE [dbo].[StoreAdChoice] CHECK CONSTRAINT [FK_StoreAdChoice_ADOption]
GO
ALTER TABLE [dbo].[StoreAdChoice]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoice_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([StoreID])
GO
ALTER TABLE [dbo].[StoreAdChoice] CHECK CONSTRAINT [FK_StoreAdChoice_Store]
GO
ALTER TABLE [dbo].[StoreAdChoice]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoice_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[StoreAdChoice] CHECK CONSTRAINT [FK_StoreAdChoice_User]
GO
ALTER TABLE [dbo].[StoreAdChoiceHistory]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoiceHistory_Coupon] FOREIGN KEY([ChoiceID])
REFERENCES [dbo].[Coupon] ([CouponID])
GO
ALTER TABLE [dbo].[StoreAdChoiceHistory] CHECK CONSTRAINT [FK_StoreAdChoiceHistory_Coupon]
GO
ALTER TABLE [dbo].[UserStore]  WITH CHECK ADD  CONSTRAINT [FK_UserStore_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([StoreID])
GO
ALTER TABLE [dbo].[UserStore] CHECK CONSTRAINT [FK_UserStore_Store]
GO
ALTER TABLE [dbo].[UserStore]  WITH CHECK ADD  CONSTRAINT [FK_UserStore_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserStore] CHECK CONSTRAINT [FK_UserStore_User]
GO
