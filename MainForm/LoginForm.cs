﻿using System;
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
    public partial class LoginForm : Form
    {
        public LoginData LoginData { get; set; }
        public LoginForm()
        {
            InitializeComponent();
            LoginData = new LoginData();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            LoginData.Server = textBoxServer.Text;
            LoginData.Login = textBoxLogin.Text;
            LoginData.Password = textBoxPassword.Text;
        }
    }
}
