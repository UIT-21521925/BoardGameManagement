namespace QuanLyBoardGame
{
    partial class DangNhap
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
            this.panelColor = new System.Windows.Forms.Panel();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.textboxMatKhau = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.panelName = new System.Windows.Forms.Panel();
            this.textboxTen = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.panelLogin.SuspendLayout();
            this.panelPassword.SuspendLayout();
            this.panelName.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelColor
            // 
            this.panelColor.BackColor = System.Drawing.Color.Teal;
            this.panelColor.Location = new System.Drawing.Point(1, 2);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(29, 292);
            this.panelColor.TabIndex = 3;
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.Color.White;
            this.panelLogin.Controls.Add(this.buttonCancel);
            this.panelLogin.Controls.Add(this.buttonLogin);
            this.panelLogin.Controls.Add(this.label1);
            this.panelLogin.Controls.Add(this.panelPassword);
            this.panelLogin.Controls.Add(this.panelName);
            this.panelLogin.Location = new System.Drawing.Point(28, 2);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(438, 292);
            this.panelLogin.TabIndex = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Teal;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(180, 223);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(105, 41);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Thoát";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.Teal;
            this.buttonLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogin.ForeColor = System.Drawing.Color.White;
            this.buttonLogin.Location = new System.Drawing.Point(291, 223);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(105, 41);
            this.buttonLogin.TabIndex = 6;
            this.buttonLogin.Text = "Đăng nhập";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(37, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "Quản lý board game";
            // 
            // panelPassword
            // 
            this.panelPassword.Controls.Add(this.textboxMatKhau);
            this.panelPassword.Controls.Add(this.labelPassword);
            this.panelPassword.Location = new System.Drawing.Point(41, 140);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Size = new System.Drawing.Size(355, 59);
            this.panelPassword.TabIndex = 4;
            // 
            // textboxMatKhau
            // 
            this.textboxMatKhau.Location = new System.Drawing.Point(99, 26);
            this.textboxMatKhau.Name = "textboxMatKhau";
            this.textboxMatKhau.PasswordChar = '*';
            this.textboxMatKhau.Size = new System.Drawing.Size(241, 20);
            this.textboxMatKhau.TabIndex = 2;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassword.Location = new System.Drawing.Point(3, 27);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(58, 15);
            this.labelPassword.TabIndex = 1;
            this.labelPassword.Text = "Mật khẩu";
            // 
            // panelName
            // 
            this.panelName.Controls.Add(this.textboxTen);
            this.panelName.Controls.Add(this.labelName);
            this.panelName.Location = new System.Drawing.Point(41, 84);
            this.panelName.Name = "panelName";
            this.panelName.Size = new System.Drawing.Size(355, 59);
            this.panelName.TabIndex = 3;
            // 
            // textboxTen
            // 
            this.textboxTen.Location = new System.Drawing.Point(99, 21);
            this.textboxTen.Name = "textboxTen";
            this.textboxTen.Size = new System.Drawing.Size(241, 20);
            this.textboxTen.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(3, 22);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(90, 15);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Tên đăng nhập";
            // 
            // DangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 294);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelColor);
            this.Name = "DangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelPassword.ResumeLayout(false);
            this.panelPassword.PerformLayout();
            this.panelName.ResumeLayout(false);
            this.panelName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.Panel panelName;
        private System.Windows.Forms.TextBox textboxTen;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textboxMatKhau;
        private System.Windows.Forms.Label labelPassword;
    }
}

