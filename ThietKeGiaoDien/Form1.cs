using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        // Cách chèn User-Control
        private void btn_Click(object sender, EventArgs e)
        {
            UserControl_HangHoa hanghoa  = new UserControl_HangHoa();
            this.Controls.Add(hanghoa);
            hanghoa.Location = new Point(352, 0);
            //hanghoa.Dock = DockStyle.Left;
        }

        FormHangHoa childHangHoa = new FormHangHoa();
        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            childHangHoa.TopLevel = false;
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childHangHoa);
            childHangHoa.Dock = DockStyle.Fill;
            childHangHoa.Show();
        }

        FormDoiTac childDoiTac = new FormDoiTac();
        private void btnDoiTac_Click(object sender, EventArgs e)
        {
            // Muốn chèn Form vào Panl, cần đặt TopLovel = false;
            childDoiTac.TopLevel = false;
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childDoiTac);
            childDoiTac.Dock = DockStyle.Fill;
            childDoiTac.Show();
        }

        FormHoaDon childHoaDon = new FormHoaDon();
        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            childHoaDon.TopLevel = false;
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childHoaDon);
            childHoaDon.Dock = DockStyle.Fill;
            childHoaDon.Show();
        }

        FormThongKe childThongKe = new FormThongKe();
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            childThongKe.TopLevel = false;
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childThongKe);
            childThongKe.Dock = DockStyle.Fill;
            childThongKe.Show();
        }
        
        FormBangGia childBangGia = new FormBangGia();
        private void btnBangGia_Click(object sender, EventArgs e)
        {
            childBangGia.TopLevel = false;
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childBangGia);
            childBangGia.Dock = DockStyle.Fill;
            childBangGia.Show();
        }

        FormTaiKhoan childTaiKhoan = new FormTaiKhoan();
        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            childTaiKhoan.TopLevel = false;
            pnlChinh.Controls.Clear(); pnlChinh.Controls.Add(childTaiKhoan);
            childTaiKhoan.Dock = DockStyle.Fill;
            childTaiKhoan.Show();
        }

    }
}
