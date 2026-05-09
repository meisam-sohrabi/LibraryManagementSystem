IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    IF SCHEMA_ID(N'author') IS NULL EXEC(N'CREATE SCHEMA [author];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    IF SCHEMA_ID(N'book') IS NULL EXEC(N'CREATE SCHEMA [book];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    IF SCHEMA_ID(N'borrow') IS NULL EXEC(N'CREATE SCHEMA [borrow];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    IF SCHEMA_ID(N'member') IS NULL EXEC(N'CREATE SCHEMA [member];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    IF SCHEMA_ID(N'Identity') IS NULL EXEC(N'CREATE SCHEMA [Identity];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE TABLE [author].[Authors] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(120) NOT NULL,
        [BirthYear] int NOT NULL,
        CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE TABLE [book].[Books] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(250) NOT NULL,
        [Genere] nvarchar(70) NOT NULL,
        [PublishYear] int NOT NULL,
        [AvailableCopies] int NOT NULL,
        [ImageUrl] nvarchar(max) NULL,
        CONSTRAINT [PK_Books] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE TABLE [member].[Members] (
        [Id] uniqueidentifier NOT NULL,
        [Email] nvarchar(200) NOT NULL,
        [JoinDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Members] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE TABLE [Identity].[Permission] (
        [Id] uniqueidentifier NOT NULL,
        [Resource] nvarchar(120) NOT NULL,
        [Action] nvarchar(120) NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Permission] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE TABLE [Identity].[User] (
        [Id] nvarchar(450) NOT NULL,
        [FullName] nvarchar(50) NOT NULL,
        [UserName] nvarchar(256) NOT NULL,
        [Email] nvarchar(256) NULL,
        [PasswordHash] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(13) NULL,
        [Role] nvarchar(max) NOT NULL,
        [UserStatus] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE TABLE [Identity].[UserSession] (
        [Id] uniqueidentifier NOT NULL,
        [LoginTime] datetime2 NOT NULL,
        [LogoutTime] datetime2 NOT NULL,
        [UserName] nvarchar(450) NOT NULL,
        [UserId] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_UserSession] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE TABLE [book].[BookAuthor] (
        [BookId] uniqueidentifier NOT NULL,
        [AuthorId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_BookAuthor] PRIMARY KEY ([AuthorId], [BookId]),
        CONSTRAINT [FK_BookAuthor_Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [author].[Authors] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_BookAuthor_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [book].[Books] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE TABLE [borrow].[Borrowings] (
        [Id] uniqueidentifier NOT NULL,
        [BorrowDate] datetime2 NOT NULL,
        [ReturnDate] datetime2 NULL,
        [ReturnCode] uniqueidentifier NOT NULL,
        [MaxBorrowDay] int NOT NULL DEFAULT 14,
        [lateFee] int NULL,
        [TotalLateFee] int NULL,
        [BookId] uniqueidentifier NOT NULL,
        [MemberId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Borrowings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Borrowings_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [book].[Books] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Borrowings_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [member].[Members] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE TABLE [Identity].[RefreshToken] (
        [Id] uniqueidentifier NOT NULL,
        [Token] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [ExpireAt] datetime2 NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_RefreshToken] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RefreshToken_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE TABLE [Identity].[UserPermission] (
        [UserId] nvarchar(450) NOT NULL,
        [PermissionId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_UserPermission] PRIMARY KEY ([UserId], [PermissionId]),
        CONSTRAINT [FK_UserPermission_Permission_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Identity].[Permission] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UserPermission_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE INDEX [IX_BookAuthor_BookId] ON [book].[BookAuthor] ([BookId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE INDEX [IX_Borrowings_BookId] ON [borrow].[Borrowings] ([BookId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE INDEX [IX_Borrowings_MemberId] ON [borrow].[Borrowings] ([MemberId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE UNIQUE INDEX [IX_RefreshToken_UserId] ON [Identity].[RefreshToken] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE INDEX [IX_User_Email] ON [Identity].[User] ([Email]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE INDEX [IX_UserPermission_PermissionId] ON [Identity].[UserPermission] ([PermissionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE INDEX [IX_UserSession_LoginTime] ON [Identity].[UserSession] ([LoginTime]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE INDEX [IX_UserSession_LogoutTime] ON [Identity].[UserSession] ([LogoutTime]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE INDEX [IX_UserSession_UserName] ON [Identity].[UserSession] ([UserName]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE INDEX [IX_UserSession_UserName_LoginTime] ON [Identity].[UserSession] ([UserName], [LoginTime]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    CREATE INDEX [IX_UserSession_UserName_LogoutTime] ON [Identity].[UserSession] ([UserName], [LogoutTime]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260324122821_init'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260324122821_init', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260327133325_add_flag_refreshtoken'
)
BEGIN
    ALTER TABLE [Identity].[RefreshToken] ADD [IsRevoked] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260327133325_add_flag_refreshtoken'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260327133325_add_flag_refreshtoken', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [Identity].[UserSession] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [Identity].[UserSession] ADD [UpdatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [Identity].[UserPermission] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [Identity].[UserPermission] ADD [UpdatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [Identity].[User] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [Identity].[User] ADD [UpdatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [Identity].[Permission] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [Identity].[Permission] ADD [UpdatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [member].[Members] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [member].[Members] ADD [UpdatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [borrow].[Borrowings] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [borrow].[Borrowings] ADD [UpdatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [book].[Books] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [book].[Books] ADD [UpdatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [book].[BookAuthor] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [book].[BookAuthor] ADD [UpdatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [author].[Authors] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    ALTER TABLE [author].[Authors] ADD [UpdatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260429192311_usingBaseClass'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260429192311_usingBaseClass', N'9.0.0');
END;

COMMIT;
GO

