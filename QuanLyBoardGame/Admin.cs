using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace QuanLyBoardGame
{
    public partial class Admin : Form
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
        private TaiKhoan taikhoan;
        internal Admin(TaiKhoan taikhoan)
        {
            InitializeComponent();
            ReadAllDocuments_ThongTinBG();
            ReadAllDocuments_TTBG();
            ReadAllDocuments_KH();
            ReadAllDocuments_UD();
            ReadAllDocuments_DSKH();
            this.taikhoan = taikhoan;
        }

        private void bDanhMuc_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 1;
            List<LoaiBG> listLBGs = collection_LBG.AsQueryable().ToList<LoaiBG>();
            cbTheLoai.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var lbg in listLBGs)
            {
                cbTheLoai.Items.Add(lbg.TenLBG);
            }
            labelText.Text = this.Text;
            ReadAllDocuments_ThongTinBG();
        }

        private void bKho_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 2;
            labelText.Text = this.Text;
            ReadAllDocuments_ThongTinBG();
            ReadAllDocuments_TTBG();
        }

        private void bKhachHang_Click(object sender, EventArgs e)
        {
            ReadAllDocuments_KH();
            tabQuanLy.SelectedIndex = 3;
            labelText.Text = this.Text;
        }

        private void bThue_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 4;
            List<UuDai> listUDs = collection_UD.AsQueryable().ToList<UuDai>();
            cbMaUuDaiSD.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var ud in listUDs)
            {
                cbMaUuDaiSD.Items.Add(ud.TenUD);
            }
            labelText.Text = this.Text;
        }

        private void bBaoCao_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 5;
            labelText.Text = this.Text;
        }
        private void bUuDai_Click(object sender, EventArgs e)
        {
            ReadAllDocuments_UD();
            tabQuanLy.SelectedIndex = 6;
            labelText.Text = this.Text;
        }

        private void bLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                this.Hide();
                DangNhap dangNhap = new DangNhap();
                dangNhap.ShowDialog();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg, *.png, *.bmp)|*.jpg;*.png;*.bmp|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Xử lý tệp tin đã chọn ở đây
                    string selectedFilePath = openFileDialog.FileName;
                    // Tiếp theo, bạn có thể thực hiện các thao tác khác với tệp tin ảnh này
                }
            }
        }

        private void bBienBan_Click(object sender, EventArgs e)
        {
            if (tbMaCTDH.Text != null)
            {
                var thongTinCTDHquery = Builders<DonHang>.Filter.Eq("MaDH", ObjectId.Parse(tbMaCTDH.Text));
                List<DonHang> filteredCTDHs = collection_DH.Find(thongTinCTDHquery).ToList();

                if (filteredCTDHs.Count > 0)
                {
                    DonHang ctdh = filteredCTDHs[0];
                    Form1 form1 = new Form1(ctdh);
                    form1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("lỗi kết nối");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn đơn hàng để lập hóa đơn");
            }
        }

        //Quản lý Board game
        public void ReadAllDocuments_ThongTinBG()
        {
            List<ThongTinBG> list = collection_BG.AsQueryable().ToList<ThongTinBG>();
            dgvTT.DataSource = list;
            tbSoLuong.ReadOnly = true;
            if (dgvTT.Rows.Count > 0)
            {
                tbMaThongTin.Text = dgvTT.Rows[0].Cells[0].Value.ToString();
                tbTenBoardGame.Text = dgvTT.Rows[0].Cells[1].Value.ToString();
                nudSoNguoiChoi.Text = dgvTT.Rows[0].Cells[2].Value.ToString();
                tbDoTuoi.Text = dgvTT.Rows[0].Cells[3].Value.ToString();
                tbTriGia.Text = dgvTT.Rows[0].Cells[4].Value.ToString();
                tbGiaThue.Text = dgvTT.Rows[0].Cells[5].Value.ToString();
                tbSoLuong.Text = dgvTT.Rows[0].Cells[6].Value.ToString();
                cbTinhTrangTTBG.Text = dgvTT.Rows[0].Cells[7].Value.ToString();
                var thongTinLBGquery = Builders<LoaiBG>.Filter.Eq("MaLBG", ObjectId.Parse(dgvTT.Rows[0].Cells[8].Value.ToString()));
                List<LoaiBG> filteredLBGs = collection_LBG.Find(thongTinLBGquery).ToList();
                LoaiBG lbg = filteredLBGs[0];
                cbTheLoai.Text = dgvTT.Rows[0].Cells[8].Value.ToString();
            }
        }
        private void dgvTT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbMaThongTin.Text = dgvTT.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbTenBoardGame.Text = dgvTT.Rows[e.RowIndex].Cells[1].Value.ToString();
            nudSoNguoiChoi.Text = dgvTT.Rows[e.RowIndex].Cells[2].Value.ToString();
            tbDoTuoi.Text = dgvTT.Rows[e.RowIndex].Cells[3].Value.ToString();
            tbTriGia.Text = dgvTT.Rows[e.RowIndex].Cells[4].Value.ToString();
            tbGiaThue.Text = dgvTT.Rows[e.RowIndex].Cells[5].Value.ToString();
            tbSoLuong.Text = dgvTT.Rows[e.RowIndex].Cells[6].Value.ToString();
            cbTinhTrangTTBG.Text = dgvTT.Rows[e.RowIndex].Cells[7].Value.ToString();
            var thongTinLBGquery = Builders<LoaiBG>.Filter.Eq("MaLBG", ObjectId.Parse(dgvTT.Rows[e.RowIndex].Cells[8].Value.ToString()));
            List<LoaiBG> filteredLBGs = collection_LBG.Find(thongTinLBGquery).ToList();
            LoaiBG lbg = filteredLBGs[0];
            cbTheLoai.Text = lbg.TenLBG;
        }

        private void bThemTT_Click(object sender, EventArgs e)
        {
            var thongTinLBGquery = Builders<LoaiBG>.Filter.Eq("TenLBG", ObjectId.Parse(cbTheLoai.Text));
            List<LoaiBG> filteredLBGs = collection_LBG.Find(thongTinLBGquery).ToList();
            LoaiBG lbg = filteredLBGs[0];
            ThongTinBG ttbg = new ThongTinBG(tbTenBoardGame.Text, int.Parse(nudSoNguoiChoi.Text)
                , int.Parse(tbDoTuoi.Text), int.Parse(tbTriGia.Text), int.Parse(tbGiaThue.Text)
                , cbTinhTrangTTBG.Text, lbg.MaLBG);
            collection_BG.InsertOneAsync(ttbg);
            ReadAllDocuments_ThongTinBG();
        }

        private void bSua_Click(object sender, EventArgs e)
        {
            var updateDef = Builders<ThongTinBG>.Update.Set("TenBoardGame", tbTenBoardGame.Text).Set("SoNguoiChoi", nudSoNguoiChoi.Text).Set("DoTuoi", tbDoTuoi.Text).Set("TriGia", tbTriGia.Text).Set("GiaThue", tbGiaThue.Text).Set("SoLuong", tbSoLuong.Text).Set("TinhTrangBG", cbTinhTrangTTBG.Text).Set("MaLBG", cbTheLoai.Text);
            collection_BG.UpdateOneAsync(ttbg => ttbg.MaTTBG == ObjectId.Parse(tbMaThongTin.Text), updateDef);
            ReadAllDocuments_ThongTinBG();
        }

        private void bMDTT_Click(object sender, EventArgs e)
        {
            tbMaThongTin.ReadOnly = true;
            tbMaThongTin.Text = "";
            tbTenBoardGame.Text = "";
            nudSoNguoiChoi.Text = "";
            tbDoTuoi.Text = "";
            tbTriGia.Text = "";
            tbGiaThue.Text = "";
            tbSoLuong.Text = "";
            cbTinhTrangTTBG.Text = "";
            cbTheLoai.Text = "";
        }

        private void bTimKiemTT_Click(object sender, EventArgs e)
        {
            List<ThongTinBG> listTTBG = collection_BG.AsQueryable().ToList<ThongTinBG>();
            List<ThongTinBG> filteredTTBGList = new List<ThongTinBG>();

            foreach (var ttbg in listTTBG)
            {
                if (ttbg.TenBoardGame == tbTimKiemTT.Text)
                {
                    filteredTTBGList.Add(ttbg);
                }
            }
            var bindingSourceTTBG = new BindingSource();
            bindingSourceTTBG.DataSource = filteredTTBGList;
            dgvTT.DataSource = bindingSourceTTBG;
        }

        private void bMacDinhTT_Click(object sender, EventArgs e)
        {
            tbTimKiemTT.Text = "";
            ReadAllDocuments_ThongTinBG();
        }

        //Quản lý Kho

        public void ReadAllDocuments_TTBG()
        {
            List<ThongTinBG> list = collection_BG.AsQueryable().ToList<ThongTinBG>();
            dgvTTBG.DataSource = list;
            if (dgvTTBG.Rows.Count > 0)
            {
                tbTTBG.Text = dgvTTBG.Rows[0].Cells[0].Value.ToString();
                tbTenTTBG.Text = dgvTTBG.Rows[0].Cells[1].Value.ToString();
            }
        }

        public void ReadAllDocuments_BG()
        {
            List<BoardGame> list = collection_G.AsQueryable().ToList<BoardGame>();
            List<BoardGame> filteredTTBGList = new List<BoardGame>();
            foreach (var bg in list)
            {
                if (bg.MaTTBG == ObjectId.Parse(tbTTBG.Text))
                {
                    filteredTTBGList.Add(bg);
                }
            }
            var bindingSourceTTBG = new BindingSource();
            bindingSourceTTBG.DataSource = filteredTTBGList;
            dgvBG.DataSource = bindingSourceTTBG;
            tbTTBG.ReadOnly = true;
            tbTenTTBG.ReadOnly = true;
            if (dgvBG.Rows.Count > 0)
            {
                tbMaBoardGame.Text = dgvBG.Rows[0].Cells[0].Value.ToString();
                cbTinhTrangBG.Text = dgvBG.Rows[0].Cells[2].Value.ToString();
                cbTinhTrangMuon.Text = dgvBG.Rows[0].Cells[3].Value.ToString();
            }
        }

        private void dgvTTBG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbTTBG.Text = dgvTTBG.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbTenTTBG.Text = dgvTTBG.Rows[e.RowIndex].Cells[1].Value.ToString();
            ReadAllDocuments_BG();
        }

        private void dgvBG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbMaBoardGame.Text = dgvBG.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbTinhTrangBG.Text = dgvBG.Rows[e.RowIndex].Cells[2].Value.ToString();
            cbTinhTrangMuon.Text = dgvBG.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void bThemBG_Click(object sender, EventArgs e)
        {
            BoardGame bg = new BoardGame(ObjectId.Parse(tbTTBG.Text), cbTinhTrangBG.Text);
            collection_G.InsertOneAsync(bg);
            var filterTTBG = Builders<ThongTinBG>.Filter.Eq("MaTTBG", ObjectId.Parse(tbTTBG.Text));
            var updateTTBG = Builders<ThongTinBG>.Update.Inc("SoLuong", 1);
            collection_BG.UpdateOneAsync(filterTTBG, updateTTBG);
            ReadAllDocuments_BG();
            ReadAllDocuments_TTBG();
            ReadAllDocuments_ThongTinBG();
        }

        private void bSuaBG_Click(object sender, EventArgs e)
        {
            var updateDef = Builders<BoardGame>.Update.Set("TinhTrangBG", cbTinhTrangBG.Text).Set("TinhTrangMuon", cbTinhTrangMuon.Text);
            collection_G.UpdateOneAsync(bg => bg.MaBG == ObjectId.Parse(tbMaBoardGame.Text), updateDef);
            ReadAllDocuments_BG();
        }

        private void bMDBG_Click(object sender, EventArgs e)
        {
            tbTTBG.Text = "";
            tbTenTTBG.Text = "";
            tbMaBoardGame.Text = "";
            cbTinhTrangBG.Text = "";
            cbTinhTrangMuon.Text = "";
        }

        private void bTimKiemTTBG_Click(object sender, EventArgs e)
        {
            List<ThongTinBG> listTTBG = collection_BG.AsQueryable().ToList<ThongTinBG>();
            List<ThongTinBG> filteredTTBGList = new List<ThongTinBG>();

            foreach (var ttbg in listTTBG)
            {
                if (ttbg.TenBoardGame == tbTimKiemTTBG.Text)
                {
                    filteredTTBGList.Add(ttbg);
                }
            }
            var bindingSourceTTBG = new BindingSource();
            bindingSourceTTBG.DataSource = filteredTTBGList;
            dgvTTBG.DataSource = bindingSourceTTBG;
        }

        private void bMacDinhTTBG_Click(object sender, EventArgs e)
        {
            tbTimKiemTTBG.Text = "";
            ReadAllDocuments_TTBG();
        }

        //Quản lý khách hàng
        public void ReadAllDocuments_KH()
        {
            List<KhachHang> list = collection_KH.AsQueryable().ToList<KhachHang>();
            dgvKhachHang.DataSource = list;
            if (dgvKhachHang.Rows.Count > 0)
            {
                tbMaKhachHang.Text = dgvKhachHang.Rows[0].Cells[0].Value.ToString();
                tbTenKhachHang.Text = dgvKhachHang.Rows[0].Cells[1].Value.ToString();
                dtpNgaySinh.Text = dgvKhachHang.Rows[0].Cells[2].Value.ToString();
                tbDiaChi.Text = dgvKhachHang.Rows[0].Cells[3].Value.ToString();
                tbSoDienThoai.Text = dgvKhachHang.Rows[0].Cells[4].Value.ToString();
                tbEmail.Text = dgvKhachHang.Rows[0].Cells[5].Value.ToString();
                tbSoTichDiem.Text = dgvKhachHang.Rows[0].Cells[6].Value.ToString();
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbMaKhachHang.Text = dgvKhachHang.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbTenKhachHang.Text = dgvKhachHang.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtpNgaySinh.Text = dgvKhachHang.Rows[e.RowIndex].Cells[2].Value.ToString();
            tbDiaChi.Text = dgvKhachHang.Rows[e.RowIndex].Cells[3].Value.ToString();
            tbSoDienThoai.Text = dgvKhachHang.Rows[e.RowIndex].Cells[4].Value.ToString();
            tbEmail.Text = dgvKhachHang.Rows[e.RowIndex].Cells[5].Value.ToString();
            tbSoTichDiem.Text = dgvKhachHang.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void bThemKhachHang_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang(tbTenKhachHang.Text, dtpNgaySinh.Value, tbDiaChi.Text, tbSoDienThoai.Text, tbEmail.Text);
            collection_KH.InsertOneAsync(kh);
            ReadAllDocuments_KH();
            ReadAllDocuments_DSKH();
        }

        private void bSuaKhachHang_Click(object sender, EventArgs e)
        {
            var updateDef = Builders<KhachHang>.Update.Set("TenKH", tbTenKhachHang.Text).Set("NgSinh", dtpNgaySinh.Value).Set("DiaChi", tbDiaChi.Text).Set("SDT", tbSoDienThoai.Text).Set("Email", tbEmail.Text).Set("TichDiem", int.Parse(tbSoTichDiem.Text));
            collection_KH.UpdateOneAsync(kh => kh.MaKH == ObjectId.Parse(tbMaKhachHang.Text), updateDef);
            ReadAllDocuments_KH();
            ReadAllDocuments_DSKH();
        }

        private void bMDKhachHang_Click(object sender, EventArgs e)
        {
            tbMaBoardGame.ReadOnly = true;
            tbMaKhachHang.Text = "";
            tbTenKhachHang.Text = "";
            dtpNgaySinh.Text = "";
            tbDiaChi.Text = "";
            tbSoDienThoai.Text = "";
            tbEmail.Text = "";
            tbSoTichDiem.Text = "";
        }

        private void bTimKiemKH_Click(object sender, EventArgs e)
        {
            List<KhachHang> listKH = collection_KH.AsQueryable().ToList<KhachHang>();
            List<KhachHang> filteredKHList = new List<KhachHang>();

            foreach (var kh in listKH)
            {
                if (kh.TenKH == tbTimKiemKH.Text)
                {
                    filteredKHList.Add(kh);
                }
            }
            var bindingSourceKH = new BindingSource();
            bindingSourceKH.DataSource = filteredKHList;
            dgvKhachHang.DataSource = bindingSourceKH;
        }

        private void bMacDinhKH_Click(object sender, EventArgs e)
        {
            tbTimKiemKH.Text = "";
            ReadAllDocuments_KH();
        }

        //Quản lí đơn hàng
        public void ReadAllDocuments_DSKH()
        {
            tbTongTien.ReadOnly = true;
            List<KhachHang> list = collection_KH.AsQueryable().ToList<KhachHang>();
            dgvDSKH.DataSource = list;
            if (dgvDSKH.Rows.Count > 0)
            {
                tbMaKHThue.Text = dgvDSKH.Rows[0].Cells[0].Value.ToString();
            }
        }

        public void ReadAllDocuments_DSDHKH()
        {
            List<DonHang> list = collection_DH.AsQueryable().ToList<DonHang>();
            List<DonHang> filteredTTBGList = new List<DonHang>();
            foreach (var dh in list)
            {
                if (dh.MaKH == ObjectId.Parse(tbMaKHThue.Text))
                {
                    filteredTTBGList.Add(dh);
                }
            }
            var bindingSourceTTBG = new BindingSource();
            bindingSourceTTBG.DataSource = filteredTTBGList;
            dgvDSDHKH.DataSource = bindingSourceTTBG;
            tbMaKHThue.ReadOnly = true;

            if (dgvDSDHKH.Rows.Count > 0)
            {
                tbMaCTDH.Text = dgvDSDHKH.Rows[0].Cells[0].Value.ToString();
                dtpNgayThueDH.Text = dgvDSDHKH.Rows[0].Cells[1].Value.ToString();
                dtpNgayTraDH.Text = dgvDSDHKH.Rows[0].Cells[2].Value.ToString();
                cbTinhTrangDH.Text = dgvDSDHKH.Rows[0].Cells[3].Value.ToString();
                var thongTinUDquery = Builders<UuDai>.Filter.Eq("MaUD", ObjectId.Parse(dgvDSDHKH.Rows[0].Cells[5].Value.ToString()));
                List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
                UuDai ud = filteredUDs[0];
                cbMaUuDaiSD.Text = ud.TenUD;
                tbTongTien.Text = dgvDSDHKH.Rows[0].Cells[6].Value.ToString();
                ReadAllDocuments_DSBGDH();
            }
            else
            {
                dgvDSBGDH.Rows.Clear();
                tbMaCTDH.Text = "";
                dtpNgayThueDH.Value = DateTime.Now;
                dtpNgayTraDH.Value = DateTime.Now;
                cbTinhTrangDH.Text = "";
                cbMaUuDaiSD.Text = "";

                tbTongTien.Text = "";
            }
        }

        public void ReadAllDocuments_DSBGDH()
        {
            List<CTDonHang> listDH = collection_CTDH.AsQueryable().ToList<CTDonHang>();
            List<BoardGame> listBG = collection_G.AsQueryable().ToList<BoardGame>();
            List<BoardGame> filteredBGList = new List<BoardGame>();
            foreach (var dh in listDH)
            {
                if (dh.MaDH == ObjectId.Parse(tbMaCTDH.Text))
                {
                    foreach (var bg in listBG)
                    {
                        if (bg.MaBG == dh.MaBG)
                        {
                            filteredBGList.Add(bg);
                        }
                    }
                }
            }
            var bindingSourceBG = new BindingSource();
            bindingSourceBG.DataSource = filteredBGList;
            dgvDSBGDH.DataSource = bindingSourceBG;
        }

        public void ReadAllDocuments_dgvDanhSachBG()
        {
            List<ThongTinBG> listTTBG = collection_BG.AsQueryable().ToList<ThongTinBG>();
            List<BoardGame> listBG = collection_G.AsQueryable().ToList<BoardGame>();
            List<BoardGame> filteredTTBGList = new List<BoardGame>();

            foreach (var ttbg in listTTBG)
            {
                if (ttbg.TenBoardGame == tbTimBG.Text)
                {
                    foreach (var bg in listBG)
                    {
                        if (bg.MaTTBG == ttbg.MaTTBG)
                        {
                            filteredTTBGList.Add(bg);
                        }
                    }
                }
            }

            var bindingSourceTTBG = new BindingSource();
            bindingSourceTTBG.DataSource = filteredTTBGList;
            dgvDanhSachBG.DataSource = bindingSourceTTBG;
        }

        private void bTimBG_Click(object sender, EventArgs e)
        {
            ReadAllDocuments_dgvDanhSachBG();
        }

        private void bMacDinhBG_Click(object sender, EventArgs e)
        {
            tbTimBG.Text = "";
        }

        private void dgvDSKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbMaKHThue.Text = dgvDSKH.Rows[e.RowIndex].Cells[0].Value.ToString();
            ReadAllDocuments_DSDHKH();
        }

        private void dgvDSDHKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            tbMaCTDH.Text = dgvDSDHKH.Rows[e.RowIndex].Cells[0].Value.ToString();
            dtpNgayThueDH.Text = dgvDSDHKH.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtpNgayTraDH.Text = dgvDSDHKH.Rows[e.RowIndex].Cells[2].Value.ToString();
            cbTinhTrangDH.Text = dgvDSDHKH.Rows[e.RowIndex].Cells[3].Value.ToString();
            var thongTinUDquery = Builders<UuDai>.Filter.Eq("MaUD", ObjectId.Parse(dgvDSDHKH.Rows[e.RowIndex].Cells[5].Value.ToString()));
            List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
            UuDai ud = filteredUDs[0];
            cbMaUuDaiSD.Text = ud.TenUD;
            tbTongTien.Text = dgvDSDHKH.Rows[e.RowIndex].Cells[6].Value.ToString();
            ReadAllDocuments_DSBGDH();
        }

        private void bMacDinhDH_Click(object sender, EventArgs e)
        {
            dgvDSBGDH.Rows.Clear();
            tbMaCTDH.Text = "";
            dtpNgayThueDH.Value = DateTime.Now;
            dtpNgayTraDH.Value = DateTime.Now;
            cbTinhTrangDH.Text = "";
            cbMaUuDaiSD.Text = "";
            tbTongTien.Text = "";

        }

        private void bThemDH_Click(object sender, EventArgs e)
        {
            if (dgvDSBGDH.Rows.Count > 0)
            {

                MessageBox.Show("Thêm đơn hàng thành công! ");

                var thongTinKHquery = Builders<KhachHang>.Filter.Eq("MaKH", ObjectId.Parse(tbMaKHThue.Text));
                List<KhachHang> filteredKHs = collection_KH.Find(thongTinKHquery).ToList();
                KhachHang kh = filteredKHs[0];
                var updateKhachHang = Builders<KhachHang>.Update.Inc("TichDiem", 10);
                collection_KH.UpdateOne(kh1 => kh1.MaKH == kh.MaKH, updateKhachHang);

                var thongTinUDquery = Builders<UuDai>.Filter.Eq("TenUD", cbMaUuDaiSD.Text);
                List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
                UuDai ud = filteredUDs[0];
                var updateUuDai = Builders<UuDai>.Update.Inc("SoLuong", -1);
                collection_UD.UpdateOne(ud1 => ud1.MaUD == ud.MaUD, updateUuDai);

                ReadAllDocuments_DSKH();
                ReadAllDocuments_DSDHKH();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một board game để thêm vào đơn hàng.");
            }
        }

        private void bSuaDH_Click(object sender, EventArgs e)
        {
            var thongTinUDquery = Builders<UuDai>.Filter.Eq("TenUD", cbMaUuDaiSD.Text);
            List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
            UuDai ud = filteredUDs[0];
            var updateDef = Builders<DonHang>.Update.Set("NgayThue", dtpNgayThueDH.Value).Set("NgayTra", dtpNgayTraDH.Value).Set("TrangThai", cbTinhTrangDH.Text).Set("MaUD", ud.MaUD);
            collection_DH.UpdateOneAsync(dh => dh.MaDH == ObjectId.Parse(tbMaCTDH.Text), updateDef);
            ReadAllDocuments_DSDHKH();
        }

        private void bThemDSDH_Click(object sender, EventArgs e)
        {

            var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("TenBoardGame", tbTimBG.Text);
            List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
            ThongTinBG ttbg = filteredTTBGs[0];
            var valueTien = ttbg.GiaThue;

            if (dgvDanhSachBG.SelectedRows.Count > 0)
            {
                // Lấy giá trị từ hàng được chọn
                var value = dgvDanhSachBG.SelectedRows[0].Cells[0].Value.ToString();

                // Kiểm tra giá trị của trường "TinhTrangMuon"
                var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaBG", ObjectId.Parse(value));
                List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();
                BoardGame bg = filteredBGs[0];
                if (cbMaUuDaiSD.Text == "")
                {
                    cbMaUuDaiSD.Text = "none";
                }

                // Kiểm tra kết quả cập nhật
                if (bg.TinhTrangMuon != "Dang thue")
                {
                    
                    var thongTinUDquery = Builders<UuDai>.Filter.Eq("TenUD", cbMaUuDaiSD.Text);
                    List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
                    UuDai ud = filteredUDs[0];
                    if (tbMaCTDH.Text == "")
                    {
                        DonHang dh = new DonHang(dtpNgayThueDH.Value, dtpNgayTraDH.Value, cbTinhTrangDH.Text, ObjectId.Parse(tbMaKHThue.Text), ud.MaUD);
                        collection_DH.InsertOneAsync(dh);
                        tbMaCTDH.Text = dh.MaDH.ToString();
                    }
                    var thongTinDHquery = Builders<DonHang>.Filter.Eq("MaDH", ObjectId.Parse(tbMaCTDH.Text));
                    List<DonHang> filteredDHs = collection_DH.Find(thongTinDHquery).ToList();
                    DonHang dh1 = filteredDHs[0];

                    

                    CTDonHang ctdh = new CTDonHang(dh1.MaDH, bg.MaBG);
                    collection_CTDH.InsertOneAsync(ctdh);

                    var updateDef = Builders<BoardGame>.Update.Set("TinhTrangMuon", "Dang thue");
                    collection_G.UpdateOneAsync(bg1 => bg1.MaBG ==bg.MaBG, updateDef);

                    int uds = ud.PhanTramGiam;
                    decimal giaThue = dh1.TongTien;
                    giaThue += valueTien;
                    giaThue = giaThue - giaThue * uds / 100;
                    tbTongTien.Text = giaThue.ToString();
                    // Cập nhật giá trị "TongTien" trong đơn hàng
                    var updateDonHang = Builders<DonHang>.Update.Set("TongTien", giaThue);
                    collection_DH.UpdateOne(dh2 => dh2.MaDH == dh1.MaDH, updateDonHang);
                    

                }
                else
                {
                    MessageBox.Show("Board game đã được đặt trước hoặc đang được thuê.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một board game để thêm vào đơn hàng.");
            }
            
            ReadAllDocuments_DSBGDH();
            ReadAllDocuments_dgvDanhSachBG();
        }

       

        private void bXoaDSDH_Click(object sender, EventArgs e)
        {
            if (dgvDSBGDH.SelectedRows.Count > 0)
            {

                var valueBG = dgvDSBGDH.SelectedRows[0].Cells[0].Value.ToString();
                var valueTTBG = dgvDSBGDH.SelectedRows[0].Cells[1].Value.ToString();
                collection_CTDH.DeleteOneAsync(bg1 => bg1.MaBG == ObjectId.Parse(valueBG));

                var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaBG", ObjectId.Parse(valueBG));
                List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();
                BoardGame bg = filteredBGs[0];

                var thongTinDHquery = Builders<DonHang>.Filter.Eq("MaDH", ObjectId.Parse(tbMaCTDH.Text));
                List<DonHang> filteredDHs = collection_DH.Find(thongTinDHquery).ToList();
                DonHang dh = filteredDHs[0];

                var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", bg.MaTTBG);
                List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
                ThongTinBG ttbg = filteredTTBGs[0];

                int giaThue = dh.TongTien;
                giaThue -= ttbg.GiaThue;
                tbTongTien.Text = giaThue.ToString();
                var updateDonHang = Builders<DonHang>.Update.Set("TongTien", giaThue);
                collection_DH.UpdateOne(dh2 => dh2.MaDH == dh.MaDH, updateDonHang);

                var updateDef = Builders<BoardGame>.Update.Set("TinhTrangMuon", "Chua duoc thue");
                collection_G.UpdateOneAsync(bg2 => bg2.MaBG == bg.MaBG, updateDef);
                // Xóa dòng đang được chọn khỏi DataGridView
                ReadAllDocuments_DSBGDH();
                ReadAllDocuments_dgvDanhSachBG();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.");
            }
        }

        private void bTimKiemDSDH_Click(object sender, EventArgs e)
        {
            List<KhachHang> listKH = collection_KH.AsQueryable().ToList<KhachHang>();
            List<KhachHang> filteredKHList = new List<KhachHang>();

            foreach (var kh in listKH)
            {
                if (kh.TenKH == tbTimKiemDSDH.Text)
                {
                    filteredKHList.Add(kh);
                }
            }
            var bindingSourceKH = new BindingSource();
            bindingSourceKH.DataSource = filteredKHList;
            dgvDSKH.DataSource = bindingSourceKH;
        }
        private void bMacDinhDSDH_Click(object sender, EventArgs e)
        {
            tbTimKiemDSDH.Text = "";
            ReadAllDocuments_DSKH();
        }


        //Quản lí ưu đãi
        public void ReadAllDocuments_UD()
        {
            List<UuDai> list = collection_UD.AsQueryable().ToList<UuDai>();
            dgvUuDai.DataSource = list;
            if (dgvUuDai.Rows.Count > 0)
            {
                tbMaUuDai.Text = dgvUuDai.Rows[0].Cells[0].Value.ToString();
                tbTenUuDai.Text = dgvUuDai.Rows[0].Cells[1].Value.ToString();
                tbMoTa.Text = dgvUuDai.Rows[0].Cells[2].Value.ToString();
                dtpNgayBD.Text = dgvUuDai.Rows[0].Cells[3].Value.ToString();
                dtpNgayKT.Text = dgvUuDai.Rows[0].Cells[4].Value.ToString();
                tbPhanTramGiam.Text = dgvUuDai.Rows[0].Cells[5].Value.ToString();
                tbSoLuongUD.Text = dgvUuDai.Rows[0].Cells[6].Value.ToString();
                tbSoLuongQD.Text = dgvUuDai.Rows[0].Cells[7].Value.ToString();
            }
        }

        private void dgvUuDai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbMaUuDai.Text = dgvUuDai.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbTenUuDai.Text = dgvUuDai.Rows[e.RowIndex].Cells[1].Value.ToString();
            tbMoTa.Text = dgvUuDai.Rows[e.RowIndex].Cells[2].Value.ToString();
            dtpNgayBD.Text = dgvUuDai.Rows[e.RowIndex].Cells[3].Value.ToString();
            dtpNgayKT.Text = dgvUuDai.Rows[e.RowIndex].Cells[4].Value.ToString();
            tbPhanTramGiam.Text = dgvUuDai.Rows[e.RowIndex].Cells[5].Value.ToString();
            tbSoLuongUD.Text = dgvUuDai.Rows[e.RowIndex].Cells[6].Value.ToString();
            tbSoLuongQD.Text = dgvUuDai.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void bThemUuDai_Click(object sender, EventArgs e)
        {
            UuDai ud = new UuDai(tbTenUuDai.Text, tbMoTa.Text, dtpNgayBD.Value, dtpNgayKT.Value, int.Parse(tbPhanTramGiam.Text), int.Parse(tbSoLuongUD.Text), int.Parse(tbSoLuongQD.Text));
            collection_UD.InsertOneAsync(ud);
            ReadAllDocuments_UD();
        }

        private void bSuaUuDai_Click(object sender, EventArgs e)
        {
            var updateDef = Builders<UuDai>.Update.Set("TenUD", tbTenUuDai.Text).Set("MoTa", tbMoTa.Text).Set("NgayBD", dtpNgayBD.Value).Set("NgayKT", dtpNgayKT.Value).Set("PhanTramGiam", double.Parse(tbPhanTramGiam.Text)).Set("SoLuong", int.Parse(tbSoLuongUD.Text)).Set("DiemQuyDoi", int.Parse(tbSoLuongQD.Text));
            collection_UD.UpdateOneAsync(ud => ud.MaUD == ObjectId.Parse(tbMaUuDai.Text), updateDef);
            ReadAllDocuments_UD();
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

        private void bTimKiemUD_Click(object sender, EventArgs e)
        {
            List<UuDai> listUD = collection_UD.AsQueryable().ToList<UuDai>();
            List<UuDai> filteredUDList = new List<UuDai>();

            foreach (var ud in listUD)
            {
                if (ud.TenUD == tbTimKiemUD.Text)
                {
                    filteredUDList.Add(ud);
                }
            }
            var bindingSourceUD = new BindingSource();
            bindingSourceUD.DataSource = filteredUDList;
            dgvUuDai.DataSource = bindingSourceUD;
        }

        private void bMacDinhUD_Click(object sender, EventArgs e)
        {
            tbTimKiemUD.Text = "";
            ReadAllDocuments_UD();
        }

        //Chức năng đổi mật khẩu và xem danh sách tài khoản

        private void bAdmin_Click(object sender, EventArgs e)
        {
            TTTaiKhoan tTTaiKhoan = new TTTaiKhoan(taikhoan);
            tTTaiKhoan.ShowDialog();
        }

        private void bMatKhau_Click(object sender, EventArgs e)
        {
            DSTaiKhoan dSTaiKhoan = new DSTaiKhoan();
            dSTaiKhoan.ShowDialog();
        }

        //Lập báo cáo theo thể loại board game

        private void bLapBaoCao_Click(object sender, EventArgs e)
        {

            int thang = int.Parse(cbChonThang.SelectedItem.ToString());
            int nam = int.Parse(cbChonNam.SelectedItem.ToString());

            List<DonHang> listCTDHs = collection_DH.AsQueryable().ToList<DonHang>();
            BaoCao bc = new BaoCao(thang, nam);
            collection_BC.InsertOneAsync(bc);
            decimal tongDoanhThu = 0;
            int soDonHang = 0;
            int soBienBan = 0;
            List<DonHang> filteredDHList = new List<DonHang>();
            foreach (DonHang dh in listCTDHs)
            {
                // Lấy thông tin ngày tháng năm từ trường DonHang
                DateTime ngayTra = dh.NgayTra;

                // Kiểm tra xem ngày tháng năm có thuộc tháng và năm hiện tại không
                if (ngayTra.Month == thang && ngayTra.Year == nam)
                {
                    CTBaoCao ctbc = new CTBaoCao(dh.MaDH, bc.MaBC);
                    collection_CTBC.InsertOneAsync(ctbc);

                    filteredDHList.Add(dh);

                    // Tính tổng doanh thu từng đơn hàng
                    decimal DoanhThu = dh.TongTien;

                    tongDoanhThu = tongDoanhThu + DoanhThu;
                    soDonHang += 1;
                    var thongTinBBquery = Builders<BienBan>.Filter.Eq("MaCTDH", dh.MaDH);
                    List<BienBan> filteredLPs = collection_BB.Find(thongTinBBquery).ToList();
                    soBienBan += filteredLPs.Count;

                }

            }
            var updateDef = Builders<BaoCao>.Update.Set("TongDoanhThu", tongDoanhThu).Set("SoDonHang", soDonHang).Set("SoBienBan", soBienBan);
            collection_BC.UpdateOneAsync(nbc => nbc.MaBC == bc.MaBC, updateDef);
            tbSoDonHang.Text = soDonHang.ToString();
            tbDoanhThu.Text = tongDoanhThu.ToString();
            tbSoBienBan.Text = soBienBan.ToString();
            var bindingSourceKH = new BindingSource();
            bindingSourceKH.DataSource = filteredDHList;
            dgvBaoCao.DataSource = bindingSourceKH;

        }

        private void cbMaUuDaiSD_TextChanged(object sender, EventArgs e)
        {
            var thongTinUDquery = Builders<UuDai>.Filter.Eq("TenUD", cbMaUuDaiSD.Text);
            List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
            UuDai ud = filteredUDs[0];
            cbMaUuDaiSD.Text = ud.MaUD.ToString();
        }

        private void cbMaUuDaiSD_SelectedValueChanged(object sender, EventArgs e)
        {
            var thongTinUDquery = Builders<UuDai>.Filter.Eq("TenUD", cbMaUuDaiSD.Text);
            List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
            UuDai ud = filteredUDs[0];
            cbMaUuDaiSD.Text = ud.MaUD.ToString();
        }
    }
}
