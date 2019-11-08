USE [master]
GO

CREATE DATABASE [PorwalGeneralStore]
GO

USE [PorwalGeneralStore]
GO

/****** Object:  Table [dbo].[CustomerAddressInfo]    Script Date: 07-11-2019 00:29:33 ******/
CREATE TABLE [dbo].[CustomerAddressInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Address1] [varchar](100) NOT NULL,
	[Address2] [varchar](100) NOT NULL,
	[Address3] [varchar](100) NULL,
	[CustomerId] [bigint] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[State] [varchar](100) NOT NULL,
	[City] [varchar](100) NOT NULL,
	[Pincode] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[AlternatePhoneNumber] [varchar](15) NULL,
 CONSTRAINT [PK_CustomerAddressInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[CustomerInfo]    Script Date: 07-11-2019 00:29:33 ******/
CREATE TABLE [dbo].[CustomerInfo](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerName] [varchar](500) NOT NULL,
	[Phone] [varchar](20) NULL,
	[City] [varchar](20) NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[Email] [varchar](100) NULL,
	[UserType] [int] NOT NULL,
 CONSTRAINT [PK_CustomerInfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_Phone_Number] UNIQUE NONCLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ItemVariantType]    Script Date: 07-11-2019 00:29:33 ******/
CREATE TABLE [dbo].[ItemVariantType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ItemVariantType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[OrderActivityInformation]    Script Date: 07-11-2019 00:29:33 ******/
CREATE TABLE [dbo].[OrderActivityInformation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NULL,
	[OrderActivityDescription] [varchar](max) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_OrderActivityInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[OrderPaymentDetail]    Script Date: 07-11-2019 00:29:33 ******/
CREATE TABLE [dbo].[OrderPaymentDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[TransactionId] [varchar](100) NULL,
	[PaymentMode] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[TransactionStatus] [varchar](50) NOT NULL,
	[TransactionNote] [varchar](500) NULL,
 CONSTRAINT [PK_OrderPaymentDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[StoreItem]    Script Date: 07-11-2019 00:29:33 ******/
CREATE TABLE [dbo].[StoreItem](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](1000) NOT NULL,
	[Title] [varchar](1000) NOT NULL,
	[Description] [varchar](max) NULL,
	[Sku] [varchar](50) NOT NULL,
	[CategoryId] [bigint] NOT NULL,
	[CostPrice] [decimal](18, 0) NOT NULL,
	[SellingPrice] [decimal](18, 0) NOT NULL,
	[Qty] [bigint] NOT NULL,
	[ItemType] [varchar](50) NOT NULL,
	[IsInStoke] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[PublishedDate] [datetime] NULL,
	[ImgUrl] [varchar](50) NULL,
	[ShortDesc] [varchar](500) NULL,
	[IsVariantProduct] [bit] NOT NULL,
 CONSTRAINT [PK_StoreItem] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[StoreItemCategory]    Script Date: 07-11-2019 00:29:33 ******/
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

/****** Object:  Table [dbo].[StoreOrder]    Script Date: 07-11-2019 00:29:33 ******/
CREATE TABLE [dbo].[StoreOrder](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[OrderTotal] [decimal](18, 0) NOT NULL,
	[TotalItem] [int] NOT NULL,
	[PaymentStatus] [varchar](50) NOT NULL,
	[PaymentMode] [varchar](50) NULL,
	[OrderStatus] [int] NOT NULL,
	[OrderCancelReason] [varchar](max) NULL,
	[IsCanceledOrder] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[OrderNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_StoreOrder] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[StoreOrderCustomerInfo]    Script Date: 07-11-2019 00:29:33 ******/
CREATE TABLE [dbo].[StoreOrderCustomerInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[CustomerName] [varchar](100) NOT NULL,
	[Address1] [varchar](100) NOT NULL,
	[Address2] [varchar](100) NOT NULL,
	[Address3] [varchar](100) NULL,
	[City] [varchar](100) NOT NULL,
	[State] [varchar](100) NOT NULL,
	[Pincode] [bigint] NOT NULL,
	[Phone] [varchar](13) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_StoreOrderCustomerInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[StoreOrderItem]    Script Date: 07-11-2019 00:29:33 ******/
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

/****** Object:  Index [IX_StoreOrder_CustomerId]    Script Date: 07-11-2019 00:29:33 ******/
CREATE NONCLUSTERED INDEX [IX_StoreOrder_CustomerId] ON [dbo].[StoreOrder]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [IX_StoreOrderItem_ItemId]    Script Date: 07-11-2019 00:29:33 ******/
CREATE NONCLUSTERED INDEX [IX_StoreOrderItem_ItemId] ON [dbo].[StoreOrderItem]
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [IX_StoreOrderItem_OrderId]    Script Date: 07-11-2019 00:29:33 ******/
CREATE NONCLUSTERED INDEX [IX_StoreOrderItem_OrderId] ON [dbo].[StoreOrderItem]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CustomerAddressInfo] ADD  CONSTRAINT [DF_CustomerAddressInfo_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[CustomerInfo] ADD  CONSTRAINT [DF_CustomerInfo_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[CustomerInfo] ADD  CONSTRAINT [DF_CustomerInfo_UserType]  DEFAULT ((1)) FOR [UserType]
GO

ALTER TABLE [dbo].[ItemVariantType] ADD  CONSTRAINT [DF_ItemVariantType_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[OrderActivityInformation] ADD  CONSTRAINT [DF_OrderActivityInformation_CreateDate]  DEFAULT (getutcdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[OrderPaymentDetail] ADD  CONSTRAINT [DF_OrderPaymentDetail_Amount]  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[OrderPaymentDetail] ADD  CONSTRAINT [DF_OrderPaymentDetail_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[OrderPaymentDetail] ADD  CONSTRAINT [DF_OrderPaymentDetail_TransactionStatus]  DEFAULT ('Success') FOR [TransactionStatus]
GO

ALTER TABLE [dbo].[StoreItem] ADD  CONSTRAINT [DF__StoreItem__ItemT__276EDEB3]  DEFAULT ('Inventory') FOR [ItemType]
GO

ALTER TABLE [dbo].[StoreItem] ADD  CONSTRAINT [DF__StoreItem__IsInS__286302EC]  DEFAULT ((1)) FOR [IsInStoke]
GO

ALTER TABLE [dbo].[StoreItem] ADD  CONSTRAINT [DF__StoreItem__Creat__29572725]  DEFAULT (getutcdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StoreItem] ADD  CONSTRAINT [DF_StoreItem_IsVariantProduct]  DEFAULT ((0)) FOR [IsVariantProduct]
GO

ALTER TABLE [dbo].[StoreItemCategory] ADD  CONSTRAINT [DF_StoreItemCategory_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[StoreItemCategory] ADD  CONSTRAINT [DF_StoreItemCategory_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[StoreOrder] ADD  CONSTRAINT [DF__StoreOrde__Payme__2C3393D0]  DEFAULT ('Pending') FOR [PaymentStatus]
GO

ALTER TABLE [dbo].[StoreOrder] ADD  CONSTRAINT [DF_StoreOrder_OrderStatus]  DEFAULT ((1)) FOR [OrderStatus]
GO

ALTER TABLE [dbo].[StoreOrder] ADD  CONSTRAINT [DF_StoreOrder_IsCanceledOrder]  DEFAULT ((0)) FOR [IsCanceledOrder]
GO

ALTER TABLE [dbo].[StoreOrder] ADD  CONSTRAINT [DF__StoreOrde__Creat__2D27B809]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[StoreOrderCustomerInfo] ADD  CONSTRAINT [DF_StoreOrderCustomerInfo_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[StoreOrderCustomerInfo] ADD  CONSTRAINT [DF_StoreOrderCustomerInfo_Type]  DEFAULT ((1)) FOR [Type]
GO

ALTER TABLE [dbo].[StoreOrderItem] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[CustomerAddressInfo]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAddressInfo_CustomerInfo] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CustomerInfo] ([id])
GO

ALTER TABLE [dbo].[CustomerAddressInfo] CHECK CONSTRAINT [FK_CustomerAddressInfo_CustomerInfo]
GO

ALTER TABLE [dbo].[StoreItem]  WITH CHECK ADD  CONSTRAINT [FK_StoreItem_StoreItemCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[StoreItemCategory] ([Id])
GO

ALTER TABLE [dbo].[StoreItem] CHECK CONSTRAINT [FK_StoreItem_StoreItemCategory]
GO

ALTER TABLE [dbo].[StoreOrder]  WITH CHECK ADD  CONSTRAINT [FK_StoreOrder_CustomerInfo] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CustomerInfo] ([id])
GO

ALTER TABLE [dbo].[StoreOrder] CHECK CONSTRAINT [FK_StoreOrder_CustomerInfo]
GO

ALTER TABLE [dbo].[StoreOrderCustomerInfo]  WITH CHECK ADD  CONSTRAINT [FK_StoreOrderCustomerInfo_StoreOrder] FOREIGN KEY([OrderId])
REFERENCES [dbo].[StoreOrder] ([id])
GO

ALTER TABLE [dbo].[StoreOrderCustomerInfo] CHECK CONSTRAINT [FK_StoreOrderCustomerInfo_StoreOrder]
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

USE [master]
GO
ALTER DATABASE [PorwalGeneralStore] SET  READ_WRITE 
GO

CREATE UNIQUE NONCLUSTERED INDEX [Unique_PhoneNumber_CustomerInfo] ON [dbo].[CustomerInfo]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PhoneNumber Will be Unique in System.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerInfo', @level2type=N'INDEX',@level2name=N'Unique_PhoneNumber_CustomerInfo'
GO