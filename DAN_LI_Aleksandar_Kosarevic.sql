--If database doesnt exist it is automatically created.
IF DB_ID('Zadatak_1') IS NULL
CREATE DATABASE Zadatak_1
GO
--Newly created database is set to be in use.
USE Zadatak_1
--All tables are reseted clean.
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblPatient')
drop table tblPatient
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblDoctor')
drop table tblDoctor

create table tblDoctor
(
DoctorID int primary key IDENTITY(1,1),
FirstName varchar(50),
LastName varchar(50),
JMBG varchar(13),
Username varchar(50),
Password varchar(50),
Account varchar(50)
)

create table tblPatient
(
PatientID int primary key IDENTITY(1,1),
FirstName varchar(50),
LastName varchar(50),
JMBG varchar(13),
Username varchar(50),
Password varchar(50),
CardNumber varchar(50),
DoctorID int
)