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
    public partial class RegisterForm : Form
    {
        private readonly UserRepository _repo = new UserRepository();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string username = txtNewUsername.Text.Trim();
            string password = txtNewPassword.Text;
            string confirm = txtConfirmPassword.Text;

            if (username.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters.");
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            if (_repo.UsernameExists(username))
            {
                MessageBox.Show("Username already exists.");
                return;
            }

            string hash = PasswordHasher.Hash(password);
            _repo.CreateUser(username, hash);

            MessageBox.Show("Account created successfully!");
            this.Close(); // returns to login if you opened it as dialog
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
