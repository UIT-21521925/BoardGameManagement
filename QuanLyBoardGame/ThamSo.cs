using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{

    //Quản lí board game
    internal class BoardGame
    {
        [BsonId]
        //MaTTBG,SoNguoiChoi,DoTuoi,TriGia,GiaThue,SoLuong,HinhAnh,TinhTrangBG,MaLBG
        public ObjectId MaBG { get; set; }
        [BsonElement("MaTTBG")]
        public ObjectId MaTTBG { get; set; }
        [BsonElement("TinhTrangBG")]
        public string TinhTrangBG { get; set; }
        [BsonElement("TinhTrangMuon")]
        public string TinhTrangMuon { get; set; }
        [BsonElement("DatHang")]
        public string DatHang { get; set; }
        public BoardGame(ObjectId maTTBG, string tinhTrangBG)
        {
            MaTTBG = maTTBG;
            TinhTrangBG = tinhTrangBG;
            TinhTrangMuon = "Chưa được thuê";
            DatHang = "";
        }
    }

    internal class ThongTinBG
    {
        [BsonId]
        //MaTTBG,SoNguoiChoi,DoTuoi,TriGia,GiaThue,SoLuong,HinhAnh,TinhTrangBG,MaLBG
        public ObjectId MaTTBG { get; set; }
        [BsonElement("TenBoardGame")]
        public string TenBoardGame { get; set; }
        [BsonElement("SoNguoiChoi")]
        public int SoNguoiChoi { get; set; }
        [BsonElement("DoTuoi")]
        public int DoTuoi { get; set; }
        [BsonElement("TriGia")]
        public int TriGia { get; set; }
        [BsonElement("GiaThue")]
        public int GiaThue { get; set; }
        [BsonElement("SoLuong")]
        public int SoLuong { get; set; }
        [BsonElement("HinhAnh")]
        public string HinhAnh { get; set; }
        [BsonElement("LuatChoi")]
        public string LuatChoi { get; set; }
        [BsonElement("ThoiGianChoi")]
        public int ThoiGianChoi { get; set; }
        [BsonElement("MaLBG")]
        public ObjectId MaLBG { get; set; }

        public ThongTinBG(string tenBoardGame, int soNguoiChoi, int doTuoi,
            int triGia, int giaThue, string hinhAnh, string luatChoi, int thoiGianChoi, ObjectId maLBG)
        {
            TenBoardGame = tenBoardGame;
            SoNguoiChoi = soNguoiChoi;
            DoTuoi = doTuoi;
            TriGia = triGia;
            GiaThue = giaThue;
            SoLuong = 0;
            HinhAnh = hinhAnh;
            LuatChoi = luatChoi;
            ThoiGianChoi = thoiGianChoi;
            MaLBG = maLBG;
        }
    }

    internal class LoaiBG
    {
        [BsonId]

        public ObjectId MaLBG { get; set; }
        [BsonElement("TenLBG")]
        public string TenLBG { get; set; }
        public LoaiBG(string tenLBG)
        {
            TenLBG = tenLBG;
        }
    }

    //------------------------------------------------------------------------------------------------------
    //Quản lí khách hàng

    internal class KhachHang
    {
        [BsonId]

        public ObjectId MaKH { get; set; }
        [BsonElement("TenKH")]
        public string TenKH { get; set; }
        [BsonElement("NgSinh")]
        public DateTime NgSinh { get; set; }
        [BsonElement("DiaChi")]
        public string DiaChi { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("SDT")]
        public string SDT { get; set; }
        [BsonElement("TichDiem")]
        public int TichDiem { get; set; }

        public KhachHang(string tenKH, DateTime ngSinh, string diaChi, string email, string sdt)
        {
            TenKH = tenKH;
            NgSinh = ngSinh;
            DiaChi = diaChi;
            Email = email;
            SDT = sdt;
            TichDiem = 0;
        }
    }
    //------------------------------------------------------------------------------------------------------
    //Quản lí ưu đãi

    internal class UuDai
    {
        [BsonId]

        public ObjectId MaUD { get; set; }
        [BsonElement("TenUD")]
        public string TenUD { get; set; }
        [BsonElement("MoTa")]
        public string MoTa { get; set; }
        [BsonElement("NgayBD")]
        public DateTime NgayBD { get; set; }
        [BsonElement("NgayKT")]
        public DateTime NgayKT { get; set; }
        [BsonElement("PhanTramGiam")]
        public int PhanTramGiam { get; set; }
        [BsonElement("SoLuong")]
        public int SoLuong { get; set; }
        [BsonElement("DiemQuyDoi")]
        public int DiemQuyDoi { get; set; }

        public UuDai(string tenUD, string moTa, DateTime ngayBD, DateTime ngayKT, int phanTramGiam, int soLuong, int diemQuyDoi)
        {
            TenUD = tenUD;
            MoTa = moTa;
            NgayBD = ngayBD;
            NgayKT = ngayKT;
            PhanTramGiam = phanTramGiam;
            SoLuong = soLuong;
            DiemQuyDoi = diemQuyDoi;
        }
    }

    //------------------------------------------------------------------------------------------------------
    //Quản lí đơn hàng

    internal class CTDonHang
    {
        [BsonId]

        public ObjectId MaCTDH { get; set; }
        [BsonElement("MaDH")]
        public ObjectId MaDH { get; set; }
        [BsonElement("MaBG")]
        public ObjectId MaBG { get; set; }

        public CTDonHang(ObjectId maDH, ObjectId maBG)
        {
            MaDH = maDH;
            MaBG = maBG;
        }
    }
    internal class DonHang
    {

        [BsonId]

        public ObjectId MaDH { get; set; }
        [BsonElement("NgayThue")]
        public DateTime NgayThue { get; set; }
        [BsonElement("NgayTra")]
        public DateTime NgayTra { get; set; }
        [BsonElement("TrangThai")]
        public string TrangThai { get; set; }
        [BsonElement("MaKH")]
        public ObjectId MaKH { get; set; }
        [BsonElement("MaUD")]
        public ObjectId MaUD { get; set; }
        [BsonElement("TienCoc")]
        public int TienCoc { get; set; }
        [BsonElement("TongTien")]
        public int TongTien { get; set; }

        public DonHang(DateTime ngayThue, DateTime ngayTra, string trangThai, ObjectId maKH, ObjectId maUD, int tienCoc, int tongTien)
        {
            NgayThue = ngayThue;
            NgayTra = ngayTra;
            TrangThai = trangThai;
            MaKH = maKH;
            MaUD = maUD;
            TongTien = tongTien;
            TienCoc = tienCoc;
        }
    }
    //------------------------------------------------------------------------------------------------------
    //Quản lí báo cáo

    internal class BaoCao
    {
        [BsonId]

        public ObjectId MaBC { get; set; }
        [BsonElement("Thang")]
        public int Thang { get; set; }
        [BsonElement("Nam")]
        public int Nam { get; set; }
        [BsonElement("SoDonHang")]
        public int SoDonHang { get; set; }
        [BsonElement("SoBienBan")]
        public int SoBIenBan { get; set; }
        [BsonElement("TongDoanhThu")]
        public decimal TongDoanhThu { get; set; }

        public BaoCao(int thang, int nam)
        {
            Thang = thang;
            Nam = nam;
            TongDoanhThu = 0;
            SoDonHang = 0;
            SoBIenBan = 0;
        }
    }
    internal class CTBaoCao
    {
        [BsonId]

        public ObjectId MaCTBC { get; set; }

        [BsonElement("MaLBG")]
        public ObjectId MaLBG { get; set; }

        [BsonElement("MaBC")]
        public ObjectId MaBC { get; set; }

        [BsonElement("SoLuongDonHang")]
        public int SoLuongDonHang { get; set; }

        [BsonElement("DoanhThu")]
        public int DoanhThu { get; set; }



        public CTBaoCao(ObjectId maLBG, ObjectId maBC, int soLuongDonHang, int doanhThu)
        {
            MaLBG = maLBG;
            MaBC = maBC;
            SoLuongDonHang = soLuongDonHang;
            DoanhThu = doanhThu;
        }
    }

    //------------------------------------------------------------------------------------------------------
    //Quản lí biên bản

    internal class BienBan
    {
        [BsonId]

        public ObjectId MaBB { get; set; }
        [BsonElement("LyDo")]
        public string LyDo { get; set; }
        [BsonElement("MaCTDH")]
        public ObjectId MaCTDH { get; set; }
        [BsonElement("MaLP")]
        public ObjectId MaLP { get; set; }


        public BienBan(string lyDo, ObjectId maCTHD, ObjectId maLP)
        {
            LyDo = lyDo;
            MaCTDH = maCTHD;
            MaLP = maLP;
        }
    }




    internal class LoaiPhat
    {
        [BsonId]

        public ObjectId MaLP { get; set; }
        [BsonElement("TenLoaiPhat")]
        public string TenLoaiPhat { get; set; }
        [BsonElement("SoTienPhat")]
        public decimal SoTienPhat { get; set; }

        public LoaiPhat(string tenLoaiPhat, decimal soTienPhat)
        {
            TenLoaiPhat = tenLoaiPhat;
            SoTienPhat = soTienPhat;
        }
    }

    //------------------------------------------------------------------------------------------------------
    //Quản lí đăng nhập

    internal class TaiKhoan
    {
        [BsonId]
        public ObjectId MaTK { get; set; }
        [BsonElement("TenTaiKhoan")]
        public string TenTaiKhoan { get; set; }
        [BsonElement("MatKhau")]
        public string MatKhau { get; set; }
        [BsonElement("ChucVu")]
        public string ChucVu { get; set; }

        public TaiKhoan(string tenTaiKhoan, string matKhau, string chucVu)
        {
            TenTaiKhoan = tenTaiKhoan;
            MatKhau = matKhau;
            ChucVu = chucVu;
        }
    }

    //------------------------------------------------------------------------------------------------------
    //Quản lí tham số

    internal class ThamSo
    {
        [BsonId]

        public ObjectId MaTS { get; set; }
        [BsonElement("SoNgayThueTD")]
        public int SoNgayThueTD { get; set; }
        [BsonElement("SoNgayThueTT")]
        public int SoNgayThueTT { get; set; }
        [BsonElement("PhanTramCoc")]
        public int PhanTramCoc { get; set; }
        [BsonElement("SoDonHangTD")]
        public int SoDonHangTD { get; set; }
        [BsonElement("SoBoardGameTD")]
        public decimal SoBoardGameTD { get; set; }

        public ThamSo(int soNgayThueTD, int soNgayThueTT, int phanTramCoc,int soDonHangTD,int soBoardGameTD)
        {
            SoBoardGameTD = soNgayThueTD;
            SoNgayThueTT = soNgayThueTT;
            PhanTramCoc = phanTramCoc;
            SoDonHangTD = soDonHangTD;
            SoBoardGameTD = soBoardGameTD;
        }
    }
    
    
    
    
   
}
