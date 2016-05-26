CREATE TABLE [MegaFileStorage].[Files]
(
	[FileID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[OwnerID] [int] NOT NULL,
	[FileName] [nchar](60) NOT NULL,
	[Extension] [nchar](10) NULL,
	[Size] [int] NULL,
	[UploadDate] [datetime] NOT NULL,
	[Downloads] [int] NULL,
	[FullName] [nchar](200) NOT NULL,
	[AccessType] [int] NOT NULL,
	[ContentType] [nvarchar](100) 
)
