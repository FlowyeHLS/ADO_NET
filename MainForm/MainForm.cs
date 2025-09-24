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

namespace MainForm
{
    public partial class MainForm : Form
    {
            string connectionString = "Data Source=FLOWYE\\SQLEXPRESS;Initial Catalog=PD_321;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection;
        Dictionary<string, int> d_groupDirection;
        Dictionary<string, int> d_groups; 
        public MainForm()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            // LoadDirections();
            //LoadGroups();
            dataGridViewDirections.DataSource = Select("*", "Directions");
            dataGridViewGroups.DataSource = Select
                (
                "group_id,group_name,direction",
                "Groups,Directions",
                "direction=direction_id"
                );

            dataGridViewStudents.DataSource = Select
                (
                "stud_id, last_name, first_name, middle_name, birth_date, group_name, direction",
                "Students,Groups,Directions",
                "[group]=group_id AND direction=direction_id"
                );

            d_groupDirection = LoadDataToComboBox("*","Directions");
           // comboBoxGroupsDirection.Items.Add(d_groupDirection);
            comboBoxGroupsDirection.Items.AddRange(d_groupDirection.Keys.ToArray());
            comboBoxGroupsDirection.SelectedIndex = 0;

            //
            d_groups = LoadDataToComboBoxGroups("*", "Groups");
            comboBoxStudentsGroups.Items.AddRange(d_groups.Keys.ToArray());
            comboBoxStudentsGroups.SelectedIndex = 0;
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
        void LoadDirections()
        {
            string cmd = 
@"SELECT direction_id AS N'ID',direction_name AS N'Направление', COUNT(group_id) AS N'Колличество групп' 
FROM Groups
RIGHT JOIN Directions ON (direction=direction_id)
GROUP BY direction_id,direction_name
";
            SqlCommand command = new SqlCommand(cmd, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            for (int i = 0; i < reader.FieldCount; ++i)
                table.Columns.Add(reader.GetName(i));
            while (reader.Read())
            { 
                DataRow row = table.NewRow();
                for (int i = 0; i < reader.FieldCount; i++)
                    row[i] = reader[i];
                table.Rows.Add(row);
            }
            reader.Close();
            connection.Close();
            dataGridViewDirections.DataSource = table;
        }
        void LoadGroups()
        {
            string cmd = 
                @"SELECT
group_id AS N'ID',group_name AS N'Группа',COUNT(stud_id) AS N'Колличество студентов',direction_name AS N'Направление обучения'
FROM  Students 
RIGHT JOIN Groups       ON ([group]=group_id)
      JOIN Directions   ON (direction=direction_id)
GROUP BY group_id, group_name, direction,direction_name;
";
            SqlCommand command = new SqlCommand(cmd, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            for (int i = 0; i < reader.FieldCount; ++i)
                table.Columns.Add(reader.GetName(i));
            while (reader.Read())
            { 
                DataRow row = table.NewRow();
                for (int i = 0; i < reader.FieldCount; i++)
                    row[i] = reader[i];
                table.Rows.Add(row);
            }
            reader.Close();
            connection.Close();
            dataGridViewGroups.DataSource = table;
        }
        void LoadStudents()
        {
            string cmd =
                   @"SELECT 
stud_id AS N'ID', last_name AS N'Фамилия', first_name AS N'Имя', middle_name AS N'Фамилия', birth_date AS N'Дата Рождения', group_name AS N'Группа', direction_name AS N'Направление'
FROM Students
RIGHT JOIN Groups       ON ([group]=group_id)
      JOIN Directions   ON (direction=direction_id)
GROUP BY group_id,group_name,direction_id,direction_name;
";

            SqlCommand command = new SqlCommand(cmd, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            for (int i = 0; i < reader.FieldCount; ++i)
                table.Columns.Add(reader.GetName(i));
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                for (int i = 0; i < reader.FieldCount; i++)
                    row[i] = reader[i];
                table.Rows.Add(row);
            }
            reader.Close();
            connection.Close();
            dataGridViewStudents.DataSource = table;
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

        Dictionary<string, int> LoadDataToComboBoxGroups(string fields, string tables)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add("Все", 0); 
            string cmd = $"SELECT {fields} FROM {tables}";
            SqlCommand command = new SqlCommand(cmd, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string groupName = reader[1].ToString();
                int groupId = Convert.ToInt32(reader[0]);
                if (!dictionary.ContainsKey(groupName))
                    dictionary.Add(groupName, groupId);
            }
            reader.Close();
            connection.Close();
            return dictionary;
        }

        private void comboBoxStudentsGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            string condition = "[group]=group_id";
            if (comboBoxStudentsGroups.SelectedItem.ToString() != "Все")
                condition += $" AND [group]={d_groups[comboBoxStudentsGroups.SelectedItem.ToString()]}";
            dataGridViewStudents.DataSource = Select
                (
                "stud_id, last_name, first_name, middle_name, birth_date, group_name, direction",
                "Students,Groups,Directions",
                condition
                );
        }
    }
}
