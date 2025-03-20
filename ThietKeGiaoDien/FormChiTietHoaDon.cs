using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThietKeGiaoDien
{
    public partial class FormChiTietHoaDon : Form
    {
        public SqlConnection BienConnect;
        private string maHD;
        public FormChiTietHoaDon(string maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
        }
        private void FormChiTietHoaDon_Load(object sender, EventArgs e)
        {
            dgvChiTietHoaDon.RowTemplate.Height = 40;
            try
            {
                string lenhSQL = @"
                SELECT 
                    sp.[Mã sản phẩm], 
                    sp.[Tên sản phẩm], 
                    dv.[Đơn vị tính], 
                    sp.[Phân loại], 
                    cthd.[Số lượng], 
                    cthd.[Đơn giá], 
                    (cthd.[Số lượng] * cthd.[Đơn giá]) AS [Thành tiền]
                FROM ChiTietHoaDon cthd
                JOIN SanPham sp ON cthd.[Mã sản phẩm] = sp.[Mã sản phẩm]
                JOIN dvtSanPham dv ON sp.[Mã sản phẩm] = dv.[Mã sản phẩm]
                WHERE cthd.[Mã hóa đơn] = @MaHoaDon";
                SqlDataAdapter adapter = new SqlDataAdapter(lenhSQL, BienConnect);
                adapter.SelectCommand.Parameters.AddWithValue("@MaHoaDon", maHD);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvChiTietHoaDon.DataSource = dt;
                dgvChiTietHoaDon.Columns["Đơn giá"].DefaultCellStyle.Format = "N0";
                dgvChiTietHoaDon.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";
                dgvChiTietHoaDon.Columns["Số lượng"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết hóa đơn: " + ex.Message);
            }
        }
    }
}
