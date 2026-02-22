USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'DungeonsAndAuctionsDBA')
BEGIN
    ALTER DATABASE DungeonsAndAuctionsDBA SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DungeonsAndAuctionsDBA;
END
GO

CREATE DATABASE DungeonsAndAuctionsDBA;
GO

USE DungeonsAndAuctionsDBA;
GO

/* =========================
   TABLAS
========================= */

CREATE TABLE Countries(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    CountryCode INT NOT NULL
);

CREATE TABLE Genders(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE Roles(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255)
);

CREATE TABLE paymentstatus(
    id INT IDENTITY(1,1) PRIMARY KEY,
    status NVARCHAR(50) NOT NULL
);

CREATE TABLE Qualities(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Quality NVARCHAR(50)
);

CREATE TABLE AunctionState(
    idstate INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50)
);

CREATE TABLE Conditions(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Categories(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255)
);

CREATE TABLE Images(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    ImageData VARBINARY(MAX) NOT NULL
);

CREATE TABLE Users(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    PasswordHash VARBINARY(256) NOT NULL,
    BirthDate DATE NOT NULL,
    CountryId INT NOT NULL,
    GenderId INT NOT NULL,
    AboutMe NVARCHAR(255),
    Email NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(20),
    RoleId INT NOT NULL,
    IsBlocked BIT NOT NULL DEFAULT 0,
    RegisterDate DATE NOT NULL,
    LastLogin DATE,
    Active BIT NOT NULL DEFAULT 1
);

CREATE TABLE Objects(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Year INT,
    Description NVARCHAR(255),
    RegistrationDate DATE NOT NULL,
    MarketPrice DECIMAL(10,2) NOT NULL,
    IsActive BIT NOT NULL,
    idCondition INT,
    idState INT,
    idimage INT
);

CREATE TABLE Auctions(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    IsActive BIT NOT NULL,
    BasePrice DECIMAL(18,2) NOT NULL,
    IncrementoMinimo DECIMAL(18,2) NOT NULL,
    idstate INT NOT NULL,
    idusercreator INT NOT NULL,
    idobject INT NOT NULL
);

CREATE TABLE AuctionBidHistory(
    id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    AuctionId INT NOT NULL,
    BidDate DATETIME NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    IsLastBid BIT NOT NULL
);

CREATE TABLE AuctionWinner(
    idauctionwinner INT IDENTITY(1,1) PRIMARY KEY,
    actionid INT NOT NULL,
    winnerid INT NOT NULL,
    finalprice DECIMAL(8,2) NOT NULL,
    closeddate DATETIME NOT NULL,
    bidwinningid INT NOT NULL
);

CREATE TABLE ObjectCategories(
    ObjectId INT NOT NULL,
    CategoryId INT NOT NULL,
    PRIMARY KEY(ObjectId, CategoryId)
);

CREATE TABLE Payment(
    paymentID INT IDENTITY(1,1) PRIMARY KEY,
    auctionID INT,
    winnerUserID INT,
    amount DECIMAL(18,0),
    paymentStatusID INT,
    dateCreated DATETIME,
    dateconfirmed DATETIME
);

CREATE TABLE ProceduresHistory(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    ProcedureType INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    CompletionDate DATE NOT NULL
);

/* =========================
   DATOS
========================= */

INSERT INTO Countries (Name,CountryCode) VALUES
('Costa Rica',506),
('México',52),
('Estados Unidos',1);


INSERT INTO Genders (Name) VALUES
('Female'),
('Male'),
('Nonbinary')


INSERT INTO Roles (Name,Description) VALUES
('Admin','Full access to the system, user management and auctions.'),
('User','Standard user: bid, create auctions and items.'),
('Moderator',' Can block users and review suspicious bids.'),
('Guest',' Can only view the auctions, cannot interact without logging in')
;

INSERT INTO Users (
UserName,FirstName,LastName,PasswordHash,BirthDate,
CountryId,GenderId,AboutMe,Email,PhoneNumber,
RoleId,IsBlocked,RegisterDate,LastLogin,Active)
VALUES
('ashley_admin','Ashley','Rivera',0x41646D696E313233,'2006-01-21',1,1,'Fundadora del sistema.','ashley.admin@mail.com','8888-1111',1,0,'2026-02-20','2026-02-20',1),
('juan_user','Juan','Perez',0x55736572313233,'1998-05-10',2,2,'Amante de las subastas.','juan.perez@mail.com','7777-2222',2,0,'2026-02-20',NULL,1),
('luna_mod','Luna','Martinez',0x4D6F64313233,'2000-09-15',1,3,'Moderador activo.','luna.mod@mail.com',NULL,3,0,'2026-02-20',NULL,1);


Insert into AunctionState(Name) 
VALUES
('Open'),
('Close'),
('Banned'),
('Inactive');

INSERT INTO Qualities(Quality)
VALUES  
('New'),
('Used - Like new'),
('Used - Good condition'),
('Used - Poor condition'), 
('Relic'),
('Relic - Perfect condition'),
('Relic - Good condition'),
('Relic - Poor condition');


Insert into paymentstatus(status)
VALUES 
('Paid'),
('In payment process'),
('Debt');



Insert into Categories(Name,Description) 
VALUES
('Grimoire',''),
('Weapon',''),
('Potion',''),
('Antique',''),
('Sword',''),
('Bow',''),
('Magician`s staff',''),
('Book',''),
('Bow',''),
('Mask',''),
('Armor',''),
('Helmet',''),
('Bow',''),
('Chess Plate',''),
('Leg Armour',''),
('Bow',''),
('Armoured Boots',''),
('Iron',''),
('Alloys',''),
('Wood',''),
('Uknown Material',''),
('Diamond',''),
('Stainless metal',''),
('Dragon skin',''),
('Weird egg',''),
('Dragon egg',''),
('Monster Part',''),
('Tail',''),
('Magic',''),
('Cloth',''),
('Enchanted',''),
('Cursed','');


/* =========================
   FOREIGN KEYS
========================= */

ALTER TABLE Users ADD FOREIGN KEY (CountryId) REFERENCES Countries(Id);
ALTER TABLE Users ADD FOREIGN KEY (GenderId) REFERENCES Genders(Id);
ALTER TABLE Users ADD FOREIGN KEY (RoleId) REFERENCES Roles(Id);

ALTER TABLE Objects ADD FOREIGN KEY (UserId) REFERENCES Users(Id);
ALTER TABLE Objects ADD FOREIGN KEY (idCondition) REFERENCES Conditions(Id);
ALTER TABLE Objects ADD FOREIGN KEY (idimage) REFERENCES Images(Id);
ALTER TABLE Objects ADD FOREIGN KEY (idState) REFERENCES Qualities(ID);

ALTER TABLE Auctions ADD FOREIGN KEY (idstate) REFERENCES AunctionState(idstate);
ALTER TABLE Auctions ADD FOREIGN KEY (idusercreator) REFERENCES Users(Id);
ALTER TABLE Auctions ADD FOREIGN KEY (idobject) REFERENCES Objects(Id);

ALTER TABLE AuctionBidHistory ADD FOREIGN KEY (UserId) REFERENCES Users(Id);
ALTER TABLE AuctionBidHistory ADD FOREIGN KEY (AuctionId) REFERENCES Auctions(Id);

ALTER TABLE AuctionWinner ADD FOREIGN KEY (actionid) REFERENCES Auctions(Id);
ALTER TABLE AuctionWinner ADD FOREIGN KEY (bidwinningid) REFERENCES AuctionBidHistory(id);

ALTER TABLE ObjectCategories ADD FOREIGN KEY (ObjectId) REFERENCES Objects(Id);
ALTER TABLE ObjectCategories ADD FOREIGN KEY (CategoryId) REFERENCES Categories(Id);

ALTER TABLE Payment ADD FOREIGN KEY (auctionID) REFERENCES Auctions(Id);
ALTER TABLE Payment ADD FOREIGN KEY (winnerUserID) REFERENCES AuctionWinner(idauctionwinner);
ALTER TABLE Payment ADD FOREIGN KEY (paymentStatusID) REFERENCES paymentstatus(id);

ALTER TABLE ProceduresHistory ADD FOREIGN KEY (UserId) REFERENCES Users(Id);