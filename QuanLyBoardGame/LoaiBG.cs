using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{
    internal class LoaiBG
    {
        [BsonId]

        public ObjectId MaLBG { get; set; }
        [BsonElement("TenLBG")]
        public string TenLBG { get; set; }
        public LoaiBG( string tenLBG)
        {
            TenLBG = tenLBG;
        }
    }
}
