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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ThietKeGiaoDien
{
    public partial class FormHangHoa : Form
    {
        public FormHangHoa()
        {
            InitializeComponent();
        }

        public SqlConnection connOfHangHoa;

        DataSet dsTableTheoHang = new DataSet();
        string nameTableNow = "";

        private void FormHangHoa_Load(object sender, EventArgs e)
        {
            string lenhTruyVanSQL1 = "SELECT DISTINCT Hãng FROM SanPham;";
            SqlDataAdapter bienSQL_DataAdapter1 = new SqlDataAdapter(lenhTruyVanSQL1, connOfHangHoa);
            bienSQL_DataAdapter1.Fill(dsTableTheoHang, "HangForComboBox");

            cbHang.DataSource = dsTableTheoHang.Tables["HangForComboBox"];

            // Dòng lệnh này gọi tới sự kiện cbHang_SelectedIndexChanged
            cbHang.DisplayMember = "Hãng";

            // Kích thước chung cho tất cả các hàng
            dgvHangHoa.RowTemplate.Height = 35;
            // Đặt kích thước cho từng cột
            dgvHangHoa.Columns["Mã"].Width = 100;
            dgvHangHoa.Columns["Tên sản phẩm"].Width = 330;
            dgvHangHoa.Columns["Phân loại"].Width = 150;
            dgvHangHoa.Columns["ĐVT"].Width = 90;
            dgvHangHoa.Columns["SL"].Width = 50;
            dgvHangHoa.Columns["Đơn giá"].Width = 125;
            dgvHangHoa.Columns["Đơn giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHangHoa.Columns["Ghi chú"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            foreach (DataGridViewColumn col in dgvHangHoa.Columns)
                col.ReadOnly = true;
            dgvHangHoa.Columns["Ghi chú"].ReadOnly = false;
        }

        // Hàm TextChanged, lọc dataTable khi nội dung thay đổi
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string filterText = tbSearch.Text;
            if (string.IsNullOrWhiteSpace(filterText))
            {
                // reset DefaultView về ""
                dsTableTheoHang.Tables[nameTableNow].DefaultView.RowFilter = "";
                dgvHangHoa.DataSource = dsTableTheoHang.Tables[nameTableNow];
                dgvHangHoa.Refresh();
                return;
            }

            // Dùng DataView để lọc
            dsTableTheoHang.Tables[nameTableNow].DefaultView.RowFilter =
               $"Mã LIKE '%{filterText}%' OR " +
               $"[Tên sản phẩm] LIKE '%{filterText}%' OR " +
               $"[Phân loại] LIKE '%{filterText}%' OR " +
               $"ĐVT LIKE '%{filterText}%' OR " +
               $"CONVERT(SL, 'System.String') LIKE '%{filterText}%' OR " +
               $"CONVERT([Đơn giá], 'System.String') LIKE '%{filterText}%' OR " +
               $"[Ghi chú] LIKE '%{filterText}%'";

        }

        // Hàm thay đổi Source của DataGripView
        private void cbHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbHang.Text = "Lệnh này được gọi trong Form_Load";
            string name_Hang = "Hang_" + cbHang.Text;
            if (dsTableTheoHang.Tables.Contains(name_Hang))
            {
                dgvHangHoa.DataSource = dsTableTheoHang.Tables[name_Hang];
                nameTableNow = name_Hang;
            }
            else
            {
                string lenhTruyVanSQL = 
                    @"
                SELECT 
                    SanPham.[Mã sản phẩm] AS Mã, 
                    SanPham.[Tên sản phẩm],  
                    SanPham.[Phân loại], 
                    dvtSanPham.[Đơn vị tính] AS ĐVT, 
                    dvtSanPham.[Số lượng] AS SL, 
                    Format(dvtSanPham.[Đơn giá], 'N0') AS [Đơn giá],
                    dvtSanPham.[Ghi chú] 
                FROM SanPham " +
                "INNER JOIN dvtSanPham ON " +
                    "SanPham.[Mã sản phẩm] = dvtSanPham.[Mã sản phẩm]" +
                $"WHERE SanPham.Hãng LIKE N'%{cbHang.Text}%'";

                SqlDataAdapter bienSQL_DataAdapter = new SqlDataAdapter(lenhTruyVanSQL, connOfHangHoa);
                bienSQL_DataAdapter.Fill(dsTableTheoHang, name_Hang);
                dgvHangHoa.DataSource = dsTableTheoHang.Tables[name_Hang];
                nameTableNow = name_Hang;
            }
        }


        // Hàm nhận DoubleClick trong DataGridView sau đó đưa con trỏ tới cột "Ghi chú"
        private void dgvHangHoa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int columnIndex = dgvHangHoa.Columns["Ghi chú"].Index;
                dgvHangHoa.CurrentCell = dgvHangHoa.Rows[e.RowIndex].Cells[columnIndex];
            }
        }

        // Hàm được gọi khi ô trong "Ghi chú" được sửa;
        // dùng để cập nhật lại cơ sở dữ liệu
        private void dgvHangHoa_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //int columnIndex_GhiChu = dgvHangHoa.Columns["Ghi chú"].Index;
            string noidungDaNhap = dgvHangHoa.Rows[e.RowIndex].Cells[dgvHangHoa.Columns["Ghi chú"].Index].Value.ToString();
            string MaSanPham = dgvHangHoa.Rows[e.RowIndex].Cells[dgvHangHoa.Columns["Mã"].Index].Value.ToString();
            string DonViTinh = dgvHangHoa.Rows[e.RowIndex].Cells[dgvHangHoa.Columns["ĐVT"].Index].Value.ToString();

            //UPDATE ten_bang
            //SET ten_cot1 = gia_tri1, ten_cot2 = gia_tri2, ...
            //WHERE dieu_kien;

            string LenhSQL = "UPDATE dvtSanPham SET [Ghi chú] = " +
                            $"'{noidungDaNhap}' " +
                            $"WHERE [Mã sản phẩm] = '{MaSanPham}' AND [Đơn vị tính] = '{DonViTinh}';";

            SqlCommand cmd = new SqlCommand(LenhSQL, connOfHangHoa);
            cmd.ExecuteNonQuery();
        }
    }
}
