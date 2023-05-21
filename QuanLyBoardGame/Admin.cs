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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
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
    }
}
