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
-- 1. Xem tất cả dữ liệu trong Table PHIM
SELECT * FROM THELOAI

-- Sau khi USE PHIM thì insert dữ liệu, lúc đó mới có dữ liệu để mà in ra màn hình.
-- Insert một số dữ liệu.
-- Thêm các dữ liệu cơ bản vào trong table THELOAI
INSERT INTO THELOAI VALUES
(N'Hài hước', '2022/12/19'),
(N'Kinh dị', '2019/11/12'),
(N'Ma quỷ', '2021/05/19')