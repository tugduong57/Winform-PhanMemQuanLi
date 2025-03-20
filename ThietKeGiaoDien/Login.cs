﻿using System;
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
        
        string ConnectString = "Data Source=DESKTOP-2TGO6QK" +
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
            // Lấy dữ liệu Tài khoản mật khẩu từ DataBase
            bienSQLDataAdapter.Fill(bienDataTable_TaiKhoan);
            // Che mật khẩu textBox Password
            tbPassword.UseSystemPasswordChar = true;
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

        private void tbUser_Enter(object sender, EventArgs e)
        {
            if (tbUser.Text == "Tài khoản")
                tbUser.Text = "";
        } // Xóa trắng cho người dùng nhập tài khoản

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            if (tbPassword.Text == "Mật khẩu")
            {
                tbPassword.Text = "";
                tbPassword.UseSystemPasswordChar = true;
            }
        } // Xóa trắng để nhập mật khẩu

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btLogin_Click(sender, e);
        } // Gọi nút Đăng nhập nếu người dùng nhấn enter

        private void btLogin_Click(object sender, EventArgs e)
        {
            string user = tbUser.Text; string password = tbPassword.Text;

            foreach(DataRow row in bienDataTable_TaiKhoan.Rows)
            {
                if (row["Tài khoản"].ToString() == user && row["Tài khoản"].ToString() != "admin")
                {
                    if (row["Mật khẩu"].ToString() == password)
                    {
                        this.Hide();  // Hiển thị trang chủ dành cho Nhân Viên
                        FormTrangChuForNhanVien formTrangChuForNhanVien = new FormTrangChuForNhanVien();
                        formTrangChuForNhanVien.ShowDialog();
                        this.Close();
                    }
                }
                if (row["Tài khoản"].ToString() == user && row["Tài khoản"].ToString() == "admin")
                {
                    if (row["Mật khẩu"].ToString() == password)
                    {
                        this.Hide(); // Hiển thị trang chủ dành cho Quản lí
                        Form1 form1 = new Form1(); form1.ShowDialog();
                        this.Close();
                    }
                }
            }
            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            bien_Connect.Close();
            bien_Connect.Dispose();
        }
    }
}
