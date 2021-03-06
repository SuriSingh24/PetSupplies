USE [PetSupplies_8517]
GO
/****** Object:  Table [dbo].[AdMonth]    Script Date: 6/20/2016 7:17:23 PM ******/
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
	[DropNumber] [int] NULL,
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
/****** Object:  Table [dbo].[ADOption]    Script Date: 6/20/2016 7:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADOption](
	[ADOptionID] [int] IDENTITY(1,1) NOT NULL,
	[ADOptionName] [nvarchar](50) NULL,
	[MinimumCirculation] [nvarchar](50) NULL,
	[Vendorname] [nvarchar](50) NULL,
	[Inactive] [bit] NULL CONSTRAINT [DF_ADOption_Inactive]  DEFAULT ((0)),
 CONSTRAINT [PK_ADOption] PRIMARY KEY CLUSTERED 
(
	[ADOptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AllowedStoreOption]    Script Date: 6/20/2016 7:17:23 PM ******/
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
/****** Object:  Table [dbo].[Coupon]    Script Date: 6/20/2016 7:17:23 PM ******/
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
/****** Object:  Table [dbo].[CouponAdMonth]    Script Date: 6/20/2016 7:17:23 PM ******/
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
/****** Object:  Table [dbo].[EventLog]    Script Date: 6/20/2016 7:17:23 PM ******/
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
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 6/20/2016 7:17:23 PM ******/
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
/****** Object:  Table [dbo].[Market]    Script Date: 6/20/2016 7:17:23 PM ******/
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
/****** Object:  Table [dbo].[Store]    Script Date: 6/20/2016 7:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[StoreID] [int] IDENTITY(1,1) NOT NULL,
	[Ownergroup] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Storename] [nvarchar](50) NOT NULL,
	[MarketID] [int] NULL,
	[Shootover] [nvarchar](100) NULL,
	[ArtworkCode] [nvarchar](100) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[StoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreAdChoice]    Script Date: 6/20/2016 7:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreAdChoice](
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
	[FollowedCorporate] [bit] NULL,
	[NotPrinting] [bit] NULL,
	[OwnDistribution] [bit] NULL,
	[CouponID] [int] NULL,
 CONSTRAINT [PK_StoreAdChoice] PRIMARY KEY CLUSTERED 
(
	[ChoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreAdChoiceHistory]    Script Date: 6/20/2016 7:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreAdChoiceHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ChoiceID] [int] NULL,
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
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 6/20/2016 7:17:23 PM ******/
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
/****** Object:  Table [dbo].[UserStore]    Script Date: 6/20/2016 7:17:23 PM ******/
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

INSERT [dbo].[AdMonth] ([AdMonthID], [Year], [Month], [LockOutEndDate], [LockOutStartDate], [DropNumber], [PetpartnerInfo], [CorpInHomeDate], [AdOptionID], [CorpPlanText]) VALUES (1, 2016, 6, CAST(N'2016-06-30 00:00:00.000' AS DateTime), CAST(N'2016-06-09 00:00:00.000' AS DateTime), NULL, N'sdf', CAST(N'2016-06-15 00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[AdMonth] ([AdMonthID], [Year], [Month], [LockOutEndDate], [LockOutStartDate], [DropNumber], [PetpartnerInfo], [CorpInHomeDate], [AdOptionID], [CorpPlanText]) VALUES (2, 2016, 2, CAST(N'2016-06-28 00:00:00.000' AS DateTime), CAST(N'2016-06-09 00:00:00.000' AS DateTime), NULL, N'test', CAST(N'2016-06-21 00:00:00.000' AS DateTime), 1, N'test')
SET IDENTITY_INSERT [dbo].[AdMonth] OFF
SET IDENTITY_INSERT [dbo].[ADOption] ON 

INSERT [dbo].[ADOption] ([ADOptionID], [ADOptionName], [MinimumCirculation], [Vendorname], [Inactive]) VALUES (1, N'Mega', NULL, N'Valassis', NULL)
INSERT [dbo].[ADOption] ([ADOptionID], [ADOptionName], [MinimumCirculation], [Vendorname], [Inactive]) VALUES (2, N'4-Page', NULL, N'Valassis', NULL)
INSERT [dbo].[ADOption] ([ADOptionID], [ADOptionName], [MinimumCirculation], [Vendorname], [Inactive]) VALUES (3, N'Value', NULL, N'Valassis', NULL)
INSERT [dbo].[ADOption] ([ADOptionID], [ADOptionName], [MinimumCirculation], [Vendorname], [Inactive]) VALUES (4, N'Post Card', NULL, N'Valassis', NULL)
SET IDENTITY_INSERT [dbo].[ADOption] OFF
SET IDENTITY_INSERT [dbo].[AllowedStoreOption] ON 

INSERT [dbo].[AllowedStoreOption] ([AllowedStoreOptionID], [StoreID], [AdOptionID]) VALUES (1, 150, 2)
INSERT [dbo].[AllowedStoreOption] ([AllowedStoreOptionID], [StoreID], [AdOptionID]) VALUES (2, 150, 1)
INSERT [dbo].[AllowedStoreOption] ([AllowedStoreOptionID], [StoreID], [AdOptionID]) VALUES (3, 9075, 4)
INSERT [dbo].[AllowedStoreOption] ([AllowedStoreOptionID], [StoreID], [AdOptionID]) VALUES (4, 20, 3)
INSERT [dbo].[AllowedStoreOption] ([AllowedStoreOptionID], [StoreID], [AdOptionID]) VALUES (5, 15, 2)
SET IDENTITY_INSERT [dbo].[AllowedStoreOption] OFF
SET IDENTITY_INSERT [dbo].[Coupon] ON 

INSERT [dbo].[Coupon] ([CouponID], [CouponText]) VALUES (1, N'This Test Coupon')
INSERT [dbo].[Coupon] ([CouponID], [CouponText]) VALUES (3, N'Next')
INSERT [dbo].[Coupon] ([CouponID], [CouponText]) VALUES (4, N'Test Coupon')
INSERT [dbo].[Coupon] ([CouponID], [CouponText]) VALUES (5, N'TEst')
SET IDENTITY_INSERT [dbo].[Coupon] OFF
INSERT [dbo].[CouponAdMonth] ([AdMonthID], [CouponId]) VALUES (1, 1)
INSERT [dbo].[CouponAdMonth] ([AdMonthID], [CouponId]) VALUES (1, 3)
INSERT [dbo].[CouponAdMonth] ([AdMonthID], [CouponId]) VALUES (1, 4)
SET IDENTITY_INSERT [dbo].[EventLog] ON 

INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (1, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.CouponService.Save(CouponModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\CouponService.cs:line 191', N'EntityFramework', N'http://localhost:60862/Admin/ManageCoupon/Save', CAST(N'2016-06-18 12:08:30.047' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (2, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.CouponService.Save(CouponModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\CouponService.cs:line 191', N'EntityFramework', N'http://localhost:60862/Admin/ManageCoupon/Save', CAST(N'2016-06-18 12:10:26.647' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (3, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.CouponService.Save(CouponModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\CouponService.cs:line 191', N'EntityFramework', N'http://localhost:60862/Admin/ManageCoupon/Save', CAST(N'2016-06-18 12:11:28.677' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (4, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-18 12:16:44.880' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (5, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-18 12:17:00.300' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (6, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-18 12:17:23.157' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (7, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-18 12:28:49.870' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (8, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-18 12:46:45.107' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (9, N'An error occurred while updating the entries. See the inner exception for details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-18 13:07:14.000' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (10, N'An error occurred while updating the entries. See the inner exception for details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-18 13:07:36.557' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (11, N'An error occurred while updating the entries. See the inner exception for details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-18 13:08:33.287' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (12, N'An error occurred while updating the entries. See the inner exception for details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-18 13:09:25.573' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (13, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-18 13:31:39.163' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (14, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-20 04:10:08.170' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (15, N'An error occurred while updating the entries. See the inner exception for details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-20 04:12:37.050' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (16, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at PetSuppliesPlus.Repository.EfUnitOfWork.Commit() in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\UnitOfWork.cs:line 71
   at PetSuppliesPlus.Repository.Service.AdMonthService.Save(AdMonthModel model) in D:\Shiv\SVN\Austin Henson\Pet Supplies Plus - Franchise Web - Portal _8517_-8517\Source Code\PetSuppliesPlus.Repository\Service\AdMonthService.cs:line 344', N'EntityFramework', N'http://localhost:60862/Admin/ManageAdMonth/Save', CAST(N'2016-06-20 05:03:25.307' AS DateTime), N'127.0.0.1')
INSERT [dbo].[EventLog] ([Id], [Message], [StackTrace], [Source], [Url], [Datetime], [IpAddress]) VALUES (17, N'Only parameterless constructors and initializers are supported in LINQ to Entities.', N'   at System.Data.Objects.ELinq.ExpressionConverter.NewTranslator.TypedTranslate(ExpressionConverter parent, NewExpression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.TypedTranslator`1.Translate(ExpressionConverter parent, Expression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.TranslateExpression(Expression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.BinaryTranslator.TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.TypedTranslator`1.Translate(ExpressionConverter parent, Expression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.TranslateExpression(Expression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.MemberInitTranslator.TypedTranslate(ExpressionConverter parent, MemberInitExpression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.TypedTranslator`1.Translate(ExpressionConverter parent, Expression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.TranslateExpression(Expression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.TranslateLambda(LambdaExpression lambda, DbExpression input)
   at System.Data.Objects.ELinq.ExpressionConverter.TranslateLambda(LambdaExpression lambda, DbExpression input, DbExpressionBinding& binding)
   at System.Data.Objects.ELinq.ExpressionConverter.MethodCallTranslator.OneLambdaTranslator.Translate(ExpressionConverter parent, MethodCallExpression call, DbExpression& source, DbExpressionBinding& sourceBinding, DbExpression& lambda)
   at System.Data.Objects.ELinq.ExpressionConverter.MethodCallTranslator.SelectTranslator.Translate(ExpressionConverter parent, MethodCallExpression call)
   at System.Data.Objects.ELinq.ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator.Translate(ExpressionConverter parent, MethodCallExpression call, SequenceMethod sequenceMethod)
   at System.Data.Objects.ELinq.ExpressionConverter.MethodCallTranslator.TypedTranslate(ExpressionConverter parent, MethodCallExpression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.TypedTranslator`1.Translate(ExpressionConverter parent, Expression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.TranslateExpression(Expression linq)
   at System.Data.Objects.ELinq.ExpressionConverter.Convert()
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Data.Entity.Internal.Linq.InternalQuery`1.GetEnumerator()
   at System.Data.Entity.Infrastructure.DbQuery`1.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at PetSuppliesPlus.Repository.Service.StoreAdChoiceService.GetDetail(Int32 userId, Int32 monthId)', N'System.Data.Entity', N'http://localhost:60862/Store', CAST(N'2016-06-20 09:43:47.827' AS DateTime), N'127.0.0.1')
SET IDENTITY_INSERT [dbo].[EventLog] OFF
SET IDENTITY_INSERT [dbo].[LoginHistory] ON 

INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1, 1, CAST(N'2016-06-01 14:01:23.780' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (2, 1, CAST(N'2016-06-01 14:33:15.320' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (3, 1, CAST(N'2016-06-02 11:54:12.077' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (4, 1, CAST(N'2016-06-02 14:43:35.503' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (5, 1, CAST(N'2016-06-02 14:45:51.597' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (6, 1, CAST(N'2016-06-02 15:13:15.917' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (7, 1, CAST(N'2016-06-02 15:37:55.803' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (8, 1, CAST(N'2016-06-02 17:48:50.210' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (9, 1, CAST(N'2016-06-02 17:53:43.797' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (10, 1, CAST(N'2016-06-03 10:36:58.270' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (11, 1, CAST(N'2016-06-03 11:20:33.337' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (12, 1, CAST(N'2016-06-03 11:27:10.950' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (13, 1008, CAST(N'2016-06-03 11:35:15.107' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (14, 1, CAST(N'2016-06-03 12:56:31.920' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (15, 1, CAST(N'2016-06-03 14:45:06.317' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (16, 1, CAST(N'2016-06-03 15:23:11.933' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (17, 1008, CAST(N'2016-06-03 15:28:39.417' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (18, 1, CAST(N'2016-06-03 15:40:06.273' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (19, 1, CAST(N'2016-06-03 15:45:06.163' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (20, 1008, CAST(N'2016-06-03 15:51:28.167' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (21, 1, CAST(N'2016-06-03 15:57:56.427' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (22, 1, CAST(N'2016-06-03 16:07:44.510' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (23, 1, CAST(N'2016-06-03 16:16:00.203' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (24, 1, CAST(N'2016-06-03 16:29:20.687' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (25, 1, CAST(N'2016-06-03 16:30:07.017' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (26, 1, CAST(N'2016-06-03 16:30:38.200' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (27, 1, CAST(N'2016-06-03 16:31:08.763' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (28, 1, CAST(N'2016-06-03 16:31:54.467' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (29, 1, CAST(N'2016-06-03 16:32:38.153' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (30, 1, CAST(N'2016-06-03 16:58:46.660' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (31, 1, CAST(N'2016-06-03 17:42:33.993' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (32, 1008, CAST(N'2016-06-03 17:45:36.060' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (33, 1008, CAST(N'2016-06-03 17:46:28.963' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (34, 1008, CAST(N'2016-06-03 17:46:57.390' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (35, 1, CAST(N'2016-06-03 17:47:02.220' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (36, 1, CAST(N'2016-06-03 17:54:16.227' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (37, 1, CAST(N'2016-06-03 17:57:04.717' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (38, 1, CAST(N'2016-06-03 18:07:22.117' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (39, 1, CAST(N'2016-06-03 18:09:59.847' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (40, 1, CAST(N'2016-06-03 18:11:05.597' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (41, 1, CAST(N'2016-06-03 18:12:24.587' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (42, 1, CAST(N'2016-06-03 18:12:28.107' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (43, 1, CAST(N'2016-06-03 18:14:19.417' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (44, 1, CAST(N'2016-06-03 18:34:52.290' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (45, 1008, CAST(N'2016-06-03 18:37:24.030' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (46, 1, CAST(N'2016-06-03 18:38:16.753' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (47, 1, CAST(N'2016-06-03 18:44:30.913' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (48, 1, CAST(N'2016-06-03 18:49:48.440' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (49, 1, CAST(N'2016-06-04 09:35:13.937' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (50, 1008, CAST(N'2016-06-04 09:43:04.600' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (51, 1, CAST(N'2016-06-04 10:01:54.363' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (52, 1008, CAST(N'2016-06-04 10:20:12.527' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (53, 1, CAST(N'2016-06-04 10:24:11.887' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (54, 1, CAST(N'2016-06-04 10:32:52.633' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (55, 1, CAST(N'2016-06-04 11:01:55.700' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (56, 1008, CAST(N'2016-06-04 11:14:46.027' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (57, 1, CAST(N'2016-06-04 11:52:47.807' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (58, 1, CAST(N'2016-06-04 12:02:16.043' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (59, 1008, CAST(N'2016-06-04 12:04:16.397' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (60, 1, CAST(N'2016-06-04 12:20:08.703' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (61, 1008, CAST(N'2016-06-04 12:21:09.377' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (62, 1, CAST(N'2016-06-04 12:25:42.253' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (63, 1, CAST(N'2016-06-04 12:32:59.800' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (64, 1, CAST(N'2016-06-04 12:33:35.650' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (65, 1, CAST(N'2016-06-04 12:50:16.520' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (66, 1, CAST(N'2016-06-04 12:59:07.620' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (67, 1, CAST(N'2016-06-04 13:03:39.173' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (68, 1, CAST(N'2016-06-04 13:14:50.833' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (69, 1, CAST(N'2016-06-04 13:44:00.303' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (70, 1, CAST(N'2016-06-04 14:32:56.347' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (71, 1, CAST(N'2016-06-04 14:36:11.470' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (72, 1008, CAST(N'2016-06-04 15:59:31.617' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (73, 1, CAST(N'2016-06-04 18:41:45.310' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (74, 1, CAST(N'2016-06-06 10:53:19.500' AS DateTime), N'::1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (75, 1, CAST(N'2016-06-06 10:58:07.687' AS DateTime), N'::1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (76, 1, CAST(N'2016-06-06 11:41:02.630' AS DateTime), N'::1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (77, 1008, CAST(N'2016-06-06 12:22:57.900' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (78, 1, CAST(N'2016-06-06 12:45:15.887' AS DateTime), N'::1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (79, 1008, CAST(N'2016-06-06 12:56:41.917' AS DateTime), N'::1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (80, 1008, CAST(N'2016-06-06 13:03:35.583' AS DateTime), N'::1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (81, 1008, CAST(N'2016-06-06 14:26:59.043' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (82, 1008, CAST(N'2016-06-06 15:02:06.773' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (83, 1008, CAST(N'2016-06-06 15:45:18.897' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (84, 1, CAST(N'2016-06-06 15:50:34.373' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (85, 1, CAST(N'2016-06-06 15:51:33.127' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (86, 1, CAST(N'2016-06-06 16:10:39.127' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (87, 1, CAST(N'2016-06-06 16:17:06.327' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (88, 1, CAST(N'2016-06-06 16:20:14.813' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (89, 1, CAST(N'2016-06-06 16:40:07.870' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (90, 1, CAST(N'2016-06-06 16:54:54.747' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (91, 1, CAST(N'2016-06-06 16:59:36.880' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (92, 1, CAST(N'2016-06-06 17:08:11.307' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (93, 1, CAST(N'2016-06-06 17:37:17.013' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (94, 1, CAST(N'2016-06-06 18:00:30.057' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (95, 1, CAST(N'2016-06-06 18:19:22.577' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (96, 1008, CAST(N'2016-06-06 19:07:23.513' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (97, 1008, CAST(N'2016-06-07 09:49:43.457' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (98, 1008, CAST(N'2016-06-07 10:07:44.747' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (99, 1008, CAST(N'2016-06-07 10:29:29.477' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (100, 1008, CAST(N'2016-06-07 12:11:44.820' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (101, 1008, CAST(N'2016-06-07 12:34:46.080' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (102, 1008, CAST(N'2016-06-07 12:48:39.427' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (103, 1008, CAST(N'2016-06-07 12:53:30.917' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (104, 1008, CAST(N'2016-06-07 13:02:12.587' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (105, 1008, CAST(N'2016-06-07 13:29:27.577' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (106, 1008, CAST(N'2016-06-07 13:35:48.640' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (107, 1008, CAST(N'2016-06-07 13:45:58.127' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (108, 1008, CAST(N'2016-06-07 14:42:13.707' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (109, 1008, CAST(N'2016-06-07 14:57:30.367' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (110, 1008, CAST(N'2016-06-07 16:56:07.550' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (111, 1008, CAST(N'2016-06-07 17:16:11.387' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1097, 1, CAST(N'2016-06-09 10:13:22.523' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1098, 1, CAST(N'2016-06-09 11:01:55.593' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1099, 1, CAST(N'2016-06-09 11:12:02.257' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1100, 1, CAST(N'2016-06-09 11:20:24.270' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1101, 1, CAST(N'2016-06-09 11:40:52.327' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1102, 1, CAST(N'2016-06-09 11:42:42.903' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1103, 1, CAST(N'2016-06-09 11:45:18.560' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1104, 1, CAST(N'2016-06-09 11:49:17.093' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1105, 1, CAST(N'2016-06-09 11:49:30.567' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1106, 1, CAST(N'2016-06-09 12:12:40.860' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1107, 1, CAST(N'2016-06-09 12:14:31.830' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1108, 1, CAST(N'2016-06-09 12:18:01.160' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1109, 1, CAST(N'2016-06-09 12:25:58.823' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1110, 1, CAST(N'2016-06-09 12:30:19.903' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1111, 1, CAST(N'2016-06-09 12:32:25.910' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1112, 1012, CAST(N'2016-06-09 12:39:00.303' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1113, 1, CAST(N'2016-06-09 12:52:13.947' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1114, 1, CAST(N'2016-06-09 13:31:41.657' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1115, 1, CAST(N'2016-06-09 14:29:57.813' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1116, 1008, CAST(N'2016-06-09 16:11:12.727' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1117, 1008, CAST(N'2016-06-09 17:12:06.660' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1118, 1008, CAST(N'2016-06-09 18:06:02.517' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1119, 1, CAST(N'2016-06-09 18:07:48.003' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1120, 1008, CAST(N'2016-06-09 18:09:15.990' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1121, 1008, CAST(N'2016-06-09 18:12:39.903' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1122, 1, CAST(N'2016-06-09 18:36:49.833' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1123, 1008, CAST(N'2016-06-09 18:39:36.347' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1124, 1008, CAST(N'2016-06-09 18:41:20.640' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1125, 1008, CAST(N'2016-06-09 18:43:21.900' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1126, 1008, CAST(N'2016-06-09 18:56:13.720' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1127, 1008, CAST(N'2016-06-09 19:17:22.057' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1128, 1008, CAST(N'2016-06-09 19:43:50.983' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1129, 1008, CAST(N'2016-06-09 19:55:00.817' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1130, 1, CAST(N'2016-06-09 20:30:58.810' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1131, 1008, CAST(N'2016-06-10 09:23:55.200' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1132, 1008, CAST(N'2016-06-10 09:54:27.340' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1133, 1008, CAST(N'2016-06-10 10:43:57.253' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1134, 1008, CAST(N'2016-06-10 11:51:22.637' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1135, 1008, CAST(N'2016-06-10 12:45:30.103' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1136, 1008, CAST(N'2016-06-10 13:24:58.813' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1137, 1008, CAST(N'2016-06-10 15:22:27.217' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1138, 1008, CAST(N'2016-06-10 15:39:59.510' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1139, 1008, CAST(N'2016-06-10 15:48:16.627' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1140, 1008, CAST(N'2016-06-10 15:52:32.767' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1141, 1008, CAST(N'2016-06-10 15:54:23.967' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1142, 1008, CAST(N'2016-06-10 16:35:10.720' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1143, 1008, CAST(N'2016-06-10 17:18:49.673' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1144, 1008, CAST(N'2016-06-10 17:35:38.403' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1145, 1008, CAST(N'2016-06-10 17:41:55.007' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1146, 1008, CAST(N'2016-06-10 17:47:22.930' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1147, 1008, CAST(N'2016-06-10 17:57:45.860' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1148, 1008, CAST(N'2016-06-10 18:13:06.523' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1149, 1, CAST(N'2016-06-10 18:19:53.677' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1150, 1008, CAST(N'2016-06-10 18:25:04.583' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1151, 1008, CAST(N'2016-06-10 18:28:28.013' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1152, 1, CAST(N'2016-06-13 11:28:59.600' AS DateTime), N'::1', N'Web', N'Chrome')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1153, 1, CAST(N'2016-06-13 15:19:56.733' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1154, 1008, CAST(N'2016-06-17 15:09:25.177' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1155, 1, CAST(N'2016-06-17 15:11:04.910' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1156, 1, CAST(N'2016-06-17 15:46:06.160' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1157, 1, CAST(N'2016-06-17 17:37:10.847' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1158, 1, CAST(N'2016-06-17 17:38:49.037' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1159, 1, CAST(N'2016-06-17 17:58:44.330' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1160, 1, CAST(N'2016-06-17 18:06:01.490' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1161, 1, CAST(N'2016-06-17 18:18:57.903' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1162, 1008, CAST(N'2016-06-17 18:25:06.033' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1163, 1, CAST(N'2016-06-17 18:26:10.140' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1164, 1008, CAST(N'2016-06-17 18:32:28.337' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1165, 1008, CAST(N'2016-06-17 18:37:20.563' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1166, 1, CAST(N'2016-06-18 09:30:21.697' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1167, 1, CAST(N'2016-06-18 10:33:51.197' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1168, 1, CAST(N'2016-06-18 11:26:14.883' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1169, 1008, CAST(N'2016-06-18 11:39:45.337' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1170, 1, CAST(N'2016-06-18 11:40:16.890' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1171, 1008, CAST(N'2016-06-18 11:55:23.420' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1172, 1008, CAST(N'2016-06-18 12:01:43.817' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1173, 1008, CAST(N'2016-06-18 12:07:35.797' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1174, 1008, CAST(N'2016-06-18 12:10:58.753' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1175, 1008, CAST(N'2016-06-18 12:58:51.347' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1176, 1008, CAST(N'2016-06-18 14:25:38.500' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1177, 1008, CAST(N'2016-06-18 14:43:33.377' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1178, 1008, CAST(N'2016-06-18 15:14:42.947' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1179, 1008, CAST(N'2016-06-18 15:32:37.737' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1180, 1008, CAST(N'2016-06-18 15:48:28.620' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1181, 1008, CAST(N'2016-06-18 15:52:19.613' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1182, 1008, CAST(N'2016-06-18 16:17:52.230' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1183, 1008, CAST(N'2016-06-18 16:29:17.170' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1184, 1008, CAST(N'2016-06-18 17:04:39.660' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
GO
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1185, 1008, CAST(N'2016-06-18 17:12:41.790' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1186, 1, CAST(N'2016-06-18 17:34:51.920' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1187, 1, CAST(N'2016-06-18 17:35:43.603' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1188, 1, CAST(N'2016-06-18 17:43:24.450' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1189, 1, CAST(N'2016-06-18 17:44:48.267' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1190, 1, CAST(N'2016-06-18 18:15:05.477' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1191, 1, CAST(N'2016-06-18 18:32:35.367' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1192, 1, CAST(N'2016-06-18 19:01:01.643' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1193, 1, CAST(N'2016-06-20 09:39:20.407' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1194, 1, CAST(N'2016-06-20 09:41:40.057' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1195, 1, CAST(N'2016-06-20 10:24:36.213' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1196, 1, CAST(N'2016-06-20 10:52:11.630' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1197, 1, CAST(N'2016-06-20 11:00:48.930' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1198, 1008, CAST(N'2016-06-20 12:59:29.170' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1199, 1008, CAST(N'2016-06-20 13:02:08.920' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1200, 1008, CAST(N'2016-06-20 13:32:00.073' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1201, 1008, CAST(N'2016-06-20 14:17:49.180' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1202, 1008, CAST(N'2016-06-20 15:12:55.037' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1203, 1008, CAST(N'2016-06-20 15:33:18.910' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1204, 1008, CAST(N'2016-06-20 15:50:33.150' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1205, 1008, CAST(N'2016-06-20 16:09:28.987' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1206, 1, CAST(N'2016-06-20 16:27:12.560' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1207, 1008, CAST(N'2016-06-20 16:39:40.353' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1208, 1, CAST(N'2016-06-20 16:40:09.627' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
INSERT [dbo].[LoginHistory] ([LoginHistoryID], [UserID], [TimeStamp], [IPaddress], [Device], [Browser]) VALUES (1209, 1008, CAST(N'2016-06-20 16:44:48.867' AS DateTime), N'127.0.0.1', N'Web', N'Firefox')
SET IDENTITY_INSERT [dbo].[LoginHistory] OFF
SET IDENTITY_INSERT [dbo].[Market] ON 

INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1, N'Mail')
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (2, N'Newspaper')
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1009, N'Sunday Market1')
INSERT [dbo].[Market] ([MarketID], [Name]) VALUES (1014, N'SMS Market')
SET IDENTITY_INSERT [dbo].[Market] OFF
SET IDENTITY_INSERT [dbo].[Store] ON 

INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (1, N'Brown', N'Redford Township', N'MI', N'REDFORD TWP MI', 1, N'shoot', N'work code')
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (2, N'Shallop S.', N'Woodhaven', N'MI', N'WOODHAVEN MI', 1, N'over', N'code')
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (3, N'PSP Stores', N'St. Clair Shores', N'MI', N'ST CLAIR SHORES MI-HARPER', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4, N'Goodwin', N'Waterford', N'MI', N'WATERFORD MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (5, N'Couet', N'Royal Oak', N'MI', N'ROYAL OAK MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (6, N'Arcori', N'Farmington Hills', N'MI', N'FARMINGTON MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7, N'Brown', N'Detroit', N'MI', N'DETROIT MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8, N'Bonanni', N'Clinton Twp', N'MI', N'CLINTON TOWNSHIP MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9, N'Holland', N'Lansing', N'MI', N'LANSING MI-PENNSYLVANIA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (12, N'Grinenko', N'Jackson', N'MI', N'JACKSON MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (13, N'Shattuck A.', N'White Lake', N'MI', N'WHITE LAKE MI-COOLEY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (14, N'PSP Stores', N'Toledo', N'OH', N'TALMADGE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (15, N'Shattuck', N'Canton', N'MI', N'CANTON MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (16, N'Williams', N'Ann Arbor', N'MI', N'AA-ANN ARBOR MI-PLYMOUTH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (17, N'Britz', N'Fort Gratiot', N'MI', N'FORT GRATIOT MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (18, N'Shallop S.', N'Taylor', N'MI', N'TAYLOR MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (19, N'Kessel', N'Owosso', N'MI', N'OWOSSO MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (20, N'PSP Stores', N'Toledo', N'OH', N'AIRPORT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (21, N'Kessel', N'Grand Blanc', N'MI', N'GRAND BLANC MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (23, N'Isgro', N'Oxford', N'MI', N'OXFORD MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (24, N'Kessel', N'Mount Morris', N'MI', N'MOUNT MORRIS MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (25, N'PSP Stores', N'South Bend', N'IN', N'SOUTH BEND IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (27, N'PSP Stores', N'Fairview Park', N'OH', N'FAIRVIEW PARK OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (28, N'PSP Stores', N'Middleburg Heights', N'OH', N'MIDDLEBURG HTS OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (29, N'PSP Stores', N'Toledo', N'OH', N'W. ALEXIS', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (30, N'McCallion', N'Novi', N'MI', N'NOVI MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (31, N'Milano', N'Indianapolis', N'IN', N'GREENWOOD', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (32, N'PSP Stores', N'St. Clair Shores', N'MI', N'ST CLAIR SHORES MI-MACK', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (35, N'Milano', N'Indianapolis', N'IN', N'BROAD RIPPLE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (38, N'PSP Stores', N'Morton Grove', N'IL', N'MORTON GROVE IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (41, N'Daczka/Spencer/Wallace', N'Brookfield', N'WI', N'BROOKFIELD WI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (42, N'PSP Stores', N'Des Plaines', N'IL', N'DES PLAINS IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (43, N'PSP Stores', N'Warren', N'MI', N'WARREN MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (44, N'PSP Stores', N'Livonia', N'MI', N'LIVONIA MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (45, N'US Retail', N'Grand Rapids', N'MI', N'ALPINE AVE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (46, N'Milano', N'Oaklawn', N'IL', N'OAKLAWN IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (48, N'Shattuck A.', N'Bloomfield Hills', N'MI', N'BLOOMFIELD HILLS MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (49, N'PSP Stores', N'Canton', N'OH', N'CANTON OH-CROMER', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (51, N'PSP Stores', N'Youngstown', N'OH', N'YOUNGSTOWN OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (52, N'PSP Stores', N'Fort Wayne', N'IN', N'FORT WAYNE IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (53, N'PSP Stores', N'Chicago', N'IL', N'CHICAGO IL-HARLEM/HIGGEN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (54, N'PSP Stores', N'Buffalo Grove', N'IL', N'BUFFALO GROVE IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (56, N'PSP Stores', N'Niles', N'OH', N'NILES OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (57, N'PSP Stores', N'Arlington Heights', N'IL', N'ARLINGTON HEIGHTS IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (59, N'PSP Stores', N'Mentor', N'OH', N'MENTOR OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (60, N'Daczka/Spencer/Wallace', N'Glendale', N'WI', N'GLENDALE WI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (62, N'Puroll', N'Westmont', N'IL', N'WESTMONT IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (63, N'PSP Stores', N'Mundelein', N'IL', N'MUNDELEIN IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (64, N'PSP Stores', N'Elmwood Park', N'IL', N'ELMWOOD PARK IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (65, N'Williams', N'Ann Arbor', N'MI', N'AA-ANN ARBOR MI-MAIN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (66, N'PSP Stores', N'Chicago', N'IL', N'N. ELSTON', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (67, N'PSP Stores', N'Alliance', N'OH', N'ALLIANCE OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (68, N'PSP Stores', N'Stow', N'OH', N'STOW OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (72, N'Lennon', N'Villa Park', N'IL', N'VILLA PARK IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (74, N'PSP Stores', N'Chesterfield Twp', N'MI', N'CHESTERFIELD MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (75, N'PSP Stores', N'Racine', N'WI', N'RACINE WI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (76, N'US Retail', N'Holland', N'MI', N'GR-HOLLAND MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (77, N'Horn', N'Portage', N'MI', N'PORTAGE MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (80, N'SMA Intl', N'Kokomo', N'IN', N'KOKOMO IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (81, N'Daczka/Spencer/Wallace', N'Greenfield', N'WI', N'GREENFIELD WI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (83, N'PSP Stores', N'Lyndhurst', N'OH', N'LYNDHURST OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (85, N'Slater', N'Brighton', N'MI', N'BRIGHTON MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (86, N'PSP Stores', N'Canton', N'OH', N'CANTON OH-TUSCARWAS', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (87, N'PSP Stores', N'Bethel Park', N'PA', N'BETHEL PARK PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (88, N'PSP Stores', N'Kettering', N'OH', N'KETTERING OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (89, N'PSP Stores', N'Findlay', N'OH', N'FINDLAY OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (90, N'Shallop S.', N'Dearborn', N'MI', N'DEARBORN MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (92, N'PSP Stores', N'Medina', N'OH', N'MEDINA OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (94, N'US Retail', N'Grand Rapids', N'MI', N'GR-GRAND RAPIDS MI-KALAM', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (97, N'US Retail', N'Appleton', N'WI', N'APPLETON WI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (98, N'PSP Stores', N'Solon', N'OH', N'SOLON OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (99, N'PSP Stores', N'St. Clairsville', N'OH', N'ST CLAIRSVILLE OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (101, N'PSP Stores', N'Lima', N'OH', N'LIMA OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (102, N'PSP Stores', N'Wooster', N'OH', N'WOOSTER OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (103, N'Arcori', N'Lapeer', N'MI', N'LAPEER MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (105, N'Kessel', N'Bay City', N'MI', N'BAY CITY MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (106, N'PSP Stores', N'Mansfield', N'OH', N'MANSFIELD OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (108, N'PSP Stores', N'Monroe', N'MI', N'MONROE MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (109, N'Phelan', N'Benton Harbor', N'MI', N'BENTON HARBOR MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (111, N'PSP Stores', N'Hermitage', N'PA', N'HERMITAGE PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (112, N'PSP Stores', N'Garfield Heights', N'OH', N'GARFIELD HTS OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (113, N'PSP Stores', N'West Seneca', N'NY', N'WEST SENECA NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (114, N'PSP Stores', N'Westlake', N'OH', N'WESTLAKE OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (115, N'PSP Stores', N'Springfield Township', N'OH', N'SPRINGFIELD TWP OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (116, N'PSP Stores', N'Buffalo ', N'NY', N'BUFFALO NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (117, N'PSP Stores', N'North Canton', N'OH', N'NORTH CANTON OH-PORTAGE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (118, N'McCallion', N'Fenton', N'MI', N'FENTON MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (119, N'PSP Stores', N'Chardon', N'OH', N'CHARDON OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (120, N'PSP Stores', N'Parma', N'OH', N'PARMA OH-PLEASANT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (121, N'US Retail', N'Grandville', N'MI', N'GR-GRANDVILLE MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (123, N'PSP Stores', N'Elyria', N'OH', N'ELYRIA OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (124, N'PSP Stores', N'Fairlawn', N'OH', N'FAIRLAWN OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (127, N'Kessel', N'Saginaw', N'MI', N'SAGINAW MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (128, N'PSP Stores', N'Lakewood', N'OH', N'LAKEWOOD OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (129, N'Quinn', N'Valparaiso', N'IN', N'VALPARAISO IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (130, N'Pochron/Maynard', N'Washington Twp.', N'MI', N'WASHINGTON TWP MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (131, N'PSP Stores', N'Streetsboro', N'OH', N'STREETSBORO OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (134, N'PSP Stores', N'Amherst', N'NY', N'AMHERST NY-SHERIDAN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (135, N'Slater', N'Howell', N'MI', N'HOWELL MI', 1, NULL, NULL)
GO
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (136, N'O''Brien', N'Burnsville', N'MN', N'BURNSVILLE MN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (138, N'O''Brien', N'Bloomington', N'MN', N'BLOOMINGTON MN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (139, N'PSP Stores', N'Clarksburg', N'WV', N'CLARKSBURG WV', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (140, N'PSP Stores', N'New Albany', N'OH', N'COLUMBUS OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (141, N'PSP Stores', N'Akron', N'OH', N'AKRON OH-MANCHESTER', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (143, N'PSP Stores', N'Centerville', N'OH', N'CENTERVILLE OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (144, N'PSP Stores', N'Sandusky', N'OH', N'SANDUSKY OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (145, N'PSP Stores', N'Steubenville', N'OH', N'STEUBENVILLE OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (148, N'PSP Stores', N'Sylvania', N'OH', N'SYLVANIA OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (150, N'US Retail', N'Grand Rapids', N'MI', N'28TH STREET', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (151, N'PSP Stores', N'Newark', N'OH', N'NEWARK OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (152, N'Grinenko', N'Adrian', N'MI', N'ADRIAN MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (153, N'PSP Stores', N'Vienna', N'WV', N'VIENNA WV', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (155, N'Kessel', N'Iron Mountain', N'MI', N'IRON MOUNTAIN MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (157, N'Milano', N'Noblesville', N'IN', N'NOBLESVILLE IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (158, N'PSP Stores', N'Upper Arlington', N'OH', N'UPPER ARLINGTON OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (159, N'PSP Stores', N'Westerville', N'OH', N'WESTERVILLE OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (162, N'Quinn', N'Warsaw', N'IN', N'WARSAW IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (163, N'Kessel', N'Sheboygan', N'WI', N'SHEBOYGAN WI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (165, N'PSP Stores', N'Beckley', N'WV', N'BECKLEY WV', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (166, N'Horn', N'Goshen', N'IN', N'GOSHEN IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (167, N'Slater', N'Milford', N'MI', N'MILFORD MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (168, N'PSP Stores', N'Reynoldsburg', N'OH', N'REYNOLDSBURG OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (169, N'Quinn', N'Dyer', N'IN', N'DYER IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (170, N'Horn', N'Kalamazoo', N'MI', N'KALAMAZOO MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (171, N'Shattuck A.', N'White Lake', N'MI', N'WHITE LAKE MI-HIGHLAND', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (172, N'PSP Stores', N'Cheektowaga', N'NY', N'CHEEKTOWAGA NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (174, N'PSP Stores', N'East Amherst', N'NY', N'EAST AMHERST NY-TRANSIT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (175, N'G Shallop', N'Rochester Hills', N'MI', N'ROCHESTER HILLS MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (177, N'Kessel', N'Petoskey', N'MI', N'PETOSKEY MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (178, N'Arcori', N'Ortonville', N'MI', N'ORTONVILLE MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (179, N'Niemann', N'Quincy', N'IL', N'QUINCY IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (180, N'Lennon', N'Bolingbrook', N'IL', N'BOLINGBROOK IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (181, N'PSP Stores', N'Fairport', N'NY', N'FAIRPORT NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (182, N'PSP Stores', N'Akron', N'OH', N'AKRON OH-ARLINGTON', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (183, N'Lennon', N'Naperville', N'IL', N'SOUTH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (184, N'Lennon', N'Naperville', N'IL', N'NORTH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (185, N'Niemann', N'Champaign', N'IL', N'CHAMPAIGN IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (186, N'PSP Stores', N'Defiance', N'OH', N'DEFIANCE OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (187, N'Sullivan', N'Ballwin', N'MO', N'BALLWIN MO', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (188, N'Lennon', N'North Aurora', N'IL', N'NORTH AURORA IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (189, N'Niemann', N'Jacksonville', N'IL', N'JACKSONVILLE IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (190, N'PSP Stores', N'Lancaster', N'OH', N'LANCASTER OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (191, N'PSP Stores', N'Willowick', N'OH', N'WILLOWWICK OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (192, N'PSP Stores', N'Blasdell ', N'NY', N'BLASDELL NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (193, N'PSP Stores', N'Okemos', N'MI', N'EAST LANSING MI-GD RIVER', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (194, N'Niemann', N'Danville', N'IL', N'DANVILLE IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (195, N'PSP Stores', N'Brooklyn', N'OH', N'BROOKLYN OH OH ', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (197, N'PSP Stores', N'Amherst', N'NY', N'AMHERST NY-NIAGARA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (198, N'Niemann', N'Pekin', N'IL', N'PEKIN IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (199, N'O''Brien', N'Minneapolis', N'MN', N'MINNEAPOLIS MN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (200, N'Wagner', N'Lafayette', N'IN', N'LAFAYETTE IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (201, N'Kessel', N'Sault Ste Marie', N'MI', N'SAULT STE MARIE MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (202, N'PSP Stores', N'Robinson Township', N'PA', N'ROBINSON TOWNSHIP PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (204, N'PSP Stores', N'Lincolnwood', N'IL', N'LINCOLNWOOD IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (205, N'PSP Stores', N'Chicago', N'IL', N'N. LINCOLN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (206, N'PSP Stores', N'Cuyahoga Falls', N'OH', N'CUYAHOGA FALLS OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (207, N'PSP Stores', N'South Lyon', N'MI', N'SOUTH LYON MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (208, N'PSP Stores', N'Waukegan', N'IL', N'WAUKEGAN IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (209, N'PSP Stores', N'Crawfordsville', N'IN', N'CRAWFORDSVILLE IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (210, N'PSP Stores', N'Delaware', N'OH', N'DELAWARE OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (211, N'PSP Stores', N'Grove City', N'OH', N'GROVE CITY OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (212, N'Horn', N'Battle Creek', N'MI', N'BATTLE CREEK', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (213, N'Lennon', N'Lake in the Hills', N'IL', N'LAKE IN THE HILLS IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (214, N'PSP Stores', N'Penfield', N'NY', N'PENFIELD NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (215, N'Decook', N'Rice Lake', N'WI', N'RCIE LAKE WI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (216, N'PSP Stores', N'Fishers', N'IN', N'FISHERS IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (217, N'PSP Stores', N'Portage', N'IN', N'PORTAGE IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (218, N'PSP Stores', N'Blue Ash', N'OH', N'BLUE ASH OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (219, N'PSP Stores', N'Lorain', N'OH', N'LORAIN OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (220, N'PSP Stores', N'Belle Vernon', N'PA', N'BELLE VERNON PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (221, N'PSP Stores', N'Beavercreek', N'OH', N'BEAVERCREEK OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (222, N'Lennon', N'Streamwood', N'IL', N'STREAMWOOD IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (223, N'Wagner', N'Crown Point', N'IN', N'CROWN POINT IN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (224, N'PSP Stores', N'Chicago', N'IL', N'WICKER PARK', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (225, N'PSP Stores', N'Powell', N'OH', N'POWELL OH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (226, N'PSP Stores', N'Bridgeville', N'PA', N'BRIDGEVILLE PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (227, N'Lennon', N'Montgomery', N'IL', N'MONTGOMERY IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (228, N'PSP Stores', N'Independence', N'KY', N'INDEPENDENCE KY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (229, N'Lennon', N'Orland Park', N'IL', N'ORLAND PARK IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (230, N'Niemann', N'Dixon', N'IL', N'DIXON IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (231, N'Shallop S.', N'Southgate', N'MI', N'SOUTHGATE MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (232, N'Puroll', N'Glen Ellyn', N'IL', N'GLEN ELLYN IL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (233, N'PSP Stores', N'Cincinnati', N'OH', N'DELHI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (234, N'PSP Stores', N'Cincinnati', N'OH', N'SYMMES', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (235, N'PSP Stores', N'Florence ', N'KY', N'FLORENCE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (236, N'Kessel', N'Gaylord', N'MI', N'GAYLORD MI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (237, N'PSP Stores', N'Deerfield', N'IL', N'HIGHLAND PARK/DEERFIELD', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (238, N'Lennon', N'Yorkville', N'IL', N'YORKVILLE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (239, N'PSP Stores', N'Indianapolis', N'IN', N'LAWRENCE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (240, N'PSP Stores', N'Painesville', N'OH', N'PAINESVILLE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (241, N'LeFort', N'Warson Woods', N'MO', N'WARSON WOODS', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (242, N'PSP Stores', N'Grosse Pointe', N'MI', N'GROSSE POINTE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (243, N'PSP Stores', N'Gurnee', N'IL', N'GURNEE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (244, N'PSP Stores', N'Burlington', N'IA', N'BURLINGTON', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (245, N'PSP Stores', N'Cincinnati', N'OH', N'OAKLEY STATION', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (246, N'PSP Stores', N'Fort Wayne', N'IN', N'FT. WAYNE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (247, N'Noeldner', N'Green Bay', N'WI', N'SUAMICO', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (248, N'PSP Stores', N'White Oak', N'PA', N'WHITE OAK', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (249, N'Schmahl', N'Fort Mitchell', N'KY', N'FORT MITCHELL', 1, NULL, NULL)
GO
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4002, N'Bechel', N'Crystal', N'MN', N'CRYSTAL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4006, N'Kresge', N'Oakland', N'NJ', N'OAKLAND', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4012, N'Otjen', N'Elizabethtown', N'KY', N'ELIZABETHTOWN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4013, N'Stoker', N'Stephenville', N'TX', N'STEPHENVILLE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4015, N'Bogans', N'Oviedo', N'FL', N'OVIEDO', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4018, N'Lieto/Grimm/Mullally', N'Charlotte', N'NC', N'CHARLOTTE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4021, N'Bechel', N'Vadnais Heights', N'MN', N'VADNAIS HEIGHTS', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4024, N'Arndt/Sharp/Kacir', N'Tampa', N'FL', N'TAMPA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4025, N'US Retail', N'Fort Worth', N'TX', N'HERITAGE TRACE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4026, N'US Retail', N'Maryville', N'TN', N'MARYVILLE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4027, N'PSP Stores', N'Salem', N'VA', N'SALEM VA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4028, N'PSP Stores', N'Wallington', N'NJ', N'WALLINGTON', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4029, N'Vernon', N'San Antonio', N'TX', N'MACARTHUR', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4032, N'PSP Stores', N'Troy', N'NY', N'TROY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4035, N'PSP Stores', N'Greece', N'NY', N'GREECE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4037, N'Lennon', N'Plainfield', N'IL', N'PLAINFIELD', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4039, N'PSP Stores', N'Oxford', N'MA', N'OXFORD', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4057, N'USR Holdings', N'Carrollton', N'TX', N'CARROLLTON', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (4058, N'Solway/Bonanni/ Buccellato', N'Hickory', N'NC', N'HICKORY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7002, N'US Retail', N'Dallas ', N'TX', N'DALLAS TX-MOCKINGBIRD', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7004, N'PSP Stores', N'Kerrville', N'TX', N'KERRVILLE TX', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7005, N'US Retail', N'Plano', N'TX', N'PLANO TX', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7006, N'PSP Stores', N'San Antonio', N'TX', N'SAN ANTONIO TX-SAN PEDRO', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7007, N'PSP Stores', N'Austin', N'TX', N'AUSTIN TX', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7008, N'PSP Stores', N'Seguin', N'TX', N'SEGUIN TX', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7009, N'PSP Stores', N'San Antonio', N'TX', N'SAN ANTONIO TX-AUSTIN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7010, N'US Retail', N'Richardson', N'TX', N'RICHARDSON TX', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7011, N'US Retail', N'Dallas ', N'TX', N'DALLAS TX-TRINITY MILLS', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7012, N'PSP Stores', N'Marble Falls', N'TX', N'MARBLE FALLS TX', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7013, N'US Retail', N'Dallas ', N'TX', N'DALLAS TX-FOREST LANE', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7014, N'US Retail', N'Lewisville', N'TX', N'LEWISVILLE TX', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7015, N'US Retail', N'Garland', N'TX', N'GARLAND TX', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7016, N'US Retail', N'Arlington  ', N'TX', N'ARLINGTON', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7017, N'US Retail', N'Dallas ', N'TX', N'DALLAS', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7018, N'US Retail', N'Hurst', N'TX', N'HURST', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (7019, N'Buchel', N'Katy', N'TX', N'KATY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8001, N'Welch', N'West Columbia', N'SC', N'WEST COLUMBIA SC', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8006, N'US Retail', N'Knoxville', N'TN', N'KNOXVILLE TN-HARVEST', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8007, N'US Retail', N'Knoxville', N'TN', N'KNOXVILLE TN-KINGSTON', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8009, N'US Retail', N'Birmingham', N'AL', N'BIRMINGHAM AL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8013, N'Jacobson/Schonberg', N'Charlottesville', N'VA', N'CHARLOTTESVILLE VA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8014, N'PSP Stores', N'Winston-Salem', N'NC', N'WINSTON-SALEM NC', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8015, N'US Retail', N'Pelham', N'AL', N'PELHAM AL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8016, N'Jacobson', N'Virginia Beach', N'VA', N'KEMPSVILLE VA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8017, N'US Retail', N'Mobile', N'AL', N'MOBILE AL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8018, N'Sands', N'Goldsboro', N'NC', N'GOLDSBORO NC', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8020, N'US Retail', N'Tuscaloosa', N'AL', N'TUSCALOOSA AL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8022, N'PSP Stores', N'Lake Park', N'FL', N'LAKE PARK FL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8024, N'Martin', N'Asheville', N'NC', N'ASHEVILLE NC', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8026, N'PSP Stores', N'Delray Beach', N'FL', N'DELRAY BEACH FL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8028, N'US Retail', N'Homewood', N'AL', N'HOMEWOOD AL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8029, N'Martin', N'Athens', N'GA', N'ATHENS GA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8030, N'Sands', N'Raleigh', N'NC', N'RALEIGH NC', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8031, N'Welch', N'Columbia', N'SC', N'COLUMBIA SC', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8032, N'Altman', N'Brunswick', N'GA', N'BRUNSWICK GA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8033, N'US Retail', N'Oak Ridge', N'TN', N'OAK RIDGE TN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8034, N'Martin', N'Carrollton', N'GA', N'CARROLLTON GA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8035, N'Arndt/Sharp/Kacir', N'New Port Richey', N'FL', N'NEW PORT RICHEY FL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8036, N'Altman', N'St. Mary''s', N'GA', N'ST MARYS GA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8038, N'PSP Stores', N'Sunrise', N'FL', N'SUNRISE FL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8039, N'Oser/Darish', N'Pinellas Park', N'FL', N'PINELLAS PARK FL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8041, N'Martin', N'Marietta', N'GA', N'MARIETTA GA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8043, N'Martin', N'Atlanta', N'GA', N'ATLANTA GA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8044, N'PSP Stores', N'Lake Worth ', N'FL', N'LAKE WORTH FL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8045, N'US Retail', N'Knoxville', N'TN', N'KNOXVILLE TN-N PETERS', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8046, N'Oser/Darish', N'Clearwater', N'FL', N'CLEARWATER FL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8047, N'Noveh', N'Hollywood', N'FL', N'HOLLYWOOD FL', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8048, N'PSP Stores', N'Boone', N'NC', N'BOONE NC', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8049, N'US Retail', N'Knoxville ', N'TN', N'KNOXVILLE TN-CHAPMAN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8050, N'US Retail', N'Falls Church', N'VA', N'FALLS CHURCH VA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8051, N'Nebel', N'Summerville', N'SC', N'SUMMERVILLE SC', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8052, N'Lieto/Grimm/Mullally', N'Charlotte', N'NC', N'CHARLOTTE NC', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8054, N'US Retail', N'Fairfax', N'VA', N'FAIRFAX VA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8055, N'US Retail', N'Franconia', N'VA', N'FRANCONIA VA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8056, N'US Retail', N'Centreville', N'VA', N'CENTERVILLE VA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8057, N'Solway/Bonanni', N'Concord', N'NC', N'CONCORD NC', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8058, N'Welch', N'Irmo', N'SC', N'IRMO SC', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8059, N'PSP Stores', N'Midlothian', N'VA', N'MIDLOTHIAN VA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8060, N'US Retail', N'Ashburn', N'VA', N'ASHBURN VA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8061, N'US Retail', N'Cookeville', N'TN', N'COOKEVILLE TN', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (8062, N'Kesselring', N'Valrico', N'FL', N'VALRICO', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9001, N'Kahl/Humphries', N'Wethersfield', N'CT', N'CT-WETHERSFIELD CT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9003, N'PSP Stores', N'West Roxbury', N'MA', N'WEST ROXBURY MA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9005, N'PSP Stores', N'Cranston', N'RI', N'CRANSTON RI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9006, N'PSP Stores', N'Westbury', N'NY', N'LONG ISLAND', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9007, N'Kahl/Humphries', N'Manchester', N'CT', N'CT-MANCHESTER CT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9009, N'PSP Stores', N'Dewitt', N'NY', N'DEWITT NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9010, N'PSP Stores', N'North Providence', N'RI', N'PROVIDENCE RI', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9012, N'PSP Stores', N'Wilkes Barre', N'PA', N'NEPA-WILKES BARRE PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9013, N'PSP Stores', N'Quincy', N'MA', N'QUINCY MA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9014, N'PSP Stores', N'Raynham', N'MA', N'RAYNHAM MA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9015, N'PSP Stores', N'West Springfield', N'MA', N'WEST SPRINGFIELD MA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9016, N'PSP Stores', N'West Hempstead', N'NY', N'LONG ISLAND', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9019, N'Kahl/Trellevik', N'Orange', N'CT', N'CT-ORANGE CT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9020, N'Kahl/Humphries', N'West Hartford', N'CT', N'CT-WEST HARTFORD CT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9021, N'Kahl/Trellevik', N'Westport', N'CT', N'CT-WESTPORT CT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9022, N'PSP Stores', N'Scranton', N'PA', N'NEPA-SCRANTON PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9023, N'Kahl/Humphries', N'Bristol', N'CT', N'CT-BRISTOL CT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9024, N'PSP Stores', N'Albany', N'NY', N'ALBANY NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9025, N'PSP Stores', N'Shillington', N'PA', N'NEPA-SHILLINGTON PA', 1, NULL, NULL)
GO
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9026, N'PSP Stores', N'Whitehall', N'PA', N'NEPA-WHITEHALL PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9027, N'PSP Stores', N'New Hartford', N'NY', N'NEW HARTFORD NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9028, N'PSP Stores', N'Medford', N'MA', N'MEDFORD MA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9030, N'PSP Stores', N'East Northport', N'NY', N'LONG ISLAND', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9031, N'PSP Stores', N'Oceanside', N'NY', N'LONG ISLAND', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9032, N'PSP Stores', N'Stroudsburg', N'PA', N'NEPA-STROUDSBURG PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9033, N'PSP Stores', N'Deer Park', N'NY', N'LONG ISLAND', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9034, N'PSP Stores', N'Fishkill', N'NY', N'LI-FISHKILL NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9035, N'PSP Stores', N'Phoenixville', N'PA', N'NEPA-PHOENIXVILLE PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9036, N'PSP Stores', N'Manhasset', N'NY', N'LONG ISLAND', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9037, N'PSP Stores', N'Lancaster', N'PA', N'NEPA-LANCASTER PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9038, N'PSP Stores', N'Valley Stream', N'NY', N'LONG ISLAND', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9040, N'PSP Stores', N'Telford', N'PA', N'NEPA-TELFORD PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9041, N'PSP Stores', N'Easton', N'PA', N'NEPA-EASTON PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9042, N'Kahl/Humphries', N'Groton', N'CT', N'CT-GROTON CT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9043, N'PSP Stores', N'Smithtown', N'NY', N'LONG ISLAND', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9044, N'PSP Stores', N'Avondale', N'PA', N'NEPA-AVONDALE PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9045, N'Kahl/Trellevik', N'Brookfield', N'CT', N'CT-BROOKFIELD CT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9046, N'Scaduto', N'Hazlet', N'NJ', N'HAZLET NJ', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9048, N'PSP Stores', N'Hamburg', N'PA', N'NEPA-HAMBURG PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9049, N'PSP Stores', N'Lake Ronkonkoma', N'NY', N'LONG ISLAND', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9050, N'Scaduto', N'Berkeley Heights', N'NJ', N'BERKLEY HEIGHTS NJ', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9051, N'Kahl/Trellevik', N'Shelton', N'CT', N'CT-SHELTON CT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9052, N'PSP Stores', N'Bedford', N'NH', N'BEDFORD NH', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9053, N'PSP Stores', N'Marlton', N'NJ', N'MARLTON NJ', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9054, N'PSP Stores', N'Brighton', N'MA', N'BRIGHTON MA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9055, N'PSP Stores', N'Warrington', N'PA', N'WARRINGTON PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9056, N'PSP Stores', N'Fair lawn', N'NJ', N'FAIR LAWN NJ', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9057, N'PSP Stores', N'Blue Bell', N'PA', N'BLUE BELL PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9058, N'PSP Stores', N'Short Hills', N'NJ', N'SHORT HILLS NJ', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9059, N'PSP Stores', N'West Chester', N'PA', N'WEST CHESTER', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9060, N'PSP Stores', N'Maspeth', N'NY', N'MESPETH NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9061, N'PSP Stores', N'Cicero', N'NY', N'CICERO NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9062, N'PSP Stores', N'Staten Island', N'NY', N'STATEN ISLAND NY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9063, N'PSP Stores', N'Howard Beach', N'NY', N'HOWARD BEACH (OZONE PARK)', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9064, N'PSP Stores', N'Waltham', N'MA', N'BOSTON MA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9065, N'Scaduto', N'Wall Township', N'NJ', N'WALL TWP NJ', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9066, N'Kahl/Humphries', N'Wethersfield', N'CT', N'WETHERSFIELD CT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9067, N'PSP Stores', N'Wayne', N'PA', N'WAYNE PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9068, N'PSP Stores', N'Haverhill', N'MA', N'HAVERHILL MA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9069, N'PSP Stores', N'Billerica', N'MA', N'BILLERICA MA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9070, N'PSP Stores', N'Levittown', N'PA', N'LEVITTOWN PA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9071, N'PSP Stores', N'Cherry Hill', N'NJ', N'CHERRY HILL NJ', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9072, N'PSP Stores', N'Brockton', N'MA', N'BROCKTON MA', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9073, N'PSP Stores', N'Wilmington', N'DE', N'WILMINGTON DE-CONCORD PK', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9074, N'PSP Stores', N'Dover ', N'NJ', N'ROCKAWAY NJ', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9075, N'PSP Stores', N'Airmont', N'NY', N'AIRMONT', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9076, N'PSP Stores', N'Glenville', N'NY', N'SCHENECTADY', 1, NULL, NULL)
INSERT [dbo].[Store] ([StoreID], [Ownergroup], [City], [State], [Storename], [MarketID], [Shootover], [ArtworkCode]) VALUES (9077, N'Deffenbaugh', N'Chester ', N'NJ', N'CHESTER', 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Store] OFF
SET IDENTITY_INSERT [dbo].[StoreAdChoice] ON 

INSERT [dbo].[StoreAdChoice] ([ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [FollowedCorporate], [NotPrinting], [OwnDistribution], [CouponID]) VALUES (8, 1, 15, 2, CAST(N'16:11:36' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', 0, 0, 0, 4)
INSERT [dbo].[StoreAdChoice] ([ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [FollowedCorporate], [NotPrinting], [OwnDistribution], [CouponID]) VALUES (9, 1, 20, NULL, CAST(N'16:11:37' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', 0, 1, 0, 3)
INSERT [dbo].[StoreAdChoice] ([ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [FollowedCorporate], [NotPrinting], [OwnDistribution], [CouponID]) VALUES (10, 1, 150, 2, CAST(N'16:11:41' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', 0, 0, 0, 3)
INSERT [dbo].[StoreAdChoice] ([ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [FollowedCorporate], [NotPrinting], [OwnDistribution], [CouponID]) VALUES (11, 1, 9075, NULL, CAST(N'16:11:42' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', 0, 0, 1, 4)
SET IDENTITY_INSERT [dbo].[StoreAdChoice] OFF
SET IDENTITY_INSERT [dbo].[StoreAdChoiceHistory] ON 

INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (1, 8, 1, 15, 1, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', NULL)
INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (2, 9, 1, 20, 3, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, NULL, 3)
INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (3, 10, 1, 150, 1, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, NULL, 1)
INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (4, 11, 1, 9075, 4, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, NULL, NULL)
INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (9, 8, 1, 15, 1, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', NULL)
INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (10, 9, 1, 20, 3, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', 3)
INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (11, 10, 1, 150, 1, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', 1)
INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (12, 11, 1, 9075, 4, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', 4)
INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (13, 8, 1, 15, 2, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', 4)
INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (14, 9, 1, 20, NULL, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', NULL)
INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (15, 10, 1, 150, 2, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', 3)
INSERT [dbo].[StoreAdChoiceHistory] ([ID], [ChoiceID], [AdMonthID], [StoreID], [AdOptionID], [TimeStamp], [UserID], [IPAddress], [Device], [Browser], [InHomeDate], [ChoiceInitials], [CouponID]) VALUES (16, 11, 1, 9075, NULL, CAST(N'00:00:00' AS Time), 1008, N'127.0.0.1', N'Web', N'Firefox', NULL, N'Test', NULL)
SET IDENTITY_INSERT [dbo].[StoreAdChoiceHistory] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1, N'Admin', N'admin@gmail.com', N'TWZd8ohZNpN7Wz9X0Ba0gA==', 1, 1)
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1007, N'Test', N'test@gmail.com', N'TWZd8ohZNpN7Wz9X0Ba0gA==', 0, 1)
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1008, N'Jack', N'jack@gmail.com', N'TWZd8ohZNpN7Wz9X0Ba0gA==', 0, 1)
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1009, N'test', N'test@test.com', N'TWZd8ohZNpN7Wz9X0Ba0gA==', 0, 0)
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1010, N'test', N'test@t.com', N'TWZd8ohZNpN7Wz9X0Ba0gA==', 0, 0)
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1011, N'Deepak', N'deepak@gmail.com', N'Iatx/i65gzY4dyYuohoTpg==', 0, 0)
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1012, N'Dan', N'dan@gmail.com', N'MBv71mvmzFPbhNHCqbmLdQ==', 0, 1)
INSERT [dbo].[User] ([UserID], [Ownername], [Email], [Password], [IsAdmin], [IsActive]) VALUES (1013, N'Test', N'Test@gmai.com', N'MBv71mvmzFPbhNHCqbmLdQ==', 0, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[UserStore] ON 

INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (1, 1008, 150)
INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (2, 1008, 20)
INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (3, 1008, 9075)
INSERT [dbo].[UserStore] ([UserStoreID], [UserID], [StoreID]) VALUES (4, 1008, 15)
SET IDENTITY_INSERT [dbo].[UserStore] OFF
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
ALTER TABLE [dbo].[StoreAdChoice]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoice_AdMonthID] FOREIGN KEY([AdMonthID])
REFERENCES [dbo].[AdMonth] ([AdMonthID])
GO
ALTER TABLE [dbo].[StoreAdChoice] CHECK CONSTRAINT [FK_StoreAdChoice_AdMonthID]
GO
ALTER TABLE [dbo].[StoreAdChoice]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoice_ADOption] FOREIGN KEY([AdOptionID])
REFERENCES [dbo].[ADOption] ([ADOptionID])
GO
ALTER TABLE [dbo].[StoreAdChoice] CHECK CONSTRAINT [FK_StoreAdChoice_ADOption]
GO
ALTER TABLE [dbo].[StoreAdChoice]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoice_Coupon] FOREIGN KEY([CouponID])
REFERENCES [dbo].[Coupon] ([CouponID])
GO
ALTER TABLE [dbo].[StoreAdChoice] CHECK CONSTRAINT [FK_StoreAdChoice_Coupon]
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
ALTER TABLE [dbo].[StoreAdChoiceHistory]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoiceHistory_Coupon] FOREIGN KEY([CouponID])
REFERENCES [dbo].[Coupon] ([CouponID])
GO
ALTER TABLE [dbo].[StoreAdChoiceHistory] CHECK CONSTRAINT [FK_StoreAdChoiceHistory_Coupon]
GO
ALTER TABLE [dbo].[StoreAdChoiceHistory]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoiceHistory_StoreAdChoice] FOREIGN KEY([ChoiceID])
REFERENCES [dbo].[StoreAdChoice] ([ChoiceID])
GO
ALTER TABLE [dbo].[StoreAdChoiceHistory] CHECK CONSTRAINT [FK_StoreAdChoiceHistory_StoreAdChoice]
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
