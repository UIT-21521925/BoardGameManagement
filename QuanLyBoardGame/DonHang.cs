using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{
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
}
