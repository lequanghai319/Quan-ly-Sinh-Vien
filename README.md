# 🎓 Hệ Thống Quản Lý Sinh Viên

> Đồ án môn Lập trình Windows – Nhóm sinh viên năm 2  
> Ngôn ngữ: **C#** | Database: **SQL Server** | IDE: **Visual Studio** | Giao diện: **Windows Forms**

---

## 📋 Mô Tả Đề Bài

Xây dựng ứng dụng desktop quản lý sinh viên bằng C# Windows Forms, kết nối cơ sở dữ liệu SQL Server. Chương trình cho phép admin đăng nhập, quản lý thông tin sinh viên, điểm số, khoá học và xem thống kê trực quan.

---

## 👥 Phân Công Công Việc

| Thành viên | MSSV | Phụ trách |
|---|---|---|
| Phạm Trung Hiếu | | Form Đăng nhập · Form Quản lý sinh viên · Form Thống kê sinh viên |
| Phạm Huy Hùng | | Form Quản lý điểm · Form Thống kê điểm trung bình môn |
| Lê Quang Hải | | Form Quản lý khoá học · Form In danh sách khoá học |

---

## 🗂️ Các Chức Năng Chính

### 1. Form Đăng Nhập *(Phạm Trung Hiếu)*
Màn hình đầu tiên khi khởi động chương trình. Admin nhập tên đăng nhập và mật khẩu để truy cập hệ thống.
- Kiểm tra thông tin đăng nhập với cơ sở dữ liệu SQL Server
- Mật khẩu được ẩn ký tự để bảo mật
- Đăng nhập thành công → mở Form Quản lý sinh viên
- Đăng nhập thất bại → hiển thị thông báo lỗi
- Nút Làm mới: xoá trắng các ô, đưa con trỏ về ô tên đăng nhập
- Nút Thoát: đóng chương trình

---

### 2. Form Quản Lý Sinh Viên *(Phạm Trung Hiếu)*
Form trung tâm của hệ thống, cho phép thao tác đầy đủ với dữ liệu sinh viên. Giao diện chia hai vùng: khu vực nhập liệu bên trái, danh sách DataGridView bên phải.
- **Thêm** sinh viên mới vào cơ sở dữ liệu
- **Sửa** thông tin sinh viên đã có (chọn dòng trên bảng → thông tin tự điền vào form → chỉnh sửa → lưu)
- **Xoá** sinh viên (có xác nhận Yes/No trước khi xoá)
- **Tìm kiếm** theo tên hoặc mã sinh viên
- **Làm mới** xoá trắng toàn bộ ô nhập liệu
- Dữ liệu lưu và đọc từ SQL Server

---

### 3. Form Thống Kê Sinh Viên *(Phạm Trung Hiếu)*
Hiển thị thống kê tổng quan về sinh viên trong hệ thống dưới dạng trực quan, giao diện chia 3 ô rõ ràng.
- Tự động kết nối và truy vấn dữ liệu khi form mở
- Hiển thị **tổng số sinh viên** hiện có
- Tính và hiển thị **tỉ lệ % Nam** = (Số Nam / Tổng) × 100, làm tròn 2 chữ số thập phân
- Tính và hiển thị **tỉ lệ % Nữ** = (Số Nữ / Tổng) × 100, làm tròn 2 chữ số thập phân
- Xử lý trường hợp chưa có sinh viên (tổng = 0): hiển thị 0.00% thay vì báo lỗi
- Tự động cập nhật khi dữ liệu thay đổi từ Form Quản lý sinh viên

---

### 4. Form Quản Lý Điểm *(Phạm Huy Hùng)*
Cho phép nhập và quản lý điểm của sinh viên theo từng môn học. Giao diện chia đôi: khu vực nhập liệu bên trái, bảng hiển thị điểm bên phải.
- Chọn sinh viên và môn học để nhập điểm
- Nhập các đầu điểm: **Chuyên cần**, **Giữa kỳ**, **Cuối kỳ**
- Tự động tính **điểm trung bình** theo công thức:
  > Điểm TB = (Chuyên cần × 0.4) + (Cuối kỳ × 0.6)
- Xoá bản ghi điểm của sinh viên
- Ràng buộc: điểm phải từ 0 đến 10, không được để trống các trường bắt buộc

---

### 5. Form Thống Kê Điểm Trung Bình Môn *(Phạm Huy Hùng)*
Hiển thị điểm trung bình của từng môn học trong toàn hệ thống dưới dạng bảng.
- Tự động tính điểm trung bình của mỗi môn học từ dữ liệu điểm đã nhập
- Hiển thị dưới dạng bảng gồm 2 cột: **Tên môn học** và **Điểm trung bình**
- Bảng tự co giãn theo kích thước form

---

### 6. Form Quản Lý Khoá Học *(Quang Hải)*
Quản lý danh sách khoá học trong hệ thống. Giao diện dùng SplitContainer chia đôi: nhập liệu bên trái, DataGridView bên phải.

Các trường thông tin: Mã khoá học, Tên khoá học, Số tín chỉ (NumericUpDown), Học kỳ (ComboBox).
- **Thêm** khoá học mới: kiểm tra không để trống → INSERT vào SQL Server
- **Sửa** khoá học: click dòng trên bảng → thông tin tự điền vào form → chỉnh sửa → UPDATE (ô Mã khoá học khoá, không cho sửa)
- **Xoá** khoá học: xác nhận Yes/No → DELETE theo mã
- **Tìm kiếm** theo tên khoá học bằng câu lệnh LIKE
- **Làm mới** xoá trắng các ô nhập, đưa ComboBox về mặc định
- Tự động tải toàn bộ danh sách lên DataGridView khi mở form

---

### 7. Form In Danh Sách Khoá Học *(Quang Hải)*
Cho phép lọc và xuất danh sách khoá học ra file Excel. Giao diện chia 3 vùng theo chiều dọc: bộ lọc phía trên, bảng kết quả ở giữa, nút chức năng phía dưới.
- **Lọc dữ liệu** theo Học kỳ cụ thể hoặc chọn "Tất cả"
- **Xuất Excel**: tạo file Excel mới, ghi dữ liệu từ DataGridView vào các ô tương ứng
- **Lưu file**: dùng SaveFileDialog để người dùng chọn đường dẫn lưu trên máy tính
- Tự động truy vấn SQL Server theo bộ lọc và đổ kết quả lên bảng theo thời gian thực
- Nút **Đóng** để quay lại form trước

---

## 🗄️ Cơ Sở Dữ Liệu

Hệ thống sử dụng SQL Server với các bảng chính:

| Bảng | Mô tả |
|---|---|
| `TaiKhoan` | Lưu tài khoản đăng nhập của admin |
| `SinhVien` | Thông tin sinh viên (mã, họ tên, giới tính, ngày sinh, ...) |
| `KhoaHoc` | Danh sách khoá học (mã, tên, số tín chỉ, học kỳ) |
| `Diem` | Điểm của sinh viên theo từng môn (chuyên cần, giữa kỳ, cuối kỳ, TB) |

---

## 🚀 Hướng Dẫn Cài Đặt

**Yêu cầu:** Visual Studio 2019/2022 · SQL Server Express · .NET Framework 4.7+

1. Mở **SQL Server Management Studio**, chạy file `Database/QuanLySinhVien.sql` để tạo database và dữ liệu mẫu
2. Mở file `QuanLySinhVien.sln` bằng Visual Studio
3. Trong file `DatabaseHelper.cs`, sửa `connectionString` cho đúng tên SQL Server instance trên máy bạn
4. Nhấn `F5` để chạy chương trình
5. Đăng nhập mặc định: **Tên đăng nhập:** `admin` · **Mật khẩu:** `123456`

---

## 📁 Cấu Trúc Dự Án

```
QuanLySinhVien/
│
├── Forms/
│   ├── frmDangNhap.cs          # Form đăng nhập
│   ├── frmQuanLySinhVien.cs    # Form quản lý sinh viên
│   ├── frmThongKeSinhVien.cs   # Form thống kê sinh viên
│   ├── frmQuanLyDiem.cs        # Form quản lý điểm
│   ├── frmThongKeDiem.cs       # Form thống kê điểm TB môn
│   ├── frmQuanLyKhoaHoc.cs     # Form quản lý khoá học
│   └── frmInKhoaHoc.cs         # Form in danh sách khoá học
│
├── DataAccess/
│   └── DatabaseHelper.cs       # Kết nối và truy vấn SQL Server
│
├── Database/
│   └── QuanLySinhVien.sql      # Script tạo database
│
└── Program.cs
```

---