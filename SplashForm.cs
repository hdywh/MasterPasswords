using PasswordManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterPasswords
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaster.Text))
            {
                lblError.Text = "Введите master password";
                return;
            }

            if (!PasswordStorage.CheckMasterPassword(txtMaster.Text))
            {
                lblError.Text = "Неверный пароль";
                return;
            }

            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
             

            Application.Exit();
        }
    }
    }

