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
    public partial class FormTaiKhoan : Form
    {
        public SqlConnection connOfTaiKhoan;
        public FormTaiKhoan()
        {
            InitializeComponent();
        }

        private void FormTaiKhoan_Load(object sender, EventArgs e)
        {
            string lenhSQl = "select * from NguoiDung";
            SqlDataAdapter ada = new SqlDataAdapter(lenhSQl, connOfTaiKhoan);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dgvTaiKhoan.DataSource = dt;
        }

        private void ClearFields()
        {
            tb_GhiChu.Text = "";
            tb_mk.Text = "";
            tb_tenND.Text = "";
            tb_tk.Text = "";
            tb_tk.Enabled = true;
            btn_them.Enabled = true;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string tenNguoiDung = tb_tenND.Text.Trim();
            string taiKhoan = tb_tk.Text.Trim();
            string matKhau = tb_mk.Text.Trim();
            string ghiChu = tb_GhiChu.Text.Trim();

            if (string.IsNullOrEmpty(tenNguoiDung) || string.IsNullOrEmpty(tenNguoiDung)
                || string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string lenhSQL = "INSERT INTO NguoiDung([Tài khoản], [Mật khẩu], [Tên người dùng], [Ghi chú])" +
                "VALUES(@taiKhoan, @matKhau, @tenNguoiDung, @ghiChu)";
            SqlCommand cmd = new SqlCommand(lenhSQL, connOfTaiKhoan);
            cmd.Parameters.AddWithValue("@taiKhoan", taiKhoan);
            cmd.Parameters.AddWithValue("@matKhau", matKhau);
            cmd.Parameters.AddWithValue("@tenNguoiDung", tenNguoiDung);
            cmd.Parameters.AddWithValue("@ghiChu", ghiChu);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormTaiKhoan_Load(sender, e);
                ClearFields();
            }
            else
            {
                MessageBox.Show("Không thể sửa tài khoản, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string tenNguoiDung = tb_tenND.Text.Trim();
            string taiKhoan = tb_tk.Text.Trim();
            string matKhau = tb_mk.Text.Trim();
            string ghiChu = tb_GhiChu.Text.Trim();

            if(string.IsNullOrEmpty(tenNguoiDung) || string.IsNullOrEmpty(tenNguoiDung)
                || string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng chọn người dùng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string lenhSQL = "UPDATE NguoiDung Set [Mật khẩu] = @matKhau," +
                "[Tên người dùng] = @tenNguoiDung, [Ghi chú] = @ghiChu where [Tài khoản] = @taiKhoan" ;
            SqlCommand cmd = new SqlCommand(lenhSQL, connOfTaiKhoan);
            cmd.Parameters.AddWithValue("@taiKhoan", taiKhoan);
            cmd.Parameters.AddWithValue("@matKhau", matKhau);
            cmd.Parameters.AddWithValue("@tenNguoiDung", tenNguoiDung);
            cmd.Parameters.AddWithValue("@ghiChu", ghiChu);
            cmd.ExecuteNonQuery();
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Sửa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormTaiKhoan_Load(sender, e);
                ClearFields();
            }
            else
            {
                MessageBox.Show("Không thể sửa tài khoản, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string taiKhoan = tb_tk.Text.Trim();
            tb_tk.Enabled = false;
            if (string.IsNullOrEmpty(taiKhoan))
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string lenhSQL = "DELETE FROM NguoiDung WHERE [Tài khoản] = @taiKhoan";
                SqlCommand cmd = new SqlCommand(lenhSQL, connOfTaiKhoan);
                cmd.Parameters.AddWithValue("@taiKhoan", taiKhoan);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormTaiKhoan_Load(sender, e);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Không thể xóa tài khoản, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTaiKhoan.Rows[e.RowIndex];
                tb_tk.Text = row.Cells["Tài khoản"].Value.ToString();
                tb_mk.Text = row.Cells["Mật khẩu"].Value.ToString();
                tb_tenND.Text = row.Cells["Tên người dùng"].Value.ToString();
                tb_GhiChu.Text = row.Cells["Ghi chú"].Value.ToString();
                btn_them.Enabled = false; tb_tk.Enabled = false;
            }
        }
    }
}
