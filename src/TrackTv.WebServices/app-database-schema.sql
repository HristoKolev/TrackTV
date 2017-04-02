IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

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

CREATE TABLE [Profiles] (
    [Id] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Profiles] PRIMARY KEY ([Id])
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
    [Title] nvarchar(255),
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

CREATE TABLE [ShowsProfiles] (
    [ProfileId] int NOT NULL,
    [ShowId] int NOT NULL,
    CONSTRAINT [PK_ShowsProfiles] PRIMARY KEY ([ProfileId], [ShowId]),
    CONSTRAINT [FK_ShowsProfiles_Profiles_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Profiles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ShowsProfiles_Shows_ShowId] FOREIGN KEY ([ShowId]) REFERENCES [Shows] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Episodes_ShowId] ON [Episodes] ([ShowId]);

GO

CREATE INDEX [IX_Shows_NetworkId] ON [Shows] ([NetworkId]);

GO

CREATE INDEX [IX_ShowsActors_ActorId] ON [ShowsActors] ([ActorId]);

GO

CREATE INDEX [IX_ShowsGenres_GenreId] ON [ShowsGenres] ([GenreId]);

GO

CREATE INDEX [IX_ShowsProfiles_ShowId] ON [ShowsProfiles] ([ShowId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170402183558_ApplicationDbContext_Created', N'1.1.1');

GO

