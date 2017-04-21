CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Actors` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Image` varchar(255),
    `LastUpdated` datetime(6) NOT NULL,
    `Name` varchar(255) NOT NULL,
    `TheTvDbId` int NOT NULL,
    CONSTRAINT `PK_Actors` PRIMARY KEY (`Id`)
);

CREATE TABLE `Genres` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(255) NOT NULL,
    CONSTRAINT `PK_Genres` PRIMARY KEY (`Id`)
);

CREATE TABLE `Networks` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(255) NOT NULL,
    CONSTRAINT `PK_Networks` PRIMARY KEY (`Id`)
);

CREATE TABLE `Profiles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Username` longtext NOT NULL,
    CONSTRAINT `PK_Profiles` PRIMARY KEY (`Id`)
);

CREATE TABLE `Shows` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `AirDay` int,
    `AirTime` datetime(6),
    `Banner` varchar(255),
    `Description` longtext,
    `FirstAired` datetime(6),
    `ImdbId` varchar(10),
    `LastUpdated` datetime(6) NOT NULL,
    `Name` varchar(255) NOT NULL,
    `NetworkId` int NOT NULL,
    `Status` int NOT NULL,
    `TheTvDbId` int NOT NULL,
    CONSTRAINT `PK_Shows` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Shows_Networks_NetworkId` FOREIGN KEY (`NetworkId`) REFERENCES `Networks` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Episodes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Description` longtext,
    `FirstAired` datetime(6),
    `ImdbId` varchar(10),
    `LastUpdated` datetime(6) NOT NULL,
    `Number` int NOT NULL,
    `SeasonNumber` int NOT NULL,
    `ShowId` int NOT NULL,
    `TheTvDbId` int NOT NULL,
    `Title` varchar(255),
    CONSTRAINT `PK_Episodes` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Episodes_Shows_ShowId` FOREIGN KEY (`ShowId`) REFERENCES `Shows` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Roles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ActorId` int NOT NULL,
    `RoleName` varchar(255),
    `ShowId` int NOT NULL,
    CONSTRAINT `PK_Roles` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Roles_Actors_ActorId` FOREIGN KEY (`ActorId`) REFERENCES `Actors` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Roles_Shows_ShowId` FOREIGN KEY (`ShowId`) REFERENCES `Shows` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ShowsGenres` (
    `ShowId` int NOT NULL,
    `GenreId` int NOT NULL,
    CONSTRAINT `PK_ShowsGenres` PRIMARY KEY (`ShowId`, `GenreId`),
    CONSTRAINT `FK_ShowsGenres_Genres_GenreId` FOREIGN KEY (`GenreId`) REFERENCES `Genres` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ShowsGenres_Shows_ShowId` FOREIGN KEY (`ShowId`) REFERENCES `Shows` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Subscriptions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ProfileId` int NOT NULL,
    `ShowId` int NOT NULL,
    CONSTRAINT `PK_Subscriptions` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Subscriptions_Profiles_ProfileId` FOREIGN KEY (`ProfileId`) REFERENCES `Profiles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Subscriptions_Shows_ShowId` FOREIGN KEY (`ShowId`) REFERENCES `Shows` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Episodes_ShowId` ON `Episodes` (`ShowId`);

CREATE INDEX `IX_Roles_ActorId` ON `Roles` (`ActorId`);

CREATE INDEX `IX_Roles_ShowId` ON `Roles` (`ShowId`);

CREATE INDEX `IX_Shows_NetworkId` ON `Shows` (`NetworkId`);

CREATE INDEX `IX_ShowsGenres_GenreId` ON `ShowsGenres` (`GenreId`);

CREATE INDEX `IX_Subscriptions_ProfileId` ON `Subscriptions` (`ProfileId`);

CREATE INDEX `IX_Subscriptions_ShowId` ON `Subscriptions` (`ShowId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20170421183849_ApplicationDbContext_Created', '1.1.1');

