# PruebaTecnicaUruIT
Este repositorio es para la prueba técnica de UruIT

Hola en este repositorio he cargado la prueba técnica propuesta para el proceso de selección.

Este se realizó en .Net como solicitaron, solo deben abrir el proyecto en visual studio. Antes de ejecturlo deben crear en SQL server
la base de datos que el proyecto requiere para funcionar y en caso de ser necesario,
modificar el string de conexión en la clase index.cs según la configuración de su SQL Server. Agradezco de antemano la oportunidad
brindada.

El script de la base de datos es el que dejo a continuación:

-----------------------------------------------------o------------------o-------------------------------------------
createdata base dbPruebaTecnica

create table tblPlayer(
Player_ID int not null  primary key identity,
Gamer_Name varchar(50))

create table tblGames(
Game_ID int not null  primary key identity,
Winner int foreign key references tblPlayer(Player_ID))

create proc SavePlayer
@NamePlayer varchar(50)
as
begin
	insert into tblPlayer(Gamer_Name) values (@NamePlayer)
	select SCOPE_IDENTITY()
end

create proc InsertWinner
@PlayerID int
as
begin
	insert into tblGames(Winner) values (@PlayerID)
end

create proc GetWinners
as
begin
select Gamer_Name as "Player's Name" from
tblGames  inner join tblPlayer  on Winner=Player_ID
end
