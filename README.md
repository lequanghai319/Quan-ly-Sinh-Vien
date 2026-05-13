# ⚽ HỆ THỐNG QUẢN LÝ CÂU LẠC BỘ BÓNG ĐÁ

> **Phiên bản:** 1.0.0 | **Cơ sở dữ liệu:** SQL Server | **Ngôn ngữ:** C# / WinForms hoặc ASP.NET

---

## 📋 MỤC LỤC

- [Tổng quan hệ thống](#tổng-quan-hệ-thống)
- [Công nghệ sử dụng](#công-nghệ-sử-dụng)
- [Cơ sở dữ liệu](#cơ-sở-dữ-liệu)
- [Chức năng đăng nhập](#1-chức-năng-đăng-nhập)
- [Quản lý cầu thủ](#2-quản-lý-cầu-thủ)
- [Quản lý đội bóng](#3-quản-lý-đội-bóng)
- [Quản lý huấn luyện viên](#4-quản-lý-huấn-luyện-viên)
- [Quản lý trận đấu](#5-quản-lý-trận-đấu)
- [Quản lý giải đấu](#6-quản-lý-giải-đấu)
- [Quản lý hợp đồng](#7-quản-lý-hợp-đồng)
- [Quản lý tài chính](#8-quản-lý-tài-chính)
- [Báo cáo & Thống kê](#9-báo-cáo--thống-kê)
- [Quản lý tài khoản người dùng](#10-quản-lý-tài-khoản-người-dùng)
- [Hướng dẫn cài đặt](#hướng-dẫn-cài-đặt)

---

## 🏟️ Tổng quan hệ thống

Hệ thống **Quản lý Câu lạc bộ Bóng đá** là ứng dụng quản lý toàn diện giúp các câu lạc bộ bóng đá theo dõi và điều hành mọi hoạt động bao gồm: quản lý cầu thủ, đội bóng, trận đấu, hợp đồng, tài chính và báo cáo thống kê.

### Đối tượng sử dụng
| Vai trò | Quyền hạn |
|--------|-----------|
| Admin | Toàn quyền truy cập, quản lý người dùng |
| Quản lý CLB | Xem, thêm, sửa, xoá tất cả dữ liệu CLB |
| Nhân viên | Xem và cập nhật dữ liệu được phân quyền |
| Khách / Chỉ đọc | Chỉ xem thông tin công khai |

---

## 🛠️ Công nghệ sử dụng

```
- Ngôn ngữ     : C# (.NET Framework / .NET 6+)
- Giao diện    : Windows Forms (WinForms) hoặc ASP.NET MVC
- Cơ sở dữ liệu: Microsoft SQL Server 2019+
- ORM / Query  : ADO.NET hoặc Entity Framework Core
- Báo cáo      : Crystal Reports / RDLC
- Version ctrl : Git + GitHub
```

---

## 🗄️ Cơ sở dữ liệu

### Sơ đồ bảng chính

```
TaiKhoan          CauThu              DoiBong
---------         --------            --------
MaTK (PK)         MaCauThu (PK)       MaDoi (PK)
TenDangNhap       HoTen               TenDoi
MatKhau           NgaySinh            Logo
VaiTro            ViTri               NamThanhLap
TrangThai         SoAo                SanVan Dong
                  MaDoi (FK)          MaHLV (FK)
                  QuocTich
                  ChieuCao / CanNang

HopDong           TranDau             GiaiDau
--------          --------            --------
MaHopDong (PK)    MaTran (PK)         MaGiai (PK)
MaCauThu (FK)     MaDoi_Nha (FK)      TenGiai
MaDoi (FK)        MaDoi_Khach (FK)    MoTa
NgayBatDau        NgayThi             NamToChuc
NgayKetThuc       DiaDiem
LuongThang        BanThang_Nha
ThuongPhat        BanThang_Khach
```

### Script tạo Database

```sql
CREATE DATABASE QuanLyCLBBongDa;
GO

USE QuanLyCLBBongDa;
GO

-- Bảng Vai trò
CREATE TABLE VaiTro (
    MaVaiTro    INT PRIMARY KEY IDENTITY(1,1),
    TenVaiTro   NVARCHAR(50) NOT NULL,
    MoTa        NVARCHAR(200)
);

-- Bảng Tài khoản
CREATE TABLE TaiKhoan (
    MaTK            INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap     VARCHAR(50) NOT NULL UNIQUE,
    MatKhau         VARCHAR(255) NOT NULL,   -- Lưu dạng hash (SHA256/bcrypt)
    HoTen           NVARCHAR(100),
    Email           VARCHAR(100),
    MaVaiTro        INT FOREIGN KEY REFERENCES VaiTro(MaVaiTro),
    TrangThai       BIT DEFAULT 1,           -- 1: Hoạt động, 0: Khoá
    NgayTao         DATETIME DEFAULT GETDATE(),
    LanDangNhapCuoi DATETIME
);

-- Bảng Đội bóng
CREATE TABLE DoiBong (
    MaDoi       INT PRIMARY KEY IDENTITY(1,1),
    TenDoi      NVARCHAR(100) NOT NULL,
    Logo        NVARCHAR(300),
    NamThanhLap INT,
    SanVanDong  NVARCHAR(200),
    DiaChi      NVARCHAR(300),
    Website     NVARCHAR(200),
    TrangThai   BIT DEFAULT 1
);

-- Bảng Huấn luyện viên
CREATE TABLE HuanLuyenVien (
    MaHLV       INT PRIMARY KEY IDENTITY(1,1),
    HoTen       NVARCHAR(100) NOT NULL,
    NgaySinh    DATE,
    QuocTich    NVARCHAR(50),
    ChuyenMon   NVARCHAR(100),
    MaDoi       INT FOREIGN KEY REFERENCES DoiBong(MaDoi),
    TrangThai   BIT DEFAULT 1
);

-- Bảng Cầu thủ
CREATE TABLE CauThu (
    MaCauThu    INT PRIMARY KEY IDENTITY(1,1),
    HoTen       NVARCHAR(100) NOT NULL,
    NgaySinh    DATE,
    ViTri       NVARCHAR(50),       -- Thủ môn, Hậu vệ, Tiền vệ, Tiền đạo
    SoAo        INT,
    QuocTich    NVARCHAR(50),
    ChieuCao    FLOAT,
    CanNang     FLOAT,
    AnhDaiDien  NVARCHAR(300),
    MaDoi       INT FOREIGN KEY REFERENCES DoiBong(MaDoi),
    TrangThai   BIT DEFAULT 1
);

-- Bảng Hợp đồng
CREATE TABLE HopDong (
    MaHopDong   INT PRIMARY KEY IDENTITY(1,1),
    MaCauThu    INT FOREIGN KEY REFERENCES CauThu(MaCauThu),
    MaDoi       INT FOREIGN KEY REFERENCES DoiBong(MaDoi),
    NgayBatDau  DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    LuongThang  DECIMAL(15,2),
    ThuongPhat  NVARCHAR(500),
    GhiChu      NVARCHAR(500),
    TrangThai   NVARCHAR(20) DEFAULT N'Hiệu lực'  -- Hiệu lực / Hết hạn / Huỷ
);

-- Bảng Giải đấu
CREATE TABLE GiaiDau (
    MaGiai      INT PRIMARY KEY IDENTITY(1,1),
    TenGiai     NVARCHAR(200) NOT NULL,
    MoTa        NVARCHAR(500),
    NamToChuc   INT,
    NgayBatDau  DATE,
    NgayKetThuc DATE,
    TrangThai   BIT DEFAULT 1
);

-- Bảng Trận đấu
CREATE TABLE TranDau (
    MaTran          INT PRIMARY KEY IDENTITY(1,1),
    MaGiai          INT FOREIGN KEY REFERENCES GiaiDau(MaGiai),
    MaDoi_Nha       INT FOREIGN KEY REFERENCES DoiBong(MaDoi),
    MaDoi_Khach     INT FOREIGN KEY REFERENCES DoiBong(MaDoi),
    NgayThiDau      DATETIME,
    DiaDiem         NVARCHAR(200),
    BanThang_Nha    INT DEFAULT 0,
    BanThang_Khach  INT DEFAULT 0,
    TrangThai       NVARCHAR(20) DEFAULT N'Chưa diễn ra'
);

-- Bảng Thống kê cầu thủ theo trận
CREATE TABLE ThongKeCauThu (
    MaThongKe   INT PRIMARY KEY IDENTITY(1,1),
    MaTran      INT FOREIGN KEY REFERENCES TranDau(MaTran),
    MaCauThu    INT FOREIGN KEY REFERENCES CauThu(MaCauThu),
    SoBanThang  INT DEFAULT 0,
    SoKienTao   INT DEFAULT 0,
    SoTheVang   INT DEFAULT 0,
    SoTheDo     INT DEFAULT 0,
    ThoiGianChoi INT DEFAULT 0      -- phút thi đấu
);

-- Bảng Thu Chi tài chính
CREATE TABLE TaiChinh (
    MaGiaoDich  INT PRIMARY KEY IDENTITY(1,1),
    LoaiGiaoDich NVARCHAR(10) NOT NULL,  -- Thu / Chi
    MoTa        NVARCHAR(300),
    SoTien      DECIMAL(18,2) NOT NULL,
    NgayGiaoDich DATE DEFAULT GETDATE(),
    MaDoi       INT FOREIGN KEY REFERENCES DoiBong(MaDoi),
    GhiChu      NVARCHAR(500)
);
```

---

## 1. 🔐 Chức năng Đăng nhập

### Mô tả
Xác thực người dùng trước khi vào hệ thống. Phân quyền theo vai trò sau khi đăng nhập thành công.

### Giao diện
- Ô nhập **Tên đăng nhập**
- Ô nhập **Mật khẩu** (ẩn ký tự)
- Nút **Đăng nhập**
- Checkbox **Ghi nhớ đăng nhập**
- Link **Quên mật khẩu**

### Luồng xử lý

```
Người dùng nhập thông tin
        ↓
Kiểm tra trường rỗng
        ↓
Truy vấn SQL kiểm tra tài khoản
        ↓
    [Tìm thấy?]
   /           \
 Có            Không
  ↓              ↓
Kiểm tra     Thông báo
trạng thái   "Sai tài khoản"
  ↓
[Đang hoạt động?]
  /         \
Có           Không
 ↓             ↓
Ghi log    Thông báo
đăng nhập  "Tài khoản bị khoá"
 ↓
Chuyển màn hình
theo vai trò
```

### Stored Procedure

```sql
CREATE PROCEDURE SP_DangNhap
    @TenDangNhap VARCHAR(50),
    @MatKhau     VARCHAR(255)    -- Truyền vào dạng đã hash
AS
BEGIN
    SELECT
        tk.MaTK,
        tk.HoTen,
        tk.Email,
        tk.TrangThai,
        vt.TenVaiTro
    FROM TaiKhoan tk
    INNER JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro
    WHERE tk.TenDangNhap = @TenDangNhap
      AND tk.MatKhau     = @MatKhau;

    -- Cập nhật thời gian đăng nhập cuối
    UPDATE TaiKhoan
    SET LanDangNhapCuoi = GETDATE()
    WHERE TenDangNhap = @TenDangNhap;
END
```

### Validate
| Trường | Quy tắc |
|--------|---------|
| Tên đăng nhập | Không rỗng, 4–50 ký tự |
| Mật khẩu | Không rỗng, tối thiểu 6 ký tự |
| Khoá tài khoản | Sau 5 lần sai liên tiếp, khoá 15 phút |

---

## 2. 👤 Quản lý Cầu thủ

### 2.1 Danh sách cầu thủ (Xem)

**Mô tả:** Hiển thị toàn bộ danh sách cầu thủ dưới dạng bảng/lưới với bộ lọc tìm kiếm.

**Bộ lọc:**
- Tìm theo tên (LIKE)
- Lọc theo đội bóng
- Lọc theo vị trí thi đấu
- Lọc theo quốc tịch
- Lọc theo trạng thái (Hoạt động / Không hoạt động)

```sql
CREATE PROCEDURE SP_LayDanhSachCauThu
    @TenTimKiem  NVARCHAR(100) = NULL,
    @MaDoi       INT = NULL,
    @ViTri       NVARCHAR(50) = NULL,
    @TrangThai   BIT = NULL
AS
BEGIN
    SELECT
        ct.MaCauThu,
        ct.HoTen,
        ct.NgaySinh,
        ct.ViTri,
        ct.SoAo,
        ct.QuocTich,
        db.TenDoi,
        ct.TrangThai
    FROM CauThu ct
    LEFT JOIN DoiBong db ON ct.MaDoi = db.MaDoi
    WHERE
        (@TenTimKiem IS NULL OR ct.HoTen LIKE N'%' + @TenTimKiem + '%')
        AND (@MaDoi  IS NULL OR ct.MaDoi   = @MaDoi)
        AND (@ViTri  IS NULL OR ct.ViTri   = @ViTri)
        AND (@TrangThai IS NULL OR ct.TrangThai = @TrangThai)
    ORDER BY ct.HoTen;
END
```

---

### 2.2 Thêm cầu thủ

**Mô tả:** Form nhập thông tin cầu thủ mới.

**Trường thông tin:**
| Trường | Kiểu | Bắt buộc | Ghi chú |
|--------|------|----------|---------|
| Họ và tên | NVARCHAR(100) | ✅ | |
| Ngày sinh | DATE | ✅ | Không nhỏ hơn 16 tuổi |
| Vị trí | NVARCHAR(50) | ✅ | Thủ môn / Hậu vệ / Tiền vệ / Tiền đạo |
| Số áo | INT | ✅ | 1–99, không trùng trong đội |
| Quốc tịch | NVARCHAR(50) | ❌ | |
| Chiều cao (cm) | FLOAT | ❌ | 140–220 |
| Cân nặng (kg) | FLOAT | ❌ | 40–150 |
| Đội bóng | INT (FK) | ✅ | Chọn từ danh sách |
| Ảnh đại diện | NVARCHAR | ❌ | Đường dẫn file ảnh |

```sql
CREATE PROCEDURE SP_ThemCauThu
    @HoTen      NVARCHAR(100),
    @NgaySinh   DATE,
    @ViTri      NVARCHAR(50),
    @SoAo       INT,
    @QuocTich   NVARCHAR(50),
    @ChieuCao   FLOAT,
    @CanNang    FLOAT,
    @MaDoi      INT,
    @AnhDaiDien NVARCHAR(300)
AS
BEGIN
    -- Kiểm tra số áo trùng trong đội
    IF EXISTS (
        SELECT 1 FROM CauThu
        WHERE MaDoi = @MaDoi AND SoAo = @SoAo AND TrangThai = 1
    )
    BEGIN
        RAISERROR(N'Số áo đã tồn tại trong đội này!', 16, 1);
        RETURN;
    END

    INSERT INTO CauThu (HoTen, NgaySinh, ViTri, SoAo, QuocTich,
                        ChieuCao, CanNang, MaDoi, AnhDaiDien, TrangThai)
    VALUES (@HoTen, @NgaySinh, @ViTri, @SoAo, @QuocTich,
            @ChieuCao, @CanNang, @MaDoi, @AnhDaiDien, 1);

    SELECT SCOPE_IDENTITY() AS MaCauThuMoi;
END
```

---

### 2.3 Sửa thông tin cầu thủ

**Mô tả:** Cập nhật thông tin cầu thủ đã có. Load thông tin hiện tại lên form, cho phép chỉnh sửa và lưu lại.

```sql
CREATE PROCEDURE SP_SuaCauThu
    @MaCauThu   INT,
    @HoTen      NVARCHAR(100),
    @NgaySinh   DATE,
    @ViTri      NVARCHAR(50),
    @SoAo       INT,
    @QuocTich   NVARCHAR(50),
    @ChieuCao   FLOAT,
    @CanNang    FLOAT,
    @MaDoi      INT,
    @AnhDaiDien NVARCHAR(300)
AS
BEGIN
    -- Kiểm tra số áo trùng (trừ chính cầu thủ này)
    IF EXISTS (
        SELECT 1 FROM CauThu
        WHERE MaDoi = @MaDoi AND SoAo = @SoAo
          AND TrangThai = 1 AND MaCauThu <> @MaCauThu
    )
    BEGIN
        RAISERROR(N'Số áo đã tồn tại trong đội này!', 16, 1);
        RETURN;
    END

    UPDATE CauThu
    SET
        HoTen      = @HoTen,
        NgaySinh   = @NgaySinh,
        ViTri      = @ViTri,
        SoAo       = @SoAo,
        QuocTich   = @QuocTich,
        ChieuCao   = @ChieuCao,
        CanNang    = @CanNang,
        MaDoi      = @MaDoi,
        AnhDaiDien = @AnhDaiDien
    WHERE MaCauThu = @MaCauThu;
END
```

---

### 2.4 Xoá cầu thủ

**Mô tả:** Xoá mềm (đặt TrangThai = 0) để giữ lịch sử. Chỉ Admin và Quản lý mới có quyền xoá.

> ⚠️ **Không xoá cứng** khỏi database để bảo toàn dữ liệu lịch sử hợp đồng và thống kê.

```sql
CREATE PROCEDURE SP_XoaCauThu
    @MaCauThu INT
AS
BEGIN
    -- Kiểm tra cầu thủ có hợp đồng đang hiệu lực không
    IF EXISTS (
        SELECT 1 FROM HopDong
        WHERE MaCauThu = @MaCauThu
          AND TrangThai = N'Hiệu lực'
          AND NgayKetThuc >= GETDATE()
    )
    BEGIN
        RAISERROR(N'Cầu thủ đang có hợp đồng hiệu lực, không thể xoá!', 16, 1);
        RETURN;
    END

    UPDATE CauThu
    SET TrangThai = 0
    WHERE MaCauThu = @MaCauThu;
END
```

---

## 3. 🏃 Quản lý Đội bóng

### 3.1 Xem danh sách đội bóng
Hiển thị danh sách các đội bóng trong CLB (có thể là nhiều đội: U17, U21, đội 1...).

### 3.2 Thêm đội bóng

```sql
CREATE PROCEDURE SP_ThemDoiBong
    @TenDoi     NVARCHAR(100),
    @Logo       NVARCHAR(300),
    @NamThanhLap INT,
    @SanVanDong NVARCHAR(200),
    @DiaChi     NVARCHAR(300),
    @Website    NVARCHAR(200)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM DoiBong WHERE TenDoi = @TenDoi AND TrangThai = 1)
    BEGIN
        RAISERROR(N'Tên đội bóng đã tồn tại!', 16, 1);
        RETURN;
    END

    INSERT INTO DoiBong (TenDoi, Logo, NamThanhLap, SanVanDong, DiaChi, Website, TrangThai)
    VALUES (@TenDoi, @Logo, @NamThanhLap, @SanVanDong, @DiaChi, @Website, 1);
END
```

### 3.3 Sửa thông tin đội bóng

```sql
CREATE PROCEDURE SP_SuaDoiBong
    @MaDoi      INT,
    @TenDoi     NVARCHAR(100),
    @Logo       NVARCHAR(300),
    @NamThanhLap INT,
    @SanVanDong NVARCHAR(200),
    @DiaChi     NVARCHAR(300),
    @Website    NVARCHAR(200)
AS
BEGIN
    UPDATE DoiBong
    SET TenDoi = @TenDoi, Logo = @Logo, NamThanhLap = @NamThanhLap,
        SanVanDong = @SanVanDong, DiaChi = @DiaChi, Website = @Website
    WHERE MaDoi = @MaDoi;
END
```

### 3.4 Xoá đội bóng

```sql
CREATE PROCEDURE SP_XoaDoiBong
    @MaDoi INT
AS
BEGIN
    -- Kiểm tra còn cầu thủ trong đội không
    IF EXISTS (SELECT 1 FROM CauThu WHERE MaDoi = @MaDoi AND TrangThai = 1)
    BEGIN
        RAISERROR(N'Đội bóng còn cầu thủ đang hoạt động, không thể xoá!', 16, 1);
        RETURN;
    END

    UPDATE DoiBong SET TrangThai = 0 WHERE MaDoi = @MaDoi;
END
```

---

## 4. 🧑‍💼 Quản lý Huấn luyện viên

### Các trường thông tin
| Trường | Kiểu | Bắt buộc |
|--------|------|----------|
| Họ tên | NVARCHAR(100) | ✅ |
| Ngày sinh | DATE | ❌ |
| Quốc tịch | NVARCHAR(50) | ❌ |
| Chuyên môn | NVARCHAR(100) | ❌ | Thủ môn / Thể lực / Chiến thuật |
| Đội phụ trách | INT (FK) | ✅ |

### Stored Procedures

```sql
-- Thêm HLV
CREATE PROCEDURE SP_ThemHLV
    @HoTen NVARCHAR(100), @NgaySinh DATE, @QuocTich NVARCHAR(50),
    @ChuyenMon NVARCHAR(100), @MaDoi INT
AS
BEGIN
    INSERT INTO HuanLuyenVien (HoTen, NgaySinh, QuocTich, ChuyenMon, MaDoi, TrangThai)
    VALUES (@HoTen, @NgaySinh, @QuocTich, @ChuyenMon, @MaDoi, 1);
END

-- Sửa HLV
CREATE PROCEDURE SP_SuaHLV
    @MaHLV INT, @HoTen NVARCHAR(100), @NgaySinh DATE,
    @QuocTich NVARCHAR(50), @ChuyenMon NVARCHAR(100), @MaDoi INT
AS
BEGIN
    UPDATE HuanLuyenVien
    SET HoTen=@HoTen, NgaySinh=@NgaySinh, QuocTich=@QuocTich,
        ChuyenMon=@ChuyenMon, MaDoi=@MaDoi
    WHERE MaHLV=@MaHLV;
END

-- Xoá HLV (mềm)
CREATE PROCEDURE SP_XoaHLV @MaHLV INT
AS
BEGIN
    UPDATE HuanLuyenVien SET TrangThai = 0 WHERE MaHLV = @MaHLV;
END
```

---

## 5. ⚔️ Quản lý Trận đấu

### 5.1 Danh sách trận đấu
- Lọc theo giải đấu, đội bóng, tháng/năm, trạng thái
- Hiển thị kết quả (tỷ số) cho các trận đã qua

### 5.2 Thêm trận đấu

```sql
CREATE PROCEDURE SP_ThemTranDau
    @MaGiai         INT,
    @MaDoi_Nha      INT,
    @MaDoi_Khach    INT,
    @NgayThiDau     DATETIME,
    @DiaDiem        NVARCHAR(200)
AS
BEGIN
    -- Không cho 2 đội giống nhau
    IF @MaDoi_Nha = @MaDoi_Khach
    BEGIN
        RAISERROR(N'Đội nhà và đội khách không được trùng nhau!', 16, 1);
        RETURN;
    END

    INSERT INTO TranDau (MaGiai, MaDoi_Nha, MaDoi_Khach, NgayThiDau,
                         DiaDiem, BanThang_Nha, BanThang_Khach, TrangThai)
    VALUES (@MaGiai, @MaDoi_Nha, @MaDoi_Khach, @NgayThiDau,
            @DiaDiem, 0, 0, N'Chưa diễn ra');
END
```

### 5.3 Cập nhật kết quả trận đấu

```sql
CREATE PROCEDURE SP_CapNhatKetQua
    @MaTran         INT,
    @BanThang_Nha   INT,
    @BanThang_Khach INT
AS
BEGIN
    UPDATE TranDau
    SET
        BanThang_Nha    = @BanThang_Nha,
        BanThang_Khach  = @BanThang_Khach,
        TrangThai       = N'Đã kết thúc'
    WHERE MaTran = @MaTran;
END
```

### 5.4 Xoá trận đấu

```sql
CREATE PROCEDURE SP_XoaTranDau @MaTran INT
AS
BEGIN
    -- Chỉ xoá trận chưa diễn ra
    IF EXISTS (SELECT 1 FROM TranDau WHERE MaTran=@MaTran AND TrangThai=N'Đã kết thúc')
    BEGIN
        RAISERROR(N'Không thể xoá trận đấu đã kết thúc!', 16, 1);
        RETURN;
    END
    DELETE FROM TranDau WHERE MaTran = @MaTran;
END
```

---

## 6. 🏆 Quản lý Giải đấu

### Các trường thông tin
| Trường | Kiểu | Bắt buộc |
|--------|------|----------|
| Tên giải | NVARCHAR(200) | ✅ |
| Mô tả | NVARCHAR(500) | ❌ |
| Năm tổ chức | INT | ✅ |
| Ngày bắt đầu | DATE | ✅ |
| Ngày kết thúc | DATE | ✅ |

```sql
-- Thêm giải đấu
CREATE PROCEDURE SP_ThemGiaiDau
    @TenGiai NVARCHAR(200), @MoTa NVARCHAR(500),
    @NamToChuc INT, @NgayBatDau DATE, @NgayKetThuc DATE
AS
BEGIN
    IF @NgayKetThuc <= @NgayBatDau
    BEGIN
        RAISERROR(N'Ngày kết thúc phải sau ngày bắt đầu!', 16, 1);
        RETURN;
    END
    INSERT INTO GiaiDau (TenGiai, MoTa, NamToChuc, NgayBatDau, NgayKetThuc, TrangThai)
    VALUES (@TenGiai, @MoTa, @NamToChuc, @NgayBatDau, @NgayKetThuc, 1);
END

-- Sửa giải đấu
CREATE PROCEDURE SP_SuaGiaiDau
    @MaGiai INT, @TenGiai NVARCHAR(200), @MoTa NVARCHAR(500),
    @NamToChuc INT, @NgayBatDau DATE, @NgayKetThuc DATE
AS
BEGIN
    UPDATE GiaiDau
    SET TenGiai=@TenGiai, MoTa=@MoTa, NamToChuc=@NamToChuc,
        NgayBatDau=@NgayBatDau, NgayKetThuc=@NgayKetThuc
    WHERE MaGiai=@MaGiai;
END

-- Xoá giải đấu (mềm)
CREATE PROCEDURE SP_XoaGiaiDau @MaGiai INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM TranDau WHERE MaGiai=@MaGiai)
    BEGIN
        RAISERROR(N'Giải đấu đã có trận đấu, chỉ có thể vô hiệu hoá!', 16, 1);
        RETURN;
    END
    UPDATE GiaiDau SET TrangThai = 0 WHERE MaGiai = @MaGiai;
END
```

---

## 7. 📄 Quản lý Hợp đồng

### Mô tả
Quản lý hợp đồng giữa cầu thủ và đội bóng, theo dõi trạng thái và lương thưởng.

### Các trường thông tin
| Trường | Kiểu | Bắt buộc |
|--------|------|----------|
| Cầu thủ | INT (FK) | ✅ |
| Đội bóng | INT (FK) | ✅ |
| Ngày bắt đầu | DATE | ✅ |
| Ngày kết thúc | DATE | ✅ |
| Lương tháng | DECIMAL(15,2) | ✅ |
| Điều khoản thưởng/phạt | NVARCHAR(500) | ❌ |
| Ghi chú | NVARCHAR(500) | ❌ |

```sql
CREATE PROCEDURE SP_ThemHopDong
    @MaCauThu INT, @MaDoi INT, @NgayBatDau DATE, @NgayKetThuc DATE,
    @LuongThang DECIMAL(15,2), @ThuongPhat NVARCHAR(500), @GhiChu NVARCHAR(500)
AS
BEGIN
    -- Kiểm tra hợp đồng chồng lấp
    IF EXISTS (
        SELECT 1 FROM HopDong
        WHERE MaCauThu = @MaCauThu
          AND TrangThai = N'Hiệu lực'
          AND NOT (@NgayBatDau > NgayKetThuc OR @NgayKetThuc < NgayBatDau)
    )
    BEGIN
        RAISERROR(N'Cầu thủ đã có hợp đồng trong khoảng thời gian này!', 16, 1);
        RETURN;
    END

    INSERT INTO HopDong (MaCauThu, MaDoi, NgayBatDau, NgayKetThuc,
                         LuongThang, ThuongPhat, GhiChu, TrangThai)
    VALUES (@MaCauThu, @MaDoi, @NgayBatDau, @NgayKetThuc,
            @LuongThang, @ThuongPhat, @GhiChu, N'Hiệu lực');
END
```

---

## 8. 💰 Quản lý Tài chính

### Mô tả
Ghi nhận và theo dõi các khoản thu/chi của câu lạc bộ.

```sql
-- Thêm giao dịch
CREATE PROCEDURE SP_ThemGiaoDich
    @LoaiGiaoDich NVARCHAR(10), @MoTa NVARCHAR(300),
    @SoTien DECIMAL(18,2), @NgayGiaoDich DATE, @MaDoi INT, @GhiChu NVARCHAR(500)
AS
BEGIN
    IF @SoTien <= 0
    BEGIN
        RAISERROR(N'Số tiền phải lớn hơn 0!', 16, 1);
        RETURN;
    END
    INSERT INTO TaiChinh (LoaiGiaoDich, MoTa, SoTien, NgayGiaoDich, MaDoi, GhiChu)
    VALUES (@LoaiGiaoDich, @MoTa, @SoTien, @NgayGiaoDich, @MaDoi, @GhiChu);
END

-- Báo cáo tổng thu chi theo tháng
CREATE PROCEDURE SP_BaoCaoThuChi
    @Thang INT, @Nam INT, @MaDoi INT = NULL
AS
BEGIN
    SELECT
        LoaiGiaoDich,
        SUM(SoTien) AS TongTien,
        COUNT(*) AS SoGiaoDich
    FROM TaiChinh
    WHERE MONTH(NgayGiaoDich) = @Thang
      AND YEAR(NgayGiaoDich)  = @Nam
      AND (@MaDoi IS NULL OR MaDoi = @MaDoi)
    GROUP BY LoaiGiaoDich;
END
```

---

## 9. 📊 Báo cáo & Thống kê

### 9.1 Bảng xếp hạng cầu thủ ghi bàn

```sql
CREATE PROCEDURE SP_BXH_GhiBan
    @MaGiai INT = NULL,
    @Top    INT = 10
AS
BEGIN
    SELECT TOP (@Top)
        ct.HoTen,
        db.TenDoi,
        SUM(tk.SoBanThang) AS TongBanThang,
        SUM(tk.SoKienTao)  AS TongKienTao,
        SUM(tk.SoTheVang)  AS TongTheVang,
        SUM(tk.SoTheDo)    AS TongTheDo
    FROM ThongKeCauThu tk
    INNER JOIN CauThu ct ON tk.MaCauThu = ct.MaCauThu
    INNER JOIN DoiBong db ON ct.MaDoi   = db.MaDoi
    INNER JOIN TranDau td ON tk.MaTran  = td.MaTran
    WHERE (@MaGiai IS NULL OR td.MaGiai = @MaGiai)
    GROUP BY ct.HoTen, db.TenDoi
    ORDER BY TongBanThang DESC;
END
```

### 9.2 Thống kê tổng quan câu lạc bộ

```sql
CREATE VIEW V_TongQuanCLB AS
SELECT
    (SELECT COUNT(*) FROM CauThu    WHERE TrangThai = 1) AS TongCauThu,
    (SELECT COUNT(*) FROM DoiBong   WHERE TrangThai = 1) AS TongDoiBong,
    (SELECT COUNT(*) FROM TranDau   WHERE TrangThai = N'Đã kết thúc') AS TongTranDaDau,
    (SELECT COUNT(*) FROM HopDong   WHERE TrangThai = N'Hiệu lực') AS TongHopDongHieuLuc,
    (SELECT SUM(SoTien) FROM TaiChinh WHERE LoaiGiaoDich = 'Thu') AS TongThu,
    (SELECT SUM(SoTien) FROM TaiChinh WHERE LoaiGiaoDich = 'Chi') AS TongChi;
```

---

## 10. 👥 Quản lý Tài khoản Người dùng

> Chỉ **Admin** mới có quyền truy cập chức năng này.

### 10.1 Thêm tài khoản

```sql
CREATE PROCEDURE SP_ThemTaiKhoan
    @TenDangNhap VARCHAR(50),
    @MatKhau     VARCHAR(255),   -- Đã hash trước khi truyền vào
    @HoTen       NVARCHAR(100),
    @Email       VARCHAR(100),
    @MaVaiTro    INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap)
    BEGIN
        RAISERROR(N'Tên đăng nhập đã tồn tại!', 16, 1);
        RETURN;
    END
    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, Email, MaVaiTro, TrangThai, NgayTao)
    VALUES (@TenDangNhap, @MatKhau, @HoTen, @Email, @MaVaiTro, 1, GETDATE());
END
```

### 10.2 Đổi mật khẩu

```sql
CREATE PROCEDURE SP_DoiMatKhau
    @MaTK        INT,
    @MatKhauCu   VARCHAR(255),
    @MatKhauMoi  VARCHAR(255)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE MaTK=@MaTK AND MatKhau=@MatKhauCu)
    BEGIN
        RAISERROR(N'Mật khẩu cũ không đúng!', 16, 1);
        RETURN;
    END
    UPDATE TaiKhoan SET MatKhau = @MatKhauMoi WHERE MaTK = @MaTK;
END
```

### 10.3 Khoá / Mở khoá tài khoản

```sql
CREATE PROCEDURE SP_KhoaTaiKhoan
    @MaTK INT, @TrangThai BIT
AS
BEGIN
    UPDATE TaiKhoan SET TrangThai = @TrangThai WHERE MaTK = @MaTK;
END
```

---

## 🚀 Hướng dẫn cài đặt

### Yêu cầu hệ thống
```
- Windows 10/11 hoặc Windows Server 2019+
- SQL Server 2019+ (hoặc SQL Server Express miễn phí)
- Visual Studio 2022 (để build)
- .NET Framework 4.8 hoặc .NET 6+
```

### Các bước cài đặt

**Bước 1: Clone repository**
```bash
git clone https://github.com/your-username/quan-ly-clb-bong-da.git
cd quan-ly-clb-bong-da
```

**Bước 2: Tạo Database**
```bash
# Mở SQL Server Management Studio (SSMS)
# Chạy file: Database/QuanLyCLBBongDa_Script.sql
```

**Bước 3: Cấu hình chuỗi kết nối**

Mở file `appsettings.json` hoặc `App.config` và sửa:
```xml
<connectionStrings>
  <add name="QLCLB"
       connectionString="Server=YOUR_SERVER;Database=QuanLyCLBBongDa;
                         Trusted_Connection=True;MultipleActiveResultSets=true"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Bước 4: Chạy dữ liệu mẫu (tuỳ chọn)**
```bash
# Chạy file: Database/SeedData.sql
```

**Bước 5: Build & Run**
```bash
# Visual Studio: nhấn F5 hoặc Ctrl+F5
# Hoặc dùng dotnet CLI:
dotnet build
dotnet run
```

### Tài khoản mặc định sau cài đặt
| Tên đăng nhập | Mật khẩu | Vai trò |
|--------------|----------|---------|
| `admin` | `Admin@123` | Admin |
| `quanly` | `Ql@12345` | Quản lý |

> ⚠️ **Hãy đổi mật khẩu mặc định ngay sau khi cài đặt!**

---

## 📁 Cấu trúc thư mục

```
quan-ly-clb-bong-da/
├── Database/
│   ├── QuanLyCLBBongDa_Script.sql    # Script tạo DB đầy đủ
│   ├── StoredProcedures.sql          # Tất cả stored procedures
│   └── SeedData.sql                  # Dữ liệu mẫu
├── Source/
│   ├── Forms/                        # Các form giao diện
│   ├── DAL/                          # Data Access Layer
│   ├── BLL/                          # Business Logic Layer
│   └── Models/                       # Các lớp model
├── Reports/                          # File báo cáo (.rdlc / .rpt)
├── Assets/                           # Ảnh, icon
└── README.md
```

---

## 🤝 Đóng góp

1. Fork repository
2. Tạo branch mới: `git checkout -b feature/ten-chuc-nang`
3. Commit: `git commit -m "Thêm chức năng ..."`
4. Push: `git push origin feature/ten-chuc-nang`
5. Tạo Pull Request

---

## 📝 Giấy phép

Dự án này được phân phối theo giấy phép [MIT License](LICENSE).

---

<div align="center">
  Made with ❤️ by Football Club Management Team
</div>