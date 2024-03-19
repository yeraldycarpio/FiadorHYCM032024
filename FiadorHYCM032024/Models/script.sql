CREATE DATABASE FiadorHYCM032024

USE FiadorHYCM032024

CREATE TABLE Fiador(
Id INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(100) NOT NULL,
Direccion VARCHAR(200) NOT NULL,
Telefono NCHAR (9) ,
Correo Varchar(100) NOT NULL,
Ocupacion Varchar (100) NOT NULL,
IngresoMensual Decimal (10,2) Not null,
FechaNacimiento DATE NOT NULL,
);

CREATE TABLE ReferenciasFamiliares(
Id INT PRIMARY KEY IDENTITY (1,1),
IdFiador INT NOT NULL,
Nombre VARCHAR(100) NOT NULL,
Relacion Varchar (100) Not null,
Telefono NCHAR(9) ,
Direccion Varchar (200) NOT NULL,
FOREIGN KEY (IdFiador) REFERENCES Fiador(Id) ON DELETE CASCADE
);