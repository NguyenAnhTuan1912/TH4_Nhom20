/*
***** LƯU Ý: Trước khi thực thi các câu lệnh trong file này, thì phải thực hiện việc tạo database trong Visual Studio
Vào trong Tools > NuGet Package Manager > Package Manager Console
Gõ lệnh: update-database
Lệnh này sẽ kiểm tra xem có Database trong Server chưa, nếu chưa có thì nó sẽ tạo, ngược lại thì nó chỉ cập nhật thôi.
Sau khi chạy xong thì mới dùng được nhé!!!
*****
*/
-- Chạy dòng lệnh này đầu tiên, nếu nó báo lỗi.
USE PHIM
/*
	Database 'PHIM' does not exist. Make sure that the name is entered correctly.
*/
-- Lỗi này có nghĩa là nó không tìm thấy database tên là 'PHIM'.
-- Thì có nghĩa là ASP.NET chưa tạo Database thành công, nhập sai tên database, hoặc tên của database được dùng để thực
-- thi trong SQL Server Management khác với tên database trong connection string, tại sao thì lưu ý trên đã nói.

-- Xem dữ liệu trong table THELOAI của database PHIM
-- 1. Xem tất cả dữ liệu trong Table THELOAI
SELECT * FROM THELOAI
-- MỚI: xem tất cả dữ liệu trong Table CHITIETTHELOAI
SELECT * FROM CHITIETTHELOAI

-- MỚI: xem tất chi tiết tất cả các thể loại
SELECT THELOAI.Id, THELOAI.Name, CHITIETTHELOAI.Description, THELOAI.DateCreated FROM THELOAI
INNER JOIN CHITIETTHELOAI ON CHITIETTHELOAI.CategoryId = THELOAI.Id

-- DANGER ZONE --
/*
	Nếu có lỡ tay xoá thì xoá hẳn cả 2 Table này luôn. Rồi chạy lại lệnh update-database.
*/
-- Drop Table
DROP TABLE THELOAI
DROP TABLE CHITIETTHELOAI

-- Xoá dữ liệu
DECLARE @id as SMALLINT = 9
DELETE FROM THELOAI WHERE Id = @id
DELETE FROM CHITIETTHELOAI WHERE Id = @id

-- Cập nhật lại Id cho table
DECLARE @id as SMALLINT = 7
DBCC CHECKIDENT ('THELOAI', RESEED, @id)
DBCC CHECKIDENT ('CHITIETTHELOAI', RESEED, @id)
-----------------

-- Sau khi USE PHIM thì insert dữ liệu, lúc đó mới có dữ liệu để mà in ra màn hình.
-- Insert một số dữ liệu.

-- Thêm các dữ liệu cơ bản vào trong table THELOAI
INSERT INTO THELOAI VALUES
(N'Hài hước', '2022/12/19'),
(N'Kinh dị', '2019/11/12'),
(N'Ma quỷ', '2021/05/19')

-- Thêm các dữ liệu cơ bản vào trong table CHITIETTHELOAI
INSERT INTO CHITIETTHELOAI VALUES
(1, N'Sẽ ra sao nếu những tình huống trớ trêu hay hài hước được ghi hình lại một cách chuyên nghiệp? Những bộ phim hài hước sẽ mang lại những điều đó cho chúng ta. Cùng sống trong những giờ phút giải trí, vui nhộn cùng với các nhân vật trong phim và tận hưởng niềm vui bên ngoài đời sống thật.', 22),
(2, N'Kinh dị là một trong những đề tài luôn hấp dẫn tới thị hiếu của chúng ta. Những thứ kinh dị luôn cho mnang cho chúng ta cảm giác sợ hãi và phấn khích. Những cảm giác trên lại còn tuyệt vời hơn nữa trong các bộ phim kinh dị, chúng ta có cảm giác như được sống cùng với các nhân vật trong phim ở cùng bối cảnh đó, đẩy cao sự thích thú và hưng phấn cùng với nỗi sợ hãi và lo lắng tột độ.', 23),
(3, N'Tâm linh luôn là một đề tài bí ẩn và thu hút, nhưng người ta rất hạn chế tiếp xúc đến tâm linh vì không muốn gặp những điều chẳng lành. Những bộ phim kinh dị mang đến cho chúng ta một cái nhìn tổng quát hơn về tâm linh, và đào sâu vào tận nỗi sợ của con người. Sợ hãi, lo lắng, bồn chồn và ngột ngạt.', 41)