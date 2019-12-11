use VroomServiceDb;
GO

-- DROP TABLE --

DROP TABLE IF EXISTS Booking;
DROP TABLE IF EXISTS Car;
DROP TABLE IF EXISTS Brand;
DROP TABLE IF EXISTS [User];

GO

-- CREATE TABLE --

CREATE TABLE [User]
(
    Id INT PRIMARY KEY IDENTITY NOT NULL,
    Username VARCHAR(100) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    Token VARCHAR(100),
    Firstname VARCHAR(100),
    Lastname VARCHAR(100)
)

CREATE TABLE Brand
(
    Id INT PRIMARY KEY IDENTITY NOT NULL,
    Name VARCHAR(100)
)

CREATE TABLE Car
(
    Id INT PRIMARY KEY IDENTITY NOT NULL,
    Name VARCHAR(100),
    Price INT,
    Description VARCHAR(500),
    PlaceNb INT,
	Brand_Id INT NOT NULL
)

CREATE TABLE Booking
(
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	StartDate DATETIME,
	EndDate DATETIME,
	[State] VARCHAR(50),
	User_Id INT NOT NULL,
	Car_Id INT NOT NULL
)

-- CONSTRAINT --


ALTER TABLE Car
ADD CONSTRAINT FK_Car_Brand
FOREIGN KEY (Brand_Id) REFERENCES Brand(Id);

ALTER TABLE Booking
ADD CONSTRAINT FK_Booking_User
FOREIGN KEY (User_Id) REFERENCES [User](Id);

ALTER TABLE Booking
ADD CONSTRAINT FK_Booking_Car
FOREIGN KEY (Car_Id) REFERENCES Car(Id);

GO

-- INSERT INIT DATA --

-- [User]
INSERT INTO [User](Username, Password, Firstname, Lastname) VALUES ('Gabz', 'vQNVTLrMX/0yuFrOgKYP7nq0aF8=', 'Gabin', 'Soutif');
INSERT INTO [User](Username, Password, Firstname, Lastname) VALUES ('Emiliego', 'vQNVTLrMX/0yuFrOgKYP7nq0aF8=', 'Emilie', 'Gougeon');

-- Brand
INSERT INTO Brand(Name) VALUES ('Lamborghini');
INSERT INTO Brand(Name) VALUES ('Ferrari');
INSERT INTO Brand(Name) VALUES ('Porsche');

-- Car
INSERT INTO Car(Name, Price, Description, PlaceNb, Brand_Id) VALUES ('Aventador SVJ Roadster', 82000, 'description L', 2, 1);
INSERT INTO Car(Name, Price, Description, PlaceNb, Brand_Id) VALUES ('Huracan Spyder', 210000, 'description L', 2, 1);
INSERT INTO Car(Name, Price, Description, PlaceNb, Brand_Id) VALUES ('Ferrari F8 Turbo', 145000, 'description F', 2, 2);
INSERT INTO Car(Name, Price, Description, PlaceNb, Brand_Id) VALUES ('Posche 911 Carrera 4S', 65000, 'description P', 4, 3);

-- Booking
INSERT INTO Booking(StartDate, EndDate, [State], User_Id, Car_Id) VALUES ('2019-11-30T00:00:00', '2019-11-30T00:00:00', 'Terminée', 1, 1);
INSERT INTO Booking(StartDate, EndDate, [State], User_Id, Car_Id) VALUES ('2019-12-13T00:00:00', '2019-12-15T00:00:00', 'En cours', 1, 1);
INSERT INTO Booking(StartDate, EndDate, [State], User_Id, Car_Id) VALUES ('2019-12-03T00:00:00', '2019-11-06T00:00:00', 'Terminée', 2, 1);

-- SELECT ALL TABLES --

SELECT * FROM [User];
SELECT * FROM Brand;
SELECT * FROM Car;
SELECT * FROM Booking;
