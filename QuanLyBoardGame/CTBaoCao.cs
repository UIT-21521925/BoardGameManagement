using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{
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
}
