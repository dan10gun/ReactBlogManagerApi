Project configuration:

Project is built on net 8 framework
Dapper 2.1.66
MySql.Data 92.2.0 
Swashbuckle.AspNetCore 6.6.2

Modify DBconnection in appsetting.json (DefaultConnection) to your local local db or server to establish connection
Add dependencies in program cs if needed

--------------------------About project---------------------------------
The project is built into 3 layers:

The controller --> service layer --> repository layer

Controller: contain api routing and general status return logic
Service layer: connect to the repository layer and contain  field validation logic
Repository layer: contain the query logic


--------------------------My Sql queries for table creation-------------

-- CREATE DATABASE IF NOT EXISTS blogmanager;

USE blogmanager;

CREATE TABLE User (
    UserId INT AUTO_INCREMENT PRIMARY KEY,
    Email VARCHAR(255) NOT NULL,
    Password VARCHAR(50) NOT NULL
);

CREATE TABLE Post (
    PostId INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT,
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(3000) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES User(UserId)
);

CREATE TABLE Favourite (
    UserId INT,
    PostId INT,
    FOREIGN KEY (UserId) REFERENCES User(UserId),
    FOREIGN KEY (PostId) REFERENCES Post(PostId),
    PRIMARY KEY (UserId, PostId)
);

--------------------------End of queries----------------------------

Note: register user and add/remove for favourite not completed - manually add value in db