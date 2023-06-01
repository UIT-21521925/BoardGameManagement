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
    public partial class DangNhap : Form
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("BoardGame");
        static IMongoCollection<TaiKhoan> collection_DN = db.GetCollection<TaiKhoan>("DangNhap");
        public DangNhap()
        {
            InitializeComponent();
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string taiKhoan = textboxTen.Text;
            string matKhau = textboxMatKhau.Text;

            var filter = Builders<TaiKhoan>.Filter.Eq("TenTaiKhoan", taiKhoan) & Builders<TaiKhoan>.Filter.Eq("MatKhau", matKhau);
            var result = collection_DN.Find(filter).ToList();

            if (result.Count > 0)
            {
                // Đăng nhập thành công
                Admin admin = new Admin();
                this.Hide();
                admin.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng. Vui lòng thử lại!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
