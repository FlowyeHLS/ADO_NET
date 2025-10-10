using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Configuration;

namespace MainForm
{
    public partial class MainForm : Form
    {
        string connectionString = "Data Source=MSI-WS\\SQLEXPRESS;Initial Catalog=PD_321;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection;
        Connector connector;
        Dictionary<string, int> d_groupDirection;

        Query[] queries = new Query[]
        {
            new Query
            (
"stud_id,FORMATMESSAGE(N'%s %s %s',last_name,first_name,middle_name) AS N'Студент',group_name AS N'Группа',direction_name AS N'Направление'",
"Students,Groups,Directions",
"[group]=group_id AND direction=direction_id"
            ),
            new Query
            (
            "group_id,group_name,learning_days,start_time,direction_name",
            "Groups,Directions",
            "direction=direction_id"
            ),
            new Query("*","Directions"),
            new Query("*","Disciplines"),
            new Query("*","Teachers")
        };

        readonly string[] statusBarMessages = new string[]
        {
            "Количество студентов ",
            "Количество групп ",
            "Количество направлений ",
            "Количество дисциплин ",
            "Количество преподавателей "
        };

        public MainForm()
        {
            InitializeComponent();
            AllocConsole();
            connectionString = ConfigurationManager.ConnectionStrings["PD_321"].ConnectionString;
            Console.WriteLine(connectionString);
            connection = new SqlConnection(connectionString);
            connector = new Connector();
            // LoadDirections();
            //LoadGroups();
            Console.WriteLine( this.Name);
            Console.WriteLine(tabControl.TabCount);
            
            d_groupDirection = LoadDataToComboBox("*","Directions");
            comboBoxGroupsDirection.Items.AddRange(d_groupDirection.Keys.ToArray());
            comboBoxGroupsDirection.SelectedIndex = 0;

            tabControl.SelectedIndex = 1;

            for(int i =0; i<tabControl.TabCount; ++i)
            {
                (this.Controls.Find($"dataGridView{tabControl.TabPages[i].Name.Remove(0, "tabPage".Length)}",true)[0] as DataGridView).RowsAdded
                    += new DataGridViewRowsAddedEventHandler(this.dataGridViewChanged);
            }    
        }
        void LoadTab(int i)
        {
            string tableName = tabControl.TabPages[i].Name.Remove(0,"tabPage".Length);
            DataGridView dataGridView = this.Controls.Find($"dataGridView{tableName}", true)[0] as DataGridView;
            //dataGridView.DataSource = Select("*", tableName);
            dataGridView.DataSource = connector.Select(queries[i].Fields, queries[i].Tables, queries[i].Condition);
            //toolStripStatusLabel.Text = $"{statusBarMessages[i]}: {dataGridView.RowCount - 1}";
            if (i == 1) ConvertLearningDays();
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ReadOnly = true;
        }
        void FillStatusBar(int i)
        {
            
        }
        DataTable Select(string fields, string tables,string condition = "")
        {
            DataTable table = new DataTable();
            string cmd = $@"SELECT {fields} FROM {tables}";
            if (!string.IsNullOrWhiteSpace(condition)) cmd += $" WHERE {condition}";
            cmd += ";";

            SqlCommand command = new SqlCommand(cmd,connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            for(int i = 0; i < reader.FieldCount;++i)
                table.Columns.Add(reader.GetName(i));
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                for (int i = 0; i < reader.FieldCount; ++i) row[i] = reader[i];
                table.Rows.Add(row);    
            }
            reader.Close();
            connection.Close();
            return table;
        }

        void Insert(string table, string fields, string values)
        {
            string cmd = $"INSERT {table}({fields}) values ({values})";
            SqlCommand command = new SqlCommand(cmd,connection);
            connection.Open();
            command.ExecuteNonQuery();  
            connection.Close();
        }

        void ConvertLearningDays()
        {
            for (int i = 0; i < dataGridViewGroups.RowCount; ++i)
            {
                object cellValue = dataGridViewGroups.Rows[i].Cells["learning_days"].Value;
                byte learningDaysValue;

                if (cellValue != null && cellValue != DBNull.Value)
                {
                    learningDaysValue = Convert.ToByte(cellValue);
                }
                else
                {
                    // Значение отсутствует, задаем по умолчанию, например 0
                    learningDaysValue = 0;
                }

                dataGridViewGroups.Rows[i].Cells["learning_days"].Value =
                    new Week(learningDaysValue);
            }
        }
        Dictionary<string,int> LoadDataToComboBox(string fields, string tables)
        {
            Dictionary<string,int> dictionary = new Dictionary<string, int>();
            dictionary.Add("Все", 0);
            string cmd = $"SELECT {fields} FROM {tables}";
            SqlCommand command = new SqlCommand(cmd, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                //comboBoxGroupsDirection.Items.Add(reader[1]);
                dictionary.Add(reader[1].ToString(), Convert.ToInt32(reader[0]));
            }
            reader.Close();
            connection.Close();
            return dictionary;
        }

        private void comboBoxGroupsDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string condition = "direction=direction_id";
            if (comboBoxGroupsDirection.SelectedItem.ToString() != "Все")
                condition+= $" AND direction={d_groupDirection[comboBoxGroupsDirection.SelectedItem.ToString()]}";
            dataGridViewGroups.DataSource = Select
                (
                "group_id,group_name,direction",
                "Groups,Directions",
                condition
                );
        }
        [DllImport("kernel32.dll")]
        static extern void AllocConsole();

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTab((sender as TabControl).SelectedIndex);
        }
        private void dataGridViewChanged(object sender,EventArgs e)
        {
            toolStripStatusLabel.Text = $"{statusBarMessages[tabControl.SelectedIndex]}: {(sender as DataGridView).RowCount - 1}";
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormAddGroups formAdd = new FormAddGroups();
            
            if(formAdd.ShowDialog() == DialogResult.OK)
            {
                LoadTab(1);
                MessageBox.Show("Группа добавленна");                
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddStudents_Click(object sender, EventArgs e)
        {
            StudentForm student = new StudentForm();
            DialogResult result = student.ShowDialog();
            if (result == DialogResult.OK)
            {
                connector.
                Insert
                    (
                    "Students",
                    "last_name,first_name,middle_name,birth_date,email,phone,[group]",
                    student.Student.ToString()
                    );
                int id = Convert.ToInt32(connector.Scalar("SELECT MAX(stud_id) FROM Students"));
                connector.UploadPhoto(student.Student.SerializePhoto(), id, "photo", "Students");
            }
        }



        private void InitializeColumnCheckBox()
        {
            checkedListBoxVisible.Items.Clear();
            foreach(DataGridViewColumn column in dataGridViewGroups.Columns)
            {
                checkedListBoxVisible.Items.Add(column.HeaderText, column.Visible);
            }
        }

        private void checkedListBoxVisible_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                string headerText = checkedListBoxVisible.Items[e.Index].ToString();
                DataGridViewColumn column = dataGridViewGroups.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.HeaderText == headerText);
                if(column != null)
                {
                    column.Visible = e.NewValue == CheckState.Checked;
                }
            }
            );
        }

        private void dataGridViewStudents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i = Convert.ToInt32(dataGridViewStudents.SelectedRows[0].Cells[0].Value);
            //StudentForm student = new StudentForm(i);
            //DialogResult result = student.ShowDialog();
            //if (result == DialogResult.OK)
            //{

            //    connector.Update
            //        (
            //        "Students",
            //        student.Student.ToStringUpdate(),
            //        $"stud_id = {i}"
            //        );
            //    connector.UploadPhoto(student.Student.SerializePhoto(), i, "photo", "students");
            //    LoadTab(0);
            //}
            DerivedStudentForm student = new DerivedStudentForm(i);
            DialogResult result = student.ShowDialog();
            if (result == DialogResult.OK)
            { 

                connector.Update
                    (
                     "Students",
                    (student.Human as Student).ToStringUpdate(),
                    $"stud_id = {i}"
                    );
                connector.UploadPhoto((student.Human as Student).SerializePhoto(), i, "photo", "students");
            }
        }
    }
}
