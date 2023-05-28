using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{
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
        public double PhanTramGiam { get; set; }
        [BsonElement("SoLuong")]
        public int SoLuong { get; set; }
        [BsonElement("DiemQuyDoi")]
        public int DiemQuyDoi { get; set; }

        public UuDai(string tenUD, string moTa, DateTime ngayBD, DateTime ngayKT, double phanTramGiam, int soLuong, int diemQuyDoi)
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
}
