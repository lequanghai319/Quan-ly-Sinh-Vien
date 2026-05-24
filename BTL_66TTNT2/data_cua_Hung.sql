CREATE TABLE DiemSinhVien (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    HoTenSV NVARCHAR(100) NOT NULL, 
    MonHoc NVARCHAR(100) NOT NULL,
    DiemChuyenCan FLOAT, 
    DiemCuoiKy FLOAT, 
    DiemTB FLOAT                      
);
INSERT INTO DiemSinhVien (HoTenSV, MonHoc, DiemChuyenCan, DiemCuoiKy, DiemTB)
VALUES 
(N'Lê Quang Hải', N'Văn', 9.0, 6.0, 6.6),
(N'Phạm Huy Hùng', N'Tiếng Anh', 8.5, 8.0, 8.13),
(N'Lê Quang Hải', N'Toán', 7.0, 2.5, 3.27),
(N'Phạm Trung Hiếu', N'Văn', 8.0, 7.5, 7.6),
(N'Phạm Huy Hùng', N'Toán', 9.5, 8.5, 8.7),
(N'Phạm Trung Hiếu', N'Tiếng Anh', 6.0, 7.0, 6.8);
