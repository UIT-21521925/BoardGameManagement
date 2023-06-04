using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{
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
        [BsonElement("TongDoanhThu")]
        public decimal TongDoanhThu { get; set; }

        public BaoCao(int thang, int nam)
        {
            Thang = thang;
            Nam = nam;
            TongDoanhThu = 0;
            SoDonHang = 0;
        }
    }
}
