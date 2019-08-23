USE [master]
GO

CREATE DATABASE [PorwalGeneralStore]
GO

ALTER DATABASE [PorwalGeneralStore] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PorwalGeneralStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PorwalGeneralStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PorwalGeneralStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PorwalGeneralStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PorwalGeneralStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PorwalGeneralStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PorwalGeneralStore] SET  MULTI_USER 
GO
ALTER DATABASE [PorwalGeneralStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PorwalGeneralStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PorwalGeneralStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PorwalGeneralStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PorwalGeneralStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PorwalGeneralStore] SET QUERY_STORE = OFF
GO
USE [PorwalGeneralStore]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [PorwalGeneralStore]
GO
/****** Object:  Table [dbo].[ProductVariantOption]    Script Date: 4/22/2019 11:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductVariantOption](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OptionName] [varchar](100) NOT NULL,
	[OptionValue] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductVariantOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreProduct]    Script Date: 4/22/2019 11:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreProduct](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](100) NOT NULL,
	[Sku] [varchar](50) NOT NULL,
	[CostPrice] [float] NOT NULL,
	[SellingPrice] [float] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CategoryId] [bigint] NULL,
	[MainProductImage] [varchar](max) NOT NULL,
	[ProductHeading] [varchar](300) NOT NULL,
	[ProductShortDesc] [varchar](1000) NOT NULL,
	[ProductFullDesc] [varchar](max) NOT NULL,
	[IsInStock] [bit] NOT NULL,
	[IsInventoryProduct] [bit] NOT NULL,
	[UPCCode] [varchar](20) NULL,
	[HasVariantProduct] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[PublishedDate] [datetime] NOT NULL,
	[Vendor] [varchar](100) NULL,
	[IsActiveProduct] [bit] NOT NULL,
 CONSTRAINT [PK_StoreProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreProductMasterCategory]    Script Date: 4/22/2019 11:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreProductMasterCategory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](100) NOT NULL,
	[CategoryCode] [varchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StoreProductMasterCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreProductVariant]    Script Date: 4/22/2019 11:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreProductVariant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[OptionId] [int] NULL,
	[Sku] [varchar](100) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CostPrice] [float] NOT NULL,
	[SellingPrice] [float] NOT NULL,
	[IsInStock] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[OptionValue] [varchar](100) NOT NULL,
 CONSTRAINT [PK_StoreProductVariant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StoreProduct] ADD  CONSTRAINT [DF_StoreProduct_CostPrice]  DEFAULT ((0)) FOR [CostPrice]
GO
ALTER TABLE [dbo].[StoreProduct] ADD  CONSTRAINT [DF_StoreProduct_SellingPrice]  DEFAULT ((0)) FOR [SellingPrice]
GO
ALTER TABLE [dbo].[StoreProduct] ADD  CONSTRAINT [DF_StoreProduct_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[StoreProduct] ADD  CONSTRAINT [DF_StoreProduct_IsInStock]  DEFAULT ((0)) FOR [IsInStock]
GO
ALTER TABLE [dbo].[StoreProduct] ADD  CONSTRAINT [DF_StoreProduct_IsInventoryProduct]  DEFAULT ((0)) FOR [IsInventoryProduct]
GO
ALTER TABLE [dbo].[StoreProduct] ADD  CONSTRAINT [DF_StoreProduct_HasVariantProduct]  DEFAULT ((0)) FOR [HasVariantProduct]
GO
ALTER TABLE [dbo].[StoreProduct] ADD  CONSTRAINT [DF_StoreProduct_IsActiveProduct]  DEFAULT ((1)) FOR [IsActiveProduct]
GO
ALTER TABLE [dbo].[StoreProductMasterCategory] ADD  CONSTRAINT [DF_StoreProductMasterCategory_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[StoreProductVariant] ADD  CONSTRAINT [DF_StoreProductVariant_OptionId]  DEFAULT ((0)) FOR [OptionId]
GO
ALTER TABLE [dbo].[StoreProductVariant] ADD  CONSTRAINT [DF_StoreProductVariant_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[StoreProductVariant] ADD  CONSTRAINT [DF_StoreProductVariant_CostPrice]  DEFAULT ((0)) FOR [CostPrice]
GO
ALTER TABLE [dbo].[StoreProductVariant] ADD  CONSTRAINT [DF_StoreProductVariant_SellingPrice]  DEFAULT ((0)) FOR [SellingPrice]
GO
ALTER TABLE [dbo].[StoreProductVariant] ADD  CONSTRAINT [DF_StoreProductVariant_IsInStock]  DEFAULT ((0)) FOR [IsInStock]
GO
ALTER TABLE [dbo].[StoreProduct]  WITH CHECK ADD  CONSTRAINT [FK_StoreProduct_StoreProductMasterCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[StoreProductMasterCategory] ([Id])
GO
ALTER TABLE [dbo].[StoreProduct] CHECK CONSTRAINT [FK_StoreProduct_StoreProductMasterCategory]
GO
ALTER TABLE [dbo].[StoreProductVariant]  WITH CHECK ADD  CONSTRAINT [FK_StoreProductVariant_ProductVariantOption] FOREIGN KEY([OptionId])
REFERENCES [dbo].[ProductVariantOption] ([Id])
GO
ALTER TABLE [dbo].[StoreProductVariant] CHECK CONSTRAINT [FK_StoreProductVariant_ProductVariantOption]
GO
ALTER TABLE [dbo].[StoreProductVariant]  WITH CHECK ADD  CONSTRAINT [FK_StoreProductVariant_StoreProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[StoreProduct] ([Id])
GO
ALTER TABLE [dbo].[StoreProductVariant] CHECK CONSTRAINT [FK_StoreProductVariant_StoreProduct]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'One Product have multiple variant.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StoreProductVariant', @level2type=N'CONSTRAINT',@level2name=N'FK_StoreProductVariant_StoreProduct'
GO
USE [master]
GO
ALTER DATABASE [PorwalGeneralStore] SET  READ_WRITE 
GO
