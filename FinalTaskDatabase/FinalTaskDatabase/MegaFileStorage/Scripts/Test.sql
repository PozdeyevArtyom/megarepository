SELECT * FROM MegaFileStorage.Users

SELECT * FROM MegaFileStorage.Files WHERE OwnerID = 8005
SELECT * FROM MegaFileStorage.Files WHERE FileID = 2031

SELECT * FROM MegaFileStorage.Folders WHERE ChID = 2030 OR ParID = 2030

SELECT * FROM MegaFileStorage.Access WHERE FileID = 2027
/*
UPDATE MegaFileStorage.Files SET Size = 0 WHERE Size IS NULL

DELETE FROM MegaFileStorage.Users WHERE UserID = 7008

DELETE FROM MegaFileStorage.Files WHERE FileID = 8010

DELETE FROM MegaFileStorage.Folders WHERE ParID = 8011
DELETE FROM MegaFileStorage.Access

DELETE FROM MegaFileStorage.Users WHERE UserName = 'MegaUser'

DELETE FROM MegaFileStorage.Files WHERE FileID = 3009
DELETE FROM MegaFileStorage.Files WHERE OwnerID = 3015
DELETE FROM MegaFileStorage.Access WHERE AccessID = 6

DELETE FROM MegaFileStorage.Folders WHERE ChID = 3009

SELECT UserName FROM MegaFileStorage.Users WHERE UserName = 'Admin' AND Pass = 'p@ssword'

INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], [AccessType])
VALUES(1005, 'file1', 'txt', 23,  CONVERT(DATETIME, '20160524', 101), 3, 'Storage\Admin\root\file1.txt', 0)

SELECT f.FileID, f.OwnerID, f.[FileName], f.Extension, f.Size, f.UploadDate, f.Dowloads, f.FullName, f.AccessType 
FROM MegaFileStorage.Files as f, MegaFileStorage.Users as u
WHERE f.OwnerID = u.UserID AND u.UserName = 'Admin' AND f.[FileName] = 'file1'

INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [UploadDate], [FullName], [AccessType])
VALUES(2006, 'User1', 'folder', CONVERT(DATETIME, '20160512', 101), 'Storage\User1', 0)

UPDATE MegaFileStorage.Files SET ContentType ='application/vnd.openxmlformats-officedocument.presentationml.presentation' WHERE FileID = 4003

*/