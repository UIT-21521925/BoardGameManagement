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
    public partial class ThongTinDonHang : Form
    {
        static MongoClient client = new MongoClient();
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
        private DonHang dh;
        internal ThongTinDonHang(DonHang dh)
        {
            InitializeComponent();
            if(dh != null)
            {
                this.dh = dh;
                HienThiDonHang();
            }
        }


        public void HienThiDonHang()
        {
            tbTenKH.ReadOnly = true;
            tbSdtKH.ReadOnly = true;
            dtpNgayTraDH.ReadOnly = true;
            dtpNgayThueDH.ReadOnly = true;
            tbTrangThaiDH.ReadOnly = true;
            tbUuDaiSD.ReadOnly = true;
            tbTienCoc.ReadOnly = true;
            tbTongTien.ReadOnly = true;

            var thongTinKHquery = Builders<KhachHang>.Filter.Eq("MaKH",dh.MaKH);
            List<KhachHang> filteredKHs = collection_KH.Find(thongTinKHquery).ToList();
            KhachHang kh = filteredKHs[0];

            tbTenKH.Text = kh.TenKH;
            tbSdtKH.Text = kh.SDT;
            dtpNgayThueDH.Text = dh.NgayThue.ToString();
            dtpNgayTraDH.Text =dh.NgayTra.ToString();
            tbTrangThaiDH.Text = dh.TrangThai;
            var thongTinUDquery = Builders<UuDai>.Filter.Eq("MaUD", dh.MaUD);
            List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
            if (filteredUDs.Count > 0)
            {
                UuDai ud = filteredUDs[0];
                tbUuDaiSD.Text = ud.TenUD;
            }
            tbTienCoc.Text = dh.TienCoc.ToString();
            tbTongTien.Text = dh.TongTien.ToString();


            var thongTinCTDHquery = Builders<CTDonHang>.Filter.Eq("MaDH", dh.MaDH);
            List<CTDonHang> filteredCTDHs = collection_CTDH.Find(thongTinCTDHquery).ToList();
            dgvDSBGDH.DataSource= filteredCTDHs;
        }


        private void bBienBan_Click(object sender, EventArgs e)
        {
                
                    var thongTinCTDHquery = Builders<DonHang>.Filter.Eq("MaDH", dh.MaDH);
                    List<DonHang> filteredCTDHs = collection_DH.Find(thongTinCTDHquery).ToList();

                    if (filteredCTDHs.Count > 0)
                    {
                        DonHang dh = filteredCTDHs[0];
                        Form1 form1 = new Form1(dh);
                        form1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("lỗi kết nối");
                    }
               
        }

        private void bDaTra_Click(object sender, EventArgs e)
        {
            dh.TrangThai = "Da tra";
            MessageBox.Show("Xác nhận đã trả thành công");
            this.Close();
            
        }
    }
}
