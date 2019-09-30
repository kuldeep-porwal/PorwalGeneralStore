CREATE DATABASE [PorwalGeneralStore]
GO

CREATE TABLE [dbo].[CustomerInfo](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerName] [varchar](500) NOT NULL,
	[Phone] [varchar](20) NULL,
	[City] [varchar](20) NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Password] [varchar](20) NOT NULL,
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

CREATE TABLE [dbo].[StoreItemCategory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_StoreItemCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[StoreItem]  WITH CHECK ADD  CONSTRAINT [FK_StoreItem_StoreItemCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[StoreItemCategory] ([Id])
GO

ALTER TABLE [dbo].[StoreItem] CHECK CONSTRAINT [FK_StoreItem_StoreItemCategory]
GO

ALTER TABLE [dbo].[StoreItem] ADD  CONSTRAINT [DF__StoreItem__ItemT__276EDEB3]  DEFAULT ('Inventory') FOR [ItemType]
GO

ALTER TABLE [dbo].[StoreItem] ADD  CONSTRAINT [DF__StoreItem__IsInS__286302EC]  DEFAULT ((1)) FOR [IsInStoke]
GO

ALTER TABLE [dbo].[StoreItem] ADD  CONSTRAINT [DF__StoreItem__Creat__29572725]  DEFAULT (getutcdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StoreOrder] ADD  DEFAULT ('Pending') FOR [PaymentStatus]
GO

ALTER TABLE [dbo].[StoreOrder] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[StoreOrder]  WITH CHECK ADD  CONSTRAINT [FK_StoreOrder_CustomerInfo] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CustomerInfo] ([id])
GO

ALTER TABLE [dbo].[StoreOrder] CHECK CONSTRAINT [FK_StoreOrder_CustomerInfo]
GO

ALTER TABLE [dbo].[StoreOrderItem] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[StoreOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_StoreOrderItem_StoreItem] FOREIGN KEY([ItemId])
REFERENCES [dbo].[StoreItem] ([id])
GO

ALTER TABLE [dbo].[StoreOrderItem] CHECK CONSTRAINT [FK_StoreOrderItem_StoreItem]
GO

ALTER TABLE [dbo].[StoreOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_StoreOrderItem_StoreOrder] FOREIGN KEY([OrderId])
REFERENCES [dbo].[StoreOrder] ([id])
GO

ALTER TABLE [dbo].[StoreOrderItem] CHECK CONSTRAINT [FK_StoreOrderItem_StoreOrder]
GO

ALTER TABLE [dbo].[StoreItemCategory] ADD  CONSTRAINT [DF_StoreItemCategory_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[StoreItemCategory] ADD  CONSTRAINT [DF_StoreItemCategory_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
