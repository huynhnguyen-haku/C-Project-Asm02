USE [master]
GO
/****** Object:  Database [CarManagement] ******/
CREATE DATABASE [CarManagement]
GO

USE [CarManagement]
GO

/****** Object:  Table [dbo].[Category] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] NOT NULL,
	[CategoryName] [varchar](40) NOT NULL,
	[CategoryDescription] [nvarchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[User] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[UserName] [varchar](180) NOT NULL,
	[City] [varchar](15) NOT NULL,
	[Country] [varchar](15) NOT NULL,
	[Password] [varchar](30) NOT NULL,
	[Birthday] [date] NULL,
	[Role] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Car] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[CarID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[CarName] [varchar](40) NOT NULL,
	[Description] [varchar](220) NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[UnitsInStock] [int] NOT NULL,
	[CarStatus] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[CarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Order] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] NOT NULL,
	[UserID] [int] NULL,
	[OrderDate] [datetime] NOT NULL,
	[ShippedDate] [datetime] NULL,
	[Total] [money] NULL,
	[OrderStatus] [nchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[OrderDetail] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderID] [int] NOT NULL,
	[CarID] [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Discount] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[CarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Car]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
ON DELETE CASCADE
GO


ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([CarID])
REFERENCES [dbo].[Car] ([CarID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
ON DELETE CASCADE
GO

/****** Object:  Insert Database ******/
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryDescription])
VALUES
(1, N'Sedan', N'A passenger car with a closed body, typically with four doors and a trunk.'),
(2, N'SUV', N'A sport utility vehicle, a type of car that is typically larger and more rugged than a sedan.'),
(3, N'Mini', N'A small car, typically with two doors and a hatchback.'),
(4, N'Van', N'A large, enclosed vehicle that is typically used for transporting people or goods.');

INSERT [dbo].[User] ([UserID], [Email], [UserName], [City], [Country], [Password], [Birthday], [Role])
VALUES
(1, N'David@gmail.com', N'David Copperfield', N'HCM', N'Viet Nam', N'123', CAST(N'1990-02-02' AS Date), N'Customer'),
(2, N'Steve@gmail.com', N'Steven Allan Spielber', N'HCM', N'Viet Nam', N'123', CAST(N'1999-05-05' AS Date), N'Customer'),
(3, N'Robert@gmail.com', N'Robert Capshaw', N'London', N'UK', N'123', CAST(N'1988-07-07' AS Date), N'Customer'),
(4, N'Amanda@gmail.com', N'Amanda Rebecca', N'New York', N'US', N'123', CAST(N'2002-03-03' AS Date), N'Customer'),
(5, N'admin@fpt.edu.vn', N'Admin', N'Ha Noi', N'Viet Nam', N'123', CAST(N'2002-03-15' AS Date), N'Admin'),
(6, N'manager@fpt.edu.vn', N'Manager', N'Da Nang', N'Viet Nam', N'123', CAST(N'2007-06-17' AS Date), N'Manager');

INSERT [dbo].[Car] ([CarID], [CategoryID], [CarName], [Description], [UnitPrice],[UnitsInStock], [CarStatus])
VALUES
(1, 1, 'Toyota Camry', 'A reliable and fuel-efficient sedan', 25000.00, 10, 1),
(2, 1, 'Honda Accord', 'A comfortable and spacious sedan', 28000.00, 5, 1),
(3, 1, 'Hyundai Sonata', 'A stylish and feature-packed sedan', 23000.00, 15, 1),
(4, 2, 'Toyota RAV4', 'A versatile and capable SUV', 30000.00, 8, 1),
(5, 2, 'Honda CR-V', 'A comfortable and fuel-efficient SUV', 32000.00, 7, 1),
(6, 2, 'Ford Escape', 'A sporty and fun-to-drive SUV', 27000.00, 12, 1),
(7, 3, 'MINI Cooper', 'A stylish and iconic small car.', 22000.00, 12, 1),
(8, 3, 'Fiat 500', 'A playful and fun-to-drive small car.', 18000.00, 8, 1),
(9, 3, 'Smart Fortwo', 'A compact and city-friendly small car.', 15000.00, 15, 1),
(10, 4, 'Chrysler Pacifica', 'A spacious and comfortable minivan.', 35000.00, 10, 1),
(11, 4, 'Honda Odyssey', 'A family-friendly minivan with plenty of features.', 38000.00, 7, 1),
(12, 4, 'Mercedes-Benz Sprinter', 'A versatile and customizable cargo van.', 45000.00, 5, 1);

INSERT [dbo].[Order] ([OrderID], [UserID], [OrderDate], [ShippedDate], [Total], [OrderStatus])
VALUES
(4006, 1, CAST(N'2023-05-05T00:00:00.000' AS DateTime), CAST(N'2023-05-06T00:00:00.000' AS DateTime), 25000.0000, N'Done'),
(4007, 1, CAST(N'2023-05-05T00:00:00.000' AS DateTime), CAST(N'2023-05-06T00:00:00.000' AS DateTime), 28000.0000, N'Done');

INSERT [dbo].[OrderDetail] ([OrderID], [CarID], [UnitPrice], [Quantity], [Discount])
VALUES
(4006, 1, 25000.0000, 1, 0),
(4007, 2, 28000, 1, 0);

USE [master]
GO
ALTER DATABASE [CarManagement] SET  READ_WRITE 
GO
