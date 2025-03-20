using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms.DataVisualization.Charting;

namespace ThietKeGiaoDien
{
    public partial class FormThongKe : Form
    {
        public FormThongKe()
        {
            InitializeComponent();
        }

        public SqlConnection connOfThongKe;
        SqlDataAdapter bienSQL_DataAdapter;

        DataTable dtForcbPhanLoai = new DataTable();
        DataTable dtForcbDoiTac = new DataTable();
        DataTable dtForcbNhanVien = new DataTable();

        bool DangCapNhat = false;
        string maDoiTacSelecting, LoaiDoiTacSelecting;
        string NgayBatDau, NgayKetThuc;
        // Các series hiển thị trong Chart
        Series seriesNgay, seriesThang, seriesNam;
        // Bảng lưu dữ liệu của Hóa đơn đã "lọc"
        DataTable dataHoaDonFiltered;

        void resetCBphanLoai()
        {
            string lenhTruyVanSQL1 = "SELECT DISTINCT [Phân loại] FROM SanPham;";
            bienSQL_DataAdapter = new SqlDataAdapter(lenhTruyVanSQL1, connOfThongKe);
            dtForcbPhanLoai = new DataTable();
            bienSQL_DataAdapter.Fill(dtForcbPhanLoai);
            // Thêm Option: "Tất cả"
            DataRow row = dtForcbPhanLoai.NewRow();
            row["Phân loại"] = "Tất cả";
            dtForcbPhanLoai.Rows.InsertAt(row, 0);

            cbPhanLoai.DataSource = dtForcbPhanLoai;
            cbPhanLoai.DisplayMember = "Phân loại";
        }

        void resetCBnhanVien()
        {
            string lenhTruyVanSQL3 = "SELECT [Tài khoản], [Tên người dùng] FROM NguoiDung;";

            bienSQL_DataAdapter = new SqlDataAdapter(lenhTruyVanSQL3, connOfThongKe);
            dtForcbNhanVien = new DataTable();
            bienSQL_DataAdapter.Fill(dtForcbNhanVien);
            // Thêm các Option Mặc định
            DataRow row = dtForcbNhanVien.NewRow();
            row = dtForcbNhanVien.NewRow();
            row["Tên người dùng"] = "Tất cả"; row["Tài khoản"] = "Tất cả";
            dtForcbNhanVien.Rows.InsertAt(row, 0);
            row = dtForcbNhanVien.NewRow();
            row["Tên người dùng"] = "Nhân viên"; row["Tài khoản"] = "NV";
            dtForcbNhanVien.Rows.InsertAt(row, 1);
            row = dtForcbNhanVien.NewRow();
            row["Tên người dùng"] = "Quản lí"; row["Tài khoản"] = "QL";
            dtForcbNhanVien.Rows.InsertAt(row, 2);

            cbNhanVien.DataSource = dtForcbNhanVien;
            cbNhanVien.DisplayMember = "Tên người dùng"; cbNhanVien.ValueMember = "Tài khoản";

        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            // Ngăn các hàm Selected của ComboBox được gọi tự động
            DangCapNhat = true;

            // Hide các phần giao diện còn lại
            lbTongKet.Text = ""; lbTongSoHoaDon.Text = "";
            lbTongKet1.Text = ""; lbTongKet2.Text = "";
            lbXemBieuDo.Text = "";
            btnNgay.Hide(); btnThang.Hide(); btnNam.Hide();

            string conn = "Data Source=DESKTOP-73HD43G\\SQLEXPRESS" +
                "; Initial Catalog=dataForProject" +
                "; Integrated Security=True";

            connOfThongKe = new SqlConnection(conn);
            connOfThongKe.Open();

            // Select MinDate, MaxDate for 2 TimePicker

            dtpDateStart.CustomFormat = "dd/MM/yyyy"; dtpDateEnd.CustomFormat = "dd/MM/yyyy";

            string lenhTruyVanSQL0 = "SELECT MIN([Ngày tạo]) FROM HoaDon;";
            bienSQL_DataAdapter = new SqlDataAdapter(lenhTruyVanSQL0, connOfThongKe);
            DataTable minDate = new DataTable();
            bienSQL_DataAdapter.Fill(minDate);

            if (minDate.Rows.Count > 0 && minDate.Rows[0][0] != DBNull.Value)
            {
                DateTime minOfDate = Convert.ToDateTime(minDate.Rows[0][0]);
                dtpDateStart.MinDate = minOfDate; dtpDateStart.Value = minOfDate;
                dtpDateEnd.MinDate = minOfDate;
                NgayBatDau = minOfDate.Date.ToString("yyyy-MM-dd");
            }
            else
            {
                dtpDateStart.MinDate = DateTime.Today; dtpDateEnd.MinDate = DateTime.Today;
                NgayBatDau = DateTime.Today.Date.ToString("yyyy-MM-dd");
            }
            dtpDateStart.MaxDate = DateTime.Today; dtpDateEnd.MaxDate = DateTime.Today;
            NgayKetThuc = DateTime.Today.Date.ToString("yyyy-MM-dd");



            // Select for ComboBox Đối tác
            string lenhTruyVanSQL2 = "SELECT [Mã đối tác], [Tên đối tác], [Phân Loại] FROM DoiTac;";
            bienSQL_DataAdapter = new SqlDataAdapter(lenhTruyVanSQL2, connOfThongKe);
            bienSQL_DataAdapter.Fill(dtForcbDoiTac);
            // Thêm các lựa chọn "Nhóm Khách hàng", "Nhóm Nhà cung cấp"
            DataRow row = dtForcbDoiTac.NewRow();
            row = dtForcbDoiTac.NewRow();
            row["Tên đối tác"] = "Khách hàng"; row["Mã đối tác"] = "KH";
            dtForcbDoiTac.Rows.InsertAt(row, 0);
            row = dtForcbDoiTac.NewRow();
            row["Tên đối tác"] = "Nhà cung cấp"; row["Mã đối tác"] = "NCC";
            dtForcbDoiTac.Rows.InsertAt(row, 1);

            cbDoiTac.DataSource = dtForcbDoiTac;
            cbDoiTac.DisplayMember = "Tên đối tác"; cbDoiTac.ValueMember = "Mã đối tác";

            // Select for ComboBox Phân Loại, Nhân viên
            resetCBphanLoai(); resetCBnhanVien();

            chartBaoCao.Series.Clear();

            DangCapNhat = false;
        }


        private void dtpDateStart_ValueChanged(object sender, EventArgs e)
        {
            NgayBatDau = dtpDateStart.Value.Date.ToString("yyyy-MM-dd");
        }
        private void dtpDateEnd_ValueChanged(object sender, EventArgs e)
        {
            NgayKetThuc = dtpDateEnd.Value.Date.ToString("yyyy-MM-dd");
        }


        void LocCBPhanLoaiTheoDoiTac(string maDoiTacSelected)
        {
            DangCapNhat = true;

            // Phân loại được lọc theo ĐỐI TÁC
            string lenhTruyVan =
                @"SELECT DISTINCT sp.[Phân loại]
                    FROM HoaDon hd
                    JOIN ChiTietHoaDon cthd ON hd.[Mã hóa đơn] = cthd.[Mã hóa đơn]
                    JOIN SanPham sp ON cthd.[Mã sản phẩm] = sp.[Mã sản phẩm] " +
                    $"WHERE hd.[Mã đối tác] = '{maDoiTacSelected}' AND " +
                    $"[Ngày tạo] >= '{NgayBatDau}' AND [Ngày tạo] <= '{NgayKetThuc}';";

            bienSQL_DataAdapter = new SqlDataAdapter(lenhTruyVan, connOfThongKe);
            dtForcbPhanLoai = new DataTable();
            bienSQL_DataAdapter.Fill(dtForcbPhanLoai);

            DataRow row1 = dtForcbPhanLoai.NewRow();
            row1["Phân loại"] = "Tất cả";
            dtForcbPhanLoai.Rows.InsertAt(row1, 0);

            cbPhanLoai.DataSource = dtForcbPhanLoai;
            cbPhanLoai.DisplayMember = "Phân loại";

            DangCapNhat = false;
        }


        void LocCBNhanVienTheoDoiTac(string phanLoaiDoiTac, string maDoiTacSelected)
        {
            DangCapNhat = true;

            // Nếu Đối tác được chọn là Khách hàng (cụ thể) 
            if (phanLoaiDoiTac == "Khách hàng")
            {
                cbNhanVien.Enabled = true;
                string lenhTruyVan2 =
                    @"SELECT DISTINCT NguoiDung.[Tài khoản], NguoiDung.[Tên người dùng]
                        FROM NguoiDung
                        JOIN HoaDon ON HoaDon.[Mã người bán] = NguoiDung.[Tài khoản] " +
                     $"WHERE HoaDon.[Mã đối tác] = '{maDoiTacSelected}' AND " +
                     $"HoaDon.[Ngày tạo] >= '{NgayBatDau}' AND HoaDon.[Ngày tạo] <= '{NgayKetThuc}';";

                dtForcbNhanVien = new DataTable();
                bienSQL_DataAdapter = new SqlDataAdapter(lenhTruyVan2, connOfThongKe);
                bienSQL_DataAdapter.Fill(dtForcbNhanVien);

                DataRow row2 = dtForcbNhanVien.NewRow();
                row2 = dtForcbNhanVien.NewRow();
                row2["Tên người dùng"] = "Tất cả"; row2["Tài khoản"] = "Tất cả";
                dtForcbNhanVien.Rows.InsertAt(row2, 0);
                row2 = dtForcbNhanVien.NewRow(); row2["Tên người dùng"] = "Nhân viên"; row2["Tài khoản"] = "NV"; dtForcbNhanVien.Rows.InsertAt(row2, 1);
                row2 = dtForcbNhanVien.NewRow(); row2["Tên người dùng"] = "Quản lí"; row2["Tài khoản"] = "QL"; dtForcbNhanVien.Rows.InsertAt(row2, 2);

                cbNhanVien.DataSource = dtForcbNhanVien;
                cbNhanVien.DisplayMember = "Tên người dùng";
                cbNhanVien.ValueMember = "Tài khoản";
            }

            // Nếu Đối tác được chọn là Nhà cung cấp (cụ thể)
            if (phanLoaiDoiTac == "Nhà cung cấp")
            {
                // Nhân viên: Quản lí, tắt chỉnh sửa
                cbNhanVien.SelectedIndex = 2;
                cbNhanVien.Enabled = false;
            }

            DangCapNhat = false;
        }

        void LocCBNhanVienTheoLoaiSP(string PhanLoaiDangChon)
        {
            cbNhanVien.Enabled = true;
            // Lọc Nhân viên theo Loại Sản phẩm (Đối tác là cả nhóm Khách hàng)
            string lenhTruyVan2 =
            @"SELECT DISTINCT nd.[Tài khoản], nd.[Tên người dùng]
            FROM NguoiDung nd
            JOIN HoaDon hd ON hd.[Mã người bán] = nd.[Tài khoản]
            JOIN ChiTietHoaDon cthd ON hd.[Mã hóa đơn] = cthd.[Mã hóa đơn]
            JOIN SanPham sp ON cthd.[Mã sản phẩm] = sp.[Mã sản phẩm] " +
            $"WHERE sp.[Phân loại] = N'{PhanLoaiDangChon}' AND " +
            $"hd.[Ngày tạo] >= '{NgayBatDau}' AND hd.[Ngày tạo] <= '{NgayKetThuc}';";

            dtForcbNhanVien = new DataTable();
            bienSQL_DataAdapter = new SqlDataAdapter(lenhTruyVan2, connOfThongKe);
            bienSQL_DataAdapter.Fill(dtForcbNhanVien);

            DataRow row2 = dtForcbNhanVien.NewRow();
            row2 = dtForcbNhanVien.NewRow();
            row2["Tên người dùng"] = "Tất cả"; row2["Tài khoản"] = "Tất cả";
            dtForcbNhanVien.Rows.InsertAt(row2, 0);
            row2 = dtForcbNhanVien.NewRow(); row2["Tên người dùng"] = "Nhân viên"; row2["Tài khoản"] = "NV"; dtForcbNhanVien.Rows.InsertAt(row2, 1);
            row2 = dtForcbNhanVien.NewRow(); row2["Tên người dùng"] = "Quản lí"; row2["Tài khoản"] = "QL"; dtForcbNhanVien.Rows.InsertAt(row2, 2);

            cbNhanVien.DataSource = dtForcbNhanVien;
            cbNhanVien.DisplayMember = "Tên người dùng";
            cbNhanVien.ValueMember = "Tài khoản";
        }

        void LocCBNhanVienTheoKhachHangVaLoaiSP(string PhanLoaiDangChon)
        {
            DangCapNhat = true;

            cbNhanVien.Enabled = true;
            // Phân loại được lọc theo ĐỐI TÁC và Loại Sản phẩm
            string lenhTruyVan5 =
                @"SELECT DISTINCT nd.[Tài khoản], nd.[Tên người dùng]
                 FROM NguoiDung nd
                 JOIN HoaDon hd ON nd.[Tài khoản] = hd.[Mã người bán]
                 JOIN ChiTietHoaDon cthd ON hd.[Mã hóa đơn] = cthd.[Mã hóa đơn]
                 JOIN SanPham sp ON cthd.[Mã sản phẩm] = sp.[Mã sản phẩm] " +
                 $"WHERE hd.[Mã đối tác] = '{maDoiTacSelecting}' " +
                 $"AND sp.[Phân loại] = N'{PhanLoaiDangChon}' AND " +
                 $"hd.[Ngày tạo] >= '{NgayBatDau}' AND hd.[Ngày tạo] <= '{NgayKetThuc}'";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(lenhTruyVan5, connOfThongKe);
            dtForcbNhanVien = new DataTable();
            dataAdapter.Fill(dtForcbNhanVien);
            //dgvCheckListHoaDon.DataSource = dtForcbNhanVien;

            DataRow row2 = dtForcbNhanVien.NewRow();
            row2 = dtForcbNhanVien.NewRow();
            row2["Tên người dùng"] = "Tất cả"; row2["Tài khoản"] = "Tất cả";
            dtForcbNhanVien.Rows.InsertAt(row2, 0);
            row2 = dtForcbNhanVien.NewRow(); row2["Tên người dùng"] = "Nhân viên"; row2["Tài khoản"] = "NV"; dtForcbNhanVien.Rows.InsertAt(row2, 1);
            row2 = dtForcbNhanVien.NewRow(); row2["Tên người dùng"] = "Quản lí"; row2["Tài khoản"] = "QL"; dtForcbNhanVien.Rows.InsertAt(row2, 2);

            cbNhanVien.DataSource = dtForcbNhanVien;
            cbNhanVien.DisplayMember = "Tên người dùng";
            cbNhanVien.ValueMember = "Tài khoản";

            DangCapNhat = false;
        }

        private void cbDoiTac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DangCapNhat)
                return;
            DangCapNhat = true;
            string DoiTacDangChon = cbDoiTac.Text;
            // Nếu là Nhóm Nhà cung cấp
            if (DoiTacDangChon == "Nhà cung cấp")
            {
                resetCBphanLoai(); // Phân loại là tất cả các loại sản phẩm
                // Nhân viên: Quản lí, tắt chỉnh sửa
                cbNhanVien.SelectedIndex = 2; cbNhanVien.Enabled = false;
            }
            // Nếu là Nhóm Khách hàng
            else if (DoiTacDangChon == "Khách hàng")
            {
                resetCBphanLoai();  // Phân loại: được reset
                cbNhanVien.Enabled = true; resetCBnhanVien(); // Nhân viên: được reset
            }

            // Nếu là Một đối tác cụ thể
            else
            {
                // Lấy ra giá trị cột [Phân loại] (khách hoặc ncc)
                int rowIndex = -1;
                for (int i = 0; i < dtForcbDoiTac.Rows.Count; i++)
                    if (dtForcbDoiTac.Rows[i]["Mã đối tác"].ToString() == cbDoiTac.SelectedValue.ToString())
                    {
                        rowIndex = i; break;
                    }
                LoaiDoiTacSelecting = dtForcbDoiTac.Rows[rowIndex]["Phân loại"].ToString();
                maDoiTacSelecting = cbDoiTac.SelectedValue.ToString();
                LocCBPhanLoaiTheoDoiTac(maDoiTacSelecting);
                LocCBNhanVienTheoDoiTac(LoaiDoiTacSelecting, maDoiTacSelecting);

            }
            DangCapNhat = false;
        }

        private void cbPhanLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DangCapNhat)
                return;
            DangCapNhat = true;
            string PhanLoaiDangChon = cbPhanLoai.Text;
            string maDoiTacDangChon = cbDoiTac.SelectedValue.ToString();

            if (PhanLoaiDangChon == "Tất cả") // Phân loại mặc định là Tất cả, 
            {
                if (maDoiTacDangChon == "KH") // Nếu đối tác là Nhóm "Khách hàng"
                    resetCBnhanVien();
                else if (maDoiTacDangChon != "NCC")
                    // Nhân viên: được reset theo Đối tác (cụ thể)
                    LocCBNhanVienTheoDoiTac(LoaiDoiTacSelecting, maDoiTacSelecting);
            }
            else // 1 loại sản phẩm cụ thể
            {
                if (maDoiTacDangChon == "KH")  // Nếu đối tác là Nhóm "Khách hàng"
                    // Nhân viên là những người từng bán Loại sản phẩm đó
                    LocCBNhanVienTheoLoaiSP(PhanLoaiDangChon);
                else if (maDoiTacDangChon != "NCC") // Khách hàng cụ thể
                    LocCBNhanVienTheoKhachHangVaLoaiSP(PhanLoaiDangChon);
            }
            DangCapNhat = false;
        }

        private void cbNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DangCapNhat)
                return;
            // Có thể gọi tới Button Xem báo cáo luôn
            btnXemBaoCao_Click(sender, e);
        }


        private void btnNam_Click(object sender, EventArgs e)
        {
            btnNgay.BackColor = Color.FromArgb(224, 224, 224);
            btnThang.BackColor = Color.FromArgb(224, 224, 224);
            btnNam.BackColor = Color.FromArgb(192, 255, 255);
            chartBaoCao.Series.Clear();
            chartBaoCao.Series.Add(seriesNam);
            lbTenBieuDo.Text = "Biểu đồ thống kê theo Năm";
        }

        private void btnThang_Click(object sender, EventArgs e)
        {
            btnNgay.BackColor = Color.FromArgb(224, 224, 224);
            btnThang.BackColor = Color.FromArgb(192, 255, 255);
            btnNam.BackColor = Color.FromArgb(224, 224, 224);

            chartBaoCao.Series.Clear();
            chartBaoCao.Series.Add(seriesThang);
            lbTenBieuDo.Text = "Biểu đồ thống kê theo Tháng";
        }

        private void btnNgay_Click(object sender, EventArgs e)
        {
            btnNgay.BackColor = Color.FromArgb(192, 255, 255);
            btnThang.BackColor = Color.FromArgb(224, 224, 224);
            btnNam.BackColor = Color.FromArgb(224, 224, 224);
            chartBaoCao.Series.Clear();
            chartBaoCao.Series.Add(seriesNgay);
            lbTenBieuDo.Text = "Biểu đồ thống kê theo Ngày";
        }
        private void chartBaoCao_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = chartBaoCao.HitTest(e.X, e.Y);

            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                Series series = result.Series;
                int pointIndex = result.PointIndex;
                DataPoint point = series.Points[pointIndex];

                string x = point.AxisLabel; double y = point.YValues[0];

                if (!point.IsValueShownAsLabel)
                {
                    point.IsValueShownAsLabel = true;
                    point.ToolTip = $"{x} : {y:N0}";
                }
                else
                    point.IsValueShownAsLabel = false;
            }
        }

        /*
         *          BUTTON XEM BAO CAO _ CLICK 
         */

        // Hàm lọc ra DataTable chuẩn cho Báo cáo
        void LocDataTableTheoBoLoc(string phanLoaiSelected, string maNhanVienSelected, string maDoiTacSelected, string startDate, string endDate)
        {
            //Nếu phân loại là Tất cả
            if (phanLoaiSelected == "Tất cả")
            {
                string lenhSQL = @"
                    SELECT hd.[Ngày tạo], 
                    dt.[Tên đối tác],
                    nd.[Tên người dùng] AS [Người bán], Format([Tổng tiền], 'N0') AS [Tổng tiền]
                    FROM HoaDon hd
                    INNER JOIN DoiTac dt ON hd.[Mã đối tác] = dt.[Mã đối tác]
                    INNER JOIN NguoiDung nd ON hd.[Mã người bán] = nd.[Tài khoản]
                    WHERE " +
                    $"[Ngày tạo] >= '{startDate}' AND [Ngày tạo] <= '{endDate}'";

                if (maNhanVienSelected == "NV")
                    lenhSQL += " AND hd.[Mã người bán] != 'admin'";
                else if (maNhanVienSelected == "QL")
                    lenhSQL += " AND hd.[Mã người bán] = 'admin'";
                else if (maNhanVienSelected != "Tất cả")
                    lenhSQL += $" AND hd.[Mã người bán] = '{maNhanVienSelected}'";

                if (maDoiTacSelected == "KH")
                    lenhSQL += " AND [Loại hóa đơn] = N'Xuất'";
                else if (maDoiTacSelected == "NCC")
                    lenhSQL += " AND [Loại hóa đơn] = N'Nhập'";
                else
                    lenhSQL += $" AND hd.[Mã đối tác] = '{maDoiTacSelected}'";

                lenhSQL += "ORDER BY hd.[Ngày tạo] ASC;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(lenhSQL, connOfThongKe);
                dataHoaDonFiltered = new DataTable(); dataAdapter.Fill(dataHoaDonFiltered);

            }
            // Nếu phân loại là 1 phân loại cụ thể 
            else
            {
                string lenhSQL = @"
                SELECT 
                    hd.[Ngày tạo], 
                    dt.[Tên đối tác], 
                    nd.[Tên người dùng] AS [Người bán],
                    Format(SUM(cthd.[Số lượng] * cthd.[Đơn giá]), 'N0') AS [Tổng tiền]
                FROM HoaDon hd
                INNER JOIN DoiTac dt ON hd.[Mã đối tác] = dt.[Mã đối tác]
                INNER JOIN NguoiDung nd ON hd.[Mã người bán] = nd.[Tài khoản]
                INNER JOIN ChiTietHoaDon cthd ON hd.[Mã hóa đơn] = cthd.[Mã hóa đơn]
                INNER JOIN SanPham sp ON cthd.[Mã sản phẩm] = sp.[Mã sản phẩm] " +
                $"WHERE sp.[Phân loại] = N'{phanLoaiSelected}' AND " +
                $"[Ngày tạo] >= '{startDate}' AND [Ngày tạo] <= '{endDate}' ";

                if (maNhanVienSelected == "NV")
                    lenhSQL += " AND hd.[Mã người bán] != 'admin'";
                else if (maNhanVienSelected == "QL")
                    lenhSQL += " AND hd.[Mã người bán] = 'admin'";
                else if (maNhanVienSelected != "Tất cả")
                    lenhSQL += $" AND hd.[Mã người bán] = '{maNhanVienSelected}'";

                if (maDoiTacSelected == "KH")
                    lenhSQL += " AND hd.[Loại hóa đơn] = N'Xuất'";
                else if (maDoiTacSelected == "NCC")
                    lenhSQL += " AND hd.[Loại hóa đơn] = N'Nhập'";
                else
                    lenhSQL += $" AND hd.[Mã đối tác] = '{maDoiTacSelected}'";

                // Gộp theo ngày 

                lenhSQL += "GROUP BY hd.[Ngày tạo], dt.[Tên đối tác], nd.[Tên người dùng] ORDER BY hd.[Ngày tạo] ASC;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(lenhSQL, connOfThongKe);
                dataHoaDonFiltered = new DataTable(); dataAdapter.Fill(dataHoaDonFiltered);

                // CHECK: textBox1.Text = "ĐT: " + maDoiTacSelected + " NV: " + maNhanVienSelected + " PL: " + phanLoaiSelected + "\nLenhSQL:" + lenhSQL;
            }
        }
        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            /*
             *      PHẦN BỘ LỌC
             */
            // Lấy ra ID đối tác, ID người bán, Phân loại sản phẩm
            string maDoiTacSelected = cbDoiTac.SelectedValue.ToString();
            string maNhanVienSelected = cbNhanVien.SelectedValue.ToString();
            string phanLoaiSelected = cbPhanLoai.Text;

            // Lấy ngày trong bộ lọc
            string startDate = dtpDateStart.Value.Date.ToString("yyyy-MM-dd");
            string endDate = dtpDateEnd.Value.Date.ToString("yyyy-MM-dd");

            LocDataTableTheoBoLoc(phanLoaiSelected, maNhanVienSelected, maDoiTacSelected, startDate, endDate);

            /*
             *      PHẦN TỔNG KẾT
             */

            btnNgay.Show(); btnThang.Show(); btnNam.Show();
            lbTongKet.Text = "Tổng kết";
            lbXemBieuDo.Text = "Xem biểu đồ theo:";

            string TongHoaDon = dataHoaDonFiltered.Rows.Count.ToString();
            lbTongSoHoaDon.Text = "Tổng số: " + TongHoaDon + " (hóa đơn)";

            decimal tongTien = 0;

            for (int i = 0; i < dataHoaDonFiltered.Rows.Count; i++)
                tongTien += Convert.ToDecimal(dataHoaDonFiltered.Rows[i]["Tổng tiền"]);

            if (maDoiTacSelected == "KH" || maDoiTacSelected == "Khách hàng")
                lbTongKet1.Text = "Doanh thu: " + tongTien.ToString("N0") + "đ";
            else if (maDoiTacSelected == "NCC" || maDoiTacSelected == "Nhà cung cấp")
                lbTongKet1.Text = "Tổng nhập: " + tongTien.ToString("N0") + "đ";

            /*
             *      PHẦN BIỂU ĐỒ
             * 
             */

            // Tính toán

            List<string> listNgay = new List<string>();     // "dd/MM/yyyy"
            List<string> listThang = new List<string>();    // "MM/yyyy"
            List<string> listNam = new List<string>();      // "yyyy"

            listThang.Add("0"); listNam.Add("0");

            List<decimal> listTongTienTheoNgay = new List<decimal>();
            List<decimal> listTongTienTheoThang = new List<decimal>();
            List<decimal> listTongTienTheoNam = new List<decimal>();

            decimal tongTienTheoNgay = 0, tongTienTheoThang = 0, tongTienTheoNam = 0;

            foreach (DataRow row in dataHoaDonFiltered.Rows)
            {
                // Ngày
                DateTime ngayTao = Convert.ToDateTime(row["Ngày tạo"]);
                listNgay.Add(ngayTao.ToString("dd/MM/yyyy"));
                tongTienTheoNgay = Convert.ToDecimal(row["Tổng tiền"]);
                listTongTienTheoNgay.Add(tongTienTheoNgay);

                // Tháng
                string Month = ngayTao.ToString("MM/yyyy");
                if (!listThang.Contains(Month))
                {
                    listTongTienTheoThang.Add(tongTienTheoThang);
                    listThang.Add(Month); tongTienTheoThang = tongTienTheoNgay;
                }
                else
                    tongTienTheoThang += tongTienTheoNgay;

                // Năm
                string nam = ngayTao.ToString("yyyy");
                if (!listNam.Contains(nam))
                {
                    listTongTienTheoNam.Add(tongTienTheoNam);
                    listNam.Add(nam); tongTienTheoNam = tongTienTheoNgay;
                }
                else
                    tongTienTheoNam += tongTienTheoNgay;
            }

            listTongTienTheoThang.Add(tongTienTheoThang); listTongTienTheoNam.Add(tongTienTheoNam);

            // Thiết lập biểu đồ

            // Tắt lưới
            chartBaoCao.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartBaoCao.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            // Format Trục Y
            chartBaoCao.ChartAreas[0].AxisY.LabelStyle.Format = "N0";

            // Ngày
            seriesNgay = new Series("Tổng tiền")
            {
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Double,
                LabelFormat = "N0",
            };

            if (listNgay.Count > 24)
                seriesNgay.ChartType = SeriesChartType.Line;
            else
                seriesNgay.ChartType = SeriesChartType.Column;


            for (int i = 0; i < listNgay.Count; i++)
                seriesNgay.Points.AddXY(listNgay[i], Convert.ToDouble(listTongTienTheoNgay[i]));

            // Tháng
            seriesThang = new Series("Tổng tiền")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Double,
                LabelFormat = "N0",
            };

            if (listThang.Count > 24)
                seriesThang.ChartType = SeriesChartType.Line;
            else
                seriesThang.ChartType = SeriesChartType.Column;


            for (int i = 1; i < listThang.Count; i++)
                seriesThang.Points.AddXY(listThang[i], Convert.ToDouble(listTongTienTheoThang[i]));

            // Năm
            seriesNam = new Series("Tổng tiền")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Double,
                LabelFormat = "N0",
            };

            for (int i = 1; i < listNam.Count; i++)
                seriesNam.Points.AddXY(listNam[i], Convert.ToDouble(listTongTienTheoNam[i]));

            btnNgay_Click(sender, e);
        }
    }
}

