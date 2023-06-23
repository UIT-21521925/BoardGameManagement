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
    public partial class ThemVaoKho : Form
    {
        //static MongoClient client = new MongoClient();
        static MongoClient client = new MongoClient("mongodb+srv://cnpm:Thuydiem29@cluster0.2jmsamm.mongodb.net/");
        static IMongoDatabase db = client.GetDatabase("BoardGame");
        static IMongoCollection<ThongTinBG> collection_BG = db.GetCollection<ThongTinBG>("BoardGame");
        static IMongoCollection<BoardGame> collection_G = db.GetCollection<BoardGame>("Game");
        static IMongoCollection<KhachHang> collection_KH = db.GetCollection<KhachHang>("KhachHang");
        static IMongoCollection<UuDai> collection_UD = db.GetCollection<UuDai>("UuDai");
        static IMongoCollection<DonHang> collection_DH = db.GetCollection<DonHang>("DonHang");
        static IMongoCollection<CTDonHang> collection_CTDH = db.GetCollection<CTDonHang>("CTDonHang");
        static IMongoCollection<CTBaoCao> collection_CTBC = db.GetCollection<CTBaoCao>("CTBaoCao");
        static IMongoCollection<BaoCao> collection_BC = db.GetCollection<BaoCao>("BaoCao");
        static IMongoCollection<LoaiBG> collection_LBG = db.GetCollection<LoaiBG>("LoaiBG");
        static IMongoCollection<BienBan> collection_BB = db.GetCollection<BienBan>("BienBan");
        static IMongoCollection<LoaiPhat> collection_LP = db.GetCollection<LoaiPhat>("LoaiPhat");

        private BoardGame bg;
        private ThongTinBG ttbg;
        internal ThemVaoKho(ThongTinBG ttbg)
        {
            InitializeComponent();
            if (ttbg != null)
            {
                this.ttbg = ttbg;
                HienThiNhap();
            }
        }
        internal ThemVaoKho( BoardGame bg)
        {
            InitializeComponent();
            this.bg = bg;
            HienThiKho();
        }

        public void HienThiKho()
        {
            tbTTBG.ReadOnly = true;
            tbTenTTBG.ReadOnly = true;
            tbMaBoardGame.ReadOnly = true;

            tbTTBG.Text =bg.MaTTBG.ToString();
            var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", bg.MaTTBG);
            List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
            ThongTinBG ttbg = filteredTTBGs[0];
            tbTenTTBG.Text = ttbg.TenBoardGame;
            tbMaBoardGame.Text = bg.MaBG.ToString();
            cbTinhTrangBG.Text = bg.TinhTrangBG;
            cbTinhTrangMuon.Text = bg.TinhTrangMuon;
            tbDatHang.Text = bg.DatHang;
            bThemBG.Text = "Sửa";
        }

        public void HienThiNhap()
        {
            tbTTBG.ReadOnly = true;
            tbTenTTBG.ReadOnly = true;
            tbMaBoardGame.ReadOnly = true;

            tbTTBG.Text = ttbg.MaTTBG.ToString();
            var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", ttbg.MaTTBG);
            List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
            ThongTinBG ttbg1 = filteredTTBGs[0];
            tbTenTTBG.Text = ttbg1.TenBoardGame;
        }

        private void bThemBG_Click(object sender, EventArgs e)
        {
            if (bThemBG.Text == "Sửa")
            {
                if (cbTinhTrangBG.Text != ""&& cbTinhTrangMuon.Text !="")
                {
                    var updateDef = Builders<BoardGame>.Update.Set("TinhTrangBG", cbTinhTrangBG.Text).Set("TinhTrangMuon", cbTinhTrangMuon.Text).Set("DatHang", tbDatHang.Text);
                    collection_G.UpdateOneAsync(bg1 => bg1.MaBG == bg.MaBG, updateDef);
                    MessageBox.Show("Cập nhật thông tin trong kho thành công");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập tình trạng board game!");
                }
            }
            else
            {
                if (cbTinhTrangBG.Text!= "")
                {
                    BoardGame bg = new BoardGame(ObjectId.Parse(tbTTBG.Text), cbTinhTrangBG.Text);
                    collection_G.InsertOneAsync(bg);
                    var filterTTBG = Builders<ThongTinBG>.Filter.Eq("MaTTBG", ttbg.MaTTBG);
                    var updateTTBG = Builders<ThongTinBG>.Update.Inc("SoLuong", 1);
                    collection_BG.UpdateOneAsync(filterTTBG, updateTTBG);
                    MessageBox.Show("Thêm thông tin trong kho thành công");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập tình trạng board game!");
                }
            }
        }

        private void bMDBG_Click(object sender, EventArgs e)
        {
            tbTTBG.Text = "";
            tbTenTTBG.Text = "";
            tbMaBoardGame.Text = "";
            cbTinhTrangBG.Text = "";
            cbTinhTrangMuon.Text = "";
        }
    }
}
