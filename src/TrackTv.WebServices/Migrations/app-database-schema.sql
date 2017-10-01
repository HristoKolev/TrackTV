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
    [ActorId] int NOT NULL IDENTITY,
    [ActorImage] nvarchar(255),
    [ActorName] nvarchar(255) NOT NULL,
    [LastUpdated] datetime2 NOT NULL,
    [TheTvDbId] int NOT NULL,
    CONSTRAINT [PK_Actors] PRIMARY KEY ([ActorId])
);

GO

CREATE TABLE [Genres] (
    [GenreId] int NOT NULL IDENTITY,
    [GenreName] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([GenreId])
);

GO

CREATE TABLE [Networks] (
    [NetworkId] int NOT NULL IDENTITY,
    [NetworkName] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Networks] PRIMARY KEY ([NetworkId])
);

GO

CREATE TABLE [Profiles] (
    [ProfileId] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Profiles] PRIMARY KEY ([ProfileId])
);

GO

CREATE TABLE [Shows] (
    [ShowId] int NOT NULL IDENTITY,
    [AirDay] int,
    [AirTime] datetime2,
    [FirstAired] datetime2,
    [ImdbId] nvarchar(10),
    [LastUpdated] datetime2 NOT NULL,
    [NetworkId] int NOT NULL,
    [ShowBanner] nvarchar(255),
    [ShowDescription] nvarchar(max),
    [ShowName] nvarchar(255) NOT NULL,
    [ShowStatus] int NOT NULL,
    [TheTvDbId] int NOT NULL,
    CONSTRAINT [PK_Shows] PRIMARY KEY ([ShowId]),
    CONSTRAINT [FK_Shows_Networks_NetworkId] FOREIGN KEY ([NetworkId]) REFERENCES [Networks] ([NetworkId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Episodes] (
    [EpisodeId] int NOT NULL IDENTITY,
    [EpisodeDescription] nvarchar(max),
    [EpisodeNumber] int NOT NULL,
    [EpisodeTitle] nvarchar(255),
    [FirstAired] datetime2,
    [ImdbId] nvarchar(10),
    [LastUpdated] datetime2 NOT NULL,
    [SeasonNumber] int NOT NULL,
    [ShowId] int NOT NULL,
    [TheTvDbId] int NOT NULL,
    CONSTRAINT [PK_Episodes] PRIMARY KEY ([EpisodeId]),
    CONSTRAINT [FK_Episodes_Shows_ShowId] FOREIGN KEY ([ShowId]) REFERENCES [Shows] ([ShowId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Roles] (
    [RoleId] int NOT NULL IDENTITY,
    [ActorId] int NOT NULL,
    [RoleName] nvarchar(255),
    [ShowId] int NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleId]),
    CONSTRAINT [FK_Roles_Actors_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [Actors] ([ActorId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Roles_Shows_ShowId] FOREIGN KEY ([ShowId]) REFERENCES [Shows] ([ShowId]) ON DELETE CASCADE
);

GO

CREATE TABLE [ShowsGenres] (
    [ShowId] int NOT NULL,
    [GenreId] int NOT NULL,
    CONSTRAINT [PK_ShowsGenres] PRIMARY KEY ([ShowId], [GenreId]),
    CONSTRAINT [FK_ShowsGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([GenreId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ShowsGenres_Shows_ShowId] FOREIGN KEY ([ShowId]) REFERENCES [Shows] ([ShowId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Subscriptions] (
    [SubscriptionId] int NOT NULL IDENTITY,
    [ProfileId] int NOT NULL,
    [ShowId] int NOT NULL,
    CONSTRAINT [PK_Subscriptions] PRIMARY KEY ([SubscriptionId]),
    CONSTRAINT [FK_Subscriptions_Profiles_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Profiles] ([ProfileId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Subscriptions_Shows_ShowId] FOREIGN KEY ([ShowId]) REFERENCES [Shows] ([ShowId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Episodes_ShowId] ON [Episodes] ([ShowId]);

GO

CREATE INDEX [IX_Roles_ActorId] ON [Roles] ([ActorId]);

GO

CREATE INDEX [IX_Roles_ShowId] ON [Roles] ([ShowId]);

GO

CREATE INDEX [IX_Shows_NetworkId] ON [Shows] ([NetworkId]);

GO

CREATE INDEX [IX_ShowsGenres_GenreId] ON [ShowsGenres] ([GenreId]);

GO

CREATE INDEX [IX_Subscriptions_ProfileId] ON [Subscriptions] ([ProfileId]);

GO

CREATE INDEX [IX_Subscriptions_ShowId] ON [Subscriptions] ([ShowId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171001153410_ApplicationDbContext_Created', N'1.1.2');

GO

