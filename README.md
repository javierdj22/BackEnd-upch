# Backend Upch

API backend desarrollada con **.NET 6** para la gestión de carros. Esta API es compatible con **AWS Lambda** usando `Amazon.Lambda.AspNetCoreServer`, soporta **CRUD** sobre la tabla de carros y proporciona documentación interactiva mediante **Swagger**.

---

## Tecnologías

- **.NET 6 / ASP.NET Core Web API**  
- **AWS Lambda** (hosting serverless)  
- **Entity Framework Core** (ORM para SQL Server)  
- **MediatR** (patrón CQRS opcional)  
- **Newtonsoft.Json** (serialización JSON)  
- **Swagger / Swashbuckle** (documentación y pruebas de API)  
- **SQL Server** (base de datos relacional)  

---

## Estructura del ProyectoBack/
├── BackEndUpch/                # Proyecto principal MVC
├── create_cars_sqlserver.sql   # Script para crear la tabla de carros
├── appsettings.json            # Configuración de la app
├── Program.cs                  # Entry point de la aplicación
└── Startup.cs                  # Configuración de servicios y middlewares


---

## Configuración de la Base de Datos

1. Crear una base de datos en **SQL Server**.  
2. Ejecutar el script `create_cars_sqlserver.sql` que contiene la tabla de carros:  

Sql

CREATE TABLE Cars (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Brand NVARCHAR(100),
    Model NVARCHAR(100),
    Year INT,
    Type NVARCHAR(50),
    Seats INT,
    Color NVARCHAR(50),
    Notes NVARCHAR(MAX)
);

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=TU_DB;User Id=USUARIO;Password=CONTRASEÑA;"
  }
}


---
## Actualizar la función AWS Lambda

Este proyecto incluye un script de PowerShell (Update-Lambda.ps1) para actualizar la función Lambda en AWS de forma rápida.

---
## Requisitos

PowerShell
AWS CLI configurado con tus credenciales:

aws configure

---
## Permisos suficientes en IAM para actualizar funciones Lambda.

Pasos para ejecutar el script
Abre PowerShell.

---
## Navega al directorio del proyecto: ROOTEAR AL DIRECCORIO DEL PROYECTO

cd E:\GIT\BackEnd-upch

---
## Si es necesario, permite la ejecución de scripts en la sesión actual:

Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process

---
## Ejecuta el script para actualizar la Lambda:

.\Update-Lambda.ps1