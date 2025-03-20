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
        public string LoaiHoaDon = "Nhập";
        //private PictureBox pictureBox;

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
            string newMaHD = "HD01"; // Mặc định nếu chưa có hóa đơn nào

            string query = "SELECT MAX(CAST(SUBSTRING([Mã hóa đơn], 4, LEN([Mã hóa đơn]) - 3) AS INT)) FROM HoaDon ";

            using (SqlCommand cmd = new SqlCommand(query, BienConnect))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int so))
                {
                    newMaHD = "HD" + (so + 1).ToString("D3");
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
                    string ghiChu = tb_GhiChu.Text;

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
                        MessageBox.Show("Vui lòng nhập thông tin sản phẩm", "Lỗi", MessageBoxButtons.OK);
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
                            decimal thanhTien = giaNhap * soLuong;
                            dgvHoaDonNhap.Rows[rowIndex].Cells[5].Value = giaNhap.ToString("N0"); // Giá nhập
                            dgvHoaDonNhap.Rows[rowIndex].Cells[6].Value = soLuong.ToString("N0"); // Số lượng
                            dgvHoaDonNhap.Rows[rowIndex].Cells[7].Value = thanhTien.ToString("N0"); // Thành tiền
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
        private void Luu_MouseLeave(object sender, EventArgs e)
        {
            Button buttonX = (Button)sender;
            buttonX.BackColor = Color.WhiteSmoke;
        }

        private void FormXX_MouseEnter(object sender, EventArgs e)
        {
            Button buttonX = (Button)sender;
            buttonX.BackColor = Color.FromArgb(247, 200, 115);
        }

        private void Huy_MouseLeave(object sender, EventArgs e)
        {
            Button buttonX = (Button)sender;
            buttonX.BackColor = Color.WhiteSmoke;
        }

        // in

        private void btn_InHD_Click(object sender, EventArgs e)
        {
            pddHoaDon.Document = pdHoaDon;
            pddHoaDon.ShowDialog();
        }

        private void DrawTable(Graphics g, int x, int y)
        {
            Font font = new Font("Arial", 12, FontStyle.Regular);
            Brush brush = Brushes.Black;
            Pen pen = new Pen(Color.Black, 2); // Bút vẽ bảng

            int rowHeight = 40;
            int colWidth1 = 50;  // STT
            int colWidth2 = 175; // Tên sản phẩm
            int colWidth3 = 100; // Hãng
            int colWidth4 = 150; // Phân loại
            int colWidth5 = 75; // Đơn vị tính
            int colWidth6 = 50; // Số lượng
            int colWidth7 = 100; // Giá nhập
            int colWidth8 = 150; // Thành tiền

            int tableWidth = colWidth1 + colWidth2 + colWidth3 + colWidth4 + colWidth5 + colWidth6 + colWidth7 + colWidth8;
            int tableHeight = rowHeight * 11; // 1 hàng tiêu đề + 10 hàng trống

            g.DrawRectangle(pen, x, y, tableWidth, tableHeight);

            g.DrawString("STT", font, brush, x + 10, y + 5);
            g.DrawString("Tên sản phẩm", font, brush, x + colWidth1 + 10, y + 5);
            g.DrawString("Hãng", font, brush, x + colWidth1 + colWidth2 + 10, y + 5);
            g.DrawString("Phân Loại", font, brush, x + colWidth1 + colWidth2 + colWidth3 + 10, y + 5);
            g.DrawString("ĐVT", font, brush, x + colWidth1 + colWidth2 + colWidth3 + colWidth4 + 10, y + 5);
            g.DrawString("SL", font, brush, x + colWidth1 + colWidth2 + colWidth3 + colWidth4 + colWidth5 + 10, y + 5);
            g.DrawString("Giá", font, brush, x + colWidth1 + colWidth2 + colWidth3 + colWidth4 + colWidth5 + colWidth6 + 10, y + 5);
            g.DrawString("Thành tiền", font, brush, x + colWidth1 + colWidth2 + colWidth3 + colWidth4 + colWidth5 + colWidth6 + colWidth7 + 10, y + 5);

            int[] columnPositions = { colWidth1, colWidth2, colWidth3, colWidth4, colWidth5, colWidth6, colWidth7, colWidth8 };
            int currentX = x;
            foreach (int width in columnPositions)
            {
                currentX += width;
                g.DrawLine(pen, currentX, y, currentX, y + tableHeight);
            }

            for (int i = 1; i <= 10; i++) // 10 hàng trống
            {
                g.DrawLine(pen, x, y + rowHeight * i, x + tableWidth, y + rowHeight * i);
            }

            int rowIndex = 1;
            int currentY = y + rowHeight;
            TongTien_public = 0;

            foreach (DataGridViewRow row in dgvHoaDonNhap.Rows)
            {
                if (!row.IsNewRow) // Bỏ qua hàng trống cuối cùng
                {
                    g.DrawString(rowIndex.ToString(), font, brush, x + 10, currentY + 5);
                    g.DrawString(row.Cells["cl_tenSP"].Value?.ToString() ?? "", font, brush, x + colWidth1 + 10, currentY + 5);
                    g.DrawString(row.Cells["cl_Hang"].Value?.ToString() ?? "", font, brush, x + colWidth1 + colWidth2 + 10, currentY + 5);
                    g.DrawString(row.Cells["cl_phanLoai"].Value?.ToString() ?? "", font, brush, x + colWidth1 + colWidth2 + colWidth3 + 10, currentY + 5);
                    g.DrawString(row.Cells["cl_DVT"].Value?.ToString() ?? "", font, brush, x + colWidth1 + colWidth2 + colWidth3 + colWidth4 + 10, currentY + 5);
                    g.DrawString(row.Cells["cl_SoLuong"].Value?.ToString() ?? "", font, brush, x + colWidth1 + colWidth2 + colWidth3 + colWidth4 + colWidth5 + 10, currentY + 5);
                    g.DrawString(Convert.ToDecimal(row.Cells["cl_Gia"].Value).ToString("N0"), font, brush, x + colWidth1 + colWidth2 + colWidth3 + colWidth4 + colWidth5 + colWidth6 + 10, currentY + 5);
                    g.DrawString(Convert.ToDecimal(row.Cells["cl_thanhTien"].Value).ToString("N0"), font, brush, x + colWidth1 + colWidth2 + colWidth3 + colWidth4 + colWidth5 + colWidth6 + colWidth7 + 10, currentY + 5);

                    string str_thanhTien = Convert.ToDecimal(row.Cells["cl_thanhTien"].Value).ToString("N0");
                    if (decimal.TryParse(str_thanhTien?.ToString(), out decimal thanhTien))
                    {
                        TongTien_public += thanhTien;
                    }
                    g.DrawLine(pen, x, currentY + rowHeight, x + tableWidth, currentY + rowHeight);
                    currentY += rowHeight;
                    rowIndex++;
                }
            }
        }


        private void pdHoaDon_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 150;
            int y = 20;
            string tencuahang = "Trung tâm phân phối và pha màu tự động";
            string diachi = "Địa chỉ: Huyện Thạch Thất, Hà Nội";
            string phone = "Điện thoại: 0198414235";
            string stk = "Số tài khoản: ";
            DateTime now = DateTime.Now;
            string today = $"Ngày {now.Day} Tháng {now.Month} Năm {now.Year} ";

            string name = "Hoá Đơn Nhập Hàng";

            //lay be rong cua giay in
            decimal w = pdHoaDon.DefaultPageSettings.PaperSize.Width;
            string imagePath = @"C:\Users\Pham Quang Anh\Downloads\logo.png";
            if (System.IO.File.Exists(imagePath)) // Kiểm tra xem file có tồn tại không
            {
                Image img = Image.FromFile(imagePath);
                e.Graphics.DrawImage(img, new Rectangle(20, y, 100, 100));
            }
            else
            {
                MessageBox.Show("Không tìm thấy ảnh!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //hienthi tencuahang
            e.Graphics.DrawString(
                tencuahang.ToUpper(),
                new Font("Courier New", 19, FontStyle.Bold),
                Brushes.Black, new Point(x, y)
                );

            //hienthi diachi
            e.Graphics.DrawString(
                diachi,
                new Font("Courier New", 12, FontStyle.Bold),
                Brushes.Black, new Point(x + 10, y + 30)
                );

            //phone
            e.Graphics.DrawString(
                phone,
                new Font("Courier New", 12, FontStyle.Bold),
                Brushes.Black, new Point(x + 10, y + 60)
                );

            //stk
            e.Graphics.DrawString(
                stk,
                new Font("Courier New", 12, FontStyle.Bold),
                Brushes.Black, new Point(x + 10, y + 90)
                );

            //kengang
            Pen pen = new Pen(Color.Black, 2);
            e.Graphics.DrawLine(pen, 20, y + 150, 800, y + 150);

            //-----------------------------------------------------------------------------------------
            //tenhoadon
            e.Graphics.DrawString(
                name.ToUpper(),
                new Font("Courier New", 20, FontStyle.Bold),
                Brushes.Black, new Point(x + 100, y + 180)
                );

            //thoigian inhoadon
            e.Graphics.DrawString(
                today,
                new Font("Courier New", 12, FontStyle.Italic),
                Brushes.Black, new Point(x + 110, y + 220)
                );

            //kengang
            e.Graphics.DrawLine(pen, 20, y + 280, 800, y + 280);

            //---------------------------------------------------------------------------------------------

            //tenkhachhang
            string tenKH = "Tên nhà cung cấp: " + cbb_NCC.Text;
            string diachiKH = "Địa chỉ: " + tb_DiaChi.Text;
            string sdtKH = "Số điện thoại: " + tb_SDT.Text;
            string tuoiKH = "Tuổi: " + tb_Tuoi.Text;
            string ghichu_HD = "Ghi chú: " + tb_GhiChu.Text;

            e.Graphics.DrawString(
                tenKH,
                new Font("Courier New", 12),
                Brushes.Black, new Point(x - 130, y + 310)
                );
            e.Graphics.DrawString(
                diachiKH,
                new Font("Courier New", 12),
                Brushes.Black, new Point(x - 130, y + 340)
                );
            e.Graphics.DrawString(
                sdtKH,
                new Font("Courier New", 12),
                Brushes.Black, new Point(x - 130, y + 370)
                );
            e.Graphics.DrawString(
                tuoiKH,
                new Font("Courier New", 12),
                Brushes.Black, new Point(x - 130, y + 400)
                );
            e.Graphics.DrawString(
                ghichu_HD,
                new Font("Courier New", 12),
                Brushes.Black, new Point(x - 130, y + 430)
                );
            DrawTable(e.Graphics, x - 140, y + 500);
            e.Graphics.DrawString(
                "Tổng tiền: " + TongTien_public.ToString("N0"),
                new Font("Courier New", 12),
                Brushes.Black, new Point(x + 300, y + 950)
                );

            //chu ky ncc
            e.Graphics.DrawString(
                "Nhà cung cấp",
                new Font("Courier New", 16, FontStyle.Bold),
                Brushes.Black, new Point(x - 120, y + 975)
                );
            e.Graphics.DrawString(
                "(Chữ ký(nếu có))",
                new Font("Courier New", 12),
                Brushes.Black, new Point(x - 120, y + 1000)
                );

            //chu ky chucuahang
            e.Graphics.DrawString(
                "Chủ cửa hàng",
                new Font("Courier New", 16, FontStyle.Bold),
                Brushes.Black, new Point(x + 450, y + 975)
                );
            e.Graphics.DrawString(
                "(Chữ ký(nếu có))",
                new Font("Courier New", 12),
                Brushes.Black, new Point(x + 450, y + 1000)
                );
        }
    }
}
