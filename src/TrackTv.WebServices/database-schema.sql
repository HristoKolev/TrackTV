IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Roles] (
    [Id] nvarchar(450) NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Name] nvarchar(max),
    [NormalizedName] nvarchar(max),
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [UserTokens] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [Value] nvarchar(max),
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([LoginProvider], [Name], [UserId])
);

GO

CREATE TABLE [OpenIddictApplications] (
    [Id] nvarchar(450) NOT NULL,
    [ClientId] nvarchar(450),
    [ClientSecret] nvarchar(max),
    [DisplayName] nvarchar(max),
    [LogoutRedirectUri] nvarchar(max),
    [RedirectUri] nvarchar(max),
    [Type] nvarchar(max),
    CONSTRAINT [PK_OpenIddictApplications] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [OpenIddictScopes] (
    [Id] nvarchar(450) NOT NULL,
    [Description] nvarchar(max),
    CONSTRAINT [PK_OpenIddictScopes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Actors] (
    [Id] int NOT NULL IDENTITY,
    [Image] nvarchar(255),
    [LastUpdated] datetime2 NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [TheTvDbId] int NOT NULL,
    CONSTRAINT [PK_Actors] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Networks] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(40) NOT NULL,
    CONSTRAINT [PK_Networks] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [RoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [IdentityRoleId] nvarchar(450),
    [RoleId] nvarchar(max),
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleClaims_Roles_IdentityRoleId] FOREIGN KEY ([IdentityRoleId]) REFERENCES [Roles] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [OpenIddictAuthorizations] (
    [Id] nvarchar(450) NOT NULL,
    [ApplicationId] nvarchar(450),
    [Scope] nvarchar(max),
    [Subject] nvarchar(max),
    CONSTRAINT [PK_OpenIddictAuthorizations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [OpenIddictApplications] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Shows] (
    [Id] int NOT NULL IDENTITY,
    [AirDay] int,
    [AirTime] datetime2,
    [Banner] nvarchar(255),
    [Description] nvarchar(max),
    [FirstAired] datetime2,
    [ImdbId] nvarchar(10),
    [LastUpdated] datetime2 NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [NetworkId] int NOT NULL,
    [Status] int NOT NULL,
    [TheTvDbId] int NOT NULL,
    CONSTRAINT [PK_Shows] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Shows_Networks_NetworkId] FOREIGN KEY ([NetworkId]) REFERENCES [Networks] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [OpenIddictTokens] (
    [Id] nvarchar(450) NOT NULL,
    [ApplicationId] nvarchar(450),
    [AuthorizationId] nvarchar(450),
    [Subject] nvarchar(max),
    [Type] nvarchar(max),
    CONSTRAINT [PK_OpenIddictTokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OpenIddictTokens_OpenIddictApplications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [OpenIddictApplications] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId] FOREIGN KEY ([AuthorizationId]) REFERENCES [OpenIddictAuthorizations] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Episodes] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max),
    [FirstAired] datetime2,
    [ImdbId] nvarchar(10),
    [LastUpdated] datetime2 NOT NULL,
    [Number] int NOT NULL,
    [SeasonNumber] int NOT NULL,
    [ShowId] int NOT NULL,
    [TheTvDbId] int NOT NULL,
    [Title] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Episodes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Episodes_Shows_ShowId] FOREIGN KEY ([ShowId]) REFERENCES [Shows] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ShowsActors] (
    [ShowId] int NOT NULL,
    [ActorId] int NOT NULL,
    [Role] nvarchar(255),
    CONSTRAINT [PK_ShowsActors] PRIMARY KEY ([ShowId], [ActorId]),
    CONSTRAINT [FK_ShowsActors_Actors_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [Actors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ShowsActors_Shows_ShowId] FOREIGN KEY ([ShowId]) REFERENCES [Shows] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ShowsGenres] (
    [ShowId] int NOT NULL,
    [GenreId] int NOT NULL,
    CONSTRAINT [PK_ShowsGenres] PRIMARY KEY ([ShowId], [GenreId]),
    CONSTRAINT [FK_ShowsGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ShowsGenres_Shows_ShowId] FOREIGN KEY ([ShowId]) REFERENCES [Shows] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [UserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    [IdentityRoleId] nvarchar(450),
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Roles_IdentityRoleId] FOREIGN KEY ([IdentityRoleId]) REFERENCES [Roles] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ShowsProfiles] (
    [ProfileId] int NOT NULL,
    [ShowId] int NOT NULL,
    CONSTRAINT [PK_ShowsProfiles] PRIMARY KEY ([ProfileId], [ShowId]),
    CONSTRAINT [FK_ShowsProfiles_Shows_ShowId] FOREIGN KEY ([ShowId]) REFERENCES [Shows] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Users] (
    [Id] nvarchar(450) NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Email] nvarchar(max),
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset,
    [NormalizedEmail] nvarchar(max),
    [NormalizedUserName] nvarchar(max),
    [PasswordHash] nvarchar(max),
    [PhoneNumber] nvarchar(max),
    [PhoneNumberConfirmed] bit NOT NULL,
    [ProfileId] int NOT NULL,
    [SecurityStamp] nvarchar(max),
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(max),
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [UserId] nvarchar(450),
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [UserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max),
    [UserId] nvarchar(450),
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Profiles] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450),
    [Username] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Profiles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Profiles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_RoleClaims_IdentityRoleId] ON [RoleClaims] ([IdentityRoleId]);

GO

CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);

GO

CREATE INDEX [IX_UserLogins_UserId] ON [UserLogins] ([UserId]);

GO

CREATE INDEX [IX_UserRoles_IdentityRoleId] ON [UserRoles] ([IdentityRoleId]);

GO

CREATE UNIQUE INDEX [IX_OpenIddictApplications_ClientId] ON [OpenIddictApplications] ([ClientId]) WHERE [ClientId] IS NOT NULL;

GO

CREATE INDEX [IX_OpenIddictAuthorizations_ApplicationId] ON [OpenIddictAuthorizations] ([ApplicationId]);

GO

CREATE INDEX [IX_OpenIddictTokens_ApplicationId] ON [OpenIddictTokens] ([ApplicationId]);

GO

CREATE INDEX [IX_OpenIddictTokens_AuthorizationId] ON [OpenIddictTokens] ([AuthorizationId]);

GO

CREATE INDEX [IX_Episodes_ShowId] ON [Episodes] ([ShowId]);

GO

CREATE INDEX [IX_Profiles_UserId] ON [Profiles] ([UserId]);

GO

CREATE INDEX [IX_Shows_NetworkId] ON [Shows] ([NetworkId]);

GO

CREATE INDEX [IX_ShowsActors_ActorId] ON [ShowsActors] ([ActorId]);

GO

CREATE INDEX [IX_ShowsGenres_GenreId] ON [ShowsGenres] ([GenreId]);

GO

CREATE INDEX [IX_ShowsProfiles_ShowId] ON [ShowsProfiles] ([ShowId]);

GO

CREATE INDEX [IX_Users_ProfileId] ON [Users] ([ProfileId]);

GO

ALTER TABLE [UserRoles] ADD CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [ShowsProfiles] ADD CONSTRAINT [FK_ShowsProfiles_Profiles_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Profiles] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_Profiles_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Profiles] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170329201628_Created', N'1.1.1');

GO

