﻿using System;
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
        private void buttonAddMember_Click(object sender, EventArgs e)
        {
            FThemKH fMember = new FThemKH();
            fMember.ShowDialog();
        }
       
        private void bDanhMuc_Click(object sender, EventArgs e)
        {
            tabManage.SelectedIndex = 0;
        }

        private void bKhachHang_Click(object sender, EventArgs e)
        {
            tabManage.SelectedIndex = 1;
        }

        private void bHoaDon_Click(object sender, EventArgs e)
        {
            tabManage.SelectedIndex = 2;
        }

        private void bThue_Click(object sender, EventArgs e)
        {
            tabManage.SelectedIndex = 3;
        }

        private void bBaoCao_Click(object sender, EventArgs e)
        {
            tabManage.SelectedIndex = 4;
        }
        private void bUuDai_Click(object sender, EventArgs e)
        {
            tabManage.SelectedIndex = 5;
        }

        private void bAddBG_Click(object sender, EventArgs e)
        {
            ThemBoardGame themBoardGame = new ThemBoardGame();
            themBoardGame.ShowDialog();
        }

        private void bThemUuDai_Click(object sender, EventArgs e)
        {
            ThemUuDai themUuDai = new ThemUuDai();  
            themUuDai.ShowDialog();
        }

        private void bThemHoaDon_Click(object sender, EventArgs e)
        {
            ThemHoaDon themDon = new ThemHoaDon();
            themDon.ShowDialog();
        }

        private void bLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
