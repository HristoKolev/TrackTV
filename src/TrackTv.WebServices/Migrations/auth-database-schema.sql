CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `AspNetRoles` (
    `Id` varchar(127) NOT NULL,
    `ConcurrencyStamp` longtext,
    `Name` varchar(256),
    `NormalizedName` varchar(256),
    CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUserTokens` (
    `UserId` varchar(127) NOT NULL,
    `LoginProvider` varchar(127) NOT NULL,
    `Name` varchar(127) NOT NULL,
    `Value` longtext,
    CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `AK_AspNetUserTokens_LoginProvider_Name_UserId` UNIQUE (`LoginProvider`, `Name`, `UserId`)
);

CREATE TABLE `OpenIddictApplications` (
    `Id` varchar(127) NOT NULL,
    `ClientId` varchar(127),
    `ClientSecret` longtext,
    `DisplayName` longtext,
    `LogoutRedirectUri` longtext,
    `RedirectUri` longtext,
    `Type` longtext,
    CONSTRAINT `PK_OpenIddictApplications` PRIMARY KEY (`Id`)
);

CREATE TABLE `OpenIddictScopes` (
    `Id` varchar(127) NOT NULL,
    `Description` longtext,
    CONSTRAINT `PK_OpenIddictScopes` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUsers` (
    `Id` varchar(127) NOT NULL,
    `AccessFailedCount` int NOT NULL,
    `ConcurrencyStamp` longtext,
    `Email` varchar(256),
    `EmailConfirmed` bit NOT NULL,
    `LockoutEnabled` bit NOT NULL,
    `LockoutEnd` datetime(6),
    `NormalizedEmail` varchar(256),
    `NormalizedUserName` varchar(256),
    `PasswordHash` longtext,
    `PhoneNumber` longtext,
    `PhoneNumberConfirmed` bit NOT NULL,
    `ProfileId` int NOT NULL,
    `SecurityStamp` longtext,
    `TwoFactorEnabled` bit NOT NULL,
    `UserName` varchar(256),
    CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ClaimType` longtext,
    `ClaimValue` longtext,
    `RoleId` varchar(127) NOT NULL,
    CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `OpenIddictAuthorizations` (
    `Id` varchar(127) NOT NULL,
    `ApplicationId` varchar(127),
    `Scope` longtext,
    `Subject` longtext,
    CONSTRAINT `PK_OpenIddictAuthorizations` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId` FOREIGN KEY (`ApplicationId`) REFERENCES `OpenIddictApplications` (`Id`) ON DELETE NO ACTION
);

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ClaimType` longtext,
    `ClaimValue` longtext,
    `UserId` varchar(127) NOT NULL,
    CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(127) NOT NULL,
    `ProviderKey` varchar(127) NOT NULL,
    `ProviderDisplayName` longtext,
    `UserId` varchar(127) NOT NULL,
    CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserRoles` (
    `UserId` varchar(127) NOT NULL,
    `RoleId` varchar(127) NOT NULL,
    CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `OpenIddictTokens` (
    `Id` varchar(127) NOT NULL,
    `ApplicationId` varchar(127),
    `AuthorizationId` varchar(127),
    `Subject` longtext,
    `Type` longtext,
    CONSTRAINT `PK_OpenIddictTokens` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_OpenIddictTokens_OpenIddictApplications_ApplicationId` FOREIGN KEY (`ApplicationId`) REFERENCES `OpenIddictApplications` (`Id`) ON DELETE NO ACTION,
    CONSTRAINT `FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId` FOREIGN KEY (`AuthorizationId`) REFERENCES `OpenIddictAuthorizations` (`Id`) ON DELETE NO ACTION
);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE UNIQUE INDEX `IX_OpenIddictApplications_ClientId` ON `OpenIddictApplications` (`ClientId`);

CREATE INDEX `IX_OpenIddictAuthorizations_ApplicationId` ON `OpenIddictAuthorizations` (`ApplicationId`);

CREATE INDEX `IX_OpenIddictTokens_ApplicationId` ON `OpenIddictTokens` (`ApplicationId`);

CREATE INDEX `IX_OpenIddictTokens_AuthorizationId` ON `OpenIddictTokens` (`AuthorizationId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20170421183836_AuthContext_Created', '1.1.1');

