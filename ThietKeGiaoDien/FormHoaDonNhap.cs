using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThietKeGiaoDien
{
    public partial class FormHoaDonNhap : Form
    {
        public SqlConnection BienConnect;
        public string maDoiTac_public;
        public string maHD_public;
        public string maSP_public;
        public string DVT_public;
        public int SL_public = 0;
        public decimal TongTien_public = 0;
        public string LoaiHoaDon = "Hóa Đơn Nhập";

        public FormHoaDonNhap()
        {
            InitializeComponent();
        }
        private void FormNhap_Load(object sender, EventArgs e)
        {
            dgvHoaDonNhap.RowTemplate.Height = 40;
            dgvHoaDonNhap.Rows[0].Height = 40;

            try
            {
                string lenhSQL = "SELECT [Mã Đối Tác], [Tên Đối Tác], [Địa Chỉ], [Số Điện Thoại], [Tuổi] FROM DoiTac " +
                                 "WHERE [Phân loại] = N'Nhà cung cấp'";

                SqlDataAdapter adapter = new SqlDataAdapter(lenhSQL, BienConnect);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cbb_NCC.DataSource = dt;
                cbb_NCC.DisplayMember = "Tên Đối Tác";
                cbb_NCC.ValueMember = "Mã Đối Tác";

                cbb_NCC.Tag = dt;
                cbb_NCC.SelectedIndex = -1;
                tb_DiaChi.Text = "";
                tb_SDT.Text = "";
                tb_Tuoi.Text = "";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải khách hàng: " + ex.Message);
            }
        }
        public string TaoMaHoaDonMoi()
        {
            string newMaHD = "HDN01"; // Mặc định nếu chưa có hóa đơn nào

            string query = "SELECT MAX(CAST(SUBSTRING([Mã hóa đơn], 4, LEN([Mã hóa đơn]) - 3) AS INT)) FROM HoaDon ";

            using (SqlCommand cmd = new SqlCommand(query, BienConnect))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int so))
                {
                    newMaHD = "HDN" + (so + 1).ToString("D3");
                }
            }
            return newMaHD;
        }

        private string TaoMaDoiTac()
        {
            string newMaDoiTac = "DT01";

            try
            {
                string query = "SELECT MAX(CAST(SUBSTRING([Mã Đối Tác], 3, LEN([Mã Đối Tác]) - 2) AS INT)) FROM DoiTac";
                SqlCommand cmd = new SqlCommand(query, BienConnect);
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


        private void cbb_NCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_NCC.SelectedIndex != -1)
            {
                DataTable dt = (DataTable)cbb_NCC.Tag;
                DataRowView row = cbb_NCC.SelectedItem as DataRowView;
                tb_DiaChi.Text = row["Địa Chỉ"].ToString();
                tb_SDT.Text = row["Số Điện Thoại"].ToString();
                tb_Tuoi.Text = row["Tuổi"].ToString();
            }
            else
            {
                tb_DiaChi.Text = "";
                tb_SDT.Text = "";
                tb_Tuoi.Text = "";
            }
        }

        private bool LuuDoiTac()
        {
            string tenDoiTac = cbb_NCC.Text.Trim();
            string tuoi = tb_Tuoi.Text.Trim();
            string DiaChi = tb_DiaChi.Text.Trim();
            string Sdt = tb_SDT.Text.Trim();
            if (string.IsNullOrEmpty(tenDoiTac))
            {
                MessageBox.Show("Vui lòng nhập tên đối tác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra nếu có ô nào bị bỏ trống
            if (string.IsNullOrEmpty(tenDoiTac) || string.IsNullOrEmpty(tuoi) ||
                string.IsNullOrEmpty(DiaChi) || string.IsNullOrEmpty(Sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                string maDoiTac = cbb_NCC.SelectedValue?.ToString();
                maDoiTac_public = maDoiTac;
                if (string.IsNullOrEmpty(maDoiTac))
                {
                    MessageBox.Show("Không thể tạo mã đối tác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                // Kiểm tra khách hàng đã tồn tại chưa
                string checkQuery = "SELECT COUNT(*) FROM DoiTac WHERE [Mã đối tác] = @maDoiTac";
                SqlCommand checkCmd = new SqlCommand(checkQuery, BienConnect);
                checkCmd.Parameters.AddWithValue("@maDoiTac", maDoiTac);

                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0) //neu khach hang chua ton tai
                {
                    string lenhSQL = "INSERT INTO DoiTac ([Mã đối tác], [Tên đối tác], [Phân loại], [Tuổi], [Địa chỉ], [Số điện thoại])" +
                             " VALUES (@maDoiTac, @tenDoiTac, N'Nhà cung cấp', @tuoi, @DiaChi, @Sdt)";
                    //thi add vao csdl
                    maDoiTac = TaoMaDoiTac();
                    SqlCommand cmd = new SqlCommand(lenhSQL, BienConnect);

                    cmd.Parameters.AddWithValue("@maDoiTac", maDoiTac);
                    cmd.Parameters.AddWithValue("@tenDoiTac", tenDoiTac);
                    cmd.Parameters.AddWithValue("@tuoi", tuoi);
                    cmd.Parameters.AddWithValue("@DiaChi", DiaChi);
                    cmd.Parameters.AddWithValue("@Sdt", Sdt);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm đối tác thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    maDoiTac_public = maDoiTac;
                }
                else
                {
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm đối tác: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool TonTaiMaSP(string maSP)
        {
            if (string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Mã sản phẩm không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string query = "SELECT COUNT(*) FROM SanPham WHERE [Mã sản phẩm] = @MaSP";
            using (SqlCommand cmd = new SqlCommand(query, BienConnect))
            {
                cmd.Parameters.AddWithValue("@MaSP", maSP);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private bool LuuSanPham()
        {
            bool isValid = true;
            foreach (DataGridViewRow row in dgvHoaDonNhap.Rows)
            {
                
                if (!row.IsNewRow)//bo qua hang rong cuoi cung
                {             
                    string maSP = row.Cells["cl_masp"].Value?.ToString().Trim();    
                    maSP_public = maSP;
                    string tenSP = row.Cells["cl_tenSP"].Value?.ToString().Trim();
                    string hang = row.Cells["cl_Hang"].Value?.ToString().Trim();
                    string phanLoai = row.Cells["cl_phanLoai"].Value?.ToString().Trim();
                    string donViTinh = row.Cells["cl_DVT"].Value?.ToString().Trim();
                    string str_giaNhap = row.Cells["cl_Gia"].Value?.ToString().Trim();
                    string str_soLuong = row.Cells["cl_SoLuong"].Value?.ToString().Trim();
                    string str_thanhTien = row.Cells["cl_thanhTien"].Value?.ToString().Trim();
                    string ghiChu = row.Cells["cl_ghiChu"].Value?.ToString().Trim() ?? " "; // Ghi chú có thể để trống
                    MessageBox.Show(maSP);
                    // Kiểm tra từng ô, nếu rỗng thì đặt dấu "!" vào ô đó
                    if (string.IsNullOrEmpty(tenSP))
                    {
                        row.Cells["cl_tenSP"].Value = "!";
                        isValid = false;
                    }
                    if (string.IsNullOrEmpty(hang))
                    {
                        row.Cells["cl_Hang"].Value = "!";
                        isValid = false;
                    }
                    if (string.IsNullOrEmpty(phanLoai))
                    {
                        row.Cells["cl_phanLoai"].Value = "!";
                        isValid = false;
                    }
                    if (string.IsNullOrEmpty(donViTinh))
                    {
                        row.Cells["cl_DVT"].Value = "!";
                        isValid = false;
                    }
                    if (!decimal.TryParse(str_giaNhap?.ToString(), out decimal giaNhap))
                    {
                        row.Cells["cl_Gia"].Value = "!";
                        isValid = false;
                    }
                    if (!int.TryParse(str_soLuong?.ToString(), out int soLuong))
                    {
                        row.Cells["cl_SoLuong"].Value = "!";
                        isValid = false;
                    }
                    if (!decimal.TryParse(str_thanhTien?.ToString(), out decimal thanhTien))
                    {
                        isValid = false;
                    }

                    TongTien_public = thanhTien;
                    DVT_public = donViTinh;
                    SL_public = soLuong;
                    // Nếu có lỗi thì dừng lại, không lưu vào CSDL
                    if (!isValid)
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin! Các ô bị thiếu đã được đánh dấu '!'.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    try
                    {
                            if (TonTaiMaSP(maSP))
                            {
                                //dvtsanpham
                                string checkSQL2 = "SELECT COUNT(*) FROM dvtSanPham WHERE [Mã sản phẩm] = @maSP AND [Đơn vị tính] = @donViTinh";
                                SqlCommand sqlCommand2 = new SqlCommand(checkSQL2, BienConnect);
                                sqlCommand2.Parameters.AddWithValue("@maSP", maSP_public);
                                sqlCommand2.Parameters.AddWithValue("@donViTinh", DVT_public);

                                int count = (int)sqlCommand2.ExecuteScalar();

                                if (count == 0) //neu chua co, them moi
                                {
                                    string insertQuery = "INSERT INTO dvtSanPham ([Mã sản phẩm], [Đơn vị tính], [Số lượng], [Giá nhập trung bình], [Đơn giá], [Ghi chú]) " +
                                            "VALUES (@MaSP, @DVT, @soLuong, @giaNhap, @thanhTien, @ghiChu)";

                                    SqlCommand cmd = new SqlCommand(insertQuery, BienConnect);
                                    cmd.Parameters.AddWithValue("@MaSP", maSP_public);
                                    cmd.Parameters.AddWithValue("@DVT", DVT_public);
                                    cmd.Parameters.AddWithValue("@soLuong", soLuong);
                                    cmd.Parameters.AddWithValue("@giaNhap", giaNhap);
                                    cmd.Parameters.AddWithValue("@thanhTien", thanhTien);
                                    cmd.Parameters.AddWithValue("@ghiChu", ghiChu);

                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Thêm dvt sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    string updateQuery = @"UPDATE dvtSanPham 
                                    SET [Giá nhập trung bình] = ([Giá nhập trung bình]*[Số lượng] + @soLuong*@GiaNhapMoi) / ([Số lượng] + @soLuong), 
                                    [Số lượng] = [Số lượng] + @soLuong, 
                                    [Đơn giá] = [Đơn giá] + @thanhTien
                                    WHERE [Mã sản phẩm] = @maSP AND [Đơn vị tính] = @donViTinh";
                                    SqlCommand cmd = new SqlCommand(updateQuery, BienConnect);

                                    cmd.Parameters.AddWithValue("@MaSP", maSP_public);
                                    cmd.Parameters.AddWithValue("@donViTinh", DVT_public);
                                    cmd.Parameters.AddWithValue("@soLuong", soLuong);
                                    cmd.Parameters.AddWithValue("@GiaNhapMoi", giaNhap);
                                    cmd.Parameters.AddWithValue("@thanhTien", thanhTien);
                                    cmd.Parameters.AddWithValue("@ghiChu", ghiChu);


                                cmd.ExecuteNonQuery();
                                    MessageBox.Show("Thêm sản dvt2phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }

                            }
                            else
                            {
                            string insertQuery = "INSERT INTO SanPham ([Mã sản phẩm], [Tên sản phẩm], [Hãng], [Phân Loại]) " +
                                                 "VALUES (@MaSP, @TenSP, @hang, @phanLoai)";
                            SqlCommand cmd = new SqlCommand(insertQuery, BienConnect);
                            cmd.Parameters.AddWithValue("@MaSP", maSP_public);
                            cmd.Parameters.AddWithValue("@TenSP", tenSP);
                            cmd.Parameters.AddWithValue("@hang", hang);
                            cmd.Parameters.AddWithValue("@phanLoai", phanLoai);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //them dvtsanpham
                            string insertQuery1 = "INSERT INTO dvtSanPham ([Mã sản phẩm], [Đơn vị tính], [Số lượng], [Giá nhập trung bình], [Đơn giá], [Ghi chú]) " +
                                                    "VALUES (@MaSP, @DVT, @soLuong, @giaNhap, @thanhTien, @ghiChu)";

                            SqlCommand cmd1 = new SqlCommand(insertQuery1, BienConnect);
                            cmd1.Parameters.AddWithValue("@MaSP", maSP_public);
                            cmd1.Parameters.AddWithValue("@DVT", DVT_public);
                            cmd1.Parameters.AddWithValue("@soLuong", soLuong);
                            cmd1.Parameters.AddWithValue("@giaNhap", giaNhap);
                            cmd1.Parameters.AddWithValue("@thanhTien", thanhTien);
                            cmd1.Parameters.AddWithValue("@ghiChu", ghiChu);

                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Thêm dvt sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Lỗi " + ex.Message);
                        return false;
                    }
                }
            }
            return true;
        }

        private void LuuHoaDon()
        {
            string maHD = TaoMaHoaDonMoi();
            //luu hoa don
            try
            {
                string lenhSQl = "INSERT INTO HoaDon ([Mã hóa đơn], [Mã đối tác], [Mã người bán], [Ngày tạo], [Loại hóa đơn], [Tổng tiền], [Ghi chú]) " +
                       "VALUES (@MaHD, @maDoiTac, @MaNguoiBan, GETDATE(), @LoaiHoaDon, @TongTien, @GhiChu)";
                SqlCommand cmd = new SqlCommand(lenhSQl, BienConnect);

                cmd.Parameters.AddWithValue("@MaHD", maHD);
                cmd.Parameters.AddWithValue("@maDoiTac", maDoiTac_public);
                cmd.Parameters.AddWithValue("@MaNguoiBan", CurrentUser.TaiKhoan);
                cmd.Parameters.AddWithValue("@LoaiHoaDon", LoaiHoaDon);
                cmd.Parameters.AddWithValue("@TongTien", TongTien_public);
                cmd.Parameters.AddWithValue("@GhiChu", " ");

                cmd.ExecuteNonQuery();
                MessageBox.Show("Lưu hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.Message);
            }


            //luu chi tiet hoa don
            try
            {
                foreach (DataGridViewRow row in dgvHoaDonNhap.Rows)
                {
                    if (row.Cells["cl_tenSP"].Value == null) continue; // Bỏ qua dòng trống


                    string maSP = row.Cells["cl_maSP"].Value.ToString();

                    if(maSP == null)
                    {
                        MessageBox.Show("Vui long nhập thông tin sản phẩm", "Lỗi", MessageBoxButtons.OK);
                    }
                    string dvt = row.Cells["cl_DVT"].Value.ToString();
                    int soLuong = Convert.ToInt32(row.Cells["cl_soLuong"].Value);
                    decimal donGia = Convert.ToDecimal(row.Cells["cl_Gia"].Value);

                    string lenhSQL = "INSERT INTO ChiTietHoaDon ([Mã hóa đơn], [Mã sản phẩm], [Đơn vị tính], [Số lượng], [Đơn giá]) " +
                                 "VALUES (@MaHD, @MaSP, @DVT, @SoLuong, @DonGia)";
                    SqlCommand cmd = new SqlCommand(lenhSQL, BienConnect);

                    cmd.Parameters.AddWithValue("@MaHD", maHD);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    cmd.Parameters.AddWithValue("@DVT", dvt);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@DonGia", donGia);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lưu chi tiết hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu chi tiết hóa đơn: " + ex.Message);
            }
        }
        private void button_Luu_Click(object sender, EventArgs e)
        {
            if (!LuuDoiTac())
            {
                return;
            }
            if(!LuuSanPham())
            {
                return;
            }
            LuuHoaDon();
            FormNhap_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn hủy hóa đơn?", "Xác Nhận",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cbb_NCC.SelectedIndex = -1;
                tb_DiaChi.Text = "";
                tb_SDT.Text = "";
            }
            dgvHoaDonNhap.Rows.Clear();
        }

        //event khi roi khoi cell (ap dung cho soluong va dongia)
        private void dgvHoaDonNhap_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 || e.ColumnIndex == 5)
            {
                int columnIndex = e.ColumnIndex;
                int rowIndex = e.RowIndex;

                //MessageBox.Show(Convert.ToString(columnIndex));
                //row[rowIndex] = hang vua chinh sua
                //Cells[columnIndex] = cot vua chinh sua

                if (dgvHoaDonNhap.Rows[rowIndex].Cells[columnIndex].Value != null)
                {
                    string cellValue = dgvHoaDonNhap.Rows[rowIndex].Cells[columnIndex].Value.ToString();
                    if (!int.TryParse(cellValue, out _)) // Kiểm tra có phải số không
                    {
                        MessageBox.Show("Chỉ được nhập số!", "Lỗi nhập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvHoaDonNhap.Rows[rowIndex].Cells[columnIndex].Value = "0"; // Reset về 0 nếu nhập sai
                        return;
                    }
                    else
                    {
                        if (decimal.TryParse(dgvHoaDonNhap.Rows[rowIndex].Cells[5].Value?.ToString(), out decimal giaNhap) &&
                        int.TryParse(dgvHoaDonNhap.Rows[rowIndex].Cells[6].Value?.ToString(), out int soLuong))
                        {
                            dgvHoaDonNhap.Rows[rowIndex].Cells[7].Value = (giaNhap * soLuong).ToString();
                        }
                    }
                }
            }
        }

        //khi bat dau chinh sua cell
        private void dgvHoaDonNhap_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox tb)
            {
                tb.Font = new Font("Arial", 18, FontStyle.Regular); // Đặt font chữ 18
            }
        }

        private void dgvHoaDonNhap_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
    }
}
