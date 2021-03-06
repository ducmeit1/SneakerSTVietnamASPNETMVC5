USE [DB_SNEAKERSTV2]
GO
/****** Object:  Table [dbo].[Business]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Business](
	[BusinessID] [int] IDENTITY(1,1) NOT NULL,
	[BusinessName] [varchar](250) NOT NULL,
	[AreaName] [nvarchar](50) NULL,
	[BusinessDescription] [nvarchar](250) NULL,
 CONSTRAINT [PK_Business] PRIMARY KEY CLUSTERED 
(
	[BusinessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] NOT NULL,
	[CategoryName] [nvarchar](250) NOT NULL,
	[CategoryLogo] [nvarchar](250) NOT NULL,
	[CategoryDescription] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ImageProduct]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ImageProduct](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[ImageURL] [varchar](max) NOT NULL,
	[ProductID] [int] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
 CONSTRAINT [PK_ImageProduct] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceID] [int] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[Postcode] [nvarchar](50) NOT NULL,
	[State] [nvarchar](100) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[DeliveryMethod] [nvarchar](50) NOT NULL,
	[InvoiceStatusID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InvoiceStatus]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceStatus](
	[InvoiceStatusID] [int] NOT NULL,
	[InvoiceStatusName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_InvoiceStatus] PRIMARY KEY CLUSTERED 
(
	[InvoiceStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Detail] [nvarchar](max) NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[SellPrice] [float] NOT NULL,
	[InStock] [bit] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[PublishDate] [date] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] NOT NULL,
	[RoleName] [varchar](50) NULL,
 CONSTRAINT [PK_RoleTbl] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleBusiness]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleBusiness](
	[RoleID] [int] NOT NULL,
	[BusinessID] [int] NOT NULL,
 CONSTRAINT [PK_RoleBusiness] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC,
	[BusinessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShoppingDetail]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingDetail](
	[DetailID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[SizeID] [int] NOT NULL,
	[SellPrice] [float] NOT NULL,
 CONSTRAINT [PK_ShoppingDetail] PRIMARY KEY CLUSTERED 
(
	[DetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Size]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Size](
	[SizeID] [int] IDENTITY(1,1) NOT NULL,
	[SizeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[SizeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Slider]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Slider](
	[SlideID] [int] IDENTITY(1,1) NOT NULL,
	[SlideName] [nvarchar](50) NOT NULL,
	[Slide Description] [nvarchar](max) NOT NULL,
	[SliderImage] [nvarchar](max) NOT NULL,
	[SlideLink] [varchar](max) NOT NULL,
	[SlideButtonName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Slider] PRIMARY KEY CLUSTERED 
(
	[SlideID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[SizeID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[SizeID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 3/27/2017 1:47:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[SecurityCode] [varchar](250) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Gender] [bit] NOT NULL,
	[Address] [nvarchar](250) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[Postcode] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[PhoneNumber] [varchar](100) NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[RoleID] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[ReceiveEmail] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Business] ON 

INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (1, N'HomeController-Index', N'SneakerSTVietnamMVC.Areas.AdminCP.Controllers', N'Access to AdminCP Home Page')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (2, N'RoleController-GetBusiness', N'SneakerSTVietnamMVC.Areas.AdminCP.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (3, N'RoleController-Index', N'SneakerSTVietnamMVC.Areas.AdminCP.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (4, N'RoleController-UpdateBusiness', N'SneakerSTVietnamMVC.Areas.AdminCP.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (5, N'RoleController-UpdateRole', N'SneakerSTVietnamMVC.Areas.AdminCP.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (6, N'AccountController-ConfirmEmail', N'SneakerSTVietnamMVC.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (7, N'AccountController-ConfirmRegister', N'SneakerSTVietnamMVC.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (8, N'AccountController-Login', N'SneakerSTVietnamMVC.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (9, N'AccountController-Register', N'SneakerSTVietnamMVC.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (10, N'AccountController-SendConfirmEmail', N'SneakerSTVietnamMVC.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (11, N'HomeController-AccessDenied', N'SneakerSTVietnamMVC.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (12, N'HomeController-Index', N'SneakerSTVietnamMVC.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (13, N'AccountController-Logout', N'SneakerSTVietnamMVC.Controllers', N'No description')
INSERT [dbo].[Business] ([BusinessID], [BusinessName], [AreaName], [BusinessDescription]) VALUES (16, N'HomeController-Logout', N'SneakerSTVietnamMVC.Areas.AdminCP.Controllers', N'No description')
SET IDENTITY_INSERT [dbo].[Business] OFF
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryLogo], [CategoryDescription]) VALUES (1, N'Adidas', N'Template/Images/Brand/adidas.png', N'Adidas is one of the most successful and dominant brands in the business and can look back on a long amazing history. In 1920 Adi Dassler produced the first shoes without electricity in his Mothers washroom. Together with his brother Rudolf he founded the Dassler OHG in 1924. The rising demand and better production facilities increased the number of produced shoes in the following years. In 1936 the legendary Jesse Owens won 4 gold medals in Dassler shoes at the Olympic games, which gave the young brand a big push. When Adi and Rudolf decided to part ways in 1948, both went on to found their own brands, Adidas (Adi) and Puma (Rudolf). In the same year Adidas presented the 3 stripes on the side panel, which were originally made just for stabilization.
                        In 1954 Adidas was in focus of the world public again. The German national soccer team won the world cup wearing the shoes containing the revolutionary screw-in studs. The brand with the three stripes developed into one of the most dominant brands at the top sport events like the Olympics.  In 1972 Adidas presented the trefoil logo which represented the Olympic spirit. In the following years the German brand which is based in Herzogenaurach made the successful transition from a normal sportswear- to a lifestyle sport brand by working together with different Artist and Stars. One of them was the famous rap group Run DMC which dedicated their favorite brand with "My Adidas" a classic rap. Run DMC was the first music artists to be signed by Adidas...')
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryLogo], [CategoryDescription]) VALUES (3, N'Asics', N'Template/Images/Brand/acis.png', N'ASICS (アシックス Ashikkusu?) (stylized as asics) is a Japanese multinational corporation athletic equipment company which produces footwear and sports equipment designed for a wide range of sports, generally in the upper price range. The name is an acronym for the Latin phrase anima sana in corpore sano[2] which translates as "a healthy soul in a healthy body". In recent years their running shoes have often been ranked among the top performance footwear in the market.')
SET IDENTITY_INSERT [dbo].[ImageProduct] ON 

INSERT [dbo].[ImageProduct] ([ImageID], [ImageURL], [ProductID], [IsDisplay]) VALUES (33, N'Template/Images/Products/TUBULAR X PRIMEKNIT A 2017/6.jpg', 2, 1)
INSERT [dbo].[ImageProduct] ([ImageID], [ImageURL], [ProductID], [IsDisplay]) VALUES (34, N'Template/Images/Products/TUBULAR X PRIMEKNIT A 2017/7.jpg', 2, 1)
INSERT [dbo].[ImageProduct] ([ImageID], [ImageURL], [ProductID], [IsDisplay]) VALUES (35, N'Template/Images/Products/NMD R1 BOOST RUNNER PRIMEKNIT/1.jpg', 4, 1)
INSERT [dbo].[ImageProduct] ([ImageID], [ImageURL], [ProductID], [IsDisplay]) VALUES (36, N'Template/Images/Products/NMD R1 BOOST RUNNER PRIMEKNIT/2.jpg', 4, 1)
INSERT [dbo].[ImageProduct] ([ImageID], [ImageURL], [ProductID], [IsDisplay]) VALUES (37, N'Template/Images/Products/NMD R1 BOOST RUNNER PRIMEKNIT/3.jpg', 4, 1)
SET IDENTITY_INSERT [dbo].[ImageProduct] OFF
INSERT [dbo].[Invoice] ([InvoiceID], [FirstName], [LastName], [Email], [Address], [City], [Postcode], [State], [PhoneNumber], [Country], [PaymentMethod], [DeliveryMethod], [InvoiceStatusID], [UserID]) VALUES (1, N'Duc', N'Ho', N'ducmeit2015@gmail.com', N'DH FPT', N'Ha Noi', N'10000', N'Ha Noi', N'01666210190', N'Viet Nam', N'Visa', N'Standard', 1, 1)
INSERT [dbo].[InvoiceStatus] ([InvoiceStatusID], [InvoiceStatusName]) VALUES (1, N'Paid')
INSERT [dbo].[InvoiceStatus] ([InvoiceStatusID], [InvoiceStatusName]) VALUES (2, N'Processing')
INSERT [dbo].[InvoiceStatus] ([InvoiceStatusID], [InvoiceStatusName]) VALUES (3, N'Delivered')
INSERT [dbo].[InvoiceStatus] ([InvoiceStatusID], [InvoiceStatusName]) VALUES (4, N'Canceled')
INSERT [dbo].[InvoiceStatus] ([InvoiceStatusID], [InvoiceStatusName]) VALUES (5, N'Unpaid')
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Detail], [Gender], [SellPrice], [InStock], [CategoryID], [IsDisplay], [PublishDate]) VALUES (2, N'TUBULAR X PRIMEKNIT A 2017', N'# M20324 Leder Oberflche R White/CoreWhite/Green 123', N'This unique Stan Smith by Adidas which is also know by the Productnumber M20324 has long durability and features as always the Trefoil emblem on the side panel of the new Sneaker. Adidas releases a cool "R White/CoreWhite/Green" Stan Smith that is one new Item of the Autumn / Winter Collection. This superb Tennis Sneaker by the in Herzogenaurach founded Lifestyle Brand has finally arrived and features the resistant "R White/CoreWhite/Green" Leather Color Combo. 123', N'Male', 155, 0, 1, 1, CAST(N'2017-03-20' AS Date))
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Detail], [Gender], [SellPrice], [InStock], [CategoryID], [IsDisplay], [PublishDate]) VALUES (4, N'NMD R1 BOOST RUNNER PRIMEKNIT ', N'Synthetic Upper Core Black/CoreBlack/RWhite ', N'The Adidas NMD is inspired by the path and going straight to the future. The Design fits perfect into the urban jungle without any borders. The combination out of 3 iconic Styles like the Micro Pacer, Boston Super and The Rising Star are responsible for the design of NMD. The Material Mix from Micro-engineered Boost and Primeknit reflects the modern Craftsmanship from Herzogenaurach. The Adidas NMD is on one side a technical Runner but on the other a piece of Lifestyle. Different angles, the shape and the Color Blocked Midsole all these together stand for the Future. The Material Mix of Boost and Primeknit stands for a durable, shock-resitant, flexibility, stability and strength. The best from the past with the power from the future. That is NMD. EXPLORE,DISCOVER;EXPERIENCE.', N'Male', 189, 1, 1, 1, CAST(N'2017-03-26' AS Date))
SET IDENTITY_INSERT [dbo].[Product] OFF
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'Administrator')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (2, N'Customer')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (3, N'Guest')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (4, N'Banned')
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 1)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 2)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 3)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 4)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 5)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 6)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 7)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 8)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 9)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 10)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 11)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 12)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 13)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (1, 16)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (2, 1)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (2, 10)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (2, 11)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (2, 12)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (2, 13)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (3, 1)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (3, 6)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (3, 7)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (3, 8)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (3, 9)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (3, 10)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (3, 11)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (3, 12)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (4, 1)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (4, 11)
INSERT [dbo].[RoleBusiness] ([RoleID], [BusinessID]) VALUES (4, 13)
SET IDENTITY_INSERT [dbo].[ShoppingDetail] ON 

INSERT [dbo].[ShoppingDetail] ([DetailID], [ProductID], [InvoiceID], [Quantity], [SizeID], [SellPrice]) VALUES (1, 4, 1, 1, 2, 189)
SET IDENTITY_INSERT [dbo].[ShoppingDetail] OFF
SET IDENTITY_INSERT [dbo].[Size] ON 

INSERT [dbo].[Size] ([SizeID], [SizeName]) VALUES (2, N'US 9')
INSERT [dbo].[Size] ([SizeID], [SizeName]) VALUES (4, N'US 7')
INSERT [dbo].[Size] ([SizeID], [SizeName]) VALUES (5, N'US 6')
INSERT [dbo].[Size] ([SizeID], [SizeName]) VALUES (6, N'US 5')
INSERT [dbo].[Size] ([SizeID], [SizeName]) VALUES (7, N'US 8')
SET IDENTITY_INSERT [dbo].[Size] OFF
SET IDENTITY_INSERT [dbo].[Slider] ON 

INSERT [dbo].[Slider] ([SlideID], [SlideName], [Slide Description], [SliderImage], [SlideLink], [SlideButtonName]) VALUES (1, N'SNEAKERHEAD HEADEWARE', N'A product of SneakerHead in autumn season 2017', N'Template/Images/Slider/full-slide1.jpg', N'#', N'MORE DETAILS')
INSERT [dbo].[Slider] ([SlideID], [SlideName], [Slide Description], [SliderImage], [SlideLink], [SlideButtonName]) VALUES (2, N'NEW BALANCE', N'The new sneakers of New Blanace brand', N'Template/Images/Slider/full-slide2.jpg', N'#', N'View More')
INSERT [dbo].[Slider] ([SlideID], [SlideName], [Slide Description], [SliderImage], [SlideLink], [SlideButtonName]) VALUES (4, N'ADIDAS SUPERSTAR', N'New product from Adidas', N'Template/Images/Slider/full-slide4.jpg', N'#', N'VIEW MORE')
INSERT [dbo].[Slider] ([SlideID], [SlideName], [Slide Description], [SliderImage], [SlideLink], [SlideButtonName]) VALUES (5, N'SALE OFF UP TO 60%', N'Happy shopping day.', N'Template/Images/Slider/full-slide5.jpg', N'#', N'MORE DETAILS')
INSERT [dbo].[Slider] ([SlideID], [SlideName], [Slide Description], [SliderImage], [SlideLink], [SlideButtonName]) VALUES (6, N'VANS', N'A new product from VANS', N'Template/Images/Slider/full-slide3.jpg', N'#', N'VIEW MORE')
SET IDENTITY_INSERT [dbo].[Slider] OFF
INSERT [dbo].[Stock] ([SizeID], [ProductID], [Quantity]) VALUES (2, 2, 1)
INSERT [dbo].[Stock] ([SizeID], [ProductID], [Quantity]) VALUES (2, 4, 3)
INSERT [dbo].[Stock] ([SizeID], [ProductID], [Quantity]) VALUES (4, 2, 3)
INSERT [dbo].[Stock] ([SizeID], [ProductID], [Quantity]) VALUES (5, 2, 2)
INSERT [dbo].[Stock] ([SizeID], [ProductID], [Quantity]) VALUES (6, 4, 4)
INSERT [dbo].[Stock] ([SizeID], [ProductID], [Quantity]) VALUES (7, 2, 5)
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [Email], [Password], [SecurityCode], [FirstName], [LastName], [Gender], [Address], [City], [Postcode], [State], [PhoneNumber], [Country], [RoleID], [Active], [ReceiveEmail]) VALUES (1, N'ducmeit2015@gmail.com', N'b31a25e92e9a815c822b83624b938237', N'd0f3292b5213525da6a0789bfb600ab0CE6q44MCIx', N'Duc', N'Ho', 1, N'Dai Hoc FPT', N'Ha Noi', N'10000', N'abc123', N'1666210190', N'Việt Nam', 1, 1, 1)
INSERT [dbo].[User] ([UserID], [Email], [Password], [SecurityCode], [FirstName], [LastName], [Gender], [Address], [City], [Postcode], [State], [PhoneNumber], [Country], [RoleID], [Active], [ReceiveEmail]) VALUES (2, N'danghung.tdh@gmail.com', N'd1dd42000105e2cb2af78584a58fdfe7', N'53064a13f8764ff58db0c5b5fb43bd158AowycDgN7', N'Hung', N'Tran', 1, N'FPT', N'HN', N'HN', N'Hung', N'12312312321', N'VN', 2, 0, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[ImageProduct]  WITH CHECK ADD  CONSTRAINT [FK_ImageProduct_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[ImageProduct] CHECK CONSTRAINT [FK_ImageProduct_Product]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InvoiceStatus] FOREIGN KEY([InvoiceStatusID])
REFERENCES [dbo].[InvoiceStatus] ([InvoiceStatusID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_InvoiceStatus]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_User]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[RoleBusiness]  WITH CHECK ADD  CONSTRAINT [FK_RoleBusiness_Business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[RoleBusiness] CHECK CONSTRAINT [FK_RoleBusiness_Business]
GO
ALTER TABLE [dbo].[RoleBusiness]  WITH CHECK ADD  CONSTRAINT [FK_RoleBusiness_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[RoleBusiness] CHECK CONSTRAINT [FK_RoleBusiness_Role]
GO
ALTER TABLE [dbo].[ShoppingDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingDetail_Invoice] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([InvoiceID])
GO
ALTER TABLE [dbo].[ShoppingDetail] CHECK CONSTRAINT [FK_ShoppingDetail_Invoice]
GO
ALTER TABLE [dbo].[ShoppingDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[ShoppingDetail] CHECK CONSTRAINT [FK_ShoppingDetail_Product]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Product]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Size] FOREIGN KEY([SizeID])
REFERENCES [dbo].[Size] ([SizeID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Size]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_UserTbl_RoleTbl] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_UserTbl_RoleTbl]
GO
