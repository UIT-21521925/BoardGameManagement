using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBoardGame
{
    internal class BienBan
    {
        [BsonId]

        public ObjectId MaBB { get; set; }
        [BsonElement("LyDo")]
        public string LyDo { get; set; }
        [BsonElement("MaCTDH")]
        public ObjectId MaCTDH { get; set; }
        [BsonElement("MaLP")]
        public ObjectId MaLP { get; set; }
        

        public BienBan( string lyDo,ObjectId maCTHD, ObjectId maLP)
        {
            LyDo = lyDo;
            MaCTDH = maCTHD;
            MaLP = maLP;
        }
    }
}
