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
    public partial class FormHoaDonLichSu : Form
    {
        public SqlConnection BienConnect;
        private int selectedRowIndex = -1;
        public FormHoaDonLichSu()
        {
           
            InitializeComponent();
            dgvHDLichSu.RowTemplate.Height = 40;
        }

        private void FormHoaDonLichSu_Load(object sender, EventArgs e)
        {
            LoadDuLieu();

            if(!dgvHDLichSu.Columns.Contains("btnChiTiet"))
            {
                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                btnColumn.Name = "btnChiTiet";
                btnColumn.HeaderText = "Chi tiết";
                btnColumn.Text = "Xem";
                btnColumn.UseColumnTextForButtonValue = true;
                dgvHDLichSu.Columns.Add(btnColumn);
            }
        }
        private void LoadDuLieu()
        {
            try
            {
                string lenhSQl = "select * from HoaDon";
                SqlDataAdapter adapter = new SqlDataAdapter(lenhSQl, BienConnect);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvHDLichSu.DataSource = dt;
                dgvHDLichSu.Columns["Tổng tiền"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử hóa đơn: "+ ex.Message);
            }
        }

        private void dgvHDLichSu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvHDLichSu.Columns[e.ColumnIndex].Name == "btnChiTiet" && e.RowIndex >= 0)
            {
                string maHD = dgvHDLichSu.Rows[e.RowIndex].Cells["Mã Hóa Đơn"].Value.ToString();
                FormChiTietHoaDon f = new FormChiTietHoaDon(maHD);
                f.BienConnect = BienConnect;
                f.ShowDialog();

            }
        }

        private void dgvHDLichSu_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0) // Kiểm tra chuột trái và hàng hợp lệ
            {
                if (dgvHDLichSu.Rows[e.RowIndex].Selected) // Chỉ hiển thị menu nếu cả hàng đã được chọn
                {
                    selectedRowIndex = e.RowIndex;
                    menu.Show(Cursor.Position); // Hiển thị menu
                }
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0)
            {
                string maHoaDon = dgvHDLichSu.Rows[selectedRowIndex].Cells["Mã hóa đơn"].Value.ToString();
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn " + maHoaDon + "?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                        string query = "DELETE FROM HoaDon WHERE [Mã hóa đơn] = @MaHD";

                        using (SqlCommand cmd = new SqlCommand(query, BienConnect))
                        {
                            cmd.Parameters.AddWithValue("@MaHD", maHoaDon);
                            cmd.ExecuteNonQuery(); // Thực thi xóa dữ liệu
                        }

                    dgvHDLichSu.Rows.RemoveAt(selectedRowIndex); // Xóa hàng khỏi DataGridView
                    selectedRowIndex = -1; // Reset giá trị hàng được chọn
                }
            }

        }
    }
}
