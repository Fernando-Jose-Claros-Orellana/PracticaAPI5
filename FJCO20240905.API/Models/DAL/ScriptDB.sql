-- Crear la base de datos
CREATE DATABASE dbFJCO20240905;

-- Usar la base de datos creada
USE dbFJCO20240905;

-- Crear la tabla
CREATE TABLE ProductsFJCO (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(500),
    Precio DECIMAL(10, 2) NOT NULL
);