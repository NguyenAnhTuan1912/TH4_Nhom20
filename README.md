# Bài thục hành buổi 4 - Nhóm 20.
## Một số lưu ý nhỏ về bài tập buổi 4.
<p>Bài này tụi mình sẽ chia thành 3 database khác nhau (Để test tính năng), cho nên ở dòng cuối file .gitignore trong Project t có để 2 tên 2 file appsettings.json và appsettings.Development.json để không đẩy 2 thg này lên Github.</p>
<ul>
  <li>Thứ nhất là để tụi m có thể chính lại cái connectionString trong file đó tuỳ theo tên Sv trong SQL Server Management của tụi m.</li>
  <li>Thứ hai là để cho mỗi đứa tụi mình có thể xài Database ở các Local Server khác nhau.</li>
</ul>
<p>Trong file Query\TH4_SQLServer_Query.sql có một số câu lệnh insert dùng để thêm vào trong Database các dữ liệu cần để test. Một số câu lệnh Select.</p>
<h3>Nhớ đọc kĩ, có gì không hiểu thì hỏi.</h3>

# UPDATE: từ giờ bài thực hành buổi 4 sẽ nâng cấp thành bài thực hành buổi 6
<p>Bài thực hành buổi 4 là sự chuẩn bị (các chức năng, khái niệm về kết nối db) cho bài thực hành này.</p>
<h2>A. Một số thay đổi nhỏ như sau: </h2>
<ol>
  <li>Thay đổi lại mẫu dữ liệu của database và cũng như trong c#, cụ thể như sau:
    <ul>
      <li>THELOAI thành BRAND</li>
      <li>CHITIETTHELOAI thành CAMERA</li>
      <li>TheLoaiModel thành BrandModel</li>
      <li>ChiTietTheLoaiModel thành CameraModel</li>
      <li>ChiTietTheLoaiViewModel thành CameraDetailsViewModel</li>
    </ul>
  </li>
  <li>Thay đổi hành vi của một số Action trong Controller. Sửa lại chức năng Delele.</li>
  <li>Thay đổi View.</li>
  <li>Không in danh sách các thể loại nữa, thay vào đó sẽ in ra danh sách các máy ảnh.</li>
</ol>
<h2>B. Một số tính năng mới sẽ thêm trong tương tai: (Yêu cầu của bài thực hành 6)</h2>
<ol>
  <li>Phân quyền User, bao gồm: Admin và Customer</li>
  <li>Khi tạo thêm thông tin cho một máy ảnh mới, thì có chức năng tải ảnh lên Server.</li>
  <li>Hiện ra các modal box thông báo khi thực hiện một chức năng nào đó (Tạo, xoá, sửa).</li>
</ol>
<h2>TH06 v2.0</h2>
<h3>A. Các thay đổi</h3>
<ol>
  <li>Thay đổi lại hết các Model. Sửa các trường thuộc tính.</li>
  <li>Thay đổi các Action trong Comtroller.</li>
</ol>
<h3>B. Các tính năng được thêm vào</h3>
<ol>
  <li>*Đặc biệt* <strong>Thêm chức năng upload và lưu trữ ảnh.</strong></li>
  <li>Thêm vào một model mới ImageModel, đồng thời tương ứng với database là table IMAGE</li>
  <li>Thêm vào cửa số thông báo mỗi khi người dùng xoá một dữ liệu nào đó.</li>
  <li>Thêm vào datatable.</li>
</ol>
<h3>C. Các tính năng sẽ có trong phiên bản tới</h3>
<ol>
  <li>Chi ứng dụng thành 2 Area, một cho admin và một cho user.</li>
  <li>User được phép xem các mặt hàng và chi tiết mặt hàng. Còn Admin vẫn là các chức năng này (Admin coi như hoàn thành).</li>
</ol>
# UPDATE: Bài này sẽ thành bài Cuối kì
