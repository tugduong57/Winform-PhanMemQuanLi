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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string ConnectString = "Data Source=DESKTOP-2TGO6QK" +
                "; Initial Catalog=dataForProject" +
                "; Integrated Security=True";

        public SqlConnection bienConnect;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            bienConnect = new SqlConnection(ConnectString);
            bienConnect.Open();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bienConnect != null && bienConnect.State == ConnectionState.Open)
            {
                bienConnect.Close();
                bienConnect.Dispose();
            }
        }

        void resetBackColorOf6Button()
        {
            foreach (Button x in tableLayoutPanel1.Controls)
            {
                x.BackColor = Color.White;
            }
        }


        // = false khi form đó đã được mở 1 lần
        public bool fHangHoa = true, fDoiTac = true, fHoaDon = true, 
            fThongKe = true, fBangGia = true, fTaiKhoan = true;

        FormHangHoa childHangHoa; // = new FormHangHoa(); 
        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            resetBackColorOf6Button();
            btnHangHoa.BackColor = Color.FromArgb(220, 220, 220);

            if (fHangHoa)
            {
                childHangHoa = new FormHangHoa();
                childHangHoa.connOfHangHoa = bienConnect;
                fHangHoa = false; childHangHoa.TopLevel = false;
            }
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childHangHoa);
            childHangHoa.Dock = DockStyle.Fill;
            childHangHoa.Show();
        }

        FormDoiTac childDoiTac; // = new FormDoiTac();
        private void btnDoiTac_Click(object sender, EventArgs e)
        {

            resetBackColorOf6Button();
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
        }

        FormHoaDon childHoaDon; // = new FormHoaDon();
        private void btnHoaDon_Click(object sender, EventArgs e)
        {

            resetBackColorOf6Button();
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
        }

        FormThongKe childThongKe; // = new FormThongKe();

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            resetBackColorOf6Button();
            btnThongKe.BackColor = Color.FromArgb(220, 220, 220);
            if (fThongKe)
            {
                childThongKe = new FormThongKe();
                childThongKe.connOfThongKe = bienConnect;
                fThongKe = false; childThongKe.TopLevel = false;
            }
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childThongKe);
            childThongKe.Dock = DockStyle.Fill;
            childThongKe.Show();
        }

        FormBangGia childBangGia;// = new FormBangGia();

        private void btnBangGia_Click(object sender, EventArgs e)
        {
            resetBackColorOf6Button();
            btnBangGia.BackColor = Color.FromArgb(220, 220, 220);
            if (fBangGia)
            { 
                childBangGia = new FormBangGia();
                childBangGia.connOfBangGia = bienConnect;
                fBangGia = false; childBangGia.TopLevel = false;
            }
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childBangGia);
            childBangGia.Dock = DockStyle.Fill;
            childBangGia.Show();
        }

        FormTaiKhoan childTaiKhoan;
        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            resetBackColorOf6Button();
            btnTaiKhoan.BackColor = Color.FromArgb(220, 220, 220);
            if (fTaiKhoan)
            {
                childTaiKhoan = new FormTaiKhoan();
                childTaiKhoan.connOfTaiKhoan = bienConnect;
                fTaiKhoan = false; childTaiKhoan.TopLevel = false;
            }
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childTaiKhoan);
            childTaiKhoan.Dock = DockStyle.Fill;
            childTaiKhoan.Show();
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
