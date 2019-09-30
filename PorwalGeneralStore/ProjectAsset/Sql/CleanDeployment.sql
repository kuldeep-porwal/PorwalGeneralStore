CREATE DATABASE [PorwalGeneralStore]
GO

CREATE TABLE [dbo].[CustomerInfo](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerName] [varchar](500) NOT NULL,
	[Phone] [varchar](20) NULL,
	[City] [varchar](20) NULL,
 CONSTRAINT [PK_CustomerInfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[StoreItem](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](1000) NOT NULL,
	[Title] [varchar](1000) NOT NULL,
	[Description] [varchar](max) NULL,
	[Sku] [varchar](50) NOT NULL,
	[CostPrice] [decimal](18, 0) NOT NULL,
	[SellingPrice] [decimal](18, 0) NOT NULL,
	[Qty] [bigint] NOT NULL,
	[ItemType] [varchar](50) NOT NULL,
	[IsInStoke] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[PublishedDate] [datetime] NULL,
 CONSTRAINT [PK_StoreItem] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[StoreOrder](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[OrderTotal] [decimal](18, 0) NOT NULL,
	[TotalItem] [int] NOT NULL,
	[PaymentStatus] [varchar](50) NOT NULL,
	[PaymentMode] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_StoreOrder] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[StoreOrderItem](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[ItemName] [varchar](1000) NOT NULL,
	[ItemId] [bigint] NOT NULL,
	[Qty] [int] NOT NULL,
	[ListPrice] [decimal](18, 0) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_StoreOrderItem] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
