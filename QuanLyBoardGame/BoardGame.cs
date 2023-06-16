using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{
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
        public BoardGame(ObjectId maTTBG,string tinhTrangBG)
        {
            MaTTBG = maTTBG;
            TinhTrangBG = tinhTrangBG;
            TinhTrangMuon = "Chưa được thuê";
            DatHang = "";
        }
    }
}
