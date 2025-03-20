namespace ThietKeGiaoDien
{
    partial class FormDoiTac
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnKH = new System.Windows.Forms.Button();
            this.btn_NCC = new System.Windows.Forms.Button();
            this.pnl_DoiTac = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnKH, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_NCC, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1426, 65);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(715, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(682, 45);
            this.panel1.TabIndex = 3;
            // 
            // btnKH
            // 
            this.btnKH.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnKH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKH.Location = new System.Drawing.Point(23, 3);
            this.btnKH.Name = "btnKH";
            this.btnKH.Size = new System.Drawing.Size(252, 59);
            this.btnKH.TabIndex = 0;
            this.btnKH.Text = "Khách Hàng";
            this.btnKH.UseVisualStyleBackColor = false;
            this.btnKH.Click += new System.EventHandler(this.btnKH_Click);
            this.btnKH.MouseEnter += new System.EventHandler(this.FormXX_MouseEnter);
            this.btnKH.MouseLeave += new System.EventHandler(this.FormXX_MouseLeave);
            // 
            // btn_NCC
            // 
            this.btn_NCC.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_NCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_NCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NCC.Location = new System.Drawing.Point(369, 3);
            this.btn_NCC.Name = "btn_NCC";
            this.btn_NCC.Size = new System.Drawing.Size(255, 59);
            this.btn_NCC.TabIndex = 1;
            this.btn_NCC.Text = "Nhà Cung Cấp";
            this.btn_NCC.UseVisualStyleBackColor = false;
            this.btn_NCC.Click += new System.EventHandler(this.button2_Click);
            this.btn_NCC.MouseEnter += new System.EventHandler(this.FormXX_MouseEnter);
            this.btn_NCC.MouseLeave += new System.EventHandler(this.FormXX_MouseLeave);
            // 
            // pnl_DoiTac
            // 
            this.pnl_DoiTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_DoiTac.Location = new System.Drawing.Point(0, 65);
            this.pnl_DoiTac.Name = "pnl_DoiTac";
            this.pnl_DoiTac.Size = new System.Drawing.Size(1426, 788);
            this.pnl_DoiTac.TabIndex = 1;
            // 
            // FormDoiTac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(23F, 46F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 853);
            this.Controls.Add(this.pnl_DoiTac);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(9);
            this.Name = "FormDoiTac";
            this.Text = "FormDoiTac";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnKH;
        private System.Windows.Forms.Button btn_NCC;
        private System.Windows.Forms.Panel pnl_DoiTac;
    }
}