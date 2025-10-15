using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class SystemLog : Form
    {
        public SystemLog()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "Admin" && textBoxPassword.Text == "12345")
            {
                MainForm s = new MainForm();
                s.Show();

                this.Hide();
            }
            else
            {
                textBoxLogin.Text = "";
                textBoxPassword.Text = "";

                MessageBox.Show("Log or paw dont true");
            }

        }
    }
}
