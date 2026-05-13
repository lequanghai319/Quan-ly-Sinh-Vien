# ⚽ Hệ Thống Quản Lý Câu Lạc Bộ Bóng Đá

> Đồ án môn học – Ứng dụng Windows Forms + SQL Server  
> Ngôn ngữ: C# | Database: SQL Server | IDE: Visual Studio

---

## 📋 Mô Tả Đề Bài

Viết chương trình quản lý doanh thu của các câu lạc bộ bóng đá với các thông tin:
- Tên câu lạc bộ
- Tên nước
- Số lượng vé
- Giá vé (theo bảng giá cố định của từng câu lạc bộ)

**Bảng giá vé mặc định:**

| Tên Câu Lạc Bộ | Giá Vé |
|----------------|--------|
| MANCHESTER     | 10     |
| PARISSG        | 12     |
| REALMADRIT     | 10     |
| BENFICA        | 7      |
| ACMILAN        | 12     |
| BARCELONA      | 10     |
| B.MUNICH       | 10     |
| JUVENTUS       | 11     |

---

## 🗂️ Cấu Trúc Dự Án

```
laptrinhwindows/
└── Quan-ly-clb-bong-da-ngoai-hang-Anh/
    │
    ├── Quan-ly-clb-bong-da-ngoai-hang-Anh.sln    # Solution file của toàn bộ dự án
    │
    ├── Quan-ly-clb-bong-da-ngoai-hang-Anh/       # Thư mục chứa Source Code C# chính
    │   ├── Forms/
    │   │   ├── frmDangNhap.cs                    # Giao diện Form đăng nhập
    │   │   ├── frmMain.cs                        # Giao diện Form chính (chứa menu điều hướng)
    │   │   ├── frmDanhSach.cs                    # Form hiển thị DataGridView và xử lý Thêm/Sửa/Xóa/Tìm kiếm
    │   │   └── frmThongKe.cs                     # Form lọc và thống kê các CLB có doanh thu > 90000
    │   │
    │   ├── Models/
    │   │   └── CauLacBo.cs                       # Class định nghĩa các thuộc tính của Câu Lạc Bộ (TenCLB, GiaVe,...)
    │   │
    │   ├── DataAccess/
    │   │   └── DatabaseHelper.cs                 # Class xử lý chuỗi kết nối (SqlConnection) và thực thi câu lệnh SQL
    │   │
    │   └── Program.cs                            # File gốc khởi chạy ứng dụng (sẽ gọi frmDangNhap chạy đầu tiên)
    │
    └── Database/
        └── QuanLyCLB.sql                         # File script chứa các câu lệnh CREATE TABLE và INSERT dữ liệu mẫu
```

---

## 🗄️ Cơ Sở Dữ Liệu (SQL Server)

### Tạo Database & Bảng

```sql
-- Tạo database
CREATE DATABASE QuanLyCauLacBo;
GO

USE QuanLyCauLacBo;
GO

-- Bảng tài khoản đăng nhập
CREATE TABLE TaiKhoan (
    ID       INT IDENTITY(1,1) PRIMARY KEY,
    TenDN    NVARCHAR(50)  NOT NULL,
    MatKhau  NVARCHAR(50)  NOT NULL,
    VaiTro   NVARCHAR(20)  DEFAULT 'admin'
);

-- Bảng câu lạc bộ
CREATE TABLE CauLacBo (
    ID          INT IDENTITY(1,1) PRIMARY KEY,
    TenCLB      NVARCHAR(100) NOT NULL,
    TenNuoc     NVARCHAR(100) NOT NULL,
    SoLuongVe   INT           NOT NULL DEFAULT 0,
    GiaVe       DECIMAL(18,2) NOT NULL DEFAULT 0
);

-- Dữ liệu mẫu tài khoản admin
INSERT INTO TaiKhoan (TenDN, MatKhau, VaiTro)
VALUES ('admin', '123456', 'admin');

-- Dữ liệu mẫu câu lạc bộ
INSERT INTO CauLacBo (TenCLB, TenNuoc, SoLuongVe, GiaVe) VALUES
(N'MANCHESTER',  N'Anh',    1000, 10),
(N'PARISSG',     N'Pháp',   800,  12),
(N'REALMADRIT',  N'Tây Ban Nha', 900, 10),
(N'BENFICA',     N'Bồ Đào Nha', 600, 7),
(N'ACMILAN',     N'Ý',      750,  12),
(N'BARCELONA',   N'Tây Ban Nha', 850, 10),
(N'B.MUNICH',    N'Đức',    700,  10),
(N'JUVENTUS',    N'Ý',      650,  11);
```

---

## 🔐 Chức Năng 1: Đăng Nhập Admin

### Mô tả
- Form đăng nhập xuất hiện đầu tiên khi chạy chương trình
- Chỉ admin mới được phép truy cập hệ thống
- Sai mật khẩu 3 lần thì thoát chương trình

### Giao diện
- TextBox: Tên đăng nhập
- TextBox: Mật khẩu (PasswordChar = `*`)
- Button: Đăng nhập
- Button: Thoát

### Code mẫu – `frmDangNhap.cs`
---

## ➕ Chức Năng 2: Thêm Câu Lạc Bộ

### Mô tả
- Nhập thông tin câu lạc bộ mới vào các ô TextBox
- Giá vé tự động điền theo tên câu lạc bộ nếu có trong danh sách mặc định
- Nhấn nút **Thêm** để lưu vào database
- Kiểm tra không được để trống, số lượng vé phải là số nguyên dương

### Các trường nhập liệu
| Trường | Kiểu dữ liệu | Ghi chú |
|--------|-------------|---------|
| Tên CLB | TextBox | Bắt buộc |
| Tên nước | TextBox | Bắt buộc |
| Số lượng vé | TextBox | Số nguyên > 0 |
| Giá vé | TextBox | Số thực > 0 |

### Code mẫu – Nút Thêm
---

## ✏️ Chức Năng 3: Sửa Câu Lạc Bộ

### Mô tả
- Chọn 1 dòng trên DataGridView → thông tin tự động điền vào form
- Chỉnh sửa thông tin cần thay đổi
- Nhấn nút **Lưu / Cập nhật** để ghi vào database
- Không cho phép sửa khi chưa chọn dòng nào

### Code mẫu – Chọn dòng trên DataGridView
### Code mẫu – Nút Sửa
---

## ❌ Chức Năng 4: Xóa Câu Lạc Bộ

### Mô tả
- Chọn dòng cần xóa trên DataGridView
- Nhấn nút **Xóa**
- Hiển thị hộp thoại xác nhận trước khi xóa
- Xóa khỏi database nếu người dùng đồng ý

### Code mẫu – Nút Xóa
---

## 🔍 Chức Năng 5: Tìm Kiếm Câu Lạc Bộ

### Mô tả
- Nhập tên câu lạc bộ (hoặc một phần tên) vào ô tìm kiếm
- Nhấn nút **Tìm kiếm** hoặc Enter
- Kết quả hiển thị trên DataGridView
- Nếu không tìm thấy thì thông báo "Không tìm thấy câu lạc bộ"

### Code mẫu – Tìm Kiếm
---

## 📊 Chức Năng 6: Thống Kê CLB Kinh Doanh Lãi

### Mô tả
- Tính doanh thu = Số lượng vé × Giá vé
- Lọc ra các câu lạc bộ có doanh thu **trên 90,000**
- Hiển thị danh sách và tổng số CLB kinh doanh lãi

### Code mẫu – Thống Kê
---
---

## 👥 Thành Viên Nhóm

| STT |    Họ và Tên    | MSSV | Công việc |
|-----|-----------------|------|-----------|
| 1   | Lê Quang Hải    |      |           |
| 2   | Phạm Trung Hiếu |      |           |
| 3   | Phạm Huy Hùng   |      |           |

---

## 📝 Ghi Chú
