CREATE TABLE [dbo].[BikeRoute](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartureTime] [datetime] NOT NULL,
	[ReturnTime] [datetime] NOT NULL,
	[DepartureStationId] [int] NOT NULL,
	[DepartureStationName] [varchar](300) NOT NULL,
	[ReturnStationId] [int] NOT NULL,
	[ReturnStationName] [varchar](300) NOT NULL,
	[CoveredDistanceInMeters] [int] NOT NULL,
	[DurationInSeconds] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedById] [int] NULL,
	CONSTRAINT PK_BikeRoute PRIMARY KEY ([Id]),
	FOREIGN KEY (CreatedById) REFERENCES [User] (Id),
	FOREIGN KEY (UpdatedById) REFERENCES [User] (Id),
) ON [PRIMARY]
GO

