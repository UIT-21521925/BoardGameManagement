using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBoardGame
{
    public partial class Admin : Form
    {

        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("BoardGame");
        static IMongoCollection<ThongTinBG> collection_TTBG = db.GetCollection<ThongTinBG>("TTBoardGame");
        static IMongoCollection<BoardGame> collection_BG = db.GetCollection<BoardGame>("BoardGame");

        public Admin()
        {
            InitializeComponent();
            ReadAllDocuments_ThongTinBG();
            ReadAllDocuments_TTBG();
        }

        private void mouseClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            this.ForeColor = Color.Black;
            this.BackColor = Color.Teal;
            labelText.Text = btn.Text;
        }
       
        private void bDanhMuc_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 1;
            mouseClick(sender, e);
        }

        private void bKho_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 2;
            mouseClick(sender, e);
        }

        private void bKhachHang_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 3;
            mouseClick(sender, e);
        }

        private void bThue_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 4;
            mouseClick(sender, e);
        }

        private void bBaoCao_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 5;
            mouseClick(sender, e);
        }
        private void bUuDai_Click(object sender, EventArgs e)
        {
            tabQuanLy.SelectedIndex = 6;
            mouseClick(sender, e);
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
                , int.Parse(tbDoTuoi.Text), int.Parse(tbTriGia.Text), int.Parse(tbGiaThue.Text),int.Parse(tbSoLuong.Text)
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
            collection_TTBG.DeleteOneAsync(ttbg => ttbg.MaTTBG == ObjectId.Parse(tbMaThongTin.Text));
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

            tbMaBoardGame.Text = dgvBG.Rows[0].Cells[0].Value.ToString();
            cbTinhTrangBG.Text = dgvBG.Rows[0].Cells[2].Value.ToString();
            cbTinhTrangMuon.Text = dgvBG.Rows[0].Cells[3].Value.ToString();
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
            BoardGame ttbg = new BoardGame(ObjectId.Parse(tbTTBG.Text) , cbTinhTrangBG.Text, cbTinhTrangMuon.Text);
            collection_BG.InsertOneAsync(ttbg);
            ReadAllDocuments_BG();
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
            ReadAllDocuments_BG();
        }
    }
}
