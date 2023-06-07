using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{
    internal class CTDonHang
    {
        [BsonId]

        public ObjectId MaCTDH { get; set; }
        [BsonElement("MaDH")]
        public ObjectId MaDH { get; set; }
        [BsonElement("MaBG")]
        public ObjectId MaBG { get; set; }
        [BsonElement("TongTien")]
        public int TongTien { get; set; }

        public CTDonHang(ObjectId maDH, ObjectId maBG)
        {
            MaDH = maDH;
            MaBG = maBG;
        }
    }
}
