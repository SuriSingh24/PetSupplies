USE [master]
GO
/****** Object:  Database [PetSupplies_8517]    Script Date: 11/29/2018 01:41:19 ******/
CREATE DATABASE [PetSupplies_8517] ON  PRIMARY 
( NAME = N'PetSupplies_8517', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\PetSupplies_8517.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PetSupplies_8517_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\PetSupplies_8517_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PetSupplies_8517] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PetSupplies_8517].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PetSupplies_8517] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [PetSupplies_8517] SET ANSI_NULLS OFF
GO
ALTER DATABASE [PetSupplies_8517] SET ANSI_PADDING OFF
GO
ALTER DATABASE [PetSupplies_8517] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [PetSupplies_8517] SET ARITHABORT OFF
GO
ALTER DATABASE [PetSupplies_8517] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [PetSupplies_8517] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [PetSupplies_8517] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [PetSupplies_8517] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [PetSupplies_8517] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [PetSupplies_8517] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [PetSupplies_8517] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [PetSupplies_8517] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [PetSupplies_8517] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [PetSupplies_8517] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [PetSupplies_8517] SET  DISABLE_BROKER
GO
ALTER DATABASE [PetSupplies_8517] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [PetSupplies_8517] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [PetSupplies_8517] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [PetSupplies_8517] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [PetSupplies_8517] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [PetSupplies_8517] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [PetSupplies_8517] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [PetSupplies_8517] SET  READ_WRITE
GO
ALTER DATABASE [PetSupplies_8517] SET RECOVERY FULL
GO
ALTER DATABASE [PetSupplies_8517] SET  MULTI_USER
GO
ALTER DATABASE [PetSupplies_8517] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [PetSupplies_8517] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'PetSupplies_8517', N'ON'
GO
USE [PetSupplies_8517]
GO
/****** Object:  Table [dbo].[ADOption]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coupon]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventLog]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Market]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/29/2018 01:41:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Ownername] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreAdChoiceHistory]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CouponAdMonth]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AllowedStoreOption]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdMonth]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserStore]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreAdChoice]    Script Date: 11/29/2018 01:41:21 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_User_IsAdmin]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
/****** Object:  Default [DF_User_IsActive]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  ForeignKey [FK_StoreAdChoiceHistory_Coupon]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[StoreAdChoiceHistory]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoiceHistory_Coupon] FOREIGN KEY([ChoiceID])
REFERENCES [dbo].[Coupon] ([CouponID])
GO
ALTER TABLE [dbo].[StoreAdChoiceHistory] CHECK CONSTRAINT [FK_StoreAdChoiceHistory_Coupon]
GO
/****** Object:  ForeignKey [FK_Store_Market]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_Market] FOREIGN KEY([MarketID])
REFERENCES [dbo].[Market] ([MarketID])
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [FK_Store_Market]
GO
/****** Object:  ForeignKey [FK_LoginHistory_User]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[LoginHistory]  WITH CHECK ADD  CONSTRAINT [FK_LoginHistory_User] FOREIGN KEY([LoginHistoryID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[LoginHistory] CHECK CONSTRAINT [FK_LoginHistory_User]
GO
/****** Object:  ForeignKey [FK_CouponAdMonth_Coupon]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[CouponAdMonth]  WITH CHECK ADD  CONSTRAINT [FK_CouponAdMonth_Coupon] FOREIGN KEY([CouponId])
REFERENCES [dbo].[Coupon] ([CouponID])
GO
ALTER TABLE [dbo].[CouponAdMonth] CHECK CONSTRAINT [FK_CouponAdMonth_Coupon]
GO
/****** Object:  ForeignKey [FK_AllowedStoreOption_ADOption]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[AllowedStoreOption]  WITH CHECK ADD  CONSTRAINT [FK_AllowedStoreOption_ADOption] FOREIGN KEY([AdOptionID])
REFERENCES [dbo].[ADOption] ([ADOptionID])
GO
ALTER TABLE [dbo].[AllowedStoreOption] CHECK CONSTRAINT [FK_AllowedStoreOption_ADOption]
GO
/****** Object:  ForeignKey [FK_AllowedStoreOption_Store]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[AllowedStoreOption]  WITH CHECK ADD  CONSTRAINT [FK_AllowedStoreOption_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([StoreID])
GO
ALTER TABLE [dbo].[AllowedStoreOption] CHECK CONSTRAINT [FK_AllowedStoreOption_Store]
GO
/****** Object:  ForeignKey [FK_AdMonth_CouponAdMonth]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[AdMonth]  WITH CHECK ADD  CONSTRAINT [FK_AdMonth_CouponAdMonth] FOREIGN KEY([AdMonthID])
REFERENCES [dbo].[CouponAdMonth] ([AdMonthID])
GO
ALTER TABLE [dbo].[AdMonth] CHECK CONSTRAINT [FK_AdMonth_CouponAdMonth]
GO
/****** Object:  ForeignKey [FK_UserStore_Store]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[UserStore]  WITH CHECK ADD  CONSTRAINT [FK_UserStore_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([StoreID])
GO
ALTER TABLE [dbo].[UserStore] CHECK CONSTRAINT [FK_UserStore_Store]
GO
/****** Object:  ForeignKey [FK_UserStore_User]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[UserStore]  WITH CHECK ADD  CONSTRAINT [FK_UserStore_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserStore] CHECK CONSTRAINT [FK_UserStore_User]
GO
/****** Object:  ForeignKey [FK_StoreAdChoice_AdMonth]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[StoreAdChoice]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoice_AdMonth] FOREIGN KEY([AdMonthID])
REFERENCES [dbo].[AdMonth] ([AdMonthID])
GO
ALTER TABLE [dbo].[StoreAdChoice] CHECK CONSTRAINT [FK_StoreAdChoice_AdMonth]
GO
/****** Object:  ForeignKey [FK_StoreAdChoice_ADOption]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[StoreAdChoice]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoice_ADOption] FOREIGN KEY([AdOptionID])
REFERENCES [dbo].[ADOption] ([ADOptionID])
GO
ALTER TABLE [dbo].[StoreAdChoice] CHECK CONSTRAINT [FK_StoreAdChoice_ADOption]
GO
/****** Object:  ForeignKey [FK_StoreAdChoice_Store]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[StoreAdChoice]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoice_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([StoreID])
GO
ALTER TABLE [dbo].[StoreAdChoice] CHECK CONSTRAINT [FK_StoreAdChoice_Store]
GO
/****** Object:  ForeignKey [FK_StoreAdChoice_User]    Script Date: 11/29/2018 01:41:21 ******/
ALTER TABLE [dbo].[StoreAdChoice]  WITH CHECK ADD  CONSTRAINT [FK_StoreAdChoice_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[StoreAdChoice] CHECK CONSTRAINT [FK_StoreAdChoice_User]
GO
