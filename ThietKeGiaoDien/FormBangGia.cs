using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ThietKeGiaoDien
{
    public partial class FormBangGia : Form
    {
        public FormBangGia()
        {
            InitializeComponent();
        }

        public SqlConnection connOfBangGia;
        DataSet dsTableTheoHang = new DataSet();
        string nameTableNow = "";

        private void FormBangGia_Load(object sender, EventArgs e)
        {
            string lenhTruyVanSQL1 = "SELECT DISTINCT Hãng FROM SanPham;";
            SqlDataAdapter bienSQL_DataAdapter1 = new SqlDataAdapter(lenhTruyVanSQL1, connOfBangGia);
            bienSQL_DataAdapter1.Fill(dsTableTheoHang, "HangForComboBox");

            cbHang.DataSource = dsTableTheoHang.Tables["HangForComboBox"];
            cbHang.DisplayMember = "Hãng"; // Cập nhật DataSource cho DataGirdView

            foreach (DataGridViewColumn col in dgvBGia.Columns)
                col.ReadOnly = true;
            dgvBGia.Columns["Đơn giá"].ReadOnly = false;
            dgvBGia.RowTemplate.Height = 35;
            dgvBGia.Columns["Mã"].Width = 100; dgvBGia.Columns["Tên sản phẩm"].Width = 370;
            dgvBGia.Columns["Phân loại"].Width = 170; dgvBGia.Columns["ĐVT"].Width = 90;
            dgvBGia.Columns["SL"].Width = 50; dgvBGia.Columns["Giá nhập"].Width = 150;
            dgvBGia.Columns["Giá nhập"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBGia.Columns["Đơn giá"].Width = 125;
            dgvBGia.Columns["Đơn giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBGia.Columns["Đơn giá"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void cbHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbHang.Text = "Lệnh này được gọi trong Form_Load";
            string name_Hang = "Hang_" + cbHang.Text;
            if (dsTableTheoHang.Tables.Contains(name_Hang))
            {
                dgvBGia.DataSource = dsTableTheoHang.Tables[name_Hang];
                nameTableNow = name_Hang;
            }
            else
            {
                string lenhTruyVanSQL = @"
                SELECT
                    SanPham.[Mã sản phẩm] AS Mã, SanPham.[Tên sản phẩm], SanPham.[Phân loại], 
                    dvtSanPham.[Đơn vị tính] AS ĐVT, dvtSanPham.[Số lượng] AS SL,
                    Format(dvtSanPham.[Giá nhập trung bình], 'N0') AS[Giá nhập],
                    Format(dvtSanPham.[Đơn giá], 'N0') AS[Đơn giá]
                FROM SanPham " +
                "INNER JOIN dvtSanPham ON SanPham.[Mã sản phẩm] = dvtSanPham.[Mã sản phẩm]" +
                $"WHERE SanPham.Hãng LIKE N'%{cbHang.Text}%'";

                SqlDataAdapter bienSQL_DataAdapter = new SqlDataAdapter(lenhTruyVanSQL, connOfBangGia);
                bienSQL_DataAdapter.Fill(dsTableTheoHang, name_Hang);
                dgvBGia.DataSource = dsTableTheoHang.Tables[name_Hang];
                nameTableNow = name_Hang;
            }
        }

        private void dgvBGia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int columnIndex = dgvBGia.Columns["Đơn giá"].Index;
                dgvBGia.CurrentCell = dgvBGia.Rows[e.RowIndex].Cells[columnIndex];
            }
        }

        private void dgvBGia_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow currentRow = dgvBGia.Rows[e.RowIndex];
            string noidungDaNhap = currentRow.Cells[dgvBGia.Columns["Đơn giá"].Index].Value.ToString();
            string MaSanPham = currentRow.Cells[dgvBGia.Columns["Mã"].Index].Value.ToString();
            string DonViTinh = currentRow.Cells[dgvBGia.Columns["ĐVT"].Index].Value.ToString();
            decimal DonGia;
            try
            {
                DonGia = Convert.ToDecimal(noidungDaNhap);
            }
            catch
            {
                MessageBox.Show("Có lỗi khi nhập đơn giá!");
                return;
            }
            string LenhSQL = "UPDATE dvtSanPham SET [Đơn giá] = " + $"'{DonGia}' " +
                        $"WHERE [Mã sản phẩm] = '{MaSanPham}' AND [Đơn vị tính] = '{DonViTinh}';";

            SqlCommand cmd = new SqlCommand(LenhSQL, connOfBangGia);
            cmd.ExecuteNonQuery();
        }

        private void ExportDataGridViewToPDF(DataGridView dgv, string pdfPath)
        {
            Document document = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
            try
            {
                PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
                document.Open();

                PdfPTable pdfTable = new PdfPTable(dgv.Columns.Count);
                pdfTable.WidthPercentage = 100;

                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = new BaseColor(240, 240, 240);
                    pdfTable.AddCell(cell);
                }

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;
                    foreach (DataGridViewCell cell in row.Cells)
                        pdfTable.AddCell(cell.Value?.ToString() ?? "");
                }
                document.Add(pdfTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message);
            }
            finally
            {
                document.Close();
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            string pdfPath = @"C:\Users\tugdu\OneDrive\Desktop\DataGridViewExport.pdf";
            ExportDataGridViewToPDF(dgvBGia, pdfPath);
            MessageBox.Show("Xuất PDF thành công: " + pdfPath);
        }
    }
}
