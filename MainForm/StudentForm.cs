using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;

namespace MainForm
{
    public partial class StudentForm : Form
    {
        internal Student Student { get; set; }

        Connector connector;
        public StudentForm()
        {
            InitializeComponent();
            connector = new Connector();
            DataTable groups = connector.Select("*", "Groups");
            comboBoxGroup.DataSource = groups;
            comboBoxGroup.DisplayMember = groups.Columns[1].ToString();
            comboBoxGroup.ValueMember = groups.Columns[0].ToString();
            InitForm();
        }
        public StudentForm(int stud_id):this()
        {
            //string[] fullName = row[1].ToString().Split(' ');
            //textBoxLastName.Text = fullName[0];
            //textBoxFirstName.Text = fullName[1];
            //textBoxMiddlename.Text = fullName[2];

            //dateTimePicker.Text = row[2].ToString();
            //textBoxEMail.Text = row[3].ToString();
            //textBoxPhone.Text = row[6].ToString();
            //comboBoxGroup.SelectedValue = row[7];
            //int i = Convert.ToInt32(row[0]);
            DataTable student = connector.Select("*", "Students", $"stud_id={stud_id}");

            textBoxLastName.Text = student.Rows[0][1].ToString();
            textBoxFirstName.Text = student.Rows[0][2].ToString();
            textBoxMiddlename.Text = student.Rows[0][3].ToString();

            dateTimePicker.Value = Convert.ToDateTime(student.Rows[0][4]);
            textBoxEMail.Text = student.Rows[0][5].ToString(); 
            textBoxPhone.Text = student.Rows[0][6].ToString();
            comboBoxGroup.SelectedValue = student.Rows[0][8].ToString();

            labelID.Visible = true;
            labelID.Text = $"ID: {student.Rows[0][0].ToString()}";
        }
        void InitForm()
        {
            textBoxLastName.Text = "Леонтьева";
            textBoxFirstName.Text = "Шарлотта";
            textBoxMiddlename.Text = "Владимировна";
            dateTimePicker.Value = new DateTime(2007, 07, 08); 
            textBoxEMail.Text = "sharlotta@gmail.com";
            textBoxPhone.Text = "+7(123)999-12-34";
            comboBoxGroup.SelectedIndex = 10;
            //Group = Convert.ToInt32(comboBoxGroup.SelectedValue);
        }
        void Compress()
        {
            Student.LastName = textBoxLastName.Text;
            Student.FirstName = textBoxFirstName.Text;
            Student.LastName = textBoxMiddlename.Text;
            Student.Email = textBoxEMail.Text;
            Student.Phone = textBoxPhone.Text;
            Student.Group = Convert.ToInt32(comboBoxGroup.SelectedValue);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Student = new Student
                (
                textBoxLastName.Text
                ,textBoxFirstName.Text
                ,textBoxMiddlename .Text
                ,dateTimePicker.Text
                ,textBoxEMail.Text
                ,textBoxPhone.Text
                ,Convert.ToInt32(comboBoxGroup.SelectedValue),
                pictureBoxPhoto.Image
                );
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
    }
}
