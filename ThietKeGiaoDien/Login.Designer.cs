namespace ThietKeGiaoDien
{
    partial class Login
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
            this.btLogin = new System.Windows.Forms.Button();
            this.lbLogin = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.pnl_SignIn = new System.Windows.Forms.Panel();
            this.btnShowPassword = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.pnl_SignIn.SuspendLayout();
            this.SuspendLayout();
            // 
            // btLogin
            // 
            this.btLogin.BackColor = System.Drawing.Color.ForestGreen;
            this.btLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btLogin.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLogin.ForeColor = System.Drawing.SystemColors.Control;
            this.btLogin.Location = new System.Drawing.Point(100, 305);
            this.btLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(360, 53);
            this.btLogin.TabIndex = 0;
            this.btLogin.Text = "Đăng Nhập";
            this.btLogin.UseVisualStyleBackColor = false;
            this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // lbLogin
            // 
            this.lbLogin.AutoSize = true;
            this.lbLogin.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLogin.Location = new System.Drawing.Point(200, 50);
            this.lbLogin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(153, 35);
            this.lbLogin.TabIndex = 1;
            this.lbLogin.Text = "Đăng nhập";
            // 
            // tbUser
            // 
            this.tbUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUser.Location = new System.Drawing.Point(100, 100);
            this.tbUser.Margin = new System.Windows.Forms.Padding(4);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(359, 38);
            this.tbUser.TabIndex = 2;
            this.tbUser.Text = "Username";
            this.tbUser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbUser_MouseDown);
            // 
            // pnl_SignIn
            // 
            this.pnl_SignIn.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pnl_SignIn.Controls.Add(this.btnShowPassword);
            this.pnl_SignIn.Controls.Add(this.tbPassword);
            this.pnl_SignIn.Controls.Add(this.lbLogin);
            this.pnl_SignIn.Controls.Add(this.btLogin);
            this.pnl_SignIn.Controls.Add(this.tbUser);
            this.pnl_SignIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_SignIn.Location = new System.Drawing.Point(0, 0);
            this.pnl_SignIn.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_SignIn.Name = "pnl_SignIn";
            this.pnl_SignIn.Size = new System.Drawing.Size(565, 536);
            this.pnl_SignIn.TabIndex = 4;
            // 
            // btnShowPassword
            // 
            this.btnShowPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowPassword.Location = new System.Drawing.Point(422, 171);
            this.btnShowPassword.Margin = new System.Windows.Forms.Padding(4);
            this.btnShowPassword.Name = "btnShowPassword";
            this.btnShowPassword.Size = new System.Drawing.Size(36, 36);
            this.btnShowPassword.TabIndex = 4;
            this.btnShowPassword.Text = "👁️";
            this.btnShowPassword.UseVisualStyleBackColor = true;
            this.btnShowPassword.Click += new System.EventHandler(this.btnShowPassword_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.Location = new System.Drawing.Point(100, 170);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(4);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(359, 38);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.Text = "Password";
            this.tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassword_KeyDown);
            this.tbPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbPassword_MouseDown);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 536);
            this.Controls.Add(this.pnl_SignIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.Load += new System.EventHandler(this.Login_Load);
            this.pnl_SignIn.ResumeLayout(false);
            this.pnl_SignIn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btLogin;
        private System.Windows.Forms.Label lbLogin;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Panel pnl_SignIn;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnShowPassword;
    }
}