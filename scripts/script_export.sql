USE [master]
GO
/****** Object:  Database [core_atlantida]    Script Date: 8/4/2024 8:18:57 AM ******/
CREATE DATABASE [core_atlantida]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'core_atlantida', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLSERVERT\MSSQL\DATA\core_atlantida.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'core_atlantida_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLSERVERT\MSSQL\DATA\core_atlantida_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [core_atlantida] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [core_atlantida].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [core_atlantida] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [core_atlantida] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [core_atlantida] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [core_atlantida] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [core_atlantida] SET ARITHABORT OFF 
GO
ALTER DATABASE [core_atlantida] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [core_atlantida] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [core_atlantida] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [core_atlantida] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [core_atlantida] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [core_atlantida] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [core_atlantida] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [core_atlantida] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [core_atlantida] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [core_atlantida] SET  ENABLE_BROKER 
GO
ALTER DATABASE [core_atlantida] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [core_atlantida] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [core_atlantida] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [core_atlantida] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [core_atlantida] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [core_atlantida] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [core_atlantida] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [core_atlantida] SET RECOVERY FULL 
GO
ALTER DATABASE [core_atlantida] SET  MULTI_USER 
GO
ALTER DATABASE [core_atlantida] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [core_atlantida] SET DB_CHAINING OFF 
GO
ALTER DATABASE [core_atlantida] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [core_atlantida] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [core_atlantida] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [core_atlantida] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'core_atlantida', N'ON'
GO
ALTER DATABASE [core_atlantida] SET QUERY_STORE = ON
GO
ALTER DATABASE [core_atlantida] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [core_atlantida]
GO
/****** Object:  Schema [PackageEstadoCuenta]    Script Date: 8/4/2024 8:18:57 AM ******/
CREATE SCHEMA [PackageEstadoCuenta]
GO
/****** Object:  UserDefinedFunction [PackageEstadoCuenta].[fn_CalcularCuotaMinima]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [PackageEstadoCuenta].[fn_CalcularCuotaMinima] (@v_numero_tarjeta VARCHAR(16))
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @CuotaMinima DECIMAL(18, 2);
    DECLARE @PorcentajeCuotaMinima DECIMAL(18, 2);
	DECLARE @saldo_actual DECIMAL(18, 2);
    SET @PorcentajeCuotaMinima = (SELECT Valor FROM Configuraciones WHERE nombre = 'Porcentaje_Cuota_Minima');
	SET @saldo_actual = (SELECT saldo_actual FROM tarjetas WHERE numero_tarjeta = @v_numero_tarjeta);   
    SET @CuotaMinima = @saldo_actual  * @PorcentajeCuotaMinima / 100;

    RETURN @CuotaMinima;
END
GO
/****** Object:  UserDefinedFunction [PackageEstadoCuenta].[fn_CalcularInteresBonificable]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [PackageEstadoCuenta].[fn_CalcularInteresBonificable] (@v_numero_tarjeta VARCHAR(16))
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @InteresBonificable DECIMAL(18, 2);
    DECLARE @PorcentajeInteresConfigurable DECIMAL(18, 2);
	DECLARE @saldo_actual DECIMAL(18, 2);
	SET @saldo_actual = (SELECT saldo_actual FROM tarjetas WHERE numero_tarjeta = @v_numero_tarjeta);
    SET @PorcentajeInteresConfigurable = (SELECT valor FROM Configuraciones WHERE nombre = 'Porcentaje_Interes');
    SET @InteresBonificable = @saldo_actual  * @PorcentajeInteresConfigurable / 100;

    RETURN @InteresBonificable;
END
GO
/****** Object:  UserDefinedFunction [PackageEstadoCuenta].[fn_CalcularMontoContadoConIntereses]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [PackageEstadoCuenta].[fn_CalcularMontoContadoConIntereses] (@v_numero_tarjeta VARCHAR(16))
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @MontoContadoConIntereses DECIMAL(18, 2);
	DECLARE @saldo_actual DECIMAL(18, 2);
	DECLARE @InteresBonificable DECIMAL(18, 2);
	SET @saldo_actual = (SELECT saldo_actual FROM tarjetas WHERE numero_tarjeta = @v_numero_tarjeta);
	SET @InteresBonificable = PackageEstadoCuenta.fn_CalcularInteresBonificable(@v_numero_tarjeta);
    SET @MontoContadoConIntereses = @saldo_actual  + @InteresBonificable;

    RETURN @MontoContadoConIntereses;
END
GO
/****** Object:  UserDefinedFunction [PackageEstadoCuenta].[fn_CalcularMontoTotal]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [PackageEstadoCuenta].[fn_CalcularMontoTotal] (@v_numero_tarjeta VARCHAR(16), @fch_inicio_compras date, @fch_fin_compras date)
RETURNS DECIMAL(18, 2)
AS
BEGIN
	DECLARE @v_monto_total DECIMAL(18, 2);
	SET @v_monto_total = (SELECT sum(monto) FROM transacciones
						 where numero_tarjeta=@v_numero_tarjeta and  
						 fecha BETWEEN @fch_inicio_compras and @fch_fin_compras);
    RETURN @v_monto_total ;
END
GO
/****** Object:  Table [dbo].[beneficios_tarjeta]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[beneficios_tarjeta](
	[beneficios_id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[beneficios_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientes](
	[cliente_id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[numero_identidad] [nvarchar](50) NOT NULL,
	[tipo_identidad_id] [int] NOT NULL,
	[tipo_cliente_id] [int] NOT NULL,
	[direccion] [nvarchar](255) NULL,
	[telefono] [nvarchar](20) NULL,
	[email] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[cliente_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[configuraciones]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[configuraciones](
	[configuracion_id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[valor] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[configuracion_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[logs]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[logs](
	[log_id] [int] IDENTITY(1,1) NOT NULL,
	[error_date] [datetime] NOT NULL,
	[error_message] [nvarchar](4000) NOT NULL,
	[error_number] [int] NOT NULL,
	[originating_component] [nvarchar](255) NOT NULL,
	[additional_info] [nvarchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pagos]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pagos](
	[pago_id] [int] IDENTITY(1,1) NOT NULL,
	[numero_tarjeta] [varchar](16) NOT NULL,
	[fecha_pago] [date] NOT NULL,
	[monto] [decimal](18, 2) NOT NULL,
	[fecha_corte_inicio] [date] NOT NULL,
	[fecha_corte_fin] [date] NOT NULL,
	[monto_disponible] [decimal](18, 2) NOT NULL,
	[monto_pagado] [decimal](18, 2) NOT NULL,
	[mora] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[pago_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tarjetas]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tarjetas](
	[numero_tarjeta] [varchar](16) NOT NULL,
	[cliente_id] [int] NULL,
	[limite_credito] [decimal](18, 2) NULL,
	[saldo_actual] [decimal](18, 2) NULL,
	[fecha_vencimiento] [date] NULL,
	[saldo_disponible] [decimal](10, 2) NOT NULL,
	[tipo_tarjeta_id] [int] NOT NULL,
	[beneficios_id] [int] NOT NULL,
	[fecha_apertura] [date] NULL,
	[fecha_corte_inicio] [date] NULL,
	[fecha_corte_fin] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[numero_tarjeta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipo_cliente]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipo_cliente](
	[tipo_cliente_id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tipo_cliente_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipo_identidad]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipo_identidad](
	[tipo_identidad_id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tipo_identidad_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipo_tarjeta]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipo_tarjeta](
	[tipo_tarjeta_id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tipo_tarjeta_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transacciones]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transacciones](
	[transaccion_id] [int] IDENTITY(1,1) NOT NULL,
	[numero_tarjeta] [varchar](16) NOT NULL,
	[fecha] [date] NOT NULL,
	[descripcion] [nvarchar](255) NULL,
	[monto] [decimal](18, 2) NOT NULL,
	[tipo_transaccion] [varchar](50) NOT NULL,
	[categoria] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[transaccion_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[logs] ADD  DEFAULT (getdate()) FOR [error_date]
GO
ALTER TABLE [dbo].[pagos] ADD  DEFAULT ((0.00)) FOR [mora]
GO
ALTER TABLE [dbo].[clientes]  WITH CHECK ADD FOREIGN KEY([tipo_cliente_id])
REFERENCES [dbo].[tipo_cliente] ([tipo_cliente_id])
GO
ALTER TABLE [dbo].[clientes]  WITH CHECK ADD FOREIGN KEY([tipo_identidad_id])
REFERENCES [dbo].[tipo_identidad] ([tipo_identidad_id])
GO
ALTER TABLE [dbo].[pagos]  WITH CHECK ADD FOREIGN KEY([numero_tarjeta])
REFERENCES [dbo].[tarjetas] ([numero_tarjeta])
GO
ALTER TABLE [dbo].[tarjetas]  WITH CHECK ADD FOREIGN KEY([beneficios_id])
REFERENCES [dbo].[beneficios_tarjeta] ([beneficios_id])
GO
ALTER TABLE [dbo].[tarjetas]  WITH CHECK ADD FOREIGN KEY([cliente_id])
REFERENCES [dbo].[clientes] ([cliente_id])
GO
ALTER TABLE [dbo].[tarjetas]  WITH CHECK ADD FOREIGN KEY([tipo_tarjeta_id])
REFERENCES [dbo].[tipo_tarjeta] ([tipo_tarjeta_id])
GO
ALTER TABLE [dbo].[transacciones]  WITH CHECK ADD FOREIGN KEY([numero_tarjeta])
REFERENCES [dbo].[tarjetas] ([numero_tarjeta])
GO
/****** Object:  StoredProcedure [dbo].[GenerarNumeroTarjeta]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerarNumeroTarjeta]
    @NumeroTarjeta VARCHAR(16) OUTPUT
AS
BEGIN
    DECLARE @NumeroUnico BIT = 0;

    WHILE @NumeroUnico = 0
    BEGIN
        -- Generar un número de tarjeta aleatorio
        SET @NumeroTarjeta = 
            FORMAT((ABS(CHECKSUM(NEWID())) % 10000), '0000') + '-' +
            FORMAT((ABS(CHECKSUM(NEWID())) % 10000), '0000') + '-' +
            FORMAT((ABS(CHECKSUM(NEWID())) % 10000), '0000') + '-' +
            FORMAT((ABS(CHECKSUM(NEWID())) % 10000), '0000');

        -- Verificar si el número de tarjeta ya existe
        IF NOT EXISTS (SELECT 1 FROM Tarjetas WHERE Numero_Tarjeta = @NumeroTarjeta)
        BEGIN
            SET @NumeroUnico = 1; -- Número único encontrado
        END
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarLog]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_RegistrarLog]
    @ErrorMessage NVARCHAR(4000),
    @ErrorNumber INT,
    @OriginatingComponent NVARCHAR(255),
    @AdditionalInfo NVARCHAR(4000) = NULL
AS
BEGIN
	/*
	 * error_number
	 * 0-error database
	 * 1-error application api(CREDIT_CARD_SERVICE)
	 */
    BEGIN TRY
        INSERT INTO logs (error_message, error_number, originating_component, additional_info)
        VALUES (@ErrorMessage, @ErrorNumber, @OriginatingComponent, @AdditionalInfo);
    END TRY
    BEGIN CATCH
        PRINT 'Error al registrar el log.';
    END CATCH
END
GO
/****** Object:  StoredProcedure [PackageEstadoCuenta].[sp_ObtenerEstadoCuenta]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [PackageEstadoCuenta].[sp_ObtenerEstadoCuenta]
    @Numero_Tarjeta NVARCHAR(16)
AS
BEGIN
    BEGIN TRY
        DECLARE @saldo_actual  DECIMAL(18, 2);
        DECLARE @Saldo_Disponible DECIMAL(18, 2);
        DECLARE @InteresBonificable DECIMAL(18, 2);
        DECLARE @CuotaMinima DECIMAL(18, 2);
        DECLARE @MontoTotal DECIMAL(18, 2);
        DECLARE @MontoContadoConIntereses DECIMAL(18, 2);
        DECLARE @FCH_CORTE_INICIO DATE;
        DECLARE @FCH_CORTE_FIN DATE;
		DECLARE @Monto_total_Mes_actual_Anterior  DECIMAL(18, 2);
	
        SELECT @saldo_actual  = saldo_actual , 
               @Saldo_Disponible = Saldo_Disponible,
               @FCH_CORTE_INICIO = fecha_corte_inicio,
               @FCH_CORTE_FIN = fecha_corte_fin 
        FROM Tarjetas
        WHERE Numero_Tarjeta = @Numero_Tarjeta;
       
       SELECT @Monto_total_Mes_actual_Anterior= SUM(monto) from transacciones t 
       where numero_tarjeta = @Numero_Tarjeta 
	   and t.fecha BETWEEN DATEADD(DAY, 1, EOMONTH(GETDATE(), -1)) AND CONVERT(DATE,GETDATE());

        SET @InteresBonificable = PackageEstadoCuenta.fn_CalcularInteresBonificable(@Numero_Tarjeta);
        SET @CuotaMinima = PackageEstadoCuenta.fn_CalcularCuotaMinima(@Numero_Tarjeta );
        SET @MontoTotal = PackageEstadoCuenta.fn_CalcularMontoTotal(@Numero_Tarjeta,@FCH_CORTE_INICIO,@FCH_CORTE_FIN);
        SET @MontoContadoConIntereses = PackageEstadoCuenta.fn_CalcularMontoContadoConIntereses(@Numero_Tarjeta);

        SELECT @saldo_actual  AS SaldoActual , 
               @Saldo_Disponible AS SaldoDisponible, 
               @InteresBonificable AS InteresBonificable,
               @CuotaMinima AS CuotaMinima,
               @MontoTotal AS MontoTotal,
               @MontoContadoConIntereses AS MontoContadoConIntereses,
               @Monto_total_Mes_actual_Anterior AS MontoTotalMesActualAnterior;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorNumber INT;
        SET @ErrorMessage = ERROR_MESSAGE();
        SET @ErrorNumber = ERROR_NUMBER();
        EXEC sp_RegistrarLog @ErrorMessage, @ErrorNumber, 'PackageEstadoCuenta.sp_ObtenerEstadoCuenta';
    END CATCH
END
GO
/****** Object:  StoredProcedure [PackageEstadoCuenta].[sp_RealizarPago]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [PackageEstadoCuenta].[sp_RealizarPago]
    @Numero_Tarjeta NVARCHAR(16),
    @Monto DECIMAL(18, 2)
AS
BEGIN
	BEGIN TRANSACTION;
	DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorNumber INT;
    BEGIN TRY
        -- Validar que los campos requeridos no sean NULL
        IF @Numero_Tarjeta IS NULL OR @Monto IS NULL
        BEGIN
            set @ErrorMessage = 'Los campos Numero_Tarjeta Y MONTO NO PUEDE SER ser NULL.';
            set @ErrorNumber = 0;
            RAISERROR(@ErrorMessage, 16, 1);
            EXEC sp_RegistrarLog @ErrorMessage, @ErrorNumber, 'PackageEstadoCuenta.sp_RegistrarTransaccion';
            RETURN;
        END    	
    	DECLARE @MONTO_DEBIDO DECIMAL(18, 2);
    	DECLARE @MONTO_DISP DECIMAL(18, 2);
    	DECLARE @MORA DECIMAL(18, 2);
    	DECLARE @FCH_CORTE_I DATE;
    	DECLARE @FCH_CORTE_F DATE;
    
    	SELECT @MONTO_DEBIDO = saldo_actual, @MONTO_DISP = saldo_disponible, 
    			@FCH_CORTE_I=fecha_corte_inicio , @FCH_CORTE_F=fecha_corte_fin 
    	FROM tarjetas WHERE numero_tarjeta = @Numero_Tarjeta;
       	SET @MORA = @MONTO_DEBIDO - @MONTO;
        INSERT INTO core_atlantida.dbo.pagos
		(numero_tarjeta, fecha_pago, monto, fecha_corte_inicio, fecha_corte_fin, monto_disponible, monto_pagado, mora)
		VALUES(@Numero_Tarjeta, GETDATE(), @MONTO_DEBIDO, @FCH_CORTE_I, @FCH_CORTE_F, @MONTO_DISP, @Monto, @MORA);

        UPDATE Tarjetas
        SET saldo_actual  = @MORA
        WHERE Numero_Tarjeta = @Numero_Tarjeta;
		commit;
--        EXEC sp_RegistrarLog 'Pago registrado', 0, 'PackageEstadoCuenta.sp_RealizarPago';
    END TRY
    BEGIN CATCH
        SET @ErrorMessage = ERROR_MESSAGE();
        SET @ErrorNumber = ERROR_NUMBER();
        EXEC sp_RegistrarLog @ErrorMessage, @ErrorNumber, 'PackageEstadoCuenta.sp_RealizarPago';
    END CATCH
END
GO
/****** Object:  StoredProcedure [PackageEstadoCuenta].[sp_RegistrarTransaccion]    Script Date: 8/4/2024 8:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [PackageEstadoCuenta].[sp_RegistrarTransaccion]
    @Numero_Tarjeta VARCHAR(16),
    @Descripcion NVARCHAR(255),
    @Monto DECIMAL(18, 2),
    @Tipo_Transaccion VARCHAR(50),
    @Categoria NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
	BEGIN TRANSACTION;
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorNumber INT;
    BEGIN TRY
        -- Validar que los campos requeridos no sean NULL
        IF @Numero_Tarjeta IS NULL  OR @Monto IS NULL OR @Tipo_Transaccion IS NULL
        BEGIN
            set @ErrorMessage = 'Los campos Numero_Tarjeta, Monto y Tipo_Transaccion no pueden ser NULL.';
            set @ErrorNumber = 0;
            RAISERROR(@ErrorMessage, 16, 1);
            EXEC sp_RegistrarLog @ErrorMessage, @ErrorNumber, 'PackageEstadoCuenta.sp_RegistrarTransaccion';
            RETURN;
        END

        -- Insertar la transacción en la tabla transacciones
        INSERT INTO transacciones (numero_tarjeta, fecha, descripcion, monto, tipo_transaccion, categoria)
        VALUES (@Numero_Tarjeta, GETDATE(), @Descripcion, @Monto, @Tipo_Transaccion, @Categoria);
       --actualiza saldos
        UPDATE tarjetas
		SET saldo_actual=saldo_actual + @Monto, saldo_disponible=saldo_disponible - @Monto
		WHERE numero_tarjeta=@Numero_Tarjeta;
		commit;
        -- Ejecutar el registro de log (si se requiere)
--        EXEC sp_RegistrarLog 'Transacción registrada', 0, 'PackageEstadoCuenta.sp_RegistrarTransaccion';
    END TRY
    BEGIN CATCH
        SET @ErrorMessage = ERROR_MESSAGE();
        SET @ErrorNumber = ERROR_NUMBER();
        EXEC sp_RegistrarLog @ErrorMessage, @ErrorNumber, 'PackageEstadoCuenta.sp_RegistrarTransaccion';
        THROW;
    END CATCH
END
GO
USE [master]
GO
ALTER DATABASE [core_atlantida] SET  READ_WRITE 
GO
