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
    public partial class FormDoiTac_KH : Form
    {
        public SqlConnection bienconnect;
        public string maDT;
        public FormDoiTac_KH()
        {
            InitializeComponent();
        }

        private void FormDoiTac_KH_Load(object sender, EventArgs e)
        {
            string lenhSQL = "select * from DoiTac where [Phân loại] = N'Khách Hàng'";
            SqlDataAdapter ada = new SqlDataAdapter(lenhSQL, bienconnect);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dgvKhachHang.DataSource = dt;
        }
        private string GenerateNewMaDoiTac()
        {
            string newMaDoiTac = "DT01";

            try
            {
                if (bienconnect.State == ConnectionState.Closed)
                    bienconnect.Open();

                string query = "SELECT MAX(CAST(SUBSTRING([Mã Đối Tác], 3, LEN([Mã Đối Tác]) - 2) AS INT)) FROM DoiTac";
                SqlCommand cmd = new SqlCommand(query, bienconnect);
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    int maxNumber = Convert.ToInt32(result);
                    maxNumber++;
                    newMaDoiTac = "DT" + maxNumber.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã đối tác: " + ex.Message);
            }

            return newMaDoiTac;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tenDoiTac = tb_TenDT.Text.Trim();
            string tuoi = tb_Tuoi.Text.Trim();
            string DiaChi = tb_DiaChi.Text.Trim();
            string Sdt = tb_Sdt.Text.Trim();
            string ghiChu = tb_GhiChu.Text.Trim();

            // Kiểm tra nếu có ô nào bị bỏ trống
            if (string.IsNullOrEmpty(tenDoiTac) || string.IsNullOrEmpty(tuoi) ||
                string.IsNullOrEmpty(DiaChi) || string.IsNullOrEmpty(Sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDoiTac = GenerateNewMaDoiTac();
            string lenhSQL = "INSERT INTO DoiTac ([Mã đối tác], [Tên đối tác], [Phân loại], [Tuổi], [Địa chỉ], [Số điện thoại], [Ghi chú])" +
                             " VALUES (@maDoiTac, @tenDoiTac, N'Khách Hàng', @tuoi, @DiaChi, @Sdt, @ghiChu)";

            try
            {
                SqlCommand cmd = new SqlCommand(lenhSQL, bienconnect);
                cmd.Parameters.AddWithValue("@maDoiTac", maDoiTac);
                cmd.Parameters.AddWithValue("@tenDoiTac", tenDoiTac);
                cmd.Parameters.AddWithValue("@tuoi", tuoi);
                cmd.Parameters.AddWithValue("@DiaChi", DiaChi);
                cmd.Parameters.AddWithValue("@Sdt", Sdt);
                cmd.Parameters.AddWithValue("@ghiChu", ghiChu);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm đối tác thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormDoiTac_KH_Load(sender, e);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Không thể thêm đối tác, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm đối tác: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ClearFields()
        {
            maDT = "";
            tb_TenDT.Text = "";
            tb_Tuoi.Text = "";
            tb_DiaChi.Text = "";
            tb_Sdt.Text = "";
            tb_GhiChu.Text = "";

            btn_them.Enabled = true; 
        }

        private void dgvKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                tb_TenDT.Text = row.Cells["Tên đối tác"].Value.ToString();
                tb_Tuoi.Text = row.Cells["Tuổi"].Value.ToString();
                tb_DiaChi.Text = row.Cells["Địa chỉ"].Value.ToString();
                tb_Sdt.Text = row.Cells["Số điện thoại"].Value.ToString();
                tb_GhiChu.Text = row.Cells["Ghi chú"].Value.ToString();
                maDT = row.Cells["Mã đối tác"].Value.ToString();
                btn_them.Enabled = false;
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maDoiTac = maDT;
            string tenDoiTac = tb_TenDT.Text.Trim();
            string tuoi = tb_Tuoi.Text.Trim();
            string diaChi = tb_DiaChi.Text.Trim();
            string sdt = tb_Sdt.Text.Trim();
            string ghiChu = tb_GhiChu.Text.Trim();

            if (string.IsNullOrEmpty(maDoiTac))
            {
                MessageBox.Show("Vui lòng chọn đối tác cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string lenhSQL = "UPDATE DoiTac SET [Tên đối tác] = @tenDoiTac, [Tuổi] = @tuoi, " +
                                 "[Địa chỉ] = @diaChi, [Số điện thoại] = @sdt, [Ghi chú] = @ghiChu Where [Mã đối tác] = @maDoiTac";

                SqlCommand cmd = new SqlCommand(lenhSQL, bienconnect);
                cmd.Parameters.AddWithValue("@maDoiTac", maDoiTac);
                cmd.Parameters.AddWithValue("@tenDoiTac", tenDoiTac);
                cmd.Parameters.AddWithValue("@tuoi", tuoi);
                cmd.Parameters.AddWithValue("@diaChi", diaChi);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@ghiChu", ghiChu);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormDoiTac_KH_Load(sender, e);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu nào được cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string maDoiTac = maDT;
            if (string.IsNullOrEmpty(maDoiTac))
            {
                MessageBox.Show("Vui lòng chọn đối tác cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đối tác này?", "Xác nhận xóa",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            try
            {
                string lenhSQL = "DELETE FROM DoiTac WHERE [Mã đối tác] = @maDoiTac";
                SqlCommand cmd = new SqlCommand(lenhSQL, bienconnect);
                cmd.Parameters.AddWithValue("@maDoiTac", maDoiTac);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Xóa đối tác thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FormDoiTac_KH_Load(sender, e);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
