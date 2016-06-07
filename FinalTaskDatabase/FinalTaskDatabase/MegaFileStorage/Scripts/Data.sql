DECLARE @OwnerID INT
DECLARE @ParID INT
DECLARE @ChID INT

/*---------------------------------Admin---------------------------------*/
/*регистрация*/
INSERT INTO [MegaFileStorage].Users ([UserName], [Pass], [RegistrationDate], [UserType], [Email]) 
VALUES ('Admin', 'p@ssword', CONVERT(DATETIME, '20160524', 101), 0, 'admin@mail.ru')
SELECT  @OwnerID=UserID FROM MegaFileStorage.Users WHERE UserName='Admin'

/*корневой каталог*/
INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'Admin', 'folder', 0, CONVERT(DATETIME, '20160524', 101), 'Admin', 0)
SELECT @ParID = FileID FROM MegaFileStorage.Files WHERE FullName='Admin'

/*содержимое корневого каталога*/
INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'root', 'folder', 0, CONVERT(DATETIME, '20160524', 101), 'Admin\root', 0)
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='Admin\root'

INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

/*---------------------------------User1---------------------------------*/
/*регистрация*/
INSERT INTO [MegaFileStorage].Users ([UserName], [Pass], [RegistrationDate], [UserType], [Email]) 
VALUES ('User1', 'qwe123', CONVERT(DATETIME, '20160524', 101), 1, 'user1@mail.ru')
SELECT  @OwnerID=UserID FROM MegaFileStorage.Users WHERE UserName='User1'

/*корневой каталог*/
INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'User1', 'folder', 0, CONVERT(DATETIME, '20160524', 101), 'User1', 0)
SELECT @ParID = FileID FROM MegaFileStorage.Files WHERE FullName='User1'

/*содержимое корневого каталога*/
INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'root', 'folder', 0, CONVERT(DATETIME, '20160524', 101), 'User1\root', 0)
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='User1\root'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)


/*---------------------------------MegaUser---------------------------------*/
/*регистрация*/
INSERT INTO [MegaFileStorage].Users ([UserName], [Pass], [RegistrationDate], [UserType], [Email]) 
VALUES ('MegaUser', 'qwe123', CONVERT(DATETIME, '20160524', 101), 1, 'superuser@mail.ru')
SELECT  @OwnerID=UserID FROM MegaFileStorage.Users WHERE UserName='MegaUser'

/*корневой каталог*/
INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'MegaUser', 'folder', 568671, CONVERT(DATETIME, '20160524', 101), 'MegaUser', 0)
SELECT @ParID = FileID FROM MegaFileStorage.Files WHERE FullName='MegaUser'

/*содержимое корневого каталога*/
INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'root', 'folder', 568671, CONVERT(DATETIME, '20160524', 101), 'MegaUser\root', 0)
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='MegaUser\root'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

/*содержимое папки root*/
SET @ParID = @ChID

INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'Картинки', 'folder', 82438, CONVERT(DATETIME, '20160524', 101), 'MegaUser\root\Картинки', 0)
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='MegaUser\root\Картинки'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'Файлы', 'folder', 486233, CONVERT(DATETIME, '20160524', 101), 'MegaUser\root\Файлы', 0)
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='MegaUser\root\Файлы'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

/*содержимое папки Картинки*/
SELECT @ParID = FileID FROM MegaFileStorage.Files WHERE FullName='MegaUser\root\Картинки'

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, 'cross.png', '.png', 31426, CONVERT(DATETIME, '20160524', 101), 1, 
'MegaUser\root\Картинки\cross.png', 2, 'image/png')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='MegaUser\root\Картинки\cross.png'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, 'TX2Idu2lbaw.jpg', '.jpg', 114287, CONVERT(DATETIME, '20160524', 101), 0, 
'MegaUser\root\Картинки\TX2Idu2lbaw.jpg', 0, 'image/jpg')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='MegaUser\root\Картинки\TX2Idu2lbaw.jpg'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, 'saveicon.png', '.png', 49255, CONVERT(DATETIME, '20160524', 101), 0, 
'MegaUser\root\Картинки\saveicon.png', 1, 'image/png')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='MegaUser\root\Картинки\saveicon.png'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

/*содержимое папки Файлы*/
SELECT @ParID = FileID FROM MegaFileStorage.Files WHERE FullName='MegaUser\root\Файлы'

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, 'Addition.pptx', '.pptx', 114287, CONVERT(DATETIME, '20160524', 101), 0, 
'MegaUser\root\Файлы\Addition.pptx', 2, 'application/vnd.openxmlformats-officedocument.presentationml.presentation')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='MegaUser\root\Файлы\Addition.pptx'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, 'Задание 14.pdf', '.pdf', 371946, CONVERT(DATETIME, '20160524', 101), 0, 
'MegaUser\root\Файлы\Задание 14.pdf', 0, 'application/rtf')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='MegaUser\root\Файлы\Задание 14.pdf'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)




/*---------------------------------SuperUser---------------------------------*/
/*регистрация*/
INSERT INTO [MegaFileStorage].Users ([UserName], [Pass], [RegistrationDate], [UserType], [Email]) 
VALUES ('SuperUser', 'qwe123', CONVERT(DATETIME, '20160524', 101), 1, 'superpuperuser@mail.ru')
SELECT  @OwnerID=UserID FROM MegaFileStorage.Users WHERE UserName='SuperUser'

/*корневой каталог*/
INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'SuperUser', 'folder', 17748091, CONVERT(DATETIME, '20160524', 101), 'SuperUser', 0)
SELECT @ParID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser'

/*содержимое корневого каталога*/
INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'root', 'folder', 17748091, CONVERT(DATETIME, '20160524', 101), 'SuperUser\root', 0)
SELECT ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

/*содержимое папки root*/
SET @ParID = @ChID

INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'Файлы', 'folder', 1004751, CONVERT(DATETIME, '20160524', 101), 'SuperUser\root\Файлы', 0)
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Файлы'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'Картинки', 'folder', 2532691, CONVERT(DATETIME, '20160524', 101), 'SuperUser\root\Картинки', 0)
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Картинки'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [FullName], [AccessType])
VALUES(@OwnerID, 'Музыка', 'folder', 14210649, CONVERT(DATETIME, '20160524', 101), 'SuperUser\root\Музыка', 0)
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Музыка'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

/*содержимое папки Файлы*/
SELECT @ParID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Файлы'

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, 'Вопросы на зачет.pdf', '.pdf', 58546, CONVERT(DATETIME, '20160524', 101), 0, 
'SuperUser\root\Файлы\Вопросы на зачет.pdf', 0, 'application/rtf')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Файлы\Вопросы на зачет.pdf'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, 'Задание 13.pdf', '.pdf', 282160, CONVERT(DATETIME, '20160524', 101), 0, 
'SuperUser\root\Файлы\Задание 13.pdf', 0, 'application/rtf')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Файлы\Задание 13.pdf'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, 'Задание 14.pdf', '.pdf', 371946, CONVERT(DATETIME, '20160524', 101), 0, 
'SuperUser\root\Файлы\Задание 14.pdf', 0, 'application/rtf')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Файлы\Задание 14.pdf'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, 'Требования к финальным проектам.pdf', '.pdf', 292099, CONVERT(DATETIME, '20160524', 101), 0, 
'SuperUser\root\Файлы\Требования к финальным проектам.pdf', 0, 'application/rtf')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Файлы\Требования к финальным проектам.pdf'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

/*содержимое папки Картинки*/
SELECT @ParID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Картинки'

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, '31.jpg', '.jpg', 997024, CONVERT(DATETIME, '20160524', 101), 0, 
'SuperUser\root\Картинки\31.jpg', 0, 'image/jpeg')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Картинки\31.jpg'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, '32.jpg', '.jpg', 763172, CONVERT(DATETIME, '20160524', 101), 0, 
'SuperUser\root\Картинки\32.jpg', 0, 'image/jpeg')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Картинки\31.jpg'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, '40.jpg', '.jpg', 772495, CONVERT(DATETIME, '20160524', 101), 0, 
'SuperUser\root\Картинки\40.jpg', 0, 'image/jpeg')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Картинки\31.jpg'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

/*содержимое папки Музыка*/
SELECT @ParID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Музыка'

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, '3rd_strike_-_flow_heat.mp3', '.mp3', 3011105, CONVERT(DATETIME, '20160524', 101), 0, 
'SuperUser\root\Музыка\3rd_strike_-_flow_heat.mp3', 1, 'audio/mpeg')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Музыка\3rd_strike_-_flow_heat.mp3'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, 'Alice Cooper - Love is a loaded gun.mp3', '.mp3', 6031777, CONVERT(DATETIME, '20160524', 101), 0, 
'SuperUser\root\Музыка\Alice Cooper - Love is a loaded gun.mp3', 1, 'audio/mpeg')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Музыка\Alice Cooper - Love is a loaded gun.mp3'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)

INSERT INTO MegaFileStorage.Files([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], 
[AccessType], [ContentType])
VALUES(@OwnerID, 'Evanescence - Going under.mp3', '.mp3', 5167767, CONVERT(DATETIME, '20160524', 101), 1, 
'SuperUser\root\Музыка\Evanescence - Going under.mp3', 1, 'audio/mpeg')
SELECT @ChID = FileID FROM MegaFileStorage.Files WHERE FullName='SuperUser\root\Музыка\Evanescence - Going under.mp3'
INSERT INTO MegaFileStorage.Folders ([ChID], [ParID]) VALUES(@ChID, @ParID)
