namespace ThietKeGiaoDien
{
    partial class FormHangHoa
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Cl_tenMatHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cl_hang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cl_phanLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cl_maMau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cl_donVi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cl_soLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cl_tacVuBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tìm kiếm:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(183, 34);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(549, 41);
            this.textBox1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cl_tenMatHang,
            this.Cl_hang,
            this.Cl_phanLoai,
            this.Cl_maMau,
            this.Cl_donVi,
            this.Cl_soLuong,
            this.Cl_tacVuBtn});
            this.dataGridView1.Location = new System.Drawing.Point(43, 135);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1132, 689);
            this.dataGridView1.TabIndex = 2;
            // 
            // Cl_tenMatHang
            // 
            this.Cl_tenMatHang.HeaderText = "Tên mặt hàng";
            this.Cl_tenMatHang.MinimumWidth = 6;
            this.Cl_tenMatHang.Name = "Cl_tenMatHang";
            this.Cl_tenMatHang.Width = 225;
            // 
            // Cl_hang
            // 
            this.Cl_hang.HeaderText = "Hãng";
            this.Cl_hang.MinimumWidth = 6;
            this.Cl_hang.Name = "Cl_hang";
            this.Cl_hang.Width = 125;
            // 
            // Cl_phanLoai
            // 
            this.Cl_phanLoai.HeaderText = "Phân loại";
            this.Cl_phanLoai.MinimumWidth = 6;
            this.Cl_phanLoai.Name = "Cl_phanLoai";
            this.Cl_phanLoai.Width = 150;
            // 
            // Cl_maMau
            // 
            this.Cl_maMau.HeaderText = "Mã màu";
            this.Cl_maMau.MinimumWidth = 6;
            this.Cl_maMau.Name = "Cl_maMau";
            this.Cl_maMau.Width = 150;
            // 
            // Cl_donVi
            // 
            this.Cl_donVi.HeaderText = "Đơn vị";
            this.Cl_donVi.MinimumWidth = 6;
            this.Cl_donVi.Name = "Cl_donVi";
            this.Cl_donVi.Width = 150;
            // 
            // Cl_soLuong
            // 
            this.Cl_soLuong.HeaderText = "Số lượng";
            this.Cl_soLuong.MinimumWidth = 6;
            this.Cl_soLuong.Name = "Cl_soLuong";
            this.Cl_soLuong.Width = 150;
            // 
            // Cl_tacVuBtn
            // 
            this.Cl_tacVuBtn.HeaderText = "Tác vụ";
            this.Cl_tacVuBtn.MinimumWidth = 6;
            this.Cl_tacVuBtn.Name = "Cl_tacVuBtn";
            this.Cl_tacVuBtn.Width = 125;
            // 
            // FormHangHoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 36F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 853);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Name = "FormHangHoa";
            this.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Text = "FormHangHoa";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cl_tenMatHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cl_hang;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cl_phanLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cl_maMau;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cl_donVi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cl_soLuong;
        private System.Windows.Forms.DataGridViewButtonColumn Cl_tacVuBtn;
    }
}