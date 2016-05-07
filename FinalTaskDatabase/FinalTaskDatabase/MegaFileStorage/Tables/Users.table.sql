CREATE TABLE [MegaFileStorage].[Users]
(
	[UserID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UserName] [nchar](40) NOT NULL,
	[Pass] [nchar](40) NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
	[UserType] [int] NOT NULL,
	[Email] [nchar](60) NOT NULL
)
