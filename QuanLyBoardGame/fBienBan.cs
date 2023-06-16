using MongoDB.Bson;
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
    public partial class Form1 : Form
    {
        //static MongoClient client = new MongoClient();
        static MongoClient client = new MongoClient("mongodb+srv://cnpm:Thuydiem29@cluster0.2jmsamm.mongodb.net/");
        static IMongoDatabase db = client.GetDatabase("BoardGame");
        static IMongoCollection<BienBan> collection_BB = db.GetCollection<BienBan>("BienBan");
        static IMongoCollection<LoaiPhat> collection_LP = db.GetCollection<LoaiPhat>("LoaiPhat");
        static IMongoCollection<KhachHang> collection_KH = db.GetCollection<KhachHang>("KhachHang");
        static IMongoCollection<DonHang> collection_DH = db.GetCollection<DonHang>("DonHang");
        static IMongoCollection<CTDonHang> collection_CTDH = db.GetCollection<CTDonHang>("CTDonHang");
        static IMongoCollection<BoardGame> collection_G = db.GetCollection<BoardGame>("Game");
        internal DonHang dh;
        internal Form1(DonHang dh)
        {
            InitializeComponent();
            if (dh != null)
            {
                this.dh = dh;
                ReadAllDocuments_BienBan();

            }
            else
            {
                MessageBox.Show("Không tìm thấy mã đơn hàng");
            }
        }

        public void ReadAllDocuments_BienBan()
        {
            var thongTinKHquery = Builders<KhachHang>.Filter.Eq("MaKH", dh.MaKH );
            List<KhachHang> filteredKHs = collection_KH.Find(thongTinKHquery).ToList();
            if (filteredKHs.Count > 0)
            {
                KhachHang kh = filteredKHs[0];
                tbTenKHVP.Text = kh.TenKH.ToString();
            }
                var thongTinDHquery = Builders<CTDonHang>.Filter.Eq("MaDH", dh.MaDH);
                List<CTDonHang> filtereDHHs = collection_CTDH.Find(thongTinDHquery).ToList();
                cbBGVP.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới

                foreach (var donHang in filtereDHHs)
                {
                    cbBGVP.Items.Add(donHang.MaBG);
                }
                List<LoaiPhat> list = collection_LP.AsQueryable().ToList<LoaiPhat>();
                foreach (var lp in list)
                {
                    cbLoaiVP.Items.Add(lp.TenLoaiPhat);
                }
            
        }

        private void bSuaBG_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bXacNhanVP_Click(object sender, EventArgs e)
        {
            var thongTinLPquery = Builders<LoaiPhat>.Filter.Eq("TenLoaiPhat", cbLoaiVP.Text);
            List<LoaiPhat> filteredLPs = collection_LP.Find(thongTinLPquery).ToList();
            LoaiPhat lp = filteredLPs[0];
            BienBan bb = new BienBan(tbLyDoVP.Text, dh.MaDH, lp.MaLP);
            collection_BB.InsertOneAsync(bb);
            MessageBox.Show("Cập nhật biên bản thành công");
            var updateDef = Builders<BoardGame>.Update.Set("TinhTrangBG", cbTTBGVP.Text);
            collection_G.UpdateOneAsync(bg => bg.MaBG == ObjectId.Parse(cbBGVP.Text), updateDef);
        }

        private void cbBGVP_SelectedIndexChanged(object sender, EventArgs e)
        {
            var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaBG", ObjectId.Parse(cbBGVP.Text));
            List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();

            if (filteredBGs.Count > 0)
            {
                BoardGame bg = filteredBGs[0];
                cbTTBGVP.Text = bg.TinhTrangBG.ToString();
            }
            else
            {
                MessageBox.Show("Trống");
            }
        }

        private void cbLoaiVP_SelectedIndexChanged(object sender, EventArgs e)
        {
            var thongTinLPquery = Builders<LoaiPhat>.Filter.Eq("TenLoaiPhat", cbLoaiVP.Text);
            List<LoaiPhat> filteredLPs = collection_LP.Find(thongTinLPquery).ToList();

            if (filteredLPs.Count > 0)
            {
                LoaiPhat lp = filteredLPs[0];
                tbTienPhatVP.Text = lp.SoTienPhat.ToString();
            }
            else
            {
                MessageBox.Show("Trống");
            }
        }
    }
}
