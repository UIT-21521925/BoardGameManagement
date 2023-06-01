﻿using MongoDB.Bson;
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
        static IMongoCollection<ThongTinBG> collection_TTBG = db.GetCollection<ThongTinBG>("TTBoardGame");
        static IMongoCollection<BoardGame> collection_BG = db.GetCollection<BoardGame>("BoardGame");
        static IMongoCollection<KhachHang> collection_KH = db.GetCollection<KhachHang>("KhachHang");
        static IMongoCollection<UuDai> collection_UD = db.GetCollection<UuDai>("UuDai");
        static IMongoCollection<CTDonHang> collection_CTDH = db.GetCollection<CTDonHang>("CTDonHang");
        static IMongoCollection<DonHang> collection_DH = db.GetCollection<DonHang>("DonHang");

        public Admin()
        {
            InitializeComponent();
            ReadAllDocuments_ThongTinBG();
            ReadAllDocuments_TTBG();
            ReadAllDocuments_KH();
            ReadAllDocuments_UD();
            ReadAllDocuments_DSKH();
        }
       
        private void bDanhMuc_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 1;
            labelText.Text = this.Text;
        }

        private void bKho_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 2;
            labelText.Text = this.Text;
        }

        private void bKhachHang_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 3;
            labelText.Text = this.Text;
        }

        private void bThue_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 4;
            labelText.Text = this.Text;
        }

        private void bBaoCao_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 5;
            labelText.Text = this.Text;
        }
        private void bUuDai_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 6;
            labelText.Text = this.Text;
        }

        private void bLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
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
           Form1 form1 = new Form1();   
           form1.ShowDialog();
        }

        //Quản lý Board game
        public void ReadAllDocuments_ThongTinBG()
        {
            List<ThongTinBG> list = collection_TTBG.AsQueryable().ToList<ThongTinBG>();
            dgvTT.DataSource = list;
            tbSoLuong.ReadOnly = true;
            tbMaThongTin.Text = dgvTT.Rows[0].Cells[0].Value.ToString();
            tbTenBoardGame.Text = dgvTT.Rows[0].Cells[1].Value.ToString();
            nudSoNguoiChoi.Text = dgvTT.Rows[0].Cells[2].Value.ToString();
            tbDoTuoi.Text = dgvTT.Rows[0].Cells[3].Value.ToString();
            tbTriGia.Text = dgvTT.Rows[0].Cells[4].Value.ToString();
            tbGiaThue.Text = dgvTT.Rows[0].Cells[5].Value.ToString();
            tbSoLuong.Text = dgvTT.Rows[0].Cells[6].Value.ToString();
            cbTinhTrangTTBG.Text = dgvTT.Rows[0].Cells[7].Value.ToString();
            cbTheLoai.Text = dgvTT.Rows[0].Cells[8].Value.ToString();
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
            cbTheLoai.Text = dgvTT.Rows[e.RowIndex].Cells[8].Value.ToString();
        }

        private void bThemTT_Click(object sender, EventArgs e)
        {
            ThongTinBG ttbg = new ThongTinBG( tbTenBoardGame.Text, int.Parse(nudSoNguoiChoi.Text)
                , int.Parse(tbDoTuoi.Text), int.Parse(tbTriGia.Text), int.Parse(tbGiaThue.Text)
                ,cbTinhTrangTTBG.Text, cbTheLoai.Text);
                collection_TTBG.InsertOneAsync(ttbg);
                ReadAllDocuments_ThongTinBG();
        }

        private void bSua_Click(object sender, EventArgs e)
        {
            var updateDef = Builders<ThongTinBG>.Update.Set("TenBoardGame", tbTenBoardGame.Text).Set("SoNguoiChoi", nudSoNguoiChoi.Text).Set("DoTuoi",tbDoTuoi.Text).Set("TriGia",tbTriGia.Text).Set("GiaThue", tbGiaThue.Text).Set("SoLuong", tbSoLuong.Text).Set("TinhTrangBG",cbTinhTrangTTBG.Text).Set("MaLBG",cbTheLoai.Text);
            collection_TTBG.UpdateOneAsync(ttbg => ttbg.MaTTBG == ObjectId.Parse(tbMaThongTin.Text), updateDef);
            ReadAllDocuments_ThongTinBG();
        }

        private void bXoaTT_Click(object sender, EventArgs e)
        {
            var filter = Builders<ThongTinBG>.Filter.And(
                Builders<ThongTinBG>.Filter.Eq("MaTTBG", ObjectId.Parse(tbMaThongTin.Text)),
                Builders<ThongTinBG>.Filter.Eq("SoLuong", 0)
            );

            var result = collection_TTBG.DeleteOne(filter);

            if (result.IsAcknowledged && result.DeletedCount > 0)
            {
                ReadAllDocuments_ThongTinBG();
                ReadAllDocuments_TTBG();
            }
            else
            {
                MessageBox.Show("Trong kho vẫn còn tồn tại Board game này");
            }
        }

        private void bTimKiemTT_Click(object sender, EventArgs e)
        {
            List<ThongTinBG> listTTBG = collection_TTBG.AsQueryable().ToList<ThongTinBG>();
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

            List<ThongTinBG> list = collection_TTBG.AsQueryable().ToList<ThongTinBG>();
            dgvTTBG.DataSource = list;
            tbTTBG.Text = dgvTTBG.Rows[0].Cells[0].Value.ToString();
            tbTenTTBG.Text = dgvTTBG.Rows[0].Cells[1].Value.ToString();
        }

        public void ReadAllDocuments_BG()
        {
            List<BoardGame> list = collection_BG.AsQueryable().ToList<BoardGame>();
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
            tbTTBG.ReadOnly=true;
            tbTenTTBG.ReadOnly=true;
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
            BoardGame bg = new BoardGame(ObjectId.Parse(tbTTBG.Text) , cbTinhTrangBG.Text);
            collection_BG.InsertOneAsync(bg);
            var filterTTBG = Builders<ThongTinBG>.Filter.Eq("MaTTBG", ObjectId.Parse(tbTTBG.Text));
            var updateTTBG = Builders<ThongTinBG>.Update.Inc("SoLuong", 1);
            collection_TTBG.UpdateOneAsync(filterTTBG, updateTTBG);
            ReadAllDocuments_BG();
            ReadAllDocuments_TTBG();
            ReadAllDocuments_ThongTinBG();
        }

        private void bSuaBG_Click(object sender, EventArgs e)
        {
            var updateDef = Builders<BoardGame>.Update.Set("TinhTrangBG", cbTinhTrangBG.Text).Set("TinhTrangMuon", cbTinhTrangMuon.Text);
            collection_BG.UpdateOneAsync(bg => bg.MaBG == ObjectId.Parse(tbMaBoardGame.Text), updateDef);
            ReadAllDocuments_BG();
        }

        private void bXoaBG_Click(object sender, EventArgs e)
        {
            collection_BG.DeleteOneAsync(bg => bg.MaBG == ObjectId.Parse(tbMaBoardGame.Text));
            var filterTTBG = Builders<ThongTinBG>.Filter.Eq("MaTTBG", ObjectId.Parse(tbTTBG.Text));
            var updateTTBG = Builders<ThongTinBG>.Update.Inc("SoLuong", -1);
            collection_TTBG.UpdateOneAsync(filterTTBG, updateTTBG);
            ReadAllDocuments_BG();
            ReadAllDocuments_TTBG();
            ReadAllDocuments_ThongTinBG();
        }

        private void bTimKiemTTBG_Click(object sender, EventArgs e)
        {
            List<ThongTinBG> listTTBG = collection_TTBG.AsQueryable().ToList<ThongTinBG>();
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
            tbMaKhachHang.Text = dgvKhachHang.Rows[0].Cells[0].Value.ToString();
            tbTenKhachHang.Text = dgvKhachHang.Rows[0].Cells[1].Value.ToString();
            dtpNgaySinh.Text = dgvKhachHang.Rows[0].Cells[2].Value.ToString();
            tbDiaChi.Text = dgvKhachHang.Rows[0].Cells[3].Value.ToString();
            tbSoDienThoai.Text = dgvKhachHang.Rows[0].Cells[4].Value.ToString();
            tbEmail.Text = dgvKhachHang.Rows[0].Cells[5].Value.ToString();
            tbSoTichDiem.Text = dgvKhachHang.Rows[0].Cells[6].Value.ToString();
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
            KhachHang kh = new KhachHang(tbTenKhachHang.Text, dtpNgaySinh.Value, tbDiaChi.Text, tbSoDienThoai.Text, tbEmail.Text, int.Parse(tbSoTichDiem.Text));
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

        private void bXoaKhachHang_Click(object sender, EventArgs e)
        {
            collection_KH.DeleteOneAsync(kh => kh.MaKH == ObjectId.Parse(tbMaKhachHang.Text));
            ReadAllDocuments_KH();
            ReadAllDocuments_DSKH();
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
            tbMaKHThue.Text = dgvDSKH.Rows[0].Cells[0].Value.ToString();
        }

        public void ReadAllDocuments_DSDHKH()
        {
            List<CTDonHang> list = collection_CTDH.AsQueryable().ToList<CTDonHang>();
            List<CTDonHang> filteredTTBGList = new List<CTDonHang>();
            foreach (var ctdh in list)
            {
                if (ctdh.MaKH == ObjectId.Parse(tbMaKHThue.Text))
                {
                    filteredTTBGList.Add(ctdh);
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
                tbMaUuDaiSD.Text = dgvDSDHKH.Rows[0].Cells[5].Value.ToString();
                tbTongTien.Text = dgvDSDHKH.Rows[0].Cells[6].Value.ToString();
                ReadAllDocuments_DSBGDH();
            } else
            {
                dgvDSBGDH.Rows.Clear();
                tbMaCTDH.Text = "";
                dtpNgayThueDH.Value = DateTime.Now;
                dtpNgayTraDH.Value = DateTime.Now;
                cbTinhTrangDH.Text = "";
                tbMaUuDaiSD.Text = "";
                tbTongTien.Text = "";
            }
        }

        public void ReadAllDocuments_DSBGDH()
        {
            List<DonHang> listDH = collection_DH.AsQueryable().ToList<DonHang>();
            List<BoardGame> listBG = collection_BG.AsQueryable().ToList<BoardGame>();
            List<BoardGame> filteredBGList = new List<BoardGame>();
            foreach (var dh in listDH)
            {
                if (dh.MaCTDH == ObjectId.Parse(tbMaCTDH.Text))
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
            List<ThongTinBG> listTTBG = collection_TTBG.AsQueryable().ToList<ThongTinBG>();
            List<BoardGame> listBG = collection_BG.AsQueryable().ToList<BoardGame>();
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
            tbMaUuDaiSD.Text = dgvDSDHKH.Rows[e.RowIndex].Cells[5].Value.ToString();
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
            tbMaUuDaiSD.Text = "";
            tbTongTien.Text = "";
            
        }

        private void bThemDH_Click(object sender, EventArgs e)
        {
            if (dgvDSBGDH.Rows.Count > 0)
            {
                CTDonHang ctdh = new CTDonHang(dtpNgayThueDH.Value, dtpNgayTraDH.Value, cbTinhTrangDH.Text, ObjectId.Parse(tbMaKHThue.Text), ObjectId.Parse(tbMaUuDaiSD.Text));
                collection_CTDH.InsertOneAsync(ctdh);
                ReadAllDocuments_DSDHKH();
            } else
            {
                MessageBox.Show("Vui lòng chọn một board game để thêm vào đơn hàng.");
            }
        }

        private void bSuaDH_Click(object sender, EventArgs e)
        {
            var updateDef = Builders<CTDonHang>.Update.Set("NgayThue", dtpNgayThueDH.Value).Set("NgayTra", dtpNgayTraDH.Value).Set("TrangThai", cbTinhTrangDH.Text).Set("MaUD", tbMaUuDaiSD.Text);
            collection_CTDH.UpdateOneAsync(ctdh => ctdh.MaCTDH == ObjectId.Parse(tbMaCTDH.Text), updateDef);
            ReadAllDocuments_DSDHKH();
        }

        private void bThemDSDH_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachBG.SelectedRows.Count > 0)
            {
                // Lấy giá trị từ hàng được chọn
                var value = dgvDanhSachBG.SelectedRows[0].Cells[0].Value.ToString();
                var valueTien = dgvDanhSachBG.SelectedRows[0].Cells[1].Value.ToString();

                // Kiểm tra giá trị của trường "TinhTrangMuon"
                var filter = Builders<BoardGame>.Filter.And(
                    Builders<BoardGame>.Filter.Eq("_id", ObjectId.Parse(value)),
                    Builders<BoardGame>.Filter.Ne("TinhTrangMuon", "Dang thue")
                );

                // Cập nhật trường "TinhTrangMuon" thành "Dang thue"
                var updateDef = Builders<BoardGame>.Update.Set("TinhTrangMuon", "Dang thue");
                var updateResult = collection_BG.UpdateOne(filter, updateDef);

                // Kiểm tra kết quả cập nhật
                if (updateResult.ModifiedCount > 0)
                {
                    // Tiếp tục thực hiện các thao tác khác
                    /*DonHang dh = new DonHang(ObjectId.Parse(tbMaCTDH.Text), ObjectId.Parse(value));
                    collection_DH.InsertOneAsync(dh);

                    var filterTongTien = Builders<ThongTinBG>.Filter.Eq("MaTTBG", ObjectId.Parse(valueTien));
                    var projection = Builders<ThongTinBG>.Projection.Expression(ttbg => ttbg.GiaThue);
                    var ttboardGame = collection_TTBG.Find(filterTongTien).Project(projection).FirstOrDefault();
                    decimal giaThue = ttboardGame;
                    // Cập nhật giá trị "TongTien" trong đơn hàng
                    var updateDonHang = Builders<CTDonHang>.Update.Inc("TongTien", giaThue);
                    collection_CTDH.UpdateOne(ctdh => ctdh.MaCTDH ==ObjectId.Parse(tbMaCTDH.Text), updateDonHang);*/

                    DonHang dh = new DonHang(ObjectId.Parse(tbMaCTDH.Text), ObjectId.Parse(value));
                    collection_DH.InsertOneAsync(dh);

                    var filterTongTien = Builders<ThongTinBG>.Filter.Eq("MaTTBG", ObjectId.Parse(valueTien));
                    var projection = Builders<ThongTinBG>.Projection.Expression(ttbg => ttbg.GiaThue);
                    var ttboardGame = collection_TTBG.Find(filterTongTien).Project(projection).FirstOrDefault();

                    int giaThue = ttboardGame;
                        // Cập nhật giá trị "TongTien" trong đơn hàng
                    var updateDonHang = Builders<CTDonHang>.Update.Inc("TongTien", giaThue);
                    collection_CTDH.UpdateOne(ctdh => ctdh.MaCTDH == ObjectId.Parse(tbMaCTDH.Text), updateDonHang);
                    ReadAllDocuments_DSDHKH();
                    ReadAllDocuments_DSBGDH();
                    ReadAllDocuments_dgvDanhSachBG();
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
        }

    private void bXoaDSDH_Click(object sender, EventArgs e)
        {
            if (dgvDSBGDH.SelectedRows.Count > 0)
            {
                
                var value = dgvDSBGDH.SelectedRows[0].Cells[0].Value.ToString();

                var updateDef = Builders<BoardGame>.Update.Set("TinhTrangMuon", "Chua duoc thue");
                collection_BG.UpdateOneAsync(bg => bg.MaBG == ObjectId.Parse(value), updateDef);
                // Xóa dòng đang được chọn khỏi DataGridView
                collection_DH.DeleteOneAsync(bg => bg.MaBG == ObjectId.Parse(value));
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
            tbMaUuDai.Text = dgvUuDai.Rows[0].Cells[0].Value.ToString();
            tbTenUuDai.Text = dgvUuDai.Rows[0].Cells[1].Value.ToString();
            tbMoTa.Text = dgvUuDai.Rows[0].Cells[2].Value.ToString();
            dtpNgayBD.Text = dgvUuDai.Rows[0].Cells[3].Value.ToString();
            dtpNgayKT.Text = dgvUuDai.Rows[0].Cells[4].Value.ToString();
            tbPhanTramGiam.Text = dgvUuDai.Rows[0].Cells[5].Value.ToString();
            tbSoLuongUD.Text = dgvUuDai.Rows[0].Cells[6].Value.ToString();
            tbSoLuongQD.Text = dgvUuDai.Rows[0].Cells[7].Value.ToString();
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
            UuDai ud = new UuDai(tbTenUuDai.Text, tbMoTa.Text, dtpNgayBD.Value, dtpNgayKT.Value, double.Parse(tbPhanTramGiam.Text), int.Parse(tbSoLuongUD.Text),int.Parse(tbSoLuongQD.Text));
            collection_UD.InsertOneAsync(ud);
            ReadAllDocuments_UD();
        }

        private void bSuaUuDai_Click(object sender, EventArgs e)
        {
            var updateDef = Builders<UuDai>.Update.Set("TenUD", tbTenUuDai.Text).Set("MoTa", tbMoTa.Text).Set("NgayBD", dtpNgayBD.Value).Set("NgayKT", dtpNgayKT.Value).Set("PhanTramGiam", double.Parse(tbPhanTramGiam.Text)).Set("SoLuong", int.Parse(tbSoLuongUD.Text)).Set("DiemQuyDoi", int.Parse(tbSoLuongQD.Text));
            collection_UD.UpdateOneAsync(ud => ud.MaUD == ObjectId.Parse(tbMaUuDai.Text), updateDef);
            ReadAllDocuments_UD();
        }

        private void bXoaUuDai_Click(object sender, EventArgs e)
        {
            collection_UD.DeleteOneAsync(ud => ud.MaUD == ObjectId.Parse(tbMaUuDai.Text));
            ReadAllDocuments_UD();
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
    }
}