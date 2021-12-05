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

create table LoaiThe(
	NhaMang nvarchar(50) primary key,
	MenhGia int,
	ChietKhauNap float,
	ChietKhauBan float,

)





create table TheNap(
	MaThe nvarchar(50) primary key,
	MenhGia nvarchar(50),
	Seri nvarchar(50),
	NhaMang nvarchar(50),
	NgayThem datetime
)

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
drop table LoaiThe
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