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
    public partial class BaseHumanForm : Form
    {
        internal Human Human { get; set; }
        internal Connector connector;
        public BaseHumanForm()
        {
            InitializeComponent();
            connector = new Connector();    
            buttonBrowsePhoto.Click += new EventHandler(buttonBrowsePhoto_Click);
            buttonOK.Click += new EventHandler(buttonOK_Click);
        }
        protected virtual void Extract()
        {
            textBoxLastName.Text = Human.LastName;
            textBoxFirstName.Text = Human.FirstName;
            textBoxMiddlename.Text = Human.MiddleName;

            dateTimePicker.Text = Human.BirthDate;
            textBoxEMail.Text = Human.Email;
            textBoxPhone.Text = Human.Phone;
            pictureBoxPhoto.Image = Human.Photo;
            labelID.Visible = true;
        }
        private void buttonBrowsePhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|All image files|*.png;*.jpg|All files (*.*)|(*.*)";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxPhoto.Image = Image.FromFile(dialog.FileName);

            }
        }

        protected virtual void buttonOK_Click(object sender, EventArgs e)
        {

        }
    }
}
