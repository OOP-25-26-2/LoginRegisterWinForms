using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginRegisterWinForms
{
    public partial class MainForm : Form
    {
        private readonly string _username;

        public MainForm(string username)
        {
            InitializeComponent();
            _username = username;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {_username}!";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart(); // simplest logout for beginners
        }
    }
}
