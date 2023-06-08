using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{
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

        public KhachHang( string tenKH, DateTime ngSinh, string diaChi, string email, string sdt)
        {
            TenKH = tenKH;
            NgSinh = ngSinh;
            DiaChi = diaChi;
            Email = email;
            SDT = sdt;
            TichDiem = 0;
        }
    }
}
