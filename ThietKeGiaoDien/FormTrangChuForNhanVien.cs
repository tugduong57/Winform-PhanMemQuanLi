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
    public partial class FormTrangChuForNhanVien : Form
    {
        public FormTrangChuForNhanVien()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
        }
        string ConnectString = "Data Source=DESKTOP-73HD43G\\SQLEXPRESS" +
                "; Initial Catalog=dataForProject" +
                "; Integrated Security=True";
        public SqlConnection bienConnect;

        private void FormTrangChuForNhanVien_Load(object sender, EventArgs e)
        {
            bienConnect = new SqlConnection(ConnectString); bienConnect.Open();
        }

        private void FormTrangChuForNhanVien_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bienConnect != null && bienConnect.State == ConnectionState.Open)
            {
                bienConnect.Close(); bienConnect.Dispose();
            }
        }
        void resetBackColorOf3Button()
        {
            foreach (Button x in tableLayoutPanel1.Controls)
                x.BackColor = Color.White;
        }
        // Các biến kiểm soát (= false khi form đó đã được mở 1 lần)
        public bool fHangHoa = true, fDoiTac = true, fHoaDon = true;

        FormHangHoa childHangHoa; // Tương tự với các Form còn lại
        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            resetBackColorOf3Button(); btnHangHoa.BackColor = Color.FromArgb(220, 220, 220);

            if (fHangHoa)
            {
                childHangHoa = new FormHangHoa(); childHangHoa.connOfHangHoa = bienConnect;
                fHangHoa = false; childHangHoa.TopLevel = false;
            }
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childHangHoa);
            childHangHoa.Dock = DockStyle.Fill; childHangHoa.Show();
        }

        FormDoiTac childDoiTac; // = new FormDoiTac();
        private void btnDoiTac_Click(object sender, EventArgs e)
        {

            resetBackColorOf3Button();
            btnDoiTac.BackColor = Color.FromArgb(220, 220, 220);

            if (fDoiTac)
            {
                childDoiTac = new FormDoiTac();
                childDoiTac.connOfDoiTac = bienConnect;
                fDoiTac = false; childDoiTac.TopLevel = false;
            }
            
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childDoiTac);
            childDoiTac.Dock = DockStyle.Fill;
            childDoiTac.Show();
            childDoiTac.HideButton();
            childDoiTac.btnKH_Click(sender, e);
        }

        FormHoaDon childHoaDon; // = new FormHoaDon();
        private void btnHoaDon_Click(object sender, EventArgs e)
        {

            resetBackColorOf3Button();
            btnHoaDon.BackColor = Color.FromArgb(220, 220, 220);

            if (fHoaDon)
            {
                childHoaDon = new FormHoaDon();
                childHoaDon.conofHoaDon = bienConnect;
                fHoaDon = false; childHoaDon.TopLevel = false;
            }
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childHoaDon);
            childHoaDon.Dock = DockStyle.Fill;
            childHoaDon.Show();
            childHoaDon.HideButton();
            childHoaDon.btn_Xuat_Click(sender, e);
        }
        private void btnX_MouseEnter(object sender, EventArgs e)
        {
            Button buttonX = (Button)sender;
            if (buttonX.BackColor != Color.FromArgb(220, 220, 220))
                buttonX.BackColor = Color.Gainsboro;
        }

        private void btnX_MouseLeave(object sender, EventArgs e)
        {
            Button buttonX = (Button)sender;
            if (buttonX.BackColor != Color.FromArgb(220, 220, 220))
                buttonX.BackColor = Color.White;
        }

    }
}
