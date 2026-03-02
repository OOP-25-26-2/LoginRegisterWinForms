using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginRegisterWinForms.Data;
using LoginRegisterWinForms.Security;

namespace LoginRegisterWinForms
{
    public partial class LoginForm : Form
    {
        private readonly UserRepository _repo = new UserRepository();

        public LoginForm()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            string? storedHash = _repo.GetPasswordHash(username);

            if (storedHash == null)
            {
                MessageBox.Show("Invalid username or password.");
                return;
            }

            bool ok = PasswordHasher.Verify(password, storedHash);
            if (!ok)
            {
                MessageBox.Show("Invalid username or password.");
                return;
            }

            // Successful login
            MainForm main = new MainForm(username);
            main.Show();
            this.Hide();
        }

        private void lnkCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using var reg = new RegisterForm();
            reg.ShowDialog();
        }
    }
}
