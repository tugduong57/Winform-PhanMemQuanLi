using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class FormHoaDonXuat : Form
    {
        public SqlConnection BienConnect;
        public string LoaiHoaDon = "Hóa Đơn Bán";
        public string maDT_public;
        public string maSP_public;
        public string DVT_public;
        public string Ghichu_public;
        public decimal SL_Ban = 0;
        public decimal TongTien_public = 0;

        public FormHoaDonXuat()
        {
            InitializeComponent();
        }
        private void FormXuat_Load(object sender, EventArgs e)
        {
            dgvHoaDonXuat.RowTemplate.Height = 40;
            dgvHoaDonXuat.Rows[0].Height = 40;
            //LoadProductData();

            // Load dữ liệu khách hàng
            try
            {
                string lenhSQL = "SELECT [Mã Đối Tác], [Tên Đối Tác], [Địa Chỉ], [Số Điện Thoại], [Tuổi] FROM DoiTac " +
                                 "WHERE [Phân loại] = 'Khách Hàng'";

                SqlDataAdapter adapter = new SqlDataAdapter(lenhSQL, BienConnect);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cbb_KhachHang.DataSource = dt;
                cbb_KhachHang.DisplayMember = "Tên Đối Tác";
                cbb_KhachHang.ValueMember = "Mã Đối Tác";

                // Đặt mặc định là không chọn gì
                cbb_KhachHang.SelectedIndex = -1;
                tb_DiaChi.Text = "";
                tb_SDT.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải khách hàng: " + ex.Message);
            }

            // Load dữ liệu sản phẩm
            try
            {
                DataTable dt = new DataTable();
                string lenhSQL = @"
                                    SELECT sp.[Mã sản phẩm], sp.[Tên sản phẩm], sp.[Phân loại], 
                                    sp.[Hãng], SUM(dvt.[Số lượng]) AS [Tổng số lượng], 
                                    AVG(dvt.[Giá nhập trung bình]) AS [Giá nhập TB]
                                    FROM SanPham sp 
                                    JOIN dvtSanPham dvt ON sp.[Mã sản phẩm] = dvt.[Mã sản phẩm]
                                    GROUP BY sp.[Mã sản phẩm], sp.[Tên sản phẩm], sp.[Phân loại], sp.[Hãng];";
                SqlDataAdapter adapter = new SqlDataAdapter(lenhSQL, BienConnect);
                adapter.Fill(dt);
                DataGridViewComboBoxColumn cbb_sp = (DataGridViewComboBoxColumn)dgvHoaDonXuat.Columns["cl_tenSP"];
                cbb_sp.DataSource = dt;
                cbb_sp.DisplayMember = "Tên sản phẩm";
                cbb_sp.ValueMember = "Mã sản phẩm";

                dgvHoaDonXuat.Tag = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu sản phẩm: " + ex.Message);
            }
        }

        private string TaoMaDoiTac()
        {
            string newMaDoiTac = "DT1";
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
        public string TaoMaHoaDonMoi()
        {
            string newMaHD = "HDX1"; // Mặc định nếu chưa có hóa đơn nào

            string query = "SELECT MAX(CAST(SUBSTRING([Mã hóa đơn], 4, LEN([Mã hóa đơn]) - 3) AS INT)) FROM HoaDon ";

            using (SqlCommand cmd = new SqlCommand(query, BienConnect))
            {
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    int so = Convert.ToInt32(result) + 1;
                    newMaHD = "HDX" + so.ToString("D3");
                }
            }
            return newMaHD;
        }
        private void cbb_KhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_KhachHang.SelectedIndex != -1)
            {
                DataTable dt = (DataTable)cbb_KhachHang.Tag;
                DataRowView row = cbb_KhachHang.SelectedItem as DataRowView;
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
            string tenDoiTac = cbb_KhachHang.Text.Trim();
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

            string lenhSQL = "INSERT INTO DoiTac ([Mã đối tác], [Tên đối tác], [Phân loại], [Tuổi], [Địa chỉ], [Số điện thoại])" +
                             " VALUES (@maDoiTac, @tenDoiTac, N'Khách Hàng', @tuoi, @DiaChi, @Sdt)";
            try
            {
                //MessageBox.Show("Ma khach: " + cbb_KhachHang.SelectedValue.ToString());

                // Kiểm tra khách hàng đã tồn tại chưa
                string checkQuery = "SELECT COUNT(*) FROM DoiTac WHERE [Mã đối tác] = @maDoiTac";
                SqlCommand checkCmd = new SqlCommand(checkQuery, BienConnect);
                if (cbb_KhachHang.SelectedValue == null)
                {
                    maDT_public = TaoMaDoiTac();
                }
                else
                {
                maDT_public = cbb_KhachHang.SelectedValue.ToString();

                }
                checkCmd.Parameters.AddWithValue("@maDoiTac", maDT_public);

                int count = (int)checkCmd.ExecuteScalar();
                if (count <= 0) //neu khach hang chua ton tai
                {
                    //thi add vao csdl
                    string maDoiTac = TaoMaDoiTac();
                    SqlCommand cmd = new SqlCommand(lenhSQL, BienConnect);
                    cmd.Parameters.AddWithValue("@maDoiTac", maDoiTac);
                    maDT_public = maDoiTac;
                    cmd.Parameters.AddWithValue("@tenDoiTac", tenDoiTac);
                    cmd.Parameters.AddWithValue("@tuoi", tuoi);
                    cmd.Parameters.AddWithValue("@DiaChi", DiaChi);
                    cmd.Parameters.AddWithValue("@Sdt", Sdt);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm đối tác thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        MessageBox.Show("Không thể thêm đối tác, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm đối tác: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void LuuHoaDon()
        {
            string maHD = TaoMaHoaDonMoi();
            decimal TongTien = 0;
            //luu hoa don
            try
            {
                foreach (DataGridViewRow row in dgvHoaDonXuat.Rows)
                {
                    if (row.Cells["cl_tenSP"].Value == null) continue;

                    int soLuong = Convert.ToInt32(row.Cells["cl_soLuong"].Value);
                    decimal donGia = Convert.ToDecimal(row.Cells["cl_Gia"].Value);
                    TongTien += soLuong * donGia;
                }
                string lenhSQl = "INSERT INTO HoaDon ([Mã hóa đơn], [Mã đối tác], [Mã người bán], [Ngày tạo], [Loại hóa đơn], [Tổng tiền]) " +
                       "VALUES (@MaHD, @maDoiTac, @MaNguoiBan, GETDATE(), @LoaiHoaDon, @TongTien)";
                SqlCommand cmd = new SqlCommand(lenhSQl, BienConnect);

                cmd.Parameters.AddWithValue("@MaHD", maHD);
                MessageBox.Show(maDT_public);
                cmd.Parameters.AddWithValue("@maDoiTac", maDT_public);
                cmd.Parameters.AddWithValue("@MaNguoiBan", CurrentUser.TaiKhoan);
                cmd.Parameters.AddWithValue("@LoaiHoaDon", LoaiHoaDon);
                cmd.Parameters.AddWithValue("@TongTien", TongTien);

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
                foreach (DataGridViewRow row in dgvHoaDonXuat.Rows)
                {
                    if (row.Cells["cl_tenSP"].Value == null) continue; // Bỏ qua dòng trống

                    string maSP = row.Cells["cl_tenSP"].Value.ToString();
                    string dvt = row.Cells["cl_DVT"].Value.ToString();
                    int soLuong = Convert.ToInt32(row.Cells["cl_soLuong"].Value);
                    decimal donGia = Convert.ToDecimal(row.Cells["cl_Gia"].Value);

                    string lenhSQLCTHD = "INSERT INTO ChiTietHoaDon ([Mã hóa đơn], [Mã sản phẩm], [Đơn vị tính], [Số lượng], [Đơn giá]) " +
                                         "VALUES (@MaHD, @MaSP, @DVT, @SoLuong, @DonGia)";
                    SqlCommand cmdCTHD = new SqlCommand(lenhSQLCTHD, BienConnect);

                    cmdCTHD.Parameters.AddWithValue("@MaHD", maHD);
                    cmdCTHD.Parameters.AddWithValue("@MaSP", maSP);
                    cmdCTHD.Parameters.AddWithValue("@DVT", dvt);
                    cmdCTHD.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmdCTHD.Parameters.AddWithValue("@DonGia", donGia);

                    cmdCTHD.ExecuteNonQuery();

                    //updat số lượng tồn kho trong dvtSanPham
                    string updateSQL = "UPDATE dvtSanPham SET [Số lượng] = [Số lượng] - @SoLuong WHERE [Mã sản phẩm] = @MaSP AND [Đơn vị tính] = @DVT";
                    SqlCommand updateCmd = new SqlCommand(updateSQL, BienConnect);

                    updateCmd.Parameters.AddWithValue("@MaSP", maSP);
                    updateCmd.Parameters.AddWithValue("@DVT", dvt);
                    updateCmd.Parameters.AddWithValue("@SoLuong", soLuong);

                    updateCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu chi tiết hóa đơn: " + ex.Message);
            }
        }

        private void button_Them_Click(object sender, EventArgs e)
        {
            if (!LuuDoiTac())
            {
                return;
            }
            LuuHoaDon();
            cbb_KhachHang.SelectedIndex = -1;
            tb_DiaChi.Text = "";
            tb_SDT.Text = "";
            tb_Tuoi.Text = "";
            tb_GhiChu.Text = " ";
            dgvHoaDonXuat.Rows.Clear();
        }


        private void button_Huy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn hủy hóa đơn?", "Xác Nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cbb_KhachHang.SelectedIndex = -1;
                tb_DiaChi.Text = "";
                tb_SDT.Text = "";
                tb_Tuoi.Text = "";
                tb_GhiChu.Text = " ";
                dgvHoaDonXuat.Rows.Clear();
            }
        }

        private void dgvHoaDonXuat_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columnName = dgvHoaDonXuat.Columns[e.ColumnIndex].Name;

                // Khi chọn sản phẩm, cập nhật hãng, phân loại và đơn vị tính
                if (columnName == "cl_tenSP")
                {
                    DataTable dt = (DataTable)dgvHoaDonXuat.Tag;
                    string maSP = dgvHoaDonXuat.Rows[e.RowIndex].Cells["cl_tenSP"].Value?.ToString();
                    maSP_public = maSP;
                    DataRow[] rows = dt.Select($"[Mã sản phẩm] = '{maSP}'");

                    if (rows.Length > 0)
                    {
                        dgvHoaDonXuat.Rows[e.RowIndex].Cells["cl_Hang"].Value = rows[0]["Hãng"];
                        dgvHoaDonXuat.Rows[e.RowIndex].Cells["cl_phanLoai"].Value = rows[0]["Phân loại"];

                        //LoadDVT để cập nhật danh sách đơn vị tính và giá
                        LoadDVT(maSP, e.RowIndex);
                    }
                }

                // Khi thay đổi đơn vị tính, cập nhật giá tương ứng
                else if (columnName == "cl_DVT")
                {
                    DataGridViewCell cellDVT = dgvHoaDonXuat.Rows[e.RowIndex].Cells["cl_DVT"];
                    DataGridViewCell cellGia = dgvHoaDonXuat.Rows[e.RowIndex].Cells["cl_Gia"];

                    if (cellDVT.Tag is Dictionary<string, decimal> dvtGiaNhap)
                    {
                        string selectedDVT = cellDVT.Value?.ToString();
                        DVT_public = selectedDVT;
                        if (selectedDVT != null && dvtGiaNhap.ContainsKey(selectedDVT))
                        {
                            decimal GiaNhap = dvtGiaNhap[selectedDVT]; // Cập nhật giá theo ĐVT
                            decimal GiaBan = GiaNhap * 1.2m;
                            cellGia.Value = GiaBan.ToString("N0");
                        }
                    }
                }
                else if(columnName == "cl_soLuong")
                {
                    DataGridViewCell cellThanhTien = dgvHoaDonXuat.Rows[e.RowIndex].Cells["cl_ThanhTien"];
                    DataGridViewCell cellGia = dgvHoaDonXuat.Rows[e.RowIndex].Cells["cl_Gia"];
                    DataGridViewCell cellSoLuong = dgvHoaDonXuat.Rows[e.RowIndex].Cells["cl_soLuong"];

                    //check
                    string checkSQL = "SELECT [Số lượng] FROM dvtSanPham WHERE [Mã sản phẩm] = @MaSP AND [Đơn vị tính] = @DVT";
                    SqlCommand checkCmd = new SqlCommand(checkSQL, BienConnect);
                    checkCmd.Parameters.AddWithValue("@MaSP", maSP_public);
                    checkCmd.Parameters.AddWithValue("@DVT", DVT_public);

                    int soLuongTonKho = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (decimal.TryParse(cellSoLuong.Value?.ToString(), out decimal soLuongNhap))
                    {
                        if (soLuongTonKho <= 0)
                        {
                            MessageBox.Show("Trong kho không còn sản phẩm này", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cellSoLuong.Value = 0;
                            return;
                        }
                        if (soLuongNhap > soLuongTonKho)
                        {
                            MessageBox.Show($"Sản phẩm này chỉ còn {soLuongTonKho} sản phẩm trong kho.",
                                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            // Đặt lại số lượng nhập về giá trị tồn kho
                            cellSoLuong.Value = soLuongTonKho.ToString("N0");
                            return;
                        }
                    }

                    if (decimal.TryParse(cellSoLuong.Value?.ToString().Replace(",", ""), out decimal SL) &&
                decimal.TryParse(cellGia.Value?.ToString().Replace(",", ""), out decimal giaBan))
                    {
                        decimal thanhTien = SL * giaBan;

                        // Hiển thị có dấu ',' 
                        cellSoLuong.Value = SL.ToString("N0");
                        cellGia.Value = giaBan.ToString("N0");
                        cellThanhTien.Value = thanhTien.ToString("N0");
                    }
                }
            }
        }
        private void LoadDVT(string maSP, int rowIndex)
        {
            string query = "SELECT [Đơn vị tính], [Giá nhập trung bình] FROM dvtSanPham WHERE [Mã sản phẩm] = @maSP";
            SqlCommand cmd = new SqlCommand(query, BienConnect);
            cmd.Parameters.AddWithValue("@maSP", maSP);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            DataGridViewComboBoxCell cb_DVT = (DataGridViewComboBoxCell)dgvHoaDonXuat.Rows[rowIndex].Cells["cl_DVT"];
            cb_DVT.Items.Clear();

            Dictionary<string, decimal> dvtGiaNhap = new Dictionary<string, decimal>();
            string firstDVT = null; // Lưu đơn vị tính đầu tiên
            decimal firstGiaNhap = 0; // Lưu giá nhập đầu tiên

            foreach (DataRow row in dt.Rows)
            {
                string dvt = row["Đơn vị tính"].ToString();
                decimal giaNhap = Convert.ToDecimal(row["Giá nhập trung bình"]);
                cb_DVT.Items.Add(dvt);
                dvtGiaNhap[dvt] = giaNhap;

                if (firstDVT == null)
                {
                    firstDVT = dvt;
                    firstGiaNhap = giaNhap;
                }
            }

            dgvHoaDonXuat.Rows[rowIndex].Cells["cl_DVT"].Tag = dvtGiaNhap;

            // Gán giá trị mặc định
            if (firstDVT != null)
            {
                dgvHoaDonXuat.Rows[rowIndex].Cells["cl_DVT"].Value = firstDVT;
                dgvHoaDonXuat.Rows[rowIndex].Cells["cl_Gia"].Value = firstGiaNhap;
            }
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Chặn ký tự không hợp lệ
            }
        }


        private void dgvHoaDonXuat_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int columnIndex = dgvHoaDonXuat.CurrentCell.ColumnIndex;
            if (dgvHoaDonXuat.Columns[columnIndex].Name == "cl_tenSP")
            {
               if (e.Control is ComboBox cb)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDown;
                    cb.DropDownHeight = 250; // Giới hạn chiều cao hiển thị
                }
            }
            
            if (dgvHoaDonXuat.CurrentCell.ColumnIndex == dgvHoaDonXuat.Columns["cl_soLuong"].Index)
            {
                TextBox txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.KeyPress -= Txt_KeyPress; // Xóa sự kiện cũ để tránh trùng lặp
                    txt.KeyPress += Txt_KeyPress; // Thêm sự kiện kiểm tra số
                }
            }
        }

        private void FormHoaDonXuat_MouseEnter(object sender, EventArgs e)
        {
            Button buttonX = (Button)sender;
            buttonX.BackColor = Color.FromArgb(64, 72, 114);
        }

        private void Luu_MouseLeave(object sender, EventArgs e)
        {
            Button buttonX = (Button)sender;
            buttonX.BackColor = Color.Olive;
        }

        private void Huy_MouseLeave(object sender, EventArgs e)
        {
            Button buttonX = (Button)sender;
            buttonX.BackColor = Color.Red;
        }

        private void dgvHoaDonXuat_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

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

            foreach (DataGridViewRow row in dgvHoaDonXuat.Rows)
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

            {
                int x = 150;
                int y = 20;
                string tencuahang = "Trung tâm phân phối và pha màu tự động";
                string diachi = "Địa chỉ: Huyện Thạch Thất, Hà Nội";
                string phone = "Điện thoại: 0198414235";
                string stk = "Số tài khoản: ";
                DateTime now = DateTime.Now;
                string today = $"Ngày {now.Day} Tháng {now.Month} Năm {now.Year} ";

                string name = "Hoá Đơn Bán Hàng";

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
                string tenKH = "Tên khách hàng: " + cbb_KhachHang.Text;
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
}
