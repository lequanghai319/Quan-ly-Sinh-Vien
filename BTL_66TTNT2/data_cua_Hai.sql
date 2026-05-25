USE [Bai_tap_lon];
GO
IF OBJECT_ID('KhoaHoc', 'U') IS NOT NULL
    DROP TABLE KhoaHoc;
GO

-- 3. Tạo lại bảng chuẩn
CREATE TABLE KhoaHoc (
    MaKhoaHoc VARCHAR(50) PRIMARY KEY,
    TenKhoaHoc NVARCHAR(100) NOT NULL,
    SoTinChi INT,
    HocKy NVARCHAR(50)
);
GO

-- 4. Bơm dữ liệu
INSERT INTO KhoaHoc (MaKhoaHoc, TenKhoaHoc, SoTinChi, HocKy) VALUES
('CSE001', N'Lập trình C++', 3, N'Học kỳ 1'),
('CSET002', N'Triết học Mác-Lênin', 3, N'Học kỳ 1'),
('CSE003', N'Giải tích hàm nhiều biến', 4, N'Học kỳ 2'),
('CSE004', N'Tiếng Anh chuyên ngành', 2, N'Học kỳ 3');
GO