CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Password] [TEXT] NOT NULL,
	[Email] [varchar](300) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedById] [int] NULL,
	CONSTRAINT PK_User PRIMARY KEY ([Id]),
	FOREIGN KEY (CreatedById) REFERENCES [User] (Id),
	FOREIGN KEY (UpdatedById) REFERENCES [User] (Id),
) ON [PRIMARY]
GO

