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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            RefreshGrid();
        }
        void RefreshGrid()
        {
                dgv.DataSource = null;
                dgv.DataSource = PasswordStorage.Passwords;

                dgv.Columns["Password"].Visible = chkShow.Checked;
            
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtService.Text) ||
             string.IsNullOrWhiteSpace(txtLogin.Text) ||
             string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            PasswordStorage.Passwords.Add(new PasswordEntry
            {
                Service = txtService.Text.Trim(),
                Login = txtLogin.Text.Trim(),
                Password = txtPassword.Text
            });

            PasswordStorage.Save();
            RefreshGrid();

            // очистка полей после сохранения
            txtService.Clear();
            txtLogin.Clear();
            txtPassword.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null || dgv.CurrentRow.Index < 0)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            int index = dgv.CurrentRow.Index;

            PasswordStorage.Passwords.RemoveAt(index);
            PasswordStorage.Save();
            RefreshGrid();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null) return;
            Clipboard.SetText(
                PasswordStorage.Passwords[dgv.CurrentRow.Index].Password);
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            btnCopy.Visible = chkShow.Checked;
            RefreshGrid();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON (*.json)|*.json";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PasswordStorage.Load(ofd.FileName);
                RefreshGrid();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PasswordStorage.Save(); // на всякий случай

            Application.Exit();
        }
    }
}
