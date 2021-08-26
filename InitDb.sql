create database "EmployeeManagementDB";

GO

use "EmployeeManagementDB";

create table LegalForm (
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Title NVARCHAR(50),
	IsDeleted BIT
);
insert into LegalForm (Title) values (N'ООО');
insert into LegalForm (Title) values (N'ОАО');
insert into LegalForm (Title) values (N'ЗАО');

create table Position (
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Title NVARCHAR(50),
	IsDeleted BIT
);
insert into Position (Title) values (N'Разработчик');
insert into Position (Title) values (N'Тестировщик');
insert into Position (Title) values (N'Бизнес-аналитик');
insert into Position (Title) values (N'Менеджер');

create table Company (
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Title NVARCHAR(50),
	LegalFormId INT FOREIGN KEY REFERENCES LegalForm,
	IsDeleted BIT
);
insert into Company (Title, LegalFormId) values ('Quilix', 2);
insert into Company (Title, LegalFormId) values ('<epam>', 2);
insert into Company (Title, LegalFormId) values ('IBA', 2);
insert into Company (Title, LegalFormId) values ('ISSoft', 1);
insert into Company (Title, LegalFormId) values ('IntexSoft', 3);
insert into Company (Title, LegalFormId) values ('Microsoft', 3);
insert into Company (Title, LegalFormId) values ('Google', 2);
insert into Company (Title, LegalFormId) values ('Facebook', 1);
insert into Company (Title, LegalFormId) values ('Amazon', 2);
insert into Company (Title, LegalFormId) values ('Apple', 1);

create table Employee (
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Surname NVARCHAR(50),
	Name NVARCHAR(50),
	Patronymic NVARCHAR(50),
	EmploymentDate DATE,
	PositionId INT FOREIGN KEY REFERENCES Position,
	CompanyId INT FOREIGN KEY REFERENCES Company,
	IsDeleted BIT
);
insert into Employee (Surname, Name, Patronymic, EmploymentDate, PositionId, CompanyId) values ('Barbe', 'Nancee', 'Stanfield', '2020-10-10 00:50:59', 2, 9);
insert into Employee (Surname, Name, Patronymic, EmploymentDate, PositionId, CompanyId) values ('Slyde', 'Godfrey', 'Paola', '2021-05-21 23:31:51', 2, 6);
insert into Employee (Surname, Name, Patronymic, EmploymentDate, PositionId, CompanyId) values ('Paige', 'Roseanne', 'Myrah', '2021-05-07 02:34:10', 1, 3);
insert into Employee (Surname, Name, Patronymic, EmploymentDate, PositionId, CompanyId) values ('Biddulph', 'Bartolomeo', 'Chad', '2020-11-11 07:48:15', 1, 9);
insert into Employee (Surname, Name, Patronymic, EmploymentDate, PositionId, CompanyId) values ('Espinoza', 'Manda', 'Lucie', '2020-10-26 15:09:31', 1, 1);
insert into Employee (Surname, Name, Patronymic, EmploymentDate, PositionId, CompanyId) values ('Lamanby', 'Deloris', 'Jana', '2021-03-23 00:47:47', 3, 9);
insert into Employee (Surname, Name, Patronymic, EmploymentDate, PositionId, CompanyId) values ('Connikie', 'Hamil', 'Maurene', '2020-08-30 13:57:23', 3, 9);
insert into Employee (Surname, Name, Patronymic, EmploymentDate, PositionId, CompanyId) values ('Willgress', 'Veradis', 'Brannon', '2021-07-09 04:50:46', 4, 3);
insert into Employee (Surname, Name, Patronymic, EmploymentDate, PositionId, CompanyId) values ('Carlson', 'Arnie', 'Ignacio', '2021-02-16 02:50:13', 4, 6);
insert into Employee (Surname, Name, Patronymic, EmploymentDate, PositionId, CompanyId) values ('Duggon', 'Reiko', 'Hardy', '2021-02-28 11:47:00', 1, 4);