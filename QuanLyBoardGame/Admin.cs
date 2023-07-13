using Amazon.SecurityToken.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace QuanLyBoardGame
{
    public partial class Admin : Form
    {

        static MongoClient client = new MongoClient();
        //static MongoClient client = new MongoClient("mongodb+srv://cnpm:Thuydiem29@cluster0.2jmsamm.mongodb.net/");
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
        static IMongoCollection<ThamSo> collection_TS = db.GetCollection<ThamSo>("ThamSo");
        private TaiKhoan taikhoan;
        private ThamSo thamso;
        internal Admin(TaiKhoan taikhoan)
        {
            InitializeComponent();
            List<ThamSo> listTSs = collection_TS.AsQueryable().ToList<ThamSo>();
            ThamSo ts = listTSs[0];

            thamso = ts;

            this.taikhoan = taikhoan;
            if (taikhoan.ChucVu == "Nhân viên")
            {
                bBaoCao.Visible = false;
                bCaiDat.Visible = false;
                bThemBoardGame.Enabled = false;
                bThemTrongKho.Enabled = false;
                bThemUD.Enabled = false;
                bDSTaiKhoan.Enabled = false;
            }
            
            
        }

        private void pbLogo_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 0;
        }
        private void bHome_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 0;
        }
        private void bDonHang_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 1;
            List<KhachHang> listKHs = collection_KH.AsQueryable().ToList<KhachHang>();
            cbTenKhachHang.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var kh in listKHs)
            {
                cbTenKhachHang.Items.Add(kh.TenKH);
            }
            List<UuDai> listUDs = collection_UD.AsQueryable().ToList<UuDai>();
            cbMaUuDaiSD.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var ud in listUDs)
            {
                cbMaUuDaiSD.Items.Add(ud.TenUD);
            }
            List<ThongTinBG> listTTBGs = collection_BG.AsQueryable().ToList<ThongTinBG>();
            cbTimBG.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var ttbg in listTTBGs)
            {
                cbTimBG.Items.Add(ttbg.TenBoardGame);
            }

            tbTienCoc.ReadOnly = true;
            tbTongTien.ReadOnly = true;
            HienThiKho();
        }
        private void bTruyXuatDH_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 2;
            List<KhachHang> listKHs = collection_KH.AsQueryable().ToList<KhachHang>();
            cbTimKiemDSDH.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var kh in listKHs)
            {
                cbTimKiemDSDH.Items.Add(kh.TenKH);
            }
            dgvDSKH.DataSource = listKHs;
        }
        private void bBaoCao_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 3;

        }
        private void bDanhSach_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 4;
            List<ThongTinBG> listTTBGs = collection_BG.AsQueryable().ToList<ThongTinBG>();
            cbTimKiemTTBG.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var ttbg in listTTBGs)
            {
                cbTimKiemTTBG.Items.Add(ttbg.TenBoardGame);
            }

            List<KhachHang> listKHs = collection_KH.AsQueryable().ToList<KhachHang>();
            cbTimKiemKH.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var kh in listKHs)
            {
                cbTimKiemKH.Items.Add(kh.TenKH);
            }

            List<UuDai> listUDs = collection_UD.AsQueryable().ToList<UuDai>();
            cbTimKiemUD.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var ud in listUDs)
            {
                cbTimKiemUD.Items.Add(ud.TenUD);
            }
            List<BienBan> listBBS = collection_BB.AsQueryable().ToList<BienBan>();
            dgvDSBienBan.DataSource = listBBS;

            ReadAllDocuments_ThongTinBG();
            ReadAllDocuments_KH();
            ReadAllDocuments_UD();
            HienThiDS_BienBan();
        }


        private void bCaiDat_Click(object sender, EventArgs e)
        {

            tbSoNgayThueMax.Text = thamso.SoNgayThueTD.ToString();
            tbSoNgayThueMin.Text = thamso.SoNgayThueTT.ToString();
            tbPhanTramCoc.Text = thamso.PhanTramCoc.ToString();
            tbSoDonHangMax.Text = thamso.SoDonHangTD.ToString();
            tbSoBGMax.Text = thamso.SoBoardGameTD.ToString();
            tabQuanLy.SelectedIndex = 5;
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






        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        //Quản lý danh sách Board game

        public void ReadAllDocuments_ThongTinBG()
        {
            List<ThongTinBG> list = collection_BG.AsQueryable().ToList<ThongTinBG>();
            dgvTTBG.DataSource = list;
        }
        private void bThemBoardGame_Click(object sender, EventArgs e)
        {
            ThemBoardGame themBoardGame = new ThemBoardGame();
            themBoardGame.ShowDialog();
            ReadAllDocuments_ThongTinBG();
        }

        private void dgvTTBG_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvTTBG.Rows.Count)
            {
                var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", ObjectId.Parse(dgvTTBG.Rows[e.RowIndex].Cells[0].Value.ToString()));
                List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
                ThongTinBG ttbg = filteredTTBGs[0];
                ThemBoardGame themBoardGame = new ThemBoardGame(ttbg);
                themBoardGame.ShowDialog();
                ReadAllDocuments_ThongTinBG();
            }

        }



        private void dgvTTBG_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var thongTinTTBGquery = Builders<BoardGame>.Filter.Eq("MaTTBG", ObjectId.Parse(dgvTTBG.Rows[e.RowIndex].Cells[0].Value.ToString()));
                List<BoardGame> filteredLBGs = collection_G.Find(thongTinTTBGquery).ToList();

                dgvBG.DataSource = filteredLBGs;

            }
        }
        private void bTimKiemTTBG_Click_1(object sender, EventArgs e)
        {
            var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("TenBoardGame", cbTimKiemTTBG.Text);
            List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
            dgvTTBG.DataSource = filteredTTBGs;
        }
        private void bMacDinhTTBG_Click_1(object sender, EventArgs e)
        {
            cbTimKiemTTBG.Text = "";
            ReadAllDocuments_ThongTinBG();
        }





        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        //Quản lý danh sách Kho

        private void dgvBG_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvBG.Rows.Count)
            {
                var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaBG", ObjectId.Parse(dgvBG.Rows[e.RowIndex].Cells[0].Value.ToString()));
                List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();
                BoardGame bg = filteredBGs[0];
                ThemVaoKho themVaoKho = new ThemVaoKho(bg);
               
                themVaoKho.ShowDialog();
                HienThidgvBG();
            }
        }

        public void HienThidgvBG()
        {
            if (dgvTTBG.SelectedRows.Count > 0)
            {
                var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaTTBG", ObjectId.Parse(dgvTTBG.SelectedRows[0].Cells[0].Value.ToString()));
                List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();
                dgvBG.DataSource = filteredBGs;
            }
        }
        private void bThemTrongKho_Click(object sender, EventArgs e)
        {
            if (dgvTTBG.SelectedRows.Count > 0)
            {
                var valueTTBG = dgvTTBG.SelectedRows[0].Cells[0].Value.ToString();
                var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", ObjectId.Parse(valueTTBG));
                List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
                if (filteredTTBGs.Count > 0)
                {
                    ThongTinBG ttbg = filteredTTBGs[0];
                    ThemVaoKho themVaoKho = new ThemVaoKho(ttbg);
                    themVaoKho.ShowDialog();
                    HienThidgvBG();
                    ReadAllDocuments_ThongTinBG();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn board game muốn thêm vào kho");
            }
        }

        //Quản lý danh sách khách hàng
        public void ReadAllDocuments_KH()
        {
            List<KhachHang> list = collection_KH.AsQueryable().ToList<KhachHang>();
            dgvKhachHang.DataSource = list;

        }

        public void ReadAllDocuments_KHDH()
        {
            List<KhachHang> list = collection_KH.AsQueryable().ToList<KhachHang>();
            dgvDSKH.DataSource = list;

        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        //Quản lí lập đơn hàng


        private void cbTenKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var thongTinKHquery = Builders<KhachHang>.Filter.Eq("TenKH", cbTenKhachHang.Text);
            List<KhachHang> filteredKHs = collection_KH.Find(thongTinKHquery).ToList();

            if (filteredKHs.Count > 0)
            {
                KhachHang kh = filteredKHs[0];
                tbSdtKH.Text = kh.SDT;
            }
            else
            {
                MessageBox.Show("Trống");
            }
        }

        public void HienThiKho()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaGame", typeof(ObjectId));
            dt.Columns.Add("TenBoardGame", typeof(string));
            dt.Columns.Add("TinhTrangBG", typeof(string));
            dt.Columns.Add("TinhTrangMuon", typeof(string));
            dt.Columns.Add("DatHang", typeof(string));

            List<BoardGame> list = collection_G.AsQueryable().ToList<BoardGame>();
            foreach (BoardGame game in list)
            {
                DataRow row = dt.NewRow();
                row["MaGame"] = game.MaBG;
                var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", game.MaTTBG);
                List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
                if (filteredTTBGs.Count > 0)
                {
                    ThongTinBG ttbg = filteredTTBGs[0];
                    row["TenBoardGame"] = ttbg.TenBoardGame;
                    row["TinhTrangBG"] = game.TinhTrangBG;
                    row["TinhTrangMuon"] = game.TinhTrangMuon;
                    row["DatHang"] = game.DatHang;
                    dt.Rows.Add(row);
                }
            }
            dgvDanhSachBG.DataSource = dt;
        }


        private void cbMaUuDaiSD_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tongtien = int.Parse(tbTongTien.Text);
            var thongTinUDquery = Builders<UuDai>.Filter.Eq("TenUD", cbMaUuDaiSD.Text);
            List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
            if (filteredUDs.Count > 0)
            {
                UuDai ud = filteredUDs[0];
                tongtien = tongtien - tongtien * ud.PhanTramGiam / 100;
                tbTongTien.Text = tongtien.ToString();
            }
        }

        private void bTimBG_Click(object sender, EventArgs e)
        {
            var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("TenBoardGame", cbTimBG.Text);
            List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
            if (filteredTTBGs.Count > 0)
            {
                ThongTinBG ttbg = filteredTTBGs[0];

                var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaTTBG", ttbg.MaTTBG);
                List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();

                dgvDanhSachBG.DataSource = filteredBGs;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin board game!");
            }
        }

        private void bMacDinhBG_Click(object sender, EventArgs e)
        {
            cbTimBG.Text = "";
            HienThiKho();

        }


        private void bMacDinhDH_Click(object sender, EventArgs e)
        {
            cbTenKhachHang.Text = "";
            tbSdtKH.Text = "";
            dgvCTDH.DataSource = new List<BoardGame>(); ;
            dtpNgayThueDH.Value = DateTime.Now;
            dtpNgayTraDH.Value = DateTime.Now;
            cbMaUuDaiSD.Text = "Không";
            tbTienCoc.Text = "0";
            tbTongTien.Text = "0";

        }

        private void bThemDH_Click(object sender, EventArgs e)
        {
            if(cbMaUuDaiSD.Text == "Không")
            {
                int index = cbMaUuDaiSD.Items.IndexOf("Không");
                cbMaUuDaiSD.SelectedIndex = index;
                cbMaUuDaiSD.Text = "Không";
            }
            


            if (cbTenKhachHang.SelectedIndex != -1 & cbMaUuDaiSD.SelectedIndex!= -1)
            {
                var thongTinKHquery = Builders<KhachHang>.Filter.Eq("TenKH", cbTenKhachHang.Text);
                List<KhachHang> filteredKHs = collection_KH.Find(thongTinKHquery).ToList();
                KhachHang kh = filteredKHs[0];

                var thongTinDHquery = Builders<DonHang>.Filter.And(
                    Builders<DonHang>.Filter.Eq("MaKH", kh.MaKH),
                    Builders<DonHang>.Filter.Eq("TrangThai", "Chưa trả")
                );

                List<DonHang> filteredDHs = collection_DH.Find(thongTinDHquery).ToList();

                if (filteredDHs.Count < thamso.SoDonHangTD)
                {

                    var thongTinUDquery = Builders<UuDai>.Filter.Eq("TenUD", cbMaUuDaiSD.Text);
                    List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
                    UuDai ud = filteredUDs[0];

                    DateTime ngayKTUD = ud.NgayKT;
                    DateTime ngayHienTai = DateTime.Now;
                    if (ngayHienTai <= ngayKTUD && ud.SoLuong >0)
                    {

                        if (dgvCTDH.Rows.Count > 0)
                        {
                            if (kh.TichDiem >= ud.DiemQuyDoi)
                            {
                                if (dtpNgayTraDH.Value > dtpNgayThueDH.Value)
                                {
                                    TimeSpan khoangThoiGianThue = dtpNgayTraDH.Value - dtpNgayThueDH.Value;
                                    if (khoangThoiGianThue.TotalDays > thamso.SoNgayThueTD || khoangThoiGianThue.TotalDays < thamso.SoNgayThueTT)
                                    {
                                        MessageBox.Show("Không cho phép thuê trên " + thamso.SoNgayThueTD + " ngày và dưới " + thamso.SoNgayThueTT + " ngày !");
                                    }
                                    else
                                    {
                                        List<BoardGame> filteredDSCTDH = new List<BoardGame>();
                                        filteredDSCTDH.AddRange((List<BoardGame>)dgvCTDH.DataSource);


                                        int tongTien = int.Parse(tbTongTien.Text);
                                        int tongtiengoc = int.Parse(tbTongTien.Text);
                                        tongTien = tongTien - tongTien * ud.PhanTramGiam / 100;
                                        
                                        int tienCoc = int.Parse(tbTienCoc.Text);
                                        int tiencocgoc = int.Parse(tbTienCoc.Text);

                                        if (khoangThoiGianThue.TotalDays > 14)
                                        {
                                            
                                            tongTien -= tongTien * 10 / 100;
                                            tbTongTien.Text = tongTien.ToString();

                                            
                                            tienCoc = tienCoc * 110 / 100;
                                            tbTienCoc.Text = tienCoc.ToString();
                                        }
                                        else if (khoangThoiGianThue.TotalDays > 7)
                                        {
                                           
                                            tongTien -= tongTien * 5 / 100;
                                            tbTongTien.Text = tongTien.ToString();
                                        }

                                        

                                        DialogResult dialogResult = MessageBox.Show("Số tiền phải cọc là: " + tienCoc + " , Tổng số tiền phải trả là: " + tongTien, "Thông báo", MessageBoxButtons.OKCancel);

                                        if (dialogResult == DialogResult.OK)
                                        {
                                            DonHang dh = new DonHang(dtpNgayThueDH.Value, dtpNgayTraDH.Value, "Chưa trả", kh.MaKH, ud.MaUD, tienCoc, tongTien);
                                            collection_DH.InsertOneAsync(dh);

                                            var updateUuDai = Builders<UuDai>.Update.Inc("SoLuong", -1);
                                            collection_UD.UpdateOne(ud1 => ud1.MaUD == ud.MaUD, updateUuDai);

                                            for (int j = 0; j < filteredDSCTDH.Count; j++)
                                            {
                                                CTDonHang ctdh = new CTDonHang(dh.MaDH, filteredDSCTDH[j].MaBG);
                                                collection_CTDH.InsertOneAsync(ctdh);

                                                var updateDefBG = Builders<BoardGame>.Update.Set("TinhTrangMuon", "Đang thuê").Set("DatHang", "");
                                                collection_G.UpdateOneAsync(bg1 => bg1.MaBG == filteredDSCTDH[j].MaBG, updateDefBG);

                                                var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", filteredDSCTDH[j].MaTTBG);
                                                List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
                                                ThongTinBG ttbg = filteredTTBGs[0];
                                            }
                                            var updateTruTichDiemKhachHang = Builders<KhachHang>.Update.Inc("TichDiem", -ud.DiemQuyDoi);
                                            collection_KH.UpdateOne(kh1 => kh1.MaKH == kh.MaKH, updateTruTichDiemKhachHang);

                                            var updateCongTichDiemKhachHang = Builders<KhachHang>.Update.Inc("TichDiem", 10);
                                            collection_KH.UpdateOne(kh1 => kh1.MaKH == kh.MaKH, updateCongTichDiemKhachHang);

                                            MessageBox.Show("Thêm đơn hàng thành công!");

                                            filteredDSCTDH = new List<BoardGame>();

                                            cbTenKhachHang.Text = "";
                                            tbSdtKH.Text = "";
                                            dgvCTDH.DataSource = new List<BoardGame>(); ;
                                            dtpNgayThueDH.Value = DateTime.Now;
                                            dtpNgayTraDH.Value = DateTime.Now;
                                            cbMaUuDaiSD.Text = "Không";
                                            tbTienCoc.Text = "0";
                                            tbTongTien.Text = "0";

                                            HienThiKho();
                                        }
                                        else if (dialogResult == DialogResult.Cancel)
                                        {
                                            tbTienCoc.Text =tiencocgoc.ToString();
                                            tbTongTien.Text= tongtiengoc.ToString();
                                            return;
                                        }

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Ngày trả và ngày nhập không hợp lệ!");
                                }
                            }

                            else
                            {
                                MessageBox.Show("Không đủ điểm để sử dụng ưu đãi vui lòng đổi mã ưu đãi khác!");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Vui lòng chọn một board game để thêm vào đơn hàng.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ưu đãi đã hết hiệu lực!");
                        
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng đơn hàng của khách đã tối đa.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin cho đơn hàng.");
            }

        }

        private void dgvDanhSachBG_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dgvCTDH.DataSource == null)
            {
                dgvCTDH.DataSource = new List<BoardGame>();
            }
            List<BoardGame> filteredDSCTDH = new List<BoardGame>();
            filteredDSCTDH.AddRange((List<BoardGame>)dgvCTDH.DataSource);
            if (e.RowIndex >= 0 && e.RowIndex < dgvDanhSachBG.Rows.Count)
            {
                    var value = dgvDanhSachBG.Rows[e.RowIndex].Cells[0].Value.ToString();
                    var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaBG", ObjectId.Parse(value));
                    List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();
                    BoardGame bg = filteredBGs[0];

                    if (bg.TinhTrangMuon == "Đang giữ hàng")
                    {

                        var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", bg.MaTTBG);
                        List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
                        ThongTinBG ttbg = filteredTTBGs[0];

                        var thongTinKHquery = Builders<KhachHang>.Filter.Eq("SDT", bg.DatHang);
                        List<KhachHang> filteredKHs = collection_KH.Find(thongTinKHquery).ToList();
                        if (filteredKHs.Count > 0)
                        {
                            KhachHang kh = filteredKHs[0];
                            bool boardGameExists = false;

                            foreach (BoardGame item in filteredDSCTDH)
                            {
                                if (item.MaBG == bg.MaBG)
                                {
                                    boardGameExists = true;
                                    break;
                                }
                            }

                            if (!boardGameExists)
                            {
                                int tongtien = int.Parse(tbTongTien.Text);
                                tongtien += ttbg.GiaThue;
                                tbTongTien.Text = tongtien.ToString();

                                if (bg.TinhTrangBG == "Trầy xước")
                                {
                                    int tiencoc = int.Parse(tbTienCoc.Text);
                                    tiencoc += ttbg.TriGia * (thamso.PhanTramCoc - 5) / 100;
                                    tbTienCoc.Text = tiencoc.ToString();
                                }
                                else
                                {

                                    int tiencoc = int.Parse(tbTienCoc.Text);
                                    tiencoc += ttbg.TriGia * thamso.PhanTramCoc / 100;
                                    tbTienCoc.Text = tiencoc.ToString();

                                }
                                filteredDSCTDH.Add(bg);
                                dgvCTDH.DataSource = filteredDSCTDH;
                                dgvCTDH.Refresh();

                                cbTenKhachHang.Text = kh.TenKH;
                                tbSdtKH.Text = kh.SDT;
                            }
                            else
                            {
                                MessageBox.Show("Board game đã tồn tại trong danh sách.");

                            }
                        }
                        else
                        {
                            MessageBox.Show("Khách hàng chưa tồn tại trong danh sách");
                        }


                    
                }
            }
        }
        private void bThemDSDH_Click(object sender, EventArgs e)
        {
            if (dgvCTDH.DataSource == null)
            {
                dgvCTDH.DataSource = new List<BoardGame>();
            }
            
            if (dgvCTDH.Rows.Count < thamso.SoBoardGameTD)
            {
               
                List<BoardGame> filteredDSCTDH = new List<BoardGame>();
                filteredDSCTDH.AddRange((List<BoardGame>)dgvCTDH.DataSource);


                    if (dgvDanhSachBG.SelectedRows.Count > 0)
                    {
                        // Lấy giá trị từ hàng được chọn
                        var value = dgvDanhSachBG.SelectedRows[0].Cells[0].Value.ToString();
                        var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaBG", ObjectId.Parse(value));
                        List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();
                        BoardGame bg = filteredBGs[0];
                        
                        var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", bg.MaTTBG);
                        List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
                        ThongTinBG ttbg = filteredTTBGs[0];
                    if (filteredTTBGs.Count > 0)
                    {
                        // Kiểm tra giá trị của trường "TinhTrangMuon"
                        

                        // Kiểm tra kết quả cập nhật
                        if (bg.TinhTrangBG != "Hỏng")
                        {

                            if (bg.TinhTrangMuon != "Đang thuê")
                            {
                                if (bg.TinhTrangMuon == "Đang giữ hàng")
                                {
                                    var thongTinKHquery = Builders<KhachHang>.Filter.Eq("TenKH", cbTenKhachHang.Text);
                                    List<KhachHang> filteredKHs = collection_KH.Find(thongTinKHquery).ToList();
                                    if (filteredKHs.Count > 0)
                                    {
                                        KhachHang kh = filteredKHs[0];
                                        if (bg.DatHang == kh.SDT)
                                        {
                                            bool boardGameExists = false;

                                            foreach (BoardGame item in filteredDSCTDH)
                                            {
                                                if (item.MaBG == bg.MaBG)
                                                {
                                                    boardGameExists = true;
                                                    break;
                                                }
                                            }

                                            if (!boardGameExists)
                                            {
                                                int tongtien = int.Parse(tbTongTien.Text);
                                                tongtien += ttbg.GiaThue;
                                                tbTongTien.Text = tongtien.ToString();

                                                if (bg.TinhTrangBG == "Trầy xước")
                                                {
                                                    int tiencoc = int.Parse(tbTienCoc.Text);
                                                    tiencoc += ttbg.TriGia * (thamso.PhanTramCoc - 5) / 100;
                                                    tbTienCoc.Text = tiencoc.ToString();
                                                }
                                                else
                                                {

                                                    int tiencoc = int.Parse(tbTienCoc.Text);
                                                    tiencoc += ttbg.TriGia * thamso.PhanTramCoc / 100;
                                                    tbTienCoc.Text = tiencoc.ToString();

                                                }
                                                filteredDSCTDH.Add(bg);
                                                dgvCTDH.DataSource = filteredDSCTDH;
                                                dgvCTDH.Refresh();
                                            }
                                            else
                                            {
                                                MessageBox.Show("Board game đã tồn tại trong danh sách.");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Board game đã được đặt trước");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Hãy nhập tên khách hàng");
                                    }
                                }
                                else
                                {
                                    bool boardGameExists = false;

                                    foreach (BoardGame item in filteredDSCTDH)
                                    {
                                        if (item.MaBG == bg.MaBG)
                                        {
                                            boardGameExists = true;
                                            break;
                                        }
                                    }

                                    if (!boardGameExists)
                                    {
                                        int tongtien = int.Parse(tbTongTien.Text);
                                        tongtien += ttbg.GiaThue;
                                        tbTongTien.Text = tongtien.ToString();

                                        if (bg.TinhTrangBG == "Trầy xước")
                                        {
                                            int tiencoc = int.Parse(tbTienCoc.Text);
                                            tiencoc += ttbg.TriGia * (thamso.PhanTramCoc - 5) / 100;
                                            tbTienCoc.Text = tiencoc.ToString();
                                        }
                                        else
                                        {

                                            int tiencoc = int.Parse(tbTienCoc.Text);
                                            tiencoc += ttbg.TriGia * thamso.PhanTramCoc / 100;
                                            tbTienCoc.Text = tiencoc.ToString();

                                        }
                                        filteredDSCTDH.Add(bg);
                                        dgvCTDH.DataSource = filteredDSCTDH;
                                        dgvCTDH.Refresh();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Board game đã tồn tại trong danh sách.");
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Board game đã được đặt trước hoặc đang được thuê.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Board game đã bị hỏng.");
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một board game để thêm vào đơn hàng.");
                }
            }
            else
            {
                MessageBox.Show("Số lượng board game cho phép thuê đã tối đa");
            }


        }





        private void bXoaDSDH_Click(object sender, EventArgs e)
        {
            List<BoardGame> filteredDSCTDH = (List<BoardGame>)dgvCTDH.DataSource;

            if (dgvCTDH.SelectedRows.Count > 0)
            {
                var valueBG = dgvCTDH.SelectedRows[0].Cells[0].Value.ToString();

                var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaBG", ObjectId.Parse(valueBG));
                List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();
                BoardGame bg = filteredBGs[0];

                var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", bg.MaTTBG);
                List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
                ThongTinBG ttbg = filteredTTBGs[0];

                int tongtien = int.Parse(tbTongTien.Text);
                tongtien -= ttbg.GiaThue;
                tbTongTien.Text = tongtien.ToString();

                int tiencoc = int.Parse(tbTienCoc.Text);
                tiencoc -= ttbg.TriGia * thamso.PhanTramCoc / 100;
                tbTienCoc.Text = tiencoc.ToString();

                int index = filteredDSCTDH.FindIndex(item => item.MaBG == bg.MaBG);
                if (index >= 0)
                {
                    filteredDSCTDH.RemoveAt(index);
                }

                dgvCTDH.DataSource = null; // Gán DataSource về null trước khi cập nhật
                dgvCTDH.DataSource = filteredDSCTDH;
                dgvCTDH.Refresh();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.");
            }

        }





        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        //Truy xuất đơn hàng

        private void bTimKiemDSDH_Click(object sender, EventArgs e)
        {
            var thongTinKHquery = Builders<KhachHang>.Filter.Eq("TenKH", cbTimKiemDSDH.Text);
            List<KhachHang> filteredKHs = collection_KH.Find(thongTinKHquery).ToList();
            dgvDSKH.DataSource = filteredKHs;
        }
        private void bMacDinhDSDH_Click(object sender, EventArgs e)
        {
            cbTimKiemDSDH.Text = "";
            ReadAllDocuments_KHDH();

        }

        private int selectedRowIndex = 0;
        private void dgvDSKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                List<DonHang> filteredDHs = new List<DonHang>();
                dgvDSDHKH.DataSource = filteredDHs;
                var thongTinDHquery = Builders<DonHang>.Filter.Eq("MaKH", ObjectId.Parse(dgvDSKH.Rows[e.RowIndex].Cells[0].Value.ToString()));
                filteredDHs = collection_DH.Find(thongTinDHquery).ToList();
                dgvDSDHKH.DataSource = filteredDHs;
                selectedRowIndex = e.RowIndex;
            }
        }

        
        private void dgvDSDHKH_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < dgvDSDHKH.Rows.Count)
            {
                var thongTinDHquery = Builders<DonHang>.Filter.Eq("MaDH", ObjectId.Parse(dgvDSDHKH.Rows[e.RowIndex].Cells[0].Value.ToString()));
                List<DonHang> filteredDHs = collection_DH.Find(thongTinDHquery).ToList();
                DonHang dh = filteredDHs[0];
                ThongTinDonHang thongTinDonHang = new ThongTinDonHang(dh);
                thongTinDonHang.ShowDialog();

                dgvDSKH.CurrentCell = dgvDSKH.Rows[selectedRowIndex].Cells[0]; // Đặt lại dòng đang chọn
            }

        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------
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





        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        //Lập báo cáo theo thể loại board game

        private void bLapBaoCao_Click(object sender, EventArgs e)
        {
            
            if (cbChonThang.Text != "" && cbChonNam.Text != "" 
                && int.Parse(cbChonThang.Text)> 0 && int.Parse(cbChonThang.Text) < 13
                && int.Parse(cbChonNam.Text) >= 2023 && int.Parse(cbChonNam.Text) <= 2030)
            {
                int thang = int.Parse(cbChonThang.Text.ToString());
                int nam = int.Parse(cbChonNam.Text.ToString());
                var thongTinDHQuery = Builders<DonHang>.Filter.Where(dh =>
                dh.NgayTra.Month == thang && dh.NgayTra.Year == nam);
                List<DonHang> listDHs = collection_DH.Find(thongTinDHQuery).ToList();

                BaoCao bc = new BaoCao(thang, nam);
                collection_BC.InsertOne(bc);
                int tongDoanhThu = 0;
                int soDonHang = 0;
                foreach (LoaiBG lbg in collection_LBG.AsQueryable().ToList<LoaiBG>())
                {
                    
                    int soLuongDonHang = 0;
                    int doanhThu = 0;


                    foreach (DonHang dh in listDHs)
                    {
                        foreach (CTDonHang ctdh in collection_CTDH.Find(Builders<CTDonHang>.Filter.Eq("MaDH", dh.MaDH)).ToList())
                        {
                            foreach (BoardGame bg in collection_G.Find(Builders<BoardGame>.Filter.Eq("MaBG", ctdh.MaBG)).ToList())
                            {
                                ThongTinBG ttbg = collection_BG.Find(Builders<ThongTinBG>.Filter.Eq("MaTTBG", bg.MaTTBG)).FirstOrDefault();

                                if (ttbg != null && ttbg.MaLBG == lbg.MaLBG)
                                {
                                    soLuongDonHang++;
                                    doanhThu += dh.TongTien;

                                }

                            }


                        }
                    }
                    CTBaoCao ctbc = new CTBaoCao(lbg.MaLBG, bc.MaBC, soLuongDonHang);
                    collection_CTBC.InsertOneAsync(ctbc);

                    tongDoanhThu += doanhThu;
                    soDonHang += soLuongDonHang;

                    tbSoDonHang.Text = soDonHang.ToString();
                    tbDoanhThu.Text = tongDoanhThu.ToString();

                    var updateDef = Builders<BaoCao>.Update.Set("TongDoanhThu", tongDoanhThu).Set("SoDonHang", soDonHang);
                    collection_BC.UpdateOneAsync(nbc => nbc.MaBC == bc.MaBC, updateDef);
                    DataTable dt = new DataTable();
                    var thongTinBCquery = Builders<CTBaoCao>.Filter.Eq("MaBC", bc.MaBC);
                    List<CTBaoCao> filteredBCs = collection_CTBC.Find(thongTinBCquery).ToList();
                    dt.Columns.Add("MaLBG", typeof(ObjectId));
                    dt.Columns.Add("TenLBG", typeof(string));
                    dt.Columns.Add("SoLuongDonHang", typeof(int));

                    // Thêm dữ liệu từ filteredBCs vào DataTable
                    foreach (CTBaoCao item in filteredBCs)
                    {
                        DataRow row = dt.NewRow();
                        row["MaLBG"] = item.MaLBG;
                        var thongTinLBGquery = Builders<LoaiBG>.Filter.Eq("MaLBG", item.MaLBG);
                        List<LoaiBG> filteredLBGs = collection_LBG.Find(thongTinLBGquery).ToList();
                        LoaiBG lbg1 = filteredLBGs[0];
                        row["TenLBG"] = lbg1.TenLBG;
                        row["SoLuongDonHang"] = item.SoLuongDonHang;
                        dt.Rows.Add(row);
                    }
                    dgvBaoCao.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show("Thông tin tháng và năm sai cú phápu!");
            }
            
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




        //-------------------------------------------------------------------------------------------------------------------------------------------------------


        //Quản lí danh sách khách hàng
        private void dgvKhachHang_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhachHang.Rows.Count)
            {
                var thongTinKHquery = Builders<KhachHang>.Filter.Eq("MaKH", ObjectId.Parse(dgvKhachHang.Rows[e.RowIndex].Cells[0].Value.ToString()));
                List<KhachHang> filteredKHs = collection_KH.Find(thongTinKHquery).ToList();
                KhachHang kh = filteredKHs[0];
                ThemKhachHang themKhachHang = new ThemKhachHang(kh);
                themKhachHang.ShowDialog();
                ReadAllDocuments_KH();
            }
        }

        private void bThemKH_Click(object sender, EventArgs e)
        {
            ThemKhachHang themKhachHang = new ThemKhachHang();
            themKhachHang.ShowDialog();
            ReadAllDocuments_KH();
        }

        private void bTimKiemKH_Click_1(object sender, EventArgs e)
        {
            var thongTinKHquery = Builders<KhachHang>.Filter.Eq("TenKH", cbTimKiemKH.Text);
            List<KhachHang> filteredKHs = collection_KH.Find(thongTinKHquery).ToList();
            dgvKhachHang.DataSource = filteredKHs;
        }

        private void bMacDinhKH_Click_1(object sender, EventArgs e)
        {
            cbTimKiemKH.Text = "";
            ReadAllDocuments_KH();
        }




        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        //Quản lí danh sách ưu đãi
        public void ReadAllDocuments_UD()
        {
            List<UuDai> list = collection_UD.AsQueryable().ToList<UuDai>();
            dgvUuDai.DataSource = list;

        }

        private void dgvUuDai_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhachHang.Rows.Count)
            {
                var thongTinUDquery = Builders<UuDai>.Filter.Eq("MaUD", ObjectId.Parse(dgvUuDai.Rows[e.RowIndex].Cells[0].Value.ToString()));
                List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
                UuDai ud = filteredUDs[0];
                ThemUuDai themUuDai = new ThemUuDai(ud);
                themUuDai.ShowDialog();
                ReadAllDocuments_UD();
            }
        }

        private void bThemUD_Click(object sender, EventArgs e)
        {

            ThemUuDai themUuDai = new ThemUuDai();
            themUuDai.ShowDialog();
            ReadAllDocuments_UD();
        }

        private void bTimKiemUD_Click_1(object sender, EventArgs e)
        {
            var thongTinUDquery = Builders<UuDai>.Filter.Eq("TenUD", cbTimKiemUD.Text);
            List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
            dgvUuDai.DataSource = filteredUDs;
        }

        private void bMacDinhUD_Click_1(object sender, EventArgs e)
        {
            cbTimKiemUD.Text = "";
            ReadAllDocuments_UD();
        }


        // Kiểm tra giá trị các text box phải là số
        private void tbSoNgayThueMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho ký tự được hiển thị trong text box
            }
        }


        //Quản lí tham số
        private void bMacDinhTS_Click(object sender, EventArgs e)
        {
            tbSoNgayThueMax.Text = thamso.SoNgayThueTD.ToString();
            tbSoNgayThueMin.Text = thamso.SoNgayThueTT.ToString();
            tbPhanTramCoc.Text = thamso.PhanTramCoc.ToString();
            tbSoDonHangMax.Text = thamso.SoDonHangTD.ToString();
            tbSoBGMax.Text = thamso.SoBoardGameTD.ToString();
        }

        private void bSuaThamSo_Click(object sender, EventArgs e)
        {
            var updateDef = Builders<ThamSo>.Update.Set("SoNgayThueTD", int.Parse(tbSoNgayThueMax.Text)).Set("SoNgayThueTT", int.Parse(tbSoNgayThueMin.Text)).Set("PhanTramCoc", int.Parse(tbPhanTramCoc.Text)).Set("SoDonHangTD", int.Parse(tbSoDonHangMax.Text)).Set("SoBoardGameTD", int.Parse(tbSoBGMax.Text));
            collection_TS.UpdateOneAsync(ts1 => ts1.MaTS == thamso.MaTS, updateDef);
            List<ThamSo> listTSs = collection_TS.AsQueryable().ToList<ThamSo>();
            ThamSo ts = listTSs[0];
            this.thamso = ts;
            MessageBox.Show("Cập nhật thông tin tham số thành công");
        }



        //------------------------------------------------------------------------------------------

        //Quản lí biên bản:

        public void HienThiDS_BienBan()
        {
            tbSoTienPhat.ReadOnly = true;
            tbSoBienBan.ReadOnly = true;
            List<BienBan> listBBs = collection_BB.AsQueryable().ToList<BienBan>();
            tbSoBienBan.Text = listBBs.Count.ToString();
            int tongTienPhat = 0;
            // Tạo DataTable mới
            DataTable dt = new DataTable();
            dt.Columns.Add("MaBB", typeof(ObjectId));
            dt.Columns.Add("TenBoardGame", typeof(string));
            dt.Columns.Add("LyDo", typeof(string));
            dt.Columns.Add("LoaiPhat", typeof(string));
            dt.Columns.Add("SoTienPhat", typeof(int));

            // Thêm dữ liệu từ filteredBCs vào DataTable
            foreach (BienBan item in listBBs)
            {
                DataRow row = dt.NewRow();
                row["MaBB"] = item.MaBB;
                var thongTinCTDHquery = Builders<CTDonHang>.Filter.Eq("MaCTDH", item.MaCTDH);
                List<CTDonHang> filteredCTDHs = collection_CTDH.Find(thongTinCTDHquery).ToList();
                if (filteredCTDHs.Count > 0)
                {
                    CTDonHang ctdh = filteredCTDHs[0];

                    var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaBG", ctdh.MaBG);
                    List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();
                    BoardGame bg = filteredBGs[0];

                    var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", bg.MaTTBG);
                    List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
                    if (filteredTTBGs.Count > 0)
                    {
                        ThongTinBG ttbg = filteredTTBGs[0];

                        row["TenBoardGame"] = ttbg.TenBoardGame;
                    }
                    row["LyDo"] = item.LyDo;

                    var thongTinLPquery = Builders<LoaiPhat>.Filter.Eq("MaLP", item.MaLP);
                    List<LoaiPhat> filteredLPs = collection_LP.Find(thongTinLPquery).ToList();
                    LoaiPhat lp = filteredLPs[0];

                    row["LoaiPhat"] = lp.TenLoaiPhat;
                    row["SoTienPhat"] = lp.SoTienPhat;
                    tongTienPhat += lp.SoTienPhat;
                    dt.Rows.Add(row);
                }
            }
            tbSoTienPhat.Text=tongTienPhat.ToString();
            dgvDSBienBan.DataSource = dt; // Sử dụng DataTable làm nguồn dữ liệu cho DataGridView

        }

        private void cbChonThang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho ký tự được hiển thị trong text box
            }
        }
    }

}
