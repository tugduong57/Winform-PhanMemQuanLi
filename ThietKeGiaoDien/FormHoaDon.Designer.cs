namespace ThietKeGiaoDien
{
    partial class FormHoaDon
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Xuat = new System.Windows.Forms.Button();
            this.btn_LichSu = new System.Windows.Forms.Button();
            this.btn_Nhap = new System.Windows.Forms.Button();
            this.pnl_hoadon = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.87562F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.68657F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.87562F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.68657F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.87562F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Controls.Add(this.btn_Xuat, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_LichSu, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Nhap, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1032, 72);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_Xuat
            // 
            this.btn_Xuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(164)))));
            this.btn_Xuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Xuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Xuat.Location = new System.Drawing.Point(23, 8);
            this.btn_Xuat.Name = "btn_Xuat";
            this.btn_Xuat.Size = new System.Drawing.Size(239, 56);
            this.btn_Xuat.TabIndex = 0;
            this.btn_Xuat.Text = "Xuất";
            this.btn_Xuat.UseVisualStyleBackColor = false;
            this.btn_Xuat.Click += new System.EventHandler(this.btn_Xuat_Click);
            this.btn_Xuat.MouseEnter += new System.EventHandler(this.FormXX_MouseEnter);
            this.btn_Xuat.MouseLeave += new System.EventHandler(this.FormXX_MouseLeave);
            // 
            // btn_LichSu
            // 
            this.btn_LichSu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(164)))));
            this.btn_LichSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_LichSu.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LichSu.Location = new System.Drawing.Point(761, 8);
            this.btn_LichSu.Name = "btn_LichSu";
            this.btn_LichSu.Size = new System.Drawing.Size(239, 56);
            this.btn_LichSu.TabIndex = 2;
            this.btn_LichSu.Text = "Lịch sử";
            this.btn_LichSu.UseVisualStyleBackColor = false;
            this.btn_LichSu.Click += new System.EventHandler(this.btn_LichSu_Click);
            this.btn_LichSu.MouseEnter += new System.EventHandler(this.FormXX_MouseEnter);
            this.btn_LichSu.MouseLeave += new System.EventHandler(this.FormXX_MouseLeave);
            // 
            // btn_Nhap
            // 
            this.btn_Nhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(164)))));
            this.btn_Nhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Nhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Nhap.Location = new System.Drawing.Point(392, 8);
            this.btn_Nhap.Name = "btn_Nhap";
            this.btn_Nhap.Size = new System.Drawing.Size(239, 56);
            this.btn_Nhap.TabIndex = 1;
            this.btn_Nhap.Text = "Nhập";
            this.btn_Nhap.UseVisualStyleBackColor = false;
            this.btn_Nhap.Click += new System.EventHandler(this.btn_Nhap_Click);
            this.btn_Nhap.MouseEnter += new System.EventHandler(this.FormXX_MouseEnter);
            this.btn_Nhap.MouseLeave += new System.EventHandler(this.FormXX_MouseLeave);
            // 
            // pnl_hoadon
            // 
            this.pnl_hoadon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_hoadon.Location = new System.Drawing.Point(0, 72);
            this.pnl_hoadon.Name = "pnl_hoadon";
            this.pnl_hoadon.Size = new System.Drawing.Size(1032, 674);
            this.pnl_hoadon.TabIndex = 1;
            // 
            // FormHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 746);
            this.Controls.Add(this.pnl_hoadon);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(9);
            this.Name = "FormHoaDon";
            this.Text = "FormHoaDon";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_Xuat;
        private System.Windows.Forms.Button btn_LichSu;
        private System.Windows.Forms.Button btn_Nhap;
        private System.Windows.Forms.Panel pnl_hoadon;
    }
}