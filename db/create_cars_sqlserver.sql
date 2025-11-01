-- SQL Server script to create database and Cars table
CREATE DATABASE CarsDb;
GO
USE CarsDb;
GO
CREATE TABLE Cars (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Brand NVARCHAR(100) NOT NULL,
  Model NVARCHAR(100) NOT NULL,
  Year INT NULL,
  Type NVARCHAR(50) NULL,
  Seats INT NULL,
  Color NVARCHAR(50) NULL,
  Notes NVARCHAR(200) NULL
);
GO
INSERT INTO Cars (Brand, Model, Year, Type, Seats, Color, Notes) VALUES
('Toyota','Corolla',2020,'Sedan',5,'White','Sample');
GO
