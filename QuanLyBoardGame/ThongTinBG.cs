using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace QuanLyBoardGame
{
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
        public decimal TriGia { get; set; }
        [BsonElement("GiaThue")]
        public decimal GiaThue { get; set; }
        [BsonElement("SoLuong")]
        public int SoLuong { get; set; }
        [BsonElement("TinhTrangBG")]
        public string TinhTrangBG { get; set; }
        [BsonElement("MaLBG")]
        public string MaLBG { get; set; }

        public ThongTinBG(string tenBoardGame,int soNguoiChoi,int doTuoi,
            int triGia, int giaThue, int soLuong, string tinhTrangBG, string maLBG)
        {
            TenBoardGame = tenBoardGame;
            SoNguoiChoi = soNguoiChoi;
            DoTuoi = doTuoi;
            TriGia = triGia;
            GiaThue = soLuong;
            SoLuong = soLuong;
            TinhTrangBG = tinhTrangBG;
            MaLBG = maLBG;
        }
    }
}
