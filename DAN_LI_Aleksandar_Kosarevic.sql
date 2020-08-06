--If database doesnt exist it is automatically created.
IF DB_ID('Zadatak_1') IS NULL
CREATE DATABASE Zadatak_1
GO
--Newly created database is set to be in use.
USE Zadatak_1
--All tables are reseted clean.
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblSong')
drop table tblSong
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblUser')
drop table tblUser

create table tblUser
(
UserID int primary key IDENTITY(1,1),
Username varchar(50),
Password varchar(50)
)

create table tblSong
(
SongID int primary key IDENTITY(1,1),
Title varchar(50),
Author varchar(50),
Length time,
UserID int foreign key references tblUser(UserID) not null
)

insert into tblUser values ('a','a');
insert into tblSong values ('song 1', 'author 1', '00:01:00', 1);
insert into tblSong values ('song 2', 'author 2', '00:02:00', 1);
insert into tblSong values ('song 3', 'author 3', '00:03:00', 1);

select * from tblSong;
select * from tblUser;