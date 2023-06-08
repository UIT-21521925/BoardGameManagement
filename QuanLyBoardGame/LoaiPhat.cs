using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{
    internal class LoaiPhat
    {
        [BsonId]

        public ObjectId MaLP { get; set; }
        [BsonElement("TenLoaiPhat")]
        public string TenLoaiPhat { get; set; }
        [BsonElement("SoTienPhat")]
        public decimal SoTienPhat { get; set; }
        
        public LoaiPhat( string tenLoaiPhat, decimal soTienPhat)
        {
            TenLoaiPhat = tenLoaiPhat;
            SoTienPhat = soTienPhat;
        }
    }
}
