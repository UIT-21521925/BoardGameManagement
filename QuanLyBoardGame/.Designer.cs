namespace QuanLyBoardGame
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.bThoat = new System.Windows.Forms.Button();
            this.bXacNhanVP = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.cbLoaiVP = new System.Windows.Forms.ComboBox();
            this.cbTTBGVP = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbBGVP = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLyDoVP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTienPhatVP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbTenKHVP = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.bThoat);
            this.panel1.Controls.Add(this.bXacNhanVP);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.cbLoaiVP);
            this.panel1.Controls.Add(this.cbTTBGVP);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbBGVP);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbLyDoVP);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbTienPhatVP);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tbTenKHVP);
            this.panel1.Location = new System.Drawing.Point(-4, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(482, 497);
            this.panel1.TabIndex = 0;
            // 
            // bThoat
            // 
            this.bThoat.BackColor = System.Drawing.Color.Teal;
            this.bThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bThoat.ForeColor = System.Drawing.Color.White;
            this.bThoat.Location = new System.Drawing.Point(250, 373);
            this.bThoat.Name = "bThoat";
            this.bThoat.Size = new System.Drawing.Size(184, 35);
            this.bThoat.TabIndex = 99;
            this.bThoat.Text = "Thoát";
            this.bThoat.UseVisualStyleBackColor = false;
            this.bThoat.Click += new System.EventHandler(this.bSuaBG_Click);
            // 
            // bXacNhanVP
            // 
            this.bXacNhanVP.BackColor = System.Drawing.Color.Teal;
            this.bXacNhanVP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bXacNhanVP.ForeColor = System.Drawing.Color.White;
            this.bXacNhanVP.Location = new System.Drawing.Point(39, 373);
            this.bXacNhanVP.Name = "bXacNhanVP";
            this.bXacNhanVP.Size = new System.Drawing.Size(184, 35);
            this.bXacNhanVP.TabIndex = 98;
            this.bXacNhanVP.Text = "Xác nhận biên bản";
            this.bXacNhanVP.UseVisualStyleBackColor = false;
            this.bXacNhanVP.Click += new System.EventHandler(this.bXacNhanVP_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label26.Location = new System.Drawing.Point(80, 195);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(83, 16);
            this.label26.TabIndex = 97;
            this.label26.Text = "Loại vi phạm";
            // 
            // cbLoaiVP
            // 
            this.cbLoaiVP.FormattingEnabled = true;
            this.cbLoaiVP.Location = new System.Drawing.Point(173, 194);
            this.cbLoaiVP.Name = "cbLoaiVP";
            this.cbLoaiVP.Size = new System.Drawing.Size(204, 21);
            this.cbLoaiVP.TabIndex = 96;
            this.cbLoaiVP.Text = "Chọn loại vi phạm";
            this.cbLoaiVP.SelectedIndexChanged += new System.EventHandler(this.cbLoaiVP_SelectedIndexChanged);
            // 
            // cbTTBGVP
            // 
            this.cbTTBGVP.FormattingEnabled = true;
            this.cbTTBGVP.Items.AddRange(new object[] {
            "Tốt",
            "Bị hỏng",
            "Trầy xước"});
            this.cbTTBGVP.Location = new System.Drawing.Point(173, 157);
            this.cbTTBGVP.Name = "cbTTBGVP";
            this.cbTTBGVP.Size = new System.Drawing.Size(204, 21);
            this.cbTTBGVP.TabIndex = 95;
            this.cbTTBGVP.Text = "Cập nhật lại tình trạng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(16, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 16);
            this.label4.TabIndex = 94;
            this.label4.Text = "Tình trạng board game";
            // 
            // cbBGVP
            // 
            this.cbBGVP.FormattingEnabled = true;
            this.cbBGVP.Location = new System.Drawing.Point(173, 117);
            this.cbBGVP.Name = "cbBGVP";
            this.cbBGVP.Size = new System.Drawing.Size(204, 21);
            this.cbBGVP.TabIndex = 92;
            this.cbBGVP.Text = "Chọn mã board game bị vi phạm";
            this.cbBGVP.SelectedIndexChanged += new System.EventHandler(this.cbBGVP_SelectedIndexChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label28.Location = new System.Drawing.Point(58, 117);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(103, 16);
            this.label28.TabIndex = 91;
            this.label28.Text = "Mã board game";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(120, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 87;
            this.label2.Text = "Lý do";
            // 
            // tbLyDoVP
            // 
            this.tbLyDoVP.Location = new System.Drawing.Point(173, 232);
            this.tbLyDoVP.Name = "tbLyDoVP";
            this.tbLyDoVP.Size = new System.Drawing.Size(204, 20);
            this.tbLyDoVP.TabIndex = 86;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(83, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 85;
            this.label1.Text = "Số tiền phạt";
            // 
            // tbTienPhatVP
            // 
            this.tbTienPhatVP.Location = new System.Drawing.Point(173, 270);
            this.tbTienPhatVP.Name = "tbTienPhatVP";
            this.tbTienPhatVP.Size = new System.Drawing.Size(204, 20);
            this.tbTienPhatVP.TabIndex = 84;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(138, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 20);
            this.label3.TabIndex = 83;
            this.label3.Text = "Biên bản xử lý vi phạm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(59, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 16);
            this.label7.TabIndex = 75;
            this.label7.Text = "Tên khách hàng";
            // 
            // tbTenKHVP
            // 
            this.tbTenKHVP.Location = new System.Drawing.Point(173, 78);
            this.tbTenKHVP.Name = "tbTenKHVP";
            this.tbTenKHVP.Size = new System.Drawing.Size(204, 20);
            this.tbTenKHVP.TabIndex = 71;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 498);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Biên bản";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbTenKHVP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLyDoVP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTienPhatVP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbBGVP;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cbLoaiVP;
        private System.Windows.Forms.ComboBox cbTTBGVP;
        private System.Windows.Forms.Button bThoat;
        private System.Windows.Forms.Button bXacNhanVP;
    }
}