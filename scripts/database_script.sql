CREATE DATABASE core_atlantida;

GO

-- Usa la base de datos core_atlantida

USE core_atlantida;

GO
-- Iniciar transacción

BEGIN TRANSACTION;
GO
-- Creación de la tabla tipo_cliente
CREATE TABLE tipo_cliente (
    tipo_cliente_id INT IDENTITY(1,1) PRIMARY KEY,
    descripcion NVARCHAR(50) NOT NULL
);
GO

-- Creación de la tabla tipo_identidad
CREATE TABLE tipo_identidad (
    tipo_identidad_id INT IDENTITY(1,1) PRIMARY KEY,
    descripcion NVARCHAR(50) NOT NULL
);
GO

-- Creación de la tabla beneficios_tarjeta
CREATE TABLE beneficios_tarjeta (
    beneficios_id INT IDENTITY(1,1) PRIMARY KEY,
    descripcion NVARCHAR(50) NOT NULL
);
GO

-- Creación de la tabla tipo_tarjeta
CREATE TABLE tipo_tarjeta (
    tipo_tarjeta_id INT IDENTITY(1,1) PRIMARY KEY,
    descripcion NVARCHAR(50) NOT NULL
);
GO

-- Creación de la tabla clientes
CREATE TABLE clientes (
    cliente_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    numero_identidad NVARCHAR(50) NOT NULL,
    tipo_identidad_id INT NOT NULL, -- FK a tipo_identidad
    tipo_cliente_id INT NOT NULL, -- FK a tipo_cliente
    direccion NVARCHAR(255),
    telefono NVARCHAR(20),
    email NVARCHAR(100),
    FOREIGN KEY (tipo_identidad_id) REFERENCES tipo_identidad(tipo_identidad_id),
    FOREIGN KEY (tipo_cliente_id) REFERENCES tipo_cliente(tipo_cliente_id)
);
GO

-- Creación de la tabla tarjetas
CREATE TABLE tarjetas (
    numero_tarjeta VARCHAR(16) PRIMARY KEY,
    cliente_id INT,
    limite_credito DECIMAL(18, 2),
    saldo_actual DECIMAL(18, 2),
    fecha_vencimiento DATE,
    fecha_apertura DATE,
    fecha_corte_inicio DATE,
    fecha_corte_fin DATE;
    saldo_disponible DECIMAL(10, 2) NOT NULL,
    tipo_tarjeta_id INT NOT NULL, -- FK a tipo_tarjeta
    beneficios_id INT NOT NULL, -- FK a beneficios_tarjeta
    FOREIGN KEY (cliente_id) REFERENCES clientes(cliente_id),
    FOREIGN KEY (tipo_tarjeta_id) REFERENCES tipo_tarjeta(tipo_tarjeta_id),
    FOREIGN KEY (beneficios_id) REFERENCES beneficios_tarjeta(beneficios_id)
);
GO

-- Creación de la tabla pagos
CREATE TABLE pagos (
    pago_id INT IDENTITY(1,1) PRIMARY KEY,
    numero_tarjeta VARCHAR(16) NOT NULL,
    fecha_pago DATE NOT NULL,  -- Cambiado de 'fecha' a 'fecha_pago'
    monto DECIMAL(18, 2) NOT NULL,
    fecha_corte_inicio DATE NOT NULL,
    fecha_corte_fin DATE NOT NULL,
    monto_disponible DECIMAL(18, 2) NOT NULL,
    monto_pagado DECIMAL(18, 2) NOT NULL,
    mora DECIMAL(18, 2) DEFAULT 0.00,
    FOREIGN KEY (numero_tarjeta) REFERENCES tarjetas(numero_tarjeta)
);
GO

-- Creación de la tabla transacciones
CREATE TABLE transacciones (
    transaccion_id INT IDENTITY(1,1) PRIMARY KEY,
    numero_tarjeta VARCHAR(16) NOT NULL,
    fecha DATE NOT NULL,
    descripcion NVARCHAR(255),
    monto DECIMAL(18, 2) NOT NULL,
    tipo_transaccion VARCHAR(50) NOT NULL, -- Ej: 'compra', 'pago', 'cargo', etc.
    categoria NVARCHAR(50), -- Categoría de la transacción (ej: 'alimentación', 'entretenimiento', etc.)
    FOREIGN KEY (numero_tarjeta) REFERENCES tarjetas(numero_tarjeta)
);

GO

-- Creación de la tabla logs para registrar errores y eventos
CREATE TABLE logs (
    log_id INT IDENTITY(1,1) PRIMARY KEY,
    error_date DATETIME NOT NULL DEFAULT GETDATE(),
    error_message NVARCHAR(4000) NOT NULL,
    error_number INT NOT NULL,
    originating_component NVARCHAR(255) NOT NULL,
    additional_info NVARCHAR(4000) NULL
);
GO

-- Creación de la tabla configuraciones para almacenar parámetros configurables
CREATE TABLE configuraciones (
    configuracion_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50),
    valor DECIMAL(18, 2)
);
GO

-- Insertar valores iniciales en la tabla tipo_cliente
INSERT INTO tipo_cliente (descripcion) VALUES ('Natural');
INSERT INTO tipo_cliente (descripcion) VALUES ('Jurídico');
GO

-- Insertar valores iniciales en la tabla tipo_identidad
INSERT INTO tipo_identidad (descripcion) VALUES ('DNI');
INSERT INTO tipo_identidad (descripcion) VALUES ('Pasaporte');
GO

-- Insertar valores iniciales en la tabla tipo_tarjeta
INSERT INTO tipo_tarjeta (descripcion) VALUES ('Débito');
INSERT INTO tipo_tarjeta (descripcion) VALUES ('Crédito');
GO

-- Insertar valores iniciales en la tabla beneficios_tarjeta
INSERT INTO beneficios_tarjeta (descripcion) VALUES ('Millas');
INSERT INTO beneficios_tarjeta (descripcion) VALUES ('Cashback');
GO

-- Insertar valores iniciales en la tabla configuraciones
INSERT INTO configuraciones (nombre, valor) VALUES ('Porcentaje_Interes', 0.25);
INSERT INTO configuraciones (nombre, valor) VALUES ('Porcentaje_Cuota_Minima', 0.05);
GO
commit;


