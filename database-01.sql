rollback;
START transaction;

GRANT Create ON labo_tp.* TO 'labo_tp'@'%';
GRANT Alter ON labo_tp.* TO 'labo_tp'@'%';
GRANT References ON labo_tp.* TO 'labo_tp'@'%';
GRANT Insert ON labo_tp.* TO 'labo_tp'@'%';
GRANT Select ON labo_tp.* TO 'labo_tp'@'%';
GRANT Trigger ON labo_tp.* TO 'labo_tp'@'%';
GRANT Update ON labo_tp.* TO 'labo_tp'@'%';
GRANT Show view ON labo_tp.* TO 'labo_tp'@'%';
GRANT Index ON labo_tp.* TO 'labo_tp'@'%' WITH GRANT OPTION;
GRANT Drop ON labo_tp.* TO 'labo_tp'@'%';
GRANT Create view ON labo_tp.* TO 'labo_tp'@'%';
GRANT Delete ON labo_tp.* TO 'labo_tp'@'%';
GRANT Execute ON labo_tp.* TO 'labo_tp'@'%';
GRANT Create routine ON labo_tp.* TO 'labo_tp'@'%';
GRANT Alter routine ON labo_tp.* TO 'labo_tp'@'%';
GRANT Create temporary tables ON labo_tp.* TO 'labo_tp'@'%';


CREATE TABLE `supervisors` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FullName` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Phone` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB;

CREATE TABLE `laboratories` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Address` varchar(255) DEFAULT NULL,
  `SupervisorId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Laboratories_Supervisor` (`SupervisorId`),
  CONSTRAINT `FK_Laboratories_Supervisor` FOREIGN KEY (`SupervisorId`) REFERENCES `supervisors` (`Id`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE `rooms` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Capacity` int NOT NULL DEFAULT '0',
  `LaboratoryId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Rooms_Laboratory` (`LaboratoryId`),
  CONSTRAINT `FK_Rooms_Laboratory` FOREIGN KEY (`LaboratoryId`) REFERENCES `laboratories` (`Id`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE `computers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `MacAddress` varchar(50) NOT NULL,
  `IPAddress` varchar(50) DEFAULT NULL,
  `RamSize` varchar(50) DEFAULT NULL,
  `OperatingSystem` varchar(255) DEFAULT NULL,
  `DiskSize` varchar(50) DEFAULT NULL,
  `DiskBrand` varchar(255) DEFAULT NULL,
  `RoomId` int DEFAULT NULL,
  `SaveDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ComputerName` varchar(254) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `MacAddress` (`MacAddress`),
  KEY `FK_Computers_Room` (`RoomId`),
  CONSTRAINT `FK_Computers_Room` FOREIGN KEY (`RoomId`) REFERENCES `rooms` (`Id`) ON DELETE SET NULL
) ENGINE=InnoDB;

COMMIT;