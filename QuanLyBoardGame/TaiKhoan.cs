using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{
    internal class TaiKhoan
    {
        [BsonId]
        public ObjectId MaTK { get; set; }
        [BsonElement("TenTaiKhoan")]
        public string TenTaiKhoan { get; set; }
        [BsonElement("MatKhau")]
        public string MatKhau { get; set; }
    }
}
