CREATE TABLE [dbo].[APIUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Password] [TEXT] NOT NULL,
	[Email] [varchar](300) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedById] [int] NULL,
	[Updated] [datetime] NULL,
	[UpdatedById] [int] NULL,
	CONSTRAINT PK_APIUser PRIMARY KEY ([Id]),
	FOREIGN KEY (CreatedById) REFERENCES [APIUser] (Id),
	FOREIGN KEY (UpdatedById) REFERENCES [APIUser] (Id),
) ON [PRIMARY]
GO

INSERT INTO APIUser (Username, Password, Email, Created) VALUES 
	('test',HashPassword('test123!'),'test@test.com',CURRENT_TIMESTAMP);
