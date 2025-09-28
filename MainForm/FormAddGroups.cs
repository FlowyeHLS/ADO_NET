using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class FormAddGroups : Form
    {
        string connectionString = "Data Source=FLOWYE\\SQLEXPRESS;Initial Catalog=PD_321;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public FormAddGroups()
        {
            InitializeComponent();
            //LoadDisciplinesToComboBox();
            LoadDirectionToComboBox();
        }

        public string GroupName
        {
            get { return textGroupsAddNew.Text; }
        }

        Query disciplineQuery = new Query
        (
            fields: "discipline_id,discipline_name",
            tables: "Disciplines"
        );

        Query directionQuery = new Query
        (
            fields: "direction_id,direction_name",
            tables: "Directions"
        );

        //private void LoadDisciplinesToComboBox()
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            FormAddGroups formAddGroups = new FormAddGroups();
        //            string queryString = $"SELECT {disciplineQuery.Fields} FROM {disciplineQuery.Tables}";
        //            SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
        //            DataTable dataTable = new DataTable();
        //            adapter.Fill(dataTable);

        //            comboBoxAddDirection.DataSource = dataTable;
        //            comboBoxAddDirection.DisplayMember = "discipline_name";
        //            comboBoxAddDirection.ValueMember = "discipline_id";
        //            comboBoxAddDirection.SelectedIndex = -1;
        //        }
        //    }
        //    catch (Exception exeption)
        //    {
        //        MessageBox.Show("Ошибка загр дисциплины: " + exeption.Message);
        //    }

        //}

        private void LoadDirectionToComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = $"SELECT {directionQuery.Fields} FROM {directionQuery.Tables}";
                    SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    comboBoxAddDirection.DataSource = dataTable;
                    comboBoxAddDirection.DisplayMember = "direction_name";
                    comboBoxAddDirection.ValueMember = "direction_id";
                    comboBoxAddDirection.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загр директион: " + ex.Message);
            }
        }



        private void buttonOkey_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textGroupsAddNew.Text))
            {
                MessageBox.Show("Пожалуйста, введите название группы.");
                return;
            }

            if (comboBoxAddDirection.SelectedIndex == -1) 
            {
                MessageBox.Show("Пожалуйста, выберите направление.");
                return;
            }

            string groupName = textGroupsAddNew.Text;
            int directionId = Convert.ToInt32(comboBoxAddDirection.SelectedValue);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    int newGroupId = 0;
                    string getMaxIdQuery = "SELECT ISNULL(MAX(group_id), 0) FROM Groups";
                    SqlCommand cmdGetMaxId = new SqlCommand(getMaxIdQuery, connection);
                    newGroupId = (int)cmdGetMaxId.ExecuteScalar() + 1;

                    string insertQuery = "INSERT INTO Groups (group_id, group_name, direction) VALUES (@GroupId, @GroupName, @DirectionId)";
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@GroupId", newGroupId);
                    command.Parameters.AddWithValue("@GroupName", groupName);
                    command.Parameters.AddWithValue("@DirectionId", directionId);

                    command.ExecuteNonQuery();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении группы: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
