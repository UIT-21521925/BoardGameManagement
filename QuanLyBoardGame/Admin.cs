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
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace QuanLyBoardGame
{
    public partial class Admin : Form
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
        private TaiKhoan taikhoan;
        internal Admin(TaiKhoan taikhoan)
        {
            InitializeComponent();
            this.taikhoan = taikhoan;
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
            dgvDSKH.DataSource=listKHs;
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
            ReadAllDocuments_ThongTinBG();
            List<KhachHang> listKHs = collection_KH.AsQueryable().ToList<KhachHang>();
            cbTimKiemKH.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var kh in listKHs)
            {
                cbTimKiemKH.Items.Add(kh.TenKH);
            }
            ReadAllDocuments_KH();
            List<UuDai> listUDs = collection_UD.AsQueryable().ToList<UuDai>();
            cbTimKiemUD.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var ud in listUDs)
            {
                cbTimKiemUD.Items.Add(ud.TenUD);
            }
            ReadAllDocuments_UD();

        }


        private void bCaiDat_Click(object sender, EventArgs e)
        {
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
            }
        }

        public void HienThidgvBG()
        {
            if (dgvTTBG.SelectedRows.Count > 0)
            {
                var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaTTBG", ObjectId.Parse(dgvTTBG.SelectedRows[0].Cells[0].Value.ToString()));
                List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();
                dgvTTBG.DataSource = filteredBGs;
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




//-------------------------------------------------------------------------------------------------------------------------------------------------------
        //Quản lí lập đơn hàng

        int SoNgayThueMax = 60;
        int PhanTramCoc = 30;
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
            List<BoardGame> list = collection_G.AsQueryable().ToList<BoardGame>();
            dgvDanhSachBG.DataSource = list;
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
        }
       

        private void bMacDinhDH_Click(object sender, EventArgs e)
        {
            cbTenKhachHang.Text = "";
            dgvCTDH.DataSource= new List<BoardGame>(); ;
            dtpNgayThueDH.Value = DateTime.Now;
            dtpNgayTraDH.Value = DateTime.Now;
            cbMaUuDaiSD.Text = "";
            tbTienCoc.Text = "0";
            tbTongTien.Text = "0";

        }

        private void bThemDH_Click(object sender, EventArgs e)
        {
            if (cbTenKhachHang.Text != "" & cbMaUuDaiSD.Text != "") {
                var thongTinKHquery = Builders<KhachHang>.Filter.Eq("TenKH", cbTenKhachHang.Text);
                List<KhachHang> filteredKHs = collection_KH.Find(thongTinKHquery).ToList();
                KhachHang kh = filteredKHs[0];

                var thongTinUDquery = Builders<UuDai>.Filter.Eq("TenUD", cbMaUuDaiSD.Text);
                List<UuDai> filteredUDs = collection_UD.Find(thongTinUDquery).ToList();
                UuDai ud = filteredUDs[0];


                if (dgvCTDH.Rows.Count > 0)
                {
                    if (kh.TichDiem > ud.DiemQuyDoi)
                    {
                        List<BoardGame> filteredDSCTDH = new List<BoardGame>();
                        filteredDSCTDH.AddRange((List<BoardGame>)dgvCTDH.DataSource);

                        var updateCongTichDiemKhachHang = Builders<KhachHang>.Update.Inc("TichDiem", 10);
                        collection_KH.UpdateOne(kh1 => kh1.MaKH == kh.MaKH, updateCongTichDiemKhachHang);

                        int tongtien = int.Parse(tbTongTien.Text);
                        tongtien = tongtien - tongtien * ud.PhanTramGiam / 100;
                        tbTongTien.Text = tongtien.ToString();

                        TimeSpan khoangThoiGianThue = dtpNgayTraDH.Value - dtpNgayThueDH.Value;

                        if (khoangThoiGianThue.TotalDays > SoNgayThueMax)
                        {
                            MessageBox.Show("Không cho phép thuê trên 60 ngày!");
                        }
                        else if (khoangThoiGianThue.TotalDays > 14)
                        {
                            int tongTien = int.Parse(tbTongTien.Text);
                            tongTien -= tongTien * 5 / 100;
                            tbTongTien.Text = tongTien.ToString();

                            int tienCoc = int.Parse(tbTienCoc.Text);
                            tienCoc = tienCoc * 110 / 100;
                            tbTienCoc.Text = tienCoc.ToString();
                        }
                        else if (khoangThoiGianThue.TotalDays > 7)
                        {
                            int tongTien = int.Parse(tbTongTien.Text);
                            tongTien -= tongTien * 5 / 100;
                            tbTongTien.Text = tongTien.ToString();
                        }

                        DonHang dh = new DonHang(dtpNgayThueDH.Value, dtpNgayTraDH.Value, "Chua tra", kh.MaKH, ud.MaUD, int.Parse(tbTienCoc.Text), int.Parse(tbTongTien.Text));
                        collection_DH.InsertOneAsync(dh);

                        var updateUuDai = Builders<UuDai>.Update.Inc("SoLuong", -1);
                        collection_UD.UpdateOne(ud1 => ud1.MaUD == ud.MaUD, updateUuDai);

                        for (int j = 0; j < filteredDSCTDH.Count; j++)
                        {
                            CTDonHang ctdh = new CTDonHang(dh.MaDH, filteredDSCTDH[j].MaBG);
                            collection_CTDH.InsertOneAsync(ctdh);

                            var updateDefBG = Builders<BoardGame>.Update.Set("TinhTrangMuon", "Dang thue");
                            collection_G.UpdateOneAsync(bg1 => bg1.MaBG == filteredDSCTDH[j].MaBG, updateDefBG);

                            var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", filteredDSCTDH[j].MaTTBG);
                            List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
                            ThongTinBG ttbg = filteredTTBGs[0];

                            
                        }




                        var updateTruTichDiemKhachHang = Builders<KhachHang>.Update.Inc("TichDiem", -ud.DiemQuyDoi);
                        collection_KH.UpdateOne(kh1 => kh1.MaKH == kh.MaKH, updateTruTichDiemKhachHang);

                        MessageBox.Show("Thêm đơn hàng thành công! ");
                        filteredDSCTDH = new List<BoardGame>();
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
                MessageBox.Show("Vui lòng nhập đủ thông tin cho đơn hàng.");
            }
        }

       

        
        private void bThemDSDH_Click(object sender, EventArgs e)
        {
            if (dgvCTDH.DataSource == null)
            {
                dgvCTDH.DataSource = new List<BoardGame>();
            }
            var valueTTBG = dgvDanhSachBG.SelectedRows[0].Cells[1].Value;
            var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", valueTTBG);
            List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
            List<BoardGame> filteredDSCTDH = new List<BoardGame>();
            filteredDSCTDH.AddRange((List<BoardGame>)dgvCTDH.DataSource);

            if (filteredTTBGs.Count > 0)
            {
                ThongTinBG ttbg = filteredTTBGs[0];

                if (dgvDanhSachBG.SelectedRows.Count > 0)
                {
                    // Lấy giá trị từ hàng được chọn
                    var value = dgvDanhSachBG.SelectedRows[0].Cells[0].Value.ToString();

                    // Kiểm tra giá trị của trường "TinhTrangMuon"
                    var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaBG", ObjectId.Parse(value));
                    List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();
                    BoardGame bg = filteredBGs[0];

                    // Kiểm tra kết quả cập nhật
                    if (bg.TinhTrangBG != "Hong")
                    {
                        if (bg.TinhTrangMuon != "Dang thue")
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

                                if (bg.TinhTrangBG == "Tray Xuoc")
                                {
                                    int tiencoc = int.Parse(tbTienCoc.Text);
                                    tiencoc += ttbg.TriGia * (PhanTramCoc - 5) / 100;
                                    tbTienCoc.Text = tiencoc.ToString();
                                }
                                else
                                {

                                    int tiencoc = int.Parse(tbTienCoc.Text);
                                    tiencoc += ttbg.TriGia * PhanTramCoc / 100;
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
                            MessageBox.Show("Board game đã được đặt trước hoặc đang được thuê.");
                        }
                    }
                    else { 
                        MessageBox.Show("Board game đã bị hỏng."); 
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một board game để thêm vào đơn hàng.");
                }
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
                tiencoc -= ttbg.TriGia * PhanTramCoc / 100;
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
        }

        private void dgvDSKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var thongTinDHquery = Builders<DonHang>.Filter.Eq("MaKH", ObjectId.Parse(dgvDSKH.Rows[e.RowIndex].Cells[0].Value.ToString()));
                List<DonHang> filteredDHs = collection_DH.Find(thongTinDHquery).ToList();
                if (filteredDHs.Count > 0)
                {
                    dgvDSDHKH.DataSource = filteredDHs;
                }
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
            if (cbChonThang.Text != "" & cbChonNam.Text != "")
            {
                int thang = int.Parse(cbChonThang.SelectedItem.ToString());
                int nam = int.Parse(cbChonNam.SelectedItem.ToString());

                List<LoaiBG> listLBGs = collection_LBG.AsQueryable().ToList<LoaiBG>();
                List<DonHang> listDHs = collection_DH.AsQueryable().ToList<DonHang>();
                BaoCao bc = new BaoCao(thang, nam);
                collection_BC.InsertOneAsync(bc);
                int tongDoanhThu = 0;
                int soDonHang = 0;
                int soBienBan = 0;
                foreach (LoaiBG lbg in listLBGs)
                {
                    int soLuongDonHang = 0;
                    int doanhThu = 0;
                    foreach (DonHang dh in listDHs)
                    {
                        // Lấy thông tin ngày tháng năm từ trường DonHang
                        DateTime ngayTra = dh.NgayTra;

                        
                        // Kiểm tra xem ngày tháng năm có thuộc tháng và năm hiện tại không
                        if (ngayTra.Month == thang && ngayTra.Year == nam)
                        {
                            
                            var thongTinCTDHquery = Builders<CTDonHang>.Filter.Eq("MaDH", dh.MaDH);
                            List<CTDonHang> filteredCTDHs = collection_CTDH.Find(thongTinCTDHquery).ToList();
                            foreach (CTDonHang ctdh in filteredCTDHs)
                            {
                                var thongTinBGquery = Builders<BoardGame>.Filter.Eq("MaBG", ctdh.MaBG);
                                List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();
                                foreach (BoardGame bg in filteredBGs)
                                {

                                    var thongTinTTBGquery = Builders<ThongTinBG>.Filter.Eq("MaTTBG", bg.MaTTBG);
                                    List<ThongTinBG> filteredTTBGs = collection_BG.Find(thongTinTTBGquery).ToList();
                                    ThongTinBG ttbg = filteredTTBGs[0];

                                    if(ttbg.MaLBG==lbg.MaLBG){
                                            soLuongDonHang++;
                                            doanhThu = doanhThu + ttbg.GiaThue;
                                     }
 
                                }
                                var thongTinBBquery = Builders<BienBan>.Filter.Eq("MaCTDH", ctdh.MaCTDH);
                                List<BienBan> filteredBBs = collection_BB.Find(thongTinBBquery).ToList();
                                soBienBan += filteredBBs.Count;
                            }
                        }
                        
                    }
                    CTBaoCao ctbc = new CTBaoCao(lbg.MaLBG, bc.MaBC, soLuongDonHang, doanhThu);
                    collection_CTBC.InsertOneAsync(ctbc);

                    // Tính tổng doanh thu từng đơn hàng
                    
                    tongDoanhThu += doanhThu;
                    soDonHang += soLuongDonHang;



                }
                var updateDef = Builders<BaoCao>.Update.Set("TongDoanhThu", tongDoanhThu).Set("SoDonHang", soDonHang).Set("SoBienBan", soBienBan);
                collection_BC.UpdateOneAsync(nbc => nbc.MaBC == bc.MaBC, updateDef);
                tbSoDonHang.Text = soDonHang.ToString();
                tbDoanhThu.Text = tongDoanhThu.ToString();
                tbSoBienBan.Text = soBienBan.ToString();
                var thongTinBCquery = Builders<CTBaoCao>.Filter.Eq("MaBC", bc.MaBC);
                List<CTBaoCao> filteredBCs = collection_CTBC.Find(thongTinBCquery).ToList();
                dgvBaoCao.DataSource = filteredBCs;
            }
            else
            {
                MessageBox.Show("Chưa chọn thông tin tháng và năm để lập báo cáo!");
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
            }
        }

        private void bThemKH_Click(object sender, EventArgs e)
        {
            ThemKhachHang themKhachHang = new ThemKhachHang();
            themKhachHang.ShowDialog();
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
            }
        }

        private void bThemUD_Click(object sender, EventArgs e)
        {

            ThemUuDai themUuDai = new ThemUuDai();
            themUuDai.ShowDialog();
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
        }

        
    }
}
