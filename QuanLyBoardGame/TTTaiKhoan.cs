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
    public partial class TTTaiKhoan : Form
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("BoardGame");
        static IMongoCollection<TaiKhoan> collection_DN = db.GetCollection<TaiKhoan>("DangNhap");

        private TaiKhoan taikhoan; 

        internal TTTaiKhoan(TaiKhoan taikhoan)
        {
            InitializeComponent();
            this.taikhoan = taikhoan;
        }

        private void TTTaiKhoan_Load(object sender, EventArgs e)
        {
            tbTenTaiKhoan.Text = taikhoan.TenTaiKhoan;
            tbMatKhau.Text = taikhoan.MatKhau;
            tbChucVu.Text = taikhoan.ChucVu;
        }

        private void bThoat_Click(object sender, EventArgs e)
        {
            if(lMatKhauMoi.Visible == true) 
            {
                lMatKhauMoi.Visible = false;
                lXacNhan.Visible = false;
                tbMatKhauMoi.Visible = false;
                tbXacNhanMK.Visible = false;
                tbMatKhauMoi.Text = "";
                tbXacNhanMK.Text = "";
                bSuaMK.Text = "Sửa mật khẩu";
            }
            else 
            {
                this.Close();
            }
            
        }

        private void bSuaMK_Click(object sender, EventArgs e)
        {
            if (lMatKhauMoi.Visible == false)
            {
                lMatKhauMoi.Visible = true;
                lXacNhan.Visible = true;
                tbMatKhauMoi.Visible = true;
                tbXacNhanMK.Visible = true;
                bSuaMK.Text = "Lưu";
            }else
            {
                if (tbMatKhauMoi.Text == "" & tbXacNhanMK.Text == "")
                {
                    MessageBox.Show("Chưa nhập mật khẩu mới");
                }else
                {
                    if(tbMatKhauMoi.Text != tbXacNhanMK.Text)
                    {
                        MessageBox.Show("Nhập sai mật khẩu xác nhận");
                    }
                    else
                    {
                        var updateDef = Builders<TaiKhoan>.Update.Set("MatKhau", tbMatKhauMoi.Text);
                        collection_DN.UpdateOneAsync(tk => tk.MaTK == taikhoan.MaTK, updateDef);
                        MessageBox.Show("Đổi mật khẩu thành công");
                        lMatKhauMoi.Visible = false;
                        lXacNhan.Visible = false;
                        tbMatKhau.Text = tbMatKhauMoi.Text;
                        tbMatKhauMoi.Visible = false;
                        tbXacNhanMK.Visible = false;
                        tbMatKhauMoi.Text = "";
                        tbXacNhanMK.Text = "";
                        bSuaMK.Text = "Sửa mật khẩu";

                    }
                }
            }

        }

        
    }
}
