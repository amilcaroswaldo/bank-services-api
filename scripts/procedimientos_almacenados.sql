USE core_atlantida;
GO

CREATE PROCEDURE sp_RegistrarLog
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
go
CREATE PROCEDURE GenerarNumeroTarjeta
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
END
-- DROP SCHEMA PackageEstadoCuenta;
GO
-- Crear esquema para el package
CREATE SCHEMA PackageEstadoCuenta;
go
CREATE FUNCTION PackageEstadoCuenta.fn_CalcularInteresBonificable (@v_numero_tarjeta VARCHAR(16))
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
go
CREATE FUNCTION PackageEstadoCuenta.fn_CalcularMontoTotal (@v_numero_tarjeta VARCHAR(16), @fch_inicio_compras date, @fch_fin_compras date)
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
CREATE FUNCTION PackageEstadoCuenta.fn_CalcularCuotaMinima (@v_numero_tarjeta VARCHAR(16))
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
CREATE FUNCTION PackageEstadoCuenta.fn_CalcularMontoContadoConIntereses (@v_numero_tarjeta VARCHAR(16))
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
CREATE PROCEDURE PackageEstadoCuenta.sp_ObtenerEstadoCuenta
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

        SELECT @saldo_actual  = saldo_actual , 
               @Saldo_Disponible = Saldo_Disponible,
               @FCH_CORTE_INICIO = fecha_corte_inicio,
               @FCH_CORTE_FIN = fecha_corte_fin 
        FROM Tarjetas
        WHERE Numero_Tarjeta = @Numero_Tarjeta;

        SET @InteresBonificable = PackageEstadoCuenta.fn_CalcularInteresBonificable(@Numero_Tarjeta);
        SET @CuotaMinima = PackageEstadoCuenta.fn_CalcularCuotaMinima(@Numero_Tarjeta );
        SET @MontoTotal = PackageEstadoCuenta.fn_CalcularMontoTotal(@Numero_Tarjeta,@FCH_CORTE_INICIO,@FCH_CORTE_FIN);
        SET @MontoContadoConIntereses = PackageEstadoCuenta.fn_CalcularMontoContadoConIntereses(@Numero_Tarjeta);

        SELECT @saldo_actual  AS SaldoActual , 
               @Saldo_Disponible AS SaldoDisponible, 
               @InteresBonificable AS InteresBonificable,
               @CuotaMinima AS CuotaMinima,
               @MontoTotal AS MontoTotal,
               @MontoContadoConIntereses AS MontoContadoConIntereses;
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
CREATE PROCEDURE PackageEstadoCuenta.sp_RealizarPago
    @Numero_Tarjeta NVARCHAR(16),
    @Monto DECIMAL(18, 2)
AS
BEGIN
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
CREATE PROCEDURE PackageEstadoCuenta.sp_RegistrarTransaccion
    @Numero_Tarjeta VARCHAR(16),
    @Fecha DATE,
    @Descripcion NVARCHAR(255),
    @Monto DECIMAL(18, 2),
    @Tipo_Transaccion VARCHAR(50),
    @Categoria NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorNumber INT;
    BEGIN TRY
        -- Validar que los campos requeridos no sean NULL
        IF @Numero_Tarjeta IS NULL OR @Fecha IS NULL OR @Monto IS NULL OR @Tipo_Transaccion IS NULL
        BEGIN
            set @ErrorMessage = 'Los campos Numero_Tarjeta, Fecha, Monto y Tipo_Transaccion no pueden ser NULL.';
            set @ErrorNumber = 0;
            RAISERROR(@ErrorMessage, 16, 1);
            EXEC sp_RegistrarLog @ErrorMessage, @ErrorNumber, 'PackageEstadoCuenta.sp_RegistrarTransaccion';
            RETURN;
        END

        -- Insertar la transacción en la tabla transacciones
        INSERT INTO transacciones (numero_tarjeta, fecha, descripcion, monto, tipo_transaccion, categoria)
        VALUES (@Numero_Tarjeta, @Fecha, @Descripcion, @Monto, @Tipo_Transaccion, @Categoria);
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
