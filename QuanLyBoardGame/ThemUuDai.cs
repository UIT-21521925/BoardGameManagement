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
    public partial class ThemUuDai : Form
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

        private UuDai ud;
        public ThemUuDai()
        {
            InitializeComponent();
        }
        internal ThemUuDai(UuDai ud)
        {
            InitializeComponent();
            if(ud != null)
            {
                this.ud=ud;
                HienThiUuDai();
            }
        }

        public void HienThiUuDai()
        {
            tbMaUuDai.ReadOnly = true;

            tbMaUuDai.Text =ud.MaUD.ToString();
            tbTenUuDai.Text = ud.TenUD;
            tbMoTa.Text = ud.MoTa;
            dtpNgayBD.Text = ud.NgayBD.ToString();
            dtpNgayKT.Text = ud.NgayKT.ToString();
            tbPhanTramGiam.Text = ud.PhanTramGiam.ToString();
            tbSoLuongUD.Text = ud.SoLuong.ToString();
            tbSoLuongQD.Text = ud.DiemQuyDoi.ToString();

            bThemUuDai.Text = "Sửa";
        }

        private void bThemUuDai_Click(object sender, EventArgs e)
        {
            if(bThemUuDai.Text == "Sửa")
            {
                var updateDef = Builders<UuDai>.Update.Set("TenUD", tbTenUuDai.Text).Set("MoTa", tbMoTa.Text).Set("NgayBD", dtpNgayBD.Value).Set("NgayKT", dtpNgayKT.Value).Set("PhanTramGiam", double.Parse(tbPhanTramGiam.Text)).Set("SoLuong", int.Parse(tbSoLuongUD.Text)).Set("DiemQuyDoi", int.Parse(tbSoLuongQD.Text));
                collection_UD.UpdateOneAsync(ud => ud.MaUD == ObjectId.Parse(tbMaUuDai.Text), updateDef);
                MessageBox.Show("Cập nhật thông tin ưu đãi thành công");
                this.Hide();
            }
            else
            {
                if (tbTenUuDai.Text != "" &
                tbMoTa.Text != "" &
                dtpNgayBD.Text != "" &
                dtpNgayKT.Text != "" &
                tbPhanTramGiam.Text != "0" &
                tbSoLuongUD.Text != "0") {
                    UuDai ud = new UuDai(tbTenUuDai.Text, tbMoTa.Text, dtpNgayBD.Value, dtpNgayKT.Value, int.Parse(tbPhanTramGiam.Text), int.Parse(tbSoLuongUD.Text), int.Parse(tbSoLuongQD.Text));
                    collection_UD.InsertOneAsync(ud);
                    MessageBox.Show("Thêm thông tin ưu đãi thành công");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin ưu đãi");
                }
            }
        }

        private void bMDUuDai_Click(object sender, EventArgs e)
        {
            tbMaUuDai.ReadOnly = true;

            tbMaUuDai.Text = "";
            tbTenUuDai.Text = "";
            tbMoTa.Text = "";
            dtpNgayBD.Text = "";
            dtpNgayKT.Text = "";
            tbPhanTramGiam.Text = "";
            tbSoLuongUD.Text = "";
            tbSoLuongQD.Text = "";
        }
    }
}
