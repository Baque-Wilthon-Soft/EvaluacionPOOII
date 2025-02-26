USE [master]
GO
/****** Object:  Database [FacturacionElectronica]    Script Date: 30/01/2025 8:06:12 ******/
CREATE DATABASE [FacturacionElectronica]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FacturacionElectronica', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\FacturacionElectronica.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FacturacionElectronica_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\FacturacionElectronica_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [FacturacionElectronica] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FacturacionElectronica].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FacturacionElectronica] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET ARITHABORT OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FacturacionElectronica] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FacturacionElectronica] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FacturacionElectronica] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FacturacionElectronica] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FacturacionElectronica] SET  MULTI_USER 
GO
ALTER DATABASE [FacturacionElectronica] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FacturacionElectronica] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FacturacionElectronica] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FacturacionElectronica] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FacturacionElectronica] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FacturacionElectronica] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FacturacionElectronica] SET QUERY_STORE = ON
GO
ALTER DATABASE [FacturacionElectronica] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [FacturacionElectronica]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 30/01/2025 8:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Cedula] [varchar](15) NOT NULL,
	[Direccion] [varchar](100) NULL,
	[Telefono] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetallesFactura]    Script Date: 30/01/2025 8:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallesFactura](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FacturaID] [int] NOT NULL,
	[ProductoID] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Subtotal] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 30/01/2025 8:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClienteID] [int] NOT NULL,
	[Fecha] [datetime] NULL,
	[Total] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 30/01/2025 8:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[Stock] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Clientes] ON 

INSERT [dbo].[Clientes] ([ID], [Nombre], [Apellido], [Cedula], [Direccion], [Telefono], [Email]) VALUES (1, N'Wilthon', N'Baque', N'2300899255', N'La Concordia', N'0963680842', N'baquewilthon@gmail.com')
INSERT [dbo].[Clientes] ([ID], [Nombre], [Apellido], [Cedula], [Direccion], [Telefono], [Email]) VALUES (2, N'Solange', N'Vega', N'111111111', N'', N'', N'')
SET IDENTITY_INSERT [dbo].[Clientes] OFF
GO
SET IDENTITY_INSERT [dbo].[DetallesFactura] ON 

INSERT [dbo].[DetallesFactura] ([ID], [FacturaID], [ProductoID], [Cantidad], [Subtotal]) VALUES (1008, 1007, 1, 1, CAST(100.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallesFactura] ([ID], [FacturaID], [ProductoID], [Cantidad], [Subtotal]) VALUES (1009, 1008, 1, 1, CAST(100.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallesFactura] ([ID], [FacturaID], [ProductoID], [Cantidad], [Subtotal]) VALUES (1010, 1009, 1, 2, CAST(200.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallesFactura] ([ID], [FacturaID], [ProductoID], [Cantidad], [Subtotal]) VALUES (1011, 1009, 2, 4, CAST(4.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[DetallesFactura] OFF
GO
SET IDENTITY_INSERT [dbo].[Facturas] ON 

INSERT [dbo].[Facturas] ([ID], [ClienteID], [Fecha], [Total]) VALUES (1007, 2, CAST(N'2025-01-30T07:23:25.667' AS DateTime), CAST(100.00 AS Decimal(10, 2)))
INSERT [dbo].[Facturas] ([ID], [ClienteID], [Fecha], [Total]) VALUES (1008, 1, CAST(N'2025-01-30T07:39:46.860' AS DateTime), CAST(100.00 AS Decimal(10, 2)))
INSERT [dbo].[Facturas] ([ID], [ClienteID], [Fecha], [Total]) VALUES (1009, 2, CAST(N'2025-01-30T07:46:12.783' AS DateTime), CAST(204.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Facturas] OFF
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 

INSERT [dbo].[Productos] ([ID], [Nombre], [Precio], [Stock]) VALUES (1, N'Galleta', CAST(100.00 AS Decimal(10, 2)), 69)
INSERT [dbo].[Productos] ([ID], [Nombre], [Precio], [Stock]) VALUES (2, N'Coca Cola', CAST(1.00 AS Decimal(10, 2)), 25)
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Clientes__B4ADFE388BAD7D25]    Script Date: 30/01/2025 8:06:13 ******/
ALTER TABLE [dbo].[Clientes] ADD UNIQUE NONCLUSTERED 
(
	[Cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Facturas] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[DetallesFactura]  WITH CHECK ADD FOREIGN KEY([FacturaID])
REFERENCES [dbo].[Facturas] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetallesFactura]  WITH CHECK ADD FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD FOREIGN KEY([ClienteID])
REFERENCES [dbo].[Clientes] ([ID])
ON DELETE CASCADE
GO
/****** Object:  StoredProcedure [dbo].[ObtenerResumenFacturas]    Script Date: 30/01/2025 8:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerResumenFacturas]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        f.ID AS FacturaID,
        c.Nombre + ' ' + c.Apellido AS Cliente,
        c.Cedula,
        f.Total AS TotalFactura
    FROM Facturas f
    INNER JOIN Clientes c ON f.ClienteID = c.ID
    ORDER BY f.ID DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ReporteFacturasPorCliente]    Script Date: 30/01/2025 8:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ReporteFacturasPorCliente]
    @ClienteID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.Nombre + ' ' + c.Apellido AS ClienteNombre, 
        f.ID AS FacturaID, 
        f.Fecha, 
        f.Total, 
        p.Nombre AS Producto, 
        df.Cantidad, 
        df.Subtotal
    FROM Facturas f
    INNER JOIN Clientes c ON f.ClienteID = c.ID
    INNER JOIN DetallesFactura df ON f.ID = df.FacturaID
    INNER JOIN Productos p ON df.ProductoID = p.ID
    WHERE c.ID = @ClienteID
    ORDER BY f.Fecha DESC;
END;
GO
USE [master]
GO
ALTER DATABASE [FacturacionElectronica] SET  READ_WRITE 
GO
