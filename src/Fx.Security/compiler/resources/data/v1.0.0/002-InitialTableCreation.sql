﻿CREATE TABLE sec.[User]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[Created] DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
	[CreatedUserId] INT NOT NULL,
	[Deleted] BIT NOT NULL DEFAULT 0,
	[Enabled] BIT NOT NULL DEFAULT 1,
	[Hidden] BIT NOT NULL DEFAULT 0,
	[ReadOnly] BIT NOT NULL DEFAULT 0,
	[Updated] DATETIMEOFFSET NULL,
	[UpdatedUserId] INT NULL,

	[Conventions] VARCHAR(500) NULL,
	[Email] NVARCHAR(1000) NULL,
	[Forename] NVARCHAR(1000) NULL,
	[Surname] NVARCHAR(1000) NULL,
	[Password] VARBINARY(255) NULL,
	[Username] NVARCHAR(500) NOT NULL

	CONSTRAINT PK_sec_User PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT FK_sec_User_CreatedUserId FOREIGN KEY ([CreatedUserId]) REFERENCES sec.[User] ([Id]),
	CONSTRAINT FK_sec_User_UpdatedUserId FOREIGN KEY ([UpdatedUserId]) REFERENCES sec.[User] ([Id])
)