/*
***** LƯU Ý: Trước khi thực thi các câu lệnh trong file này, thì phải thực hiện việc tạo database trong Visual Studio
Vào trong Tools > NuGet Package Manager > Package Manager Console
Gõ lệnh: update-database
Lệnh này sẽ kiểm tra xem có Database trong Server chưa, nếu chưa có thì nó sẽ tạo, ngược lại thì nó chỉ cập nhật thôi.
Sau khi chạy xong thì mới dùng được nhé!!!
*****
*/
-- Chạy dòng lệnh này đầu tiên, nếu nó báo lỗi.
USE CAMERA
/*
	Database 'PHIM' does not exist. Make sure that the name is entered correctly.
*/
-- Lỗi này có nghĩa là nó không tìm thấy database tên là 'PHIM'.
-- Thì có nghĩa là ASP.NET chưa tạo Database thành công, nhập sai tên database, hoặc tên của database được dùng để thực
-- thi trong SQL Server Management khác với tên database trong connection string, tại sao thì lưu ý trên đã nói.

-- Xem dữ liệu trong table BRAND của database PHIM
-- 1. Xem tất cả dữ liệu trong Table BRAND
SELECT * FROM BRAND
-- xem tất cả dữ liệu trong Database
SELECT * FROM CAMERA
SELECT * FROM IMAGE
SELECT * FROM AspNetUsers
SELECT * FROM AspNetRoles
SELECT * FROM AspNetUserRoles
SELECT * FROM ORDERDETAILS
SELECT * FROM CART
SELECT * FROM [ORDER]
SELECT * FROM AspNetUsers
DELETE FROM IMAGE

-- Xem tất chi tiết tất cả các thể loại
SELECT BRAND.Id, BRAND.Name, CAMERA.Description, BRAND.DateCreated FROM BRAND
INNER JOIN CAMERA ON CAMERA.CategoryId = BRAND.Id

-- MỚI: Reset ID tự động về 1
DECLARE @name as varchar(30) = 'IMAGE'
PRINT @name
TRUNCATE TABLE IMAGE DBCC CHECKIDENT (IMAGE, RESEED, 1)


-- Sau khi USE PHIM thì insert dữ liệu, lúc đó mới có dữ liệu để mà in ra màn hình.
-- Insert một số dữ liệu.

-- Thêm các dữ liệu cơ bản vào trong table BRAND
INSERT INTO BRAND VALUES
('Canon', 'Compact;DSLR;Mirrorless'),
('Nikon', 'Compact;DSLR;Mirrorless'),
('Sony', 'Compact;DSLR;Mirrorless')

-- Thêm các dữ liệu cơ bản vào trong table CAMERA







-- DANGER ZONE --
/*
	Nếu có lỡ tay xoá thì xoá hẳn cả 2 Table này luôn. Rồi chạy lại lệnh update-database.
*/
-- Drop Table
DROP TABLE BRAND
DROP TABLE CAMERA

-- Xoá dữ liệu
DECLARE @id as SMALLINT = 4
DELETE FROM IMAGE WHERE Id = @id
DELETE FROM CAMERA WHERE Id = @id
DELETE FROM BRAND WHERE Id = @id

-- Cập nhật lại Id cho table
DECLARE @id as SMALLINT = 7
DBCC CHECKIDENT ('BRAND', RESEED, @id)
DBCC CHECKIDENT ('CAMERA', RESEED, @id)
-----------------