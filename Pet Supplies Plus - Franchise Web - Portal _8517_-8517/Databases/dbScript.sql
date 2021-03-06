USE [PetSupplies_8517]
GO
/****** Object:  Table [dbo].[AdMonth]    Script Date: 5/31/2016 3:49:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdMonth](
	[AdMonthID] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NULL,
	[Month] [int] NULL,
	[LockoutendDate] [datetime] NULL,
	[LockoutStartDate] [datetime] NULL,
	[InHomeStart] [datetime] NULL,
	[InHomeEnd] [datetime] NULL,
	[Petpartnerinfo] [nvarchar](50) NULL,
	[InHomeDate] [datetime] NULL,
	[AdoptionID] [int] NULL,
 CONSTRAINT [PK_AdMonth] PRIMARY KEY CLUSTERED 
(
	[AdMonthID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ADOption]    Script Date: 5/31/2016 3:49:08 PM ******/
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
/****** Object:  Table [dbo].[AllowedStoreOption]    Script Date: 5/31/2016 3:49:08 PM ******/
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
/****** Object:  Table [dbo].[Coupon]    Script Date: 5/31/2016 3:49:08 PM ******/
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
/****** Object:  Table [dbo].[CouponAdMonth]    Script Date: 5/31/2016 3:49:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CouponAdMonth](
	[AdMonthID] [int] IDENTITY(1,1) NOT NULL,
	[CouponId] [int] NOT NULL,
 CONSTRAINT [PK_CouponAdMonth] PRIMARY KEY CLUSTERED 
(
	[AdMonthID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventLog]    Script Date: 5/31/2016 3:49:08 PM ******/
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
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 5/31/2016 3:49:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistory](
	[LoginHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[TimeStamp] [time](7) NULL,
	[IPaddress] [nvarchar](50) NULL,
	[Device] [nvarchar](50) NULL,
	[Browser] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
(
	[LoginHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Market]    Script Date: 5/31/2016 3:49:08 PM ******/
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
/****** Object:  Table [dbo].[Store]    Script Date: 5/31/2016 3:49:08 PM ******/
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
/****** Object:  Table [dbo].[StoreAdChoice]    Script Date: 5/31/2016 3:49:08 PM ******/
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
	[FollowedCorporate] [nvarchar](50) NULL,
 CONSTRAINT [PK_StoreAdChoice] PRIMARY KEY CLUSTERED 
(
	[AdMonthID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreAdChoiceHistory]    Script Date: 5/31/2016 3:49:08 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 5/31/2016 3:49:08 PM ******/
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
/****** Object:  Table [dbo].[UserStore]    Script Date: 5/31/2016 3:49:08 PM ******/
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
ALTER TABLE [dbo].[AdMonth]  WITH CHECK ADD  CONSTRAINT [FK_AdMonth_CouponAdMonth] FOREIGN KEY([AdMonthID])
REFERENCES [dbo].[CouponAdMonth] ([AdMonthID])
GO
ALTER TABLE [dbo].[AdMonth] CHECK CONSTRAINT [FK_AdMonth_CouponAdMonth]
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
ALTER TABLE [dbo].[CouponAdMonth]  WITH CHECK ADD  CONSTRAINT [FK_CouponAdMonth_Coupon] FOREIGN KEY([CouponId])
REFERENCES [dbo].[Coupon] ([CouponID])
GO
ALTER TABLE [dbo].[CouponAdMonth] CHECK CONSTRAINT [FK_CouponAdMonth_Coupon]
GO
ALTER TABLE [dbo].[LoginHistory]  WITH CHECK ADD  CONSTRAINT [FK_LoginHistory_User] FOREIGN KEY([LoginHistoryID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[LoginHistory] CHECK CONSTRAINT [FK_LoginHistory_User]
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_Market] FOREIGN KEY([MarketID])
REFERENCES [dbo].[Market] ([MarketID])
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [FK_Store_Market]
GO
ALTER TABLE [dbo].[StoreAdChoice]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoice_AdMonth] FOREIGN KEY([AdMonthID])
REFERENCES [dbo].[AdMonth] ([AdMonthID])
GO
ALTER TABLE [dbo].[StoreAdChoice] CHECK CONSTRAINT [FK_StoreAdChoice_AdMonth]
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
