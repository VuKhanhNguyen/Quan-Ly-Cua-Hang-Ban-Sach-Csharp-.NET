-- Tạo Cơ Sở Dữ Liệu
CREATE DATABASE QuanLyBanSach;
GO

USE QuanLyBanSach;
GO

-- Tạo Bảng THELOAI
CREATE TABLE THELOAI (
    IDTheLoai VARCHAR(10) PRIMARY KEY,
    TenTheLoai NVARCHAR(100) 
);
GO

-- Thêm Dữ Liệu vào Bảng THELOAI
INSERT INTO THELOAI (IDTheLoai, TenTheLoai) VALUES
('TL01', N'Sách văn học'),
('TL02', N'Sách thiếu nhi'),
('TL03', N'Sách kinh tế'),
('TL04', N'Sách tiểu sử-hồi ký'),
('TL05', N'Sách tâm lý kĩ năng sống'),
('TL06', N'Sách dạy con'),
('TL07', N'Sách giáo khoa-tham khảo'),
('TL08', N'Sách ngoại ngữ');
GO

-- Tạo Bảng SACH
CREATE TABLE SACH (
    IDSach VARCHAR(10) PRIMARY KEY,
    TenSach NVARCHAR(100) ,
    TacGia NVARCHAR(100) ,
    IDTheLoai VARCHAR(10) ,
    NhaXuatBan NVARCHAR(100) ,
    NamXuatBan INT ,
    GiaNhap DECIMAL(18, 0) ,
    GiaBan DECIMAL(18, 0) ,
    SoLuongTon INT ,
    AnhSach NVARCHAR(MAX), -- Cột AnhSach được đổi sang NVARCHAR(MAX)
    MoTa NVARCHAR(MAX),
    FOREIGN KEY (IDTheLoai) REFERENCES THELOAI(IDTheLoai)
);
GO

-- Thêm Dữ Liệu vào Bảng SACH
INSERT INTO SACH (IDSach, TenSach, TacGia, IDTheLoai, NhaXuatBan, NamXuatBan, GiaNhap, GiaBan, SoLuongTon, AnhSach, MoTa)
VALUES
('SNN1', N'Giải Thích Ngữ Pháp Tiếng Anh', N'Mai Lan Hương', 'TL08', N'Đà Nẵng', 2023, 200000, 250000, 100, N'https://cdn0.fahasa.com/media/catalog/product/g/i/giaithichnguphapta-tb2024_bia1.jpg', N'Ngữ pháp Tiếng Anh tổng hợp các chủ điểm ngữ pháp trọng yếu mà học sinh cần nắm vững. Các chủ điểm ngữ pháp được trình bày rõ ràng, chi tiết. Sau mỗi chủ điểm ngữ pháp là phần bài tập & đáp án nhằm giúp các em củng cố kiến thức đã học, đồng thời tự kiểm tra kết quả.'),
('SNN2', N'200 Chữ Kanji Căn Bản', N'Phan Văn B', 'TL08', N'Đại Học Quốc Gia TPHCM', 2022, 150000, 200000, 150, N'https://cdn0.fahasa.com/media/catalog/product/8/9/8935086855928.jpg', N'Bất cứ người học tiếng Nhật nào cũng gặp khó khăn khi phải ghi nhớ và học viết chữ Kanji. Tự Học Viết Tiếng Nhật - 200 Chữ Kanji Căn Bản - Tập 1 có hướng tiếp cận theo cấu tạo chữ, nhằm giúp người học mau chóng nắm vững cách viết Kanji căn bản - vốn là nền tảng cho việc học tiếng Nhật.');
GO
INSERT INTO SACH (IDSach, TenSach, TacGia, IDTheLoai, NhaXuatBan, NamXuatBan, GiaNhap, GiaBan, SoLuongTon, AnhSach, MoTa)
VALUES
('SVH1', N'Cây Cam Ngọt Của Tôi', N'José Mauro de Vasconcelos', 'TL01', N'NXB Hội Nhà Văn', 2020, 200000, 250000, 100, N'https://cdn0.fahasa.com/media/catalog/product/i/m/image_217480.jpg', N'Ngữ pháp Tiếng Anh tổng hợp các chủ điểm ngữ pháp trọng yếu mà học sinh cần nắm vững. Các chủ điểm ngữ pháp được trình bày rõ ràng, chi tiết. Sau mỗi chủ điểm ngữ pháp là phần bài tập & đáp án nhằm giúp các em củng cố kiến thức đã học, đồng thời tự kiểm tra kết quả.');

GO

INSERT INTO SACH (IDSach, TenSach, TacGia, IDTheLoai, NhaXuatBan, NamXuatBan, GiaNhap, GiaBan, SoLuongTon, AnhSach, MoTa)
VALUES
('SVH2', N'Đám Trẻ Ở Đại Dương Đen', N'Châu Sa Đáy Mắt', 'TL01', N'Thế Giới', 2023, 50000, 60000, 100, N'https://cdn0.fahasa.com/media/catalog/product/8/9/8935325011559.jpg', N'Đám trẻ ở đại dương đen là lời độc thoại và đối thoại của những đứa trẻ ở đại dương đen, nơi từng lớp sóng của nỗi buồn và tuyệt vọng không ngừng cuộn trào, lúc âm ỉ, khi dữ dội. Những đứa trẻ ấy phải vật lộn trong những góc tối tâm lý, với sự u uất đè nén từ tổn thương khi không được sinh ra trong một gia đình toàn vẹn, ấm êm, khi phải mang trên đôi vai non dại những gánh nặng không tưởng.'),
('STN2', N'Hoàng Tử Bé', N'Antoine De Saint-Exupéry', 'TL02', N'Kim Đồng', 2022, 20000, 30000, 100, N'https://cdn0.fahasa.com/media/catalog/product/8/9/8935244868999.jpg', N'“...Cậu hoàng tử chợp mắt ngủ, tôi bế em lên vòng tay tôi và lại lên đường. Lòng tôi xúc động. Tôi có cảm giác như trên Mặt Đất này không có gì mong manh hơn. Nhờ ánh sáng trăng, tôi nhìn thấy vầng trán nhợt nhạt ấy, đôi mắt nhắm nghiền các lẵng tóc run rẩy trước gió, và tôi nghĩ thầm: "Cái mà ta nhìn thấy đây chỉ là cái vỏ. Cái quan trọng nhất thì không nhìn thấy được..." ANTOINE DE SAINT-EXUPÉRY'),
('STS1', N'Kể Chuyện Cuộc Đời Các Thiên Tài: Albert Einstein - Tuổi Thơ Gian Khó Và Cuộc Đời Khoa Học Vĩ Đại', N'Rasmus Hoài Nam', 'TL04', N'NXB Thanh Niên', 2021, 40000, 50000, 100, N'https://cdn0.fahasa.com/media/catalog/product/i/m/image_237520.jpg', N'Cuốn sách gồm những âu chuyện viết về cuộc đời của nhà khoa học thiên tài Albert Einstein, người đã làm thay đổi cả thế giới cũng như quan niệm khoa học đương thời.'),
('STS2', N'Kể Chuyện Cuộc Đời Các Thiên Tài: Leonardo Da Vinci - Thiên Tài Toàn Năng', N'Rasmus Hoài Nam', 'TL04', N'NXB Thanh Niên', 2021, 40000, 50000, 100, N'https://cdn0.fahasa.com/media/catalog/product/i/m/image_237517.jpg', N'Cuốn sách kể về cuộc đời của thiên tài Leonardo da Vinci, theo diễn biến thời gian, từ khi sinh ra ở xứ Vinci, đến khi lớn lên theo cha đến Florence, rồi học việc tại xưởng vẽ của họa sĩ tài danh nhất tại đây –  Andrea del Verrocchio. Khi đạt được những thành công nhất định, Leonardo rời Florence đến Milan, rồi từ đó, ông sống cuộc sống nay đây mai đó, di chuyển qua lại giữa các cung điện khắp nước Ý.'),
('STL1', N'Tư Duy Ngược', N'Nguyễn Anh Dũng', 'TL05', N'Dân Trí', 2021, 70000, 80000, 100, N'https://cdn0.fahasa.com/media/catalog/product/9/7/9786043440287.jpg', N'Chúng ta thực sự có hạnh phúc không? Chúng ta có đang sống cuộc đời mình không? Chúng ta có dám dũng cảm chiến thắng mọi khuôn mẫu, định kiến, đi ngược đám đông để khẳng định bản sắc riêng của mình không?. Có bao giờ bạn tự hỏi như thế, rồi có câu trả lời cho chính mình?'),
('STL2', N'Tư Duy Mở', N'Nguyễn Anh Dũng', 'TL05', N'Dân Trí', 2021, 70000, 80000, 100, N'https://cdn0.fahasa.com/media/catalog/product/9/7/9786044742250.jpg', N'Con người đang sống trong thời đại công nghệ, khi mọi thứ thay đổi chóng mặt, điều đó đòi hỏi chúng ta phải linh hoạt trong cách tư duy để bắt kịp xu hướng toàn cầu. Hay nói cách khác, chúng ta cần có một tư duy mở để đón nhận và khai phá kiến thức mới, bởi nếu chúng ta cứ khăng khăng giữ định kiến của mình thì sự phát triển sẽ đi vào ngõ cụt.'),
('SDC1', N'Để Con Được Ốm', N'Nguyễn Trí Đoàn, Uyên Bùi', 'TL06', N'Thế Giới', 2022, 90000, 100000, 100, N'https://cdn0.fahasa.com/media/catalog/product/8/9/8935235235168.jpg', N'Để con được ốm cần có sự kiên nhẫn giải thích hay thuyết phục của bác sĩ cùng sự thông hiểu và hợp tác từ phía gia đình bé. Đôi khi, sự hợp tác và hiểu biết của phụ huynh còn quan trọng hơn nỗ lực (hay thời gian) của bác sĩ giải thích nữa.'),
('SDC2', N'Cha Voi: Dạy Con Nên Người Ở Thời Đại Số', N'Trương Nguyện Thành', 'TL06', N'NXB Tổng Hợp TPHCM', 2019, 100000, 110000, 100, N'https://cdn0.fahasa.com/media/catalog/product/8/9/8935278604327_11.jpg', N'Rất nhiều bậc cha mẹ trên thế giới đang chứng kiến sự thay đổi nhanh chóng của môi trường sống cũng như những chuẩn mực đạo đức xã hội. Sự thay đổi đó đặt ra nhiều thử thách mới trong việc dạy con mà cha ông của chúng ta chưa hề trải qua. Chúng ta cảm thấy hoang mang khi dạy con bởi những thay đổi không ngừng của môi trường sống và sự thiếu hiểu biết của chính chúng ta về những phương pháp dạy con. Rất có thể cách dạy con mặc định từ gia phong đã không còn phù hợp nữa, thế nên chúng ta cần điều chỉnh cách dạy con theo chiều hướng khoa học hơn.'),
('SGK1', N'Bộ Đề Thi Thử Tốt Nghiệp THPT Môn Tiếng Anh', N'Trần Thanh Hương, Hoàng Thị Hồng Nhung', 'TL07', N'Thế Giới', 2024, 170000, 200000, 100, N'https://cdn0.fahasa.com/media/catalog/product/_/f/_final_-b_a-b_-_-thi-th_-t_t-nghi_p-thpt-ti_ng-anh-28.08.2024-01-1.jpg', N'Bộ đề thi thử tốt nghiệp THPT môn Tiếng Anh là cuốn sách được xây dựng dựa trên định dạng đề thi mới sẽ được Bộ GD-ĐT áp dụng từ năm 2025. Cuốn sách là tài liệu ôn tập thiết thực và kịp thời dành cho những học sinh và giáo viên đang chuẩn bị cho kỳ thi tốt nghiệp THPT Quốc gia.'),
('SGK2', N'550 Bài Luyện Đọc Hiểu - Đọc Điền Tiếng Anh', N'Trang Anh', 'TL07', N'Dân Trí', 2024, 250000, 300000, 100, N'https://cdn0.fahasa.com/media/catalog/product/9/7/9786044050072.jpg', N'Các bài đọc hiểu - đọc điền theo chủ đề: 39 chủ đề và 450 bài luyện bao quát tất cả các chủ đề trong chương trình học phổ thông và các chủ đề thường gặp trong thi. Mỗi chủ đề gồm có đa dang các bài đục lỗ - đọc hiểu với các dạng câu hỏi theo các mức nhận biết - thông hiểu - vận dụng và các vận dụng cao. Ngoài ra, mỗi bài đều có phần đáp án chi tiết, nhấn mạnh vào hệ thống kiến thức trọng tâm cũng như các kỹ thuật làm từng loại câu hỏi. Cuối mỗi bài đều có phần tổng hợp từ vựng và cấu trúc của toàn bài nhằm cung cấp thêm vốn từ vựng theo chủ đề cho học sinh.');

GO

INSERT INTO SACH (IDSach, TenSach, TacGia, IDTheLoai, NhaXuatBan, NamXuatBan, GiaNhap, GiaBan, SoLuongTon, AnhSach, MoTa)
values ('SGK2', N'550 Bài Luyện Đọc Hiểu - Đọc Điền Tiếng Anh', N'Trang Anh', 'TL07', N'Dân Trí', 2024, 250000, 300000, 100, N'https://cdn0.fahasa.com/media/catalog/product/9/7/9786044050072.jpg', N'Các bài đọc hiểu - đọc điền theo chủ đề: 39 chủ đề và 450 bài luyện bao quát tất cả các chủ đề trong chương trình học phổ thông và các chủ đề thường gặp trong thi. Mỗi chủ đề gồm có đa dang các bài đục lỗ - đọc hiểu với các dạng câu hỏi theo các mức nhận biết - thông hiểu - vận dụng và các vận dụng cao. Ngoài ra, mỗi bài đều có phần đáp án chi tiết, nhấn mạnh vào hệ thống kiến thức trọng tâm cũng như các kỹ thuật làm từng loại câu hỏi. Cuối mỗi bài đều có phần tổng hợp từ vựng và cấu trúc của toàn bài nhằm cung cấp thêm vốn từ vựng theo chủ đề cho học sinh.');
go


-- Tạo Bảng KHACHHANG
CREATE TABLE KHACHHANG (
    IDKhachHang INT IDENTITY(1,1) PRIMARY KEY, -- IDKhachHang là INT IDENTITY
    HoTen NVARCHAR(100),
    SoDienThoai VARCHAR(10) ,
    DiaChi NVARCHAR(100) ,
    Email NVARCHAR(100),
    TaiKhoan VARCHAR(100) ,
    MatKhau VARCHAR(100) 
);
GO

-- Thêm Dữ Liệu vào Bảng KHACHHANG
INSERT INTO KHACHHANG (HoTen, SoDienThoai, DiaChi, Email, TaiKhoan, MatKhau)
VALUES
(N'Nguyễn Văn A', '0123456789', N'Quận 1, TP.HCM', 'a@gmail.com', 'taikhoan1', 'matkhau1'),
(N'Phan Thị B', '0123456790', N'Quận 2, TP.HCM', 'b@gmail.com', 'taikhoan2', 'matkhau2'),
(N'Hoàng Minh C', '0123456791', N'Quận 3, TP.HCM', 'c@gmail.com', 'taikhoan3', 'matkhau3');
GO

-- Tạo Bảng GIOHANG
CREATE TABLE GIOHANG (
    IDGioHang int identity(1,1) PRIMARY KEY,
    IDKhachHang INT ,
    --NgayTao DATETIME ,
    IDSach VARCHAR(10) ,
    SoLuongDat INT ,
    FOREIGN KEY (IDKhachHang) REFERENCES KHACHHANG(IDKhachHang),
    FOREIGN KEY (IDSach) REFERENCES SACH(IDSach)
);
GO

-- Thêm Dữ Liệu vào Bảng GIOHANG
--INSERT INTO GIOHANG (IDGioHang, IDKhachHang, NgayTao, IDSach, SoLuongDat)
--VALUES
--('GH001', 1, '2024-11-01', 'S001', 2),
--('GH002', 2, '2024-11-02', 'S002', 1),
--('GH003', 3, '2024-11-03', 'S003', 3);
--GOx

-- Tạo Bảng DONHANG
CREATE TABLE DONHANG (
    IDDonHang int identity(1,1) PRIMARY KEY,
    IDKhachHang INT ,
    NgayDatHang DATETIME ,
    TongTien DECIMAL(18, 0) ,
    TrangThai NVARCHAR(100) ,
    FOREIGN KEY (IDKhachHang) REFERENCES KHACHHANG(IDKhachHang)
);
GO
--drop table DONHANG
--drop table  CHITIETDONHANG
create table CHITIETDONHANG(
	IDChiTiet int identity(1,1) primary key,
	IDDonHang int,
	IDSach varchar(10),
	SoLuong int,
	DonGia decimal(18,0),
	foreign key (IDDonHang) references DONHANG(IDDonHang),
	foreign key (IDSach) references SACH (IDSach)
)
go

-- Thêm Dữ Liệu vào Bảng DONHANG
--INSERT INTO DONHANG (IDDonHang, IDKhachHang, NgayDatHang, TongTien, TrangThai)
--VALUES
--('DH001', 1, '2024-11-01', 500000, N'Đang xử lý'),
--('DH002', 2, '2024-11-02', 600000, N'Đã giao'),
--('DH003', 3, '2024-11-03', 450000, N'Đang xử lý');
--GO


-- Tạo View vw_DonHangChiTiet
CREATE VIEW vw_DonHangChiTiet AS
SELECT 
    dh.IDDonHang,
    dh.IDKhachHang,
    ct.IDSach,
    ct.SoLuong,
    dh.NgayDatHang,
    ct.DonGia,
    dh.TongTien,
    dh.TrangThai,
    s.TenSach,
    s.GiaBan,
    kh.HoTen
FROM 
    DONHANG dh
JOIN 
    CHITIETDONHANG ct ON dh.IDDonHang = ct.IDDonHang
JOIN 
    SACH s ON ct.IDSach = s.IDSach
JOIN 
    KHACHHANG kh ON dh.IDKhachHang = kh.IDKhachHang;
GO

-- Kiểm tra View
SELECT * FROM vw_DonHangChiTiet;
GO


CREATE TRIGGER trg_GiamSoLuongTon 
ON CHITIETDONHANG
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Giảm số lượng tồn trong bảng SACH khi có đơn hàng mới
    UPDATE SACH
    SET SoLuongTon = SoLuongTon - i.SoLuong
    FROM SACH s
    JOIN INSERTED i ON s.IDSach = i.IDSach;

    -- Ngăn số lượng tồn âm
    UPDATE SACH
    SET SoLuongTon = 0
    WHERE SoLuongTon < 0;
END;




select * from THELOAI
select * from SACH
select * from KHACHHANG
select * from GIOHANG
select * from DONHANG
select * from CHITIETDONHANG