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
        [BsonElement("MaCTDH")]
        public ObjectId MaCTDH { get; set; }
        [BsonElement("MaBG")]
        public ObjectId MaBG { get; set; }

        public DonHang(ObjectId maCTDH,ObjectId maBG)
        {
           MaCTDH = maCTDH;
           MaBG = maBG;
        }
    }
}
