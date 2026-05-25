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
(N'Lê Quang Hải', N'Lập trình C++', 9.0, 6.0, 6.6),
(N'Phạm Huy Hùng', N'Triết học Mác-Lênin', 8.5, 8.0, 8.13),
(N'Lê Quang Hải', N'Giải tích hàm nhiều biến', 7.0, 2.5, 3.27),
(N'Hồ Lan Anh', N'Triết học Mác-Lênin', 8.0, 7.5, 7.6),
(N'Phạm Huy Hùng', N'Giải tích hàm nhiều biến', 9.5, 8.5, 8.7),
(N'Phạm Trung Hiếu', N'Tiếng Anh chuyên ngành', 6.0, 7.0, 6.8),
(N'Nguyễn Như Ngọc', N'Lập trình C++', 8.0, 6.0, 6.8),
(N'Hồ Lan Anh', N'Lập trình C++', 7.3, 6.4, 6.76),
(N'Nguyễn Như Ngọc', N'Giải tích hàm nhiều biến', 5.5, 5.0, 5.2),
(N'Phạm Trung Hiếu', N'Triết học Mác-Lênin', 9.0, 7.0, 7.8);