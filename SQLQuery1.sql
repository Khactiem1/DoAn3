create database tksr
use tksr

create table DichVu(
	MaDichVu nvarchar(50) primary key,
	TenKhachHang nvarchar(50),
	TienThanhToan int,
	TenDichVu nvarchar(50),
	DoiTac nvarchar(50),
	NgayThem DateTime
)

create table YeuCauGachThe(
	MaHoaDon nvarchar(200) primary key,
	TenTaiKhoan nvarchar(50),
	NhaMang nvarchar(50),
	MaThe nvarchar(50),
	Seri nvarchar(50),
	MenhGia int,
	TienThucNhan nvarchar(50),
	NgayNap DateTime,
	TrangThai nvarchar(50)
)

create table YeuCauNapATM(
	MaHoaDon nvarchar(200) primary key,
	TenTaiKhoan nvarchar(50),
	NganHang nvarchar(50),
	SoTien int,
	NgayNap DateTime,
	TrangThai nvarchar(50)
)

create table RutTienATM(
	MaHoaDon nvarchar(200) primary key,
	TenTaiKhoan nvarchar(50),
	NganHang nvarchar(50),
	STK nvarchar(50),
	ChuTK nvarchar(50),
	SoTien int,
	NgayRut DateTime,
	TrangThai nvarchar(50)
)


create table ChuyenTien(
	MaHoaDon nvarchar(200) primary key,
	TenTaiKhoanChuyen nvarchar(50),
	TenTaiKhoanNhan nvarchar(50),
	SoTien int,
	LoiNhan nvarchar(250),
	NgayChuyen DateTime,
)



create table TheNap(
	MaThe nvarchar(50) primary key,
	MenhGia nvarchar(50),
	Seri nvarchar(50),
	NhaMang nvarchar(50),
	NgayThem datetime
)
ALTER TABLE TheNap
ADD MaChietKhau nvarchar(50) FOREIGN KEY REFERENCES dbo.ChietKhau


create table ChietKhau(
	MaChietKhau nvarchar(50) primary key,
	NhaMang nvarchar(50),
	MenhGia nvarchar(50),
	ChietKhauNap float,
	ChietKhauBan float,
)
CREATE TABLE NhaMang(
	TenNhaMang nvarchar(50) primary key,
	Logo nvarchar(50)
)


drop table ChietKhau



CREATE TRIGGER tg__NhaMang ON dbo.NhaMang
FOR INSERT
AS
BEGIN
	DECLARE @NhaMang NVARCHAR(50)
	SELECT @NhaMang = TenNhaMang FROM INSERTED
	INSERT INTO ChietKhau
	VALUES(@NhaMang + N'1', @NhaMang, N'10000',0.7,1)
	INSERT INTO ChietKhau
	VALUES(@NhaMang + N'2', @NhaMang, N'20000',0.7,1)
	INSERT INTO ChietKhau
	VALUES(@NhaMang + N'3', @NhaMang, N'30000',0.7,1)
	INSERT INTO ChietKhau
	VALUES(@NhaMang + N'4', @NhaMang, N'50000',0.7,1)
	INSERT INTO ChietKhau
	VALUES(@NhaMang + N'5', @NhaMang, N'100000',0.7,1)
	INSERT INTO ChietKhau
	VALUES(@NhaMang + N'6', @NhaMang, N'200000',0.7,1)
	INSERT INTO ChietKhau
	VALUES(@NhaMang + N'7', @NhaMang, N'300000',0.7,1)
	INSERT INTO ChietKhau
	VALUES(@NhaMang + N'8', @NhaMang, N'500000',0.7,1)
	INSERT INTO ChietKhau
	VALUES(@NhaMang + N'9', @NhaMang, N'1000000',0.7,1)
END


CREATE TRIGGER tg__DELETENhaMang ON dbo.NhaMang
FOR DELETE
AS
BEGIN
	DECLARE @NhaMang NVARCHAR(50)
	SELECT @NhaMang = TenNhaMang FROM DELETED
	DELETE dbo.ChietKhau WHERE NhaMang = @NhaMang
END

DELETE NhaMang where TenNhaMang = N'Garena'

INSERT INTO NhaMang
VALUES(N'Viettel', N'logo5.jpg')
INSERT INTO NhaMang
VALUES(N'Mobifone', N'logo1.jpg')
INSERT INTO NhaMang
VALUES(N'vietnamobile', N'logo4.jpg')
INSERT INTO NhaMang
VALUES(N'Vinaphone', N'logo2.jpg')
INSERT INTO NhaMang
VALUES(N'Gmobile', N'logo3.jpg')


delete MuaThe


CREATE TRIGGER tg__01 ON dbo.TheNap
FOR INSERT
AS
BEGIN
	DECLARE @MaThe NVARCHAR(50)
	DECLARE @MenhGia NVARCHAR(50)
	DECLARE @NhaMang NVARCHAR(50)
	DECLARE @MaChietKhau NVARCHAR(50)
	SELECT @MaThe = MaThe FROM INSERTED
	SELECT @MenhGia = MenhGia FROM INSERTED
	SELECT @NhaMang = NhaMang FROM INSERTED
	SELECT @MaChietKhau = MaChietKhau FROM dbo.ChietKhau AS CK WHERE CK.MenhGia = @MenhGia AND CK.NhaMang = @NhaMang
	UPDATE dbo.TheNap SET MaChietKhau = @MaChietKhau WHERE MaThe = @MaThe;
END









create table MuaThe(
	tenTK nvarchar(50),
	MaThe nvarchar(50),
	MenhGia nvarchar(50),
	Seri nvarchar(50),
	NhaMang nvarchar(50),
	NgayMua datetime
)
delete MuaThe
select * from MuaThe order by(NgayMua) desc

drop table TheNap
drop table ChiTietHoaDon
drop table DichVuThanhToan
drop table DichVu

create table DichVuThanhToan(
	MaDichVu nvarchar(50),
	MaHoaDon nvarchar(50) primary key,
	DonGia int,
	KhuVuc nvarchar(50),
	DacDiemKhac nvarchar(50),
)

create table TaiKhoan(
	tenTK nvarchar(50) primary key,
	MatKhau nvarchar(50),
	LoaiTK nvarchar(10),
	sdt nvarchar(20),
	ngayLap Datetime,
	DacDiem nvarchar(100),
	SoDu int
)

ALTER TABLE TaiKhoan
  ADD NumberLock int;
ALTER TABLE TaiKhoan
  ADD IP nvarchar(250);

ALTER TABLE TaiKhoan
  ADD TrangThai Nvarchar(50);

ALTER TABLE TaiKhoan
ALTER COLUMN SoDu float;

ALTER TABLE TaiKhoan
  add MatKhauC2 nvarchar(50)

ALTER TABLE TaiKhoan
  add Email nvarchar(100)

create table Register(
	TaiKhoan nvarchar(50),
	Email nvarchar(50),
	Code nvarchar(10)
)
ALTER TABLE TaiKhoan
  add Email nvarchar(100)

ALTER TABLE Register
  add NgayThem DateTime

select top 20 * from TaiKhoan where ngayLap < '14-6-2021 15:15:14'  order by ngaylap desc 


select * from TaiKhoan 
insert into TaiKhoan(tenTK,MatKhau, LoaiTK, sdt, ngayLap,DacDiem,SoDu)
values(N'CHUhoas',N'Khac21','admin','03565485548',GetDate(),'no',0)

create table HoaDon(
	MaHoaDon nvarchar(50) primary key,
	TaiKhoan nvarchar(50),
	NgayGD datetime
)

create table ChiTietHoaDon(
	MaHoaDon nvarchar(50),
	MaKhachHang nvarchar(50),
	Gia int
)

create table TheLienKet(
	NganHang nvarchar(50),
	TaiKhoan nvarchar(50),
	SoThe nvarchar(50),
	NgayLK DateTime,
	TenChuThe nvarchar(50)
)
create table PhanHoi(
	TaiKhoan nvarchar(50),
	TieuDe nvarchar(50),
	NoiDung nvarchar(max),
	Img nvarchar(max)
)
ALTER TABLE PhanHoi
  add NgayPhanHoi datetime

Create table ThongBao(
	MaThongBao nvarchar(250) primary key,
	NoiDung nvarchar(max),
	NgayThem DateTime
)
