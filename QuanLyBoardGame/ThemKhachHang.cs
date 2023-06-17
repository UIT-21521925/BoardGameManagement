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
    public partial class ThemKhachHang : Form
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
        private KhachHang kh;
        public ThemKhachHang()
        {
            InitializeComponent();
            tbSoTichDiem.ReadOnly = true;
        }
        internal ThemKhachHang(KhachHang kh)
        {
            InitializeComponent();
            if (kh != null)
            {
                this.kh = kh;
                HienThiKhachHang();
            }
        }

        public void HienThiKhachHang()
        {
            tbMaKhachHang.ReadOnly = true;
            tbSoTichDiem.ReadOnly = true;

            tbMaKhachHang.Text =kh.MaKH.ToString();
            tbTenKhachHang.Text = kh.TenKH;
            dtpNgaySinh.Text = kh.NgSinh.ToString();
            tbDiaChi.Text = kh.DiaChi;
            tbSoDienThoai.Text = kh.SDT;
            tbEmail.Text = kh.Email;
            tbSoTichDiem.Text = kh.TichDiem.ToString();
            bThemKhachHang.Text = "Sửa";
        }
        private void bThemKhachHang_Click(object sender, EventArgs e)
        {
            if(bThemKhachHang.Text == "Sửa")
            {
                var updateDef = Builders<KhachHang>.Update.Set("TenKH", tbTenKhachHang.Text).Set("NgSinh", dtpNgaySinh.Value).Set("DiaChi", tbDiaChi.Text).Set("SDT", tbSoDienThoai.Text).Set("Email", tbEmail.Text).Set("TichDiem", int.Parse(tbSoTichDiem.Text));
                collection_KH.UpdateOneAsync(kh => kh.MaKH == ObjectId.Parse(tbMaKhachHang.Text), updateDef);
                MessageBox.Show("Cập nhật thông tin khách hàng thành công");
                this.Hide();
            }
            else
            {
                if (tbTenKhachHang.Text != ""&
                dtpNgaySinh.Text != ""&
                tbDiaChi.Text != ""&
                tbSoDienThoai.Text != ""&
                tbEmail.Text != "") 
                {
                    KhachHang kh = new KhachHang(tbTenKhachHang.Text, dtpNgaySinh.Value, tbDiaChi.Text, tbEmail.Text, tbSoDienThoai.Text);
                    collection_KH.InsertOneAsync(kh);
                    MessageBox.Show("Thêm thông tin khách hàng thành công");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin khách hàng");
                }
            }
        }

        private void bMDKhachHang_Click(object sender, EventArgs e)
        {
            tbMaKhachHang.Text = "";
            tbTenKhachHang.Text = "";
            dtpNgaySinh.Text = "";
            tbDiaChi.Text = "";
            tbSoDienThoai.Text = "";
            tbEmail.Text = "";
            tbSoTichDiem.Text = "0";
        }

        private void tbSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho ký tự được hiển thị trong text box
            }
        }
    }
}
