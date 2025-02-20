USE [master]
GO
/****** Object:  Database [GestionEmpleados]    Script Date: 06/02/2025 11:27:33 ******/
CREATE DATABASE [GestionEmpleados]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GestionEmpleados', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\GestionEmpleados.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GestionEmpleados_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\GestionEmpleados_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [GestionEmpleados] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GestionEmpleados].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GestionEmpleados] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GestionEmpleados] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GestionEmpleados] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GestionEmpleados] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GestionEmpleados] SET ARITHABORT OFF 
GO
ALTER DATABASE [GestionEmpleados] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GestionEmpleados] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GestionEmpleados] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GestionEmpleados] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GestionEmpleados] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GestionEmpleados] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GestionEmpleados] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GestionEmpleados] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GestionEmpleados] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GestionEmpleados] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GestionEmpleados] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GestionEmpleados] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GestionEmpleados] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GestionEmpleados] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GestionEmpleados] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GestionEmpleados] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GestionEmpleados] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GestionEmpleados] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GestionEmpleados] SET  MULTI_USER 
GO
ALTER DATABASE [GestionEmpleados] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GestionEmpleados] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GestionEmpleados] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GestionEmpleados] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GestionEmpleados] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GestionEmpleados] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [GestionEmpleados] SET QUERY_STORE = ON
GO
ALTER DATABASE [GestionEmpleados] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [GestionEmpleados]
GO
/****** Object:  Table [dbo].[Cargo]    Script Date: 06/02/2025 11:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cargo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 06/02/2025 11:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departamento](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Ubicacion] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 06/02/2025 11:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Apellido] [nvarchar](100) NOT NULL,
	[CargoID] [int] NOT NULL,
	[DepartamentoID] [int] NOT NULL,
	[Salario] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cargo] ON 

INSERT [dbo].[Cargo] ([ID], [Nombre], [Descripcion]) VALUES (1, N'Desarrollador', N'Encargado de desarrollar Software')
INSERT [dbo].[Cargo] ([ID], [Nombre], [Descripcion]) VALUES (2, N'Contador', N'Maneja la contabilidad de la empresa')
INSERT [dbo].[Cargo] ([ID], [Nombre], [Descripcion]) VALUES (3, N'Gerente', N'Supervisa el departamento de trabajo')
SET IDENTITY_INSERT [dbo].[Cargo] OFF
GO
SET IDENTITY_INSERT [dbo].[Departamento] ON 

INSERT [dbo].[Departamento] ([ID], [Nombre], [Ubicacion]) VALUES (1, N'TI', N'Edificio A, Piso 3')
INSERT [dbo].[Departamento] ([ID], [Nombre], [Ubicacion]) VALUES (2, N'Finanzas', N'Edificio B, Piso 2')
INSERT [dbo].[Departamento] ([ID], [Nombre], [Ubicacion]) VALUES (3, N'RRHH', N'Edificio C, Piso 1')
SET IDENTITY_INSERT [dbo].[Departamento] OFF
GO
SET IDENTITY_INSERT [dbo].[Empleado] ON 

INSERT [dbo].[Empleado] ([ID], [Nombre], [Apellido], [CargoID], [DepartamentoID], [Salario]) VALUES (4, N'Baque', N'Wilthon', 1, 1, CAST(5000.10 AS Decimal(18, 2)))
INSERT [dbo].[Empleado] ([ID], [Nombre], [Apellido], [CargoID], [DepartamentoID], [Salario]) VALUES (6, N'Junior', N'Moreira', 3, 3, CAST(10000.60 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Empleado] OFF
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD FOREIGN KEY([CargoID])
REFERENCES [dbo].[Cargo] ([ID])
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD FOREIGN KEY([DepartamentoID])
REFERENCES [dbo].[Departamento] ([ID])
GO
USE [master]
GO
ALTER DATABASE [GestionEmpleados] SET  READ_WRITE 
GO
