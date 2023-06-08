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

        [BsonElement("MaCTDH")]
        public ObjectId MaCTDH { get; set; }
        [BsonElement("MaBC")]
        public ObjectId MaBC { get; set; }

        public CTBaoCao(ObjectId maCTBG, ObjectId maBC)
        {
            MaCTDH = maCTBG;
            MaBC = maBC;
        }
    }
}
