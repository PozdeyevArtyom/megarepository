SELECT * FROM MegaFileStorage.Users

SELECT * FROM MegaFileStorage.Files

DELETE FROM MegaFileStorage.Users

DELETE FROM MegaFileStorage.Files

DELETE FROM MegaFileStorage.Access

SELECT UserName FROM MegaFileStorage.Users WHERE UserName = 'Admin' AND Pass = 'p@ssword'

INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [Size], [UploadDate], [Downloads], [FullName], [AccessType])
VALUES(1005, 'file1', 'txt', 23,  CONVERT(DATETIME, '20160524', 101), 3, 'Storage\Admin\root\file1.txt', 0)

SELECT f.FileID, f.OwnerID, f.[FileName], f.Extension, f.Size, f.UploadDate, f.Dowloads, f.FullName, f.AccessType 
FROM MegaFileStorage.Files as f, MegaFileStorage.Users as u
WHERE f.OwnerID = u.UserID AND u.UserName = 'Admin' AND f.[FileName] = 'file1'

INSERT INTO MegaFileStorage.Files ([OwnerID], [FileName], [Extension], [UploadDate], [FullName], [AccessType])
VALUES(1005, 'root', 'folder', CONVERT(DATETIME, '20160524', 101), 'Storage\Admin\root\file1.txt', 0)

UPDATE MegaFileStorage.Files SET FullName = 'User2\root' WHERE FullName = 'Storage\User2\root'
