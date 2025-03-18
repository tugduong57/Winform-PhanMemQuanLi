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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        string ConnectString = "Data Source=DESKTOP-73HD43G\\SQLEXPRESS" +
                "; Initial Catalog=dataForProject" +
                "; Integrated Security=True";
        SqlConnection bien_Connect = null;

        DataTable bienDataTable_TaiKhoan = new DataTable();
        private void Login_Load(object sender, EventArgs e)
        {
            bien_Connect = new SqlConnection(ConnectString);
            bien_Connect.Open();

            string lenhTruyVanSQL = "Select [Tài khoản], [Mật khẩu] from NguoiDung";
            SqlDataAdapter bienSQLDataAdapter = new SqlDataAdapter(lenhTruyVanSQL, bien_Connect);

            bienSQLDataAdapter.Fill(bienDataTable_TaiKhoan);

            //this.Hide();
            //Form1 form1 = new Form1();
            //form1.ShowDialog();
            //this.Close();
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            if (tbPassword.UseSystemPasswordChar)
            {
                tbPassword.UseSystemPasswordChar = false; 
                btnShowPassword.Text = "👁️";
            }
            else
            {
                tbPassword.UseSystemPasswordChar = true;
                btnShowPassword.Text = "🙈";
            }
        }

        private void tbPassword_MouseDown(object sender, MouseEventArgs e)
        {
            if (tbPassword.Text == "Password")
            {
                tbPassword.Text = "";
                tbPassword.UseSystemPasswordChar = true;
            }
        }
        private void tbUser_MouseDown(object sender, MouseEventArgs e)
        {
            if(tbUser.Text == "Username")
            {
                tbUser.Text = "";
            }
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            string user = tbUser.Text;
            string password = tbPassword.Text;
            CurrentUser.TaiKhoan = user;

            user = "admin";
            password = "admin";

            //foreach (DataRow row in bienDataTable_TaiKhoan.Rows)
            //{
            //    if (row["Tài khoản"].ToString() == user)
            //    {
            //        if (row["Mật khẩu"].ToString() == password)
            //        {
            //            this.Hide();
            //            FormTrangChuForNhanVien formTrangChuForNhanVien = new FormTrangChuForNhanVien();
            //            formTrangChuForNhanVien.ShowDialog();
            //            this.Close();
            //            return;
            //        }
            //    }
            //}

            if (user == "admin" && password == "admin")
            {
                //this.Hide();
                Form1 form1 = new Form1();
                form1.ShowDialog();
                //this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
            }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btLogin_Click(sender, e);
            }
        }
    }
}
