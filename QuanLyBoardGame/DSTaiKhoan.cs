using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBoardGame
{
    public partial class DSTaiKhoan : Form
    {
        static MongoClient client = new MongoClient();
        //static MongoClient client = new MongoClient("mongodb+srv://cnpm:Thuydiem29@cluster0.2jmsamm.mongodb.net/");
        static IMongoDatabase db = client.GetDatabase("BoardGame");
        static IMongoCollection<TaiKhoan> collection_DN = db.GetCollection<TaiKhoan>("DangNhap");

        public DSTaiKhoan()
        {
            InitializeComponent();
            ReadAllDocuments_DSTaiKhoan(); 
        }
        public void ReadAllDocuments_DSTaiKhoan()
        {
            List<TaiKhoan> list = collection_DN.AsQueryable().ToList<TaiKhoan>();
            dgvDSTaiKhoan.DataSource = list;
            
        }
    }
}
