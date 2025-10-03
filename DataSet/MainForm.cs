using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Data.SqlClient;
using System.Configuration;

namespace DataSet
{
    public partial class MainForm : Form
    {
        string connectionString = "";
        SqlConnection connection = null;
        System.Data.DataSet GroupsRelatedData = null;
        System.Data.DataSet DisciplinesDirectionsRelation = null;
        public MainForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["PD_321"].ConnectionString;
            connection = new SqlConnection(connectionString);
            InitGroupsRelatedData();
            InitDisciplinesDirectionsRelation();
        }
        void InitGroupsRelatedData()
        {
            //1) Создаем DataSet
            GroupsRelatedData = new System.Data.DataSet(nameof(GroupsRelatedData));

            //2) Добавляем таблицы в DataSet: 
            const string dsTable_Directions = "Directions";
            const string dstDirections_col_direction_id = "direction_id";
            const string dstDirections_col_direction_name = "direction_name";
            GroupsRelatedData.Tables.Add(dsTable_Directions);
            //Добавляем поля в таблицу 
            GroupsRelatedData.Tables[dsTable_Directions].Columns.Add(dstDirections_col_direction_id);
            GroupsRelatedData.Tables[dsTable_Directions].Columns.Add(dstDirections_col_direction_name);
            //Выбираем первичный ключ
            GroupsRelatedData.Tables[dsTable_Directions].PrimaryKey =
                new DataColumn[] { GroupsRelatedData.Tables[dsTable_Directions].Columns[dstDirections_col_direction_id] };

            const string dsTable_Groups = "Groups";
            const string dstGroups_col_group_id = "group_id";
            const string dstGroups_col_group_name = "group_name";
            const string dstGroups_col_direction = "direction";
            const string dstGroups_col_learning_days = "learning_days";
            const string dstGroups_col_start_time = "start_time";
            GroupsRelatedData.Tables.Add (dsTable_Groups);
            GroupsRelatedData.Tables[dsTable_Groups].Columns.Add(dstGroups_col_group_id);
            GroupsRelatedData.Tables[dsTable_Groups].Columns.Add(dstGroups_col_group_name);
            GroupsRelatedData.Tables[dsTable_Groups].Columns.Add(dstGroups_col_direction);
            GroupsRelatedData.Tables[dsTable_Groups].Columns.Add(dstGroups_col_learning_days);
            GroupsRelatedData.Tables[dsTable_Groups].Columns.Add(dstGroups_col_start_time);
            GroupsRelatedData.Tables[dsTable_Groups].PrimaryKey =
                new DataColumn[] { GroupsRelatedData.Tables[dsTable_Groups].Columns[dstGroups_col_group_id] };

            //3) Строим связи между таблицами: 
            string dsRelation_GroupsDirections = "GroupsDirection";
            GroupsRelatedData.Relations.Add
                (
                   dsRelation_GroupsDirections,
                   GroupsRelatedData.Tables[dsTable_Directions].Columns[dstDirections_col_direction_id],  //parent field(PrimaryKey)
                   GroupsRelatedData.Tables[dsTable_Groups].Columns[dstGroups_col_direction]    //Child field (ForeignKey)
                );

            //4) Загружаем данные в таблицу 
            string direction_cmd = "SELECT * FROM Directions";
            string groups_cmd = "SELECT * FROM Groups";

            SqlDataAdapter directionsAdapter = new SqlDataAdapter(direction_cmd,connection);
            SqlDataAdapter groupsAdapter = new SqlDataAdapter(groups_cmd,connection);

            directionsAdapter.Fill(GroupsRelatedData.Tables[dsTable_Directions]);
            groupsAdapter.Fill(GroupsRelatedData.Tables[dsTable_Groups]);

            AllocConsole();
            
            foreach(DataRow row in GroupsRelatedData.Tables[dsTable_Directions].Rows)
            {
                Console.WriteLine($"{row[dstDirections_col_direction_id]}\\t{row[dstDirections_col_direction_name]}");
            }

            Console.WriteLine("\n=============================================================\n");

            //foreach(DataRow row in GroupsRelatedData.Tables[dsTable_Directions].ChildRelations)
            //{
            //    for(int i = 0; i<row.GetChildRows(dsRelation_GroupsDirections).Length;++i)
            //    {
            //        Console.Write($"{row[i]}\t");
            //    }
            //    Console.WriteLine();
            //}
            DataRow[] RPO = GroupsRelatedData.Tables[dsTable_Directions].Rows[0].GetChildRows(dsRelation_GroupsDirections); 
            for(int i = 0;i < RPO.Length;i++)
            {
                for(int j = 0; j <RPO[i].ItemArray.Length;j++)
                {
                    Console.Write($"{RPO[i].ItemArray[j]}\t\t");
                }
                Console.WriteLine();
            }
            comboBoxStudendsGroup.DataSource = GroupsRelatedData.Tables[dsTable_Groups];
            comboBoxStudendsGroup.DisplayMember = GroupsRelatedData.Tables[dsTable_Groups].Columns[dstGroups_col_group_name].ToString();
            comboBoxStudendsGroup.ValueMember = GroupsRelatedData.Tables[dsTable_Groups].Columns[dstGroups_col_group_id].ToString();

            comboBoxStudentsDirection.DataSource = GroupsRelatedData.Tables[dsTable_Directions];
            comboBoxStudentsDirection.DisplayMember = GroupsRelatedData.Tables[dsTable_Directions].Columns[dstDirections_col_direction_name].ToString();
            comboBoxStudentsDirection.ValueMember = GroupsRelatedData.Tables[dsTable_Directions].Columns[dstDirections_col_direction_id].ToString();

            comboBoxStudendsGroup.SelectedIndexChanged += new EventHandler(GetKeyvalue);
            comboBoxStudentsDirection.SelectedIndexChanged += new EventHandler(GetKeyvalue);

        }

        void InitDisciplinesDirectionsRelation()
        {
            DisciplinesDirectionsRelation = new System.Data.DataSet(nameof(DisciplinesDirectionsRelation));
            string dstable_Disciplines = "Disciplines";
            string dstDisciplines_discipline_id = "discipline_id";
            string dstDisciplines_discipline_name = "discipline_name";
            string dstDisciplines_number_of_lessons = "number_of_lessons";
            DisciplinesDirectionsRelation.Tables.Add(dstable_Disciplines);
            DisciplinesDirectionsRelation.Tables[dstable_Disciplines].Columns.Add(dstDisciplines_discipline_id);
            DisciplinesDirectionsRelation.Tables[dstable_Disciplines].Columns.Add(dstDisciplines_discipline_name);
            DisciplinesDirectionsRelation.Tables[dstable_Disciplines].Columns.Add(dstDisciplines_number_of_lessons);
            DisciplinesDirectionsRelation.Tables[dstable_Disciplines].PrimaryKey =
                new DataColumn[] { DisciplinesDirectionsRelation.Tables[dstable_Disciplines].Columns[dstDisciplines_discipline_id] };

            string dsTable_Directions = "Directions";
            string dsDirections_direction_id = "direction_id";
            string dsDirections_direction_name = "direction_name";
            DisciplinesDirectionsRelation.Tables.Add(dsTable_Directions);
            DisciplinesDirectionsRelation.Tables [dsTable_Directions].Columns.Add(dsDirections_direction_id);
            DisciplinesDirectionsRelation.Tables[dsTable_Directions].Columns.Add(dstDisciplines_discipline_name);
            DisciplinesDirectionsRelation.Tables[dsTable_Directions].PrimaryKey =
                new DataColumn[] { DisciplinesDirectionsRelation.Tables[dsTable_Directions].Columns[dstDisciplines_discipline_id] };

            string dstable_DDR = "DisciplinesDirectionsRelation";
            string dstDDR_discipline = "discipline";
            string dstDDR_direction = "direction";
            DisciplinesDirectionsRelation.Tables.Add(dstable_DDR);
            DisciplinesDirectionsRelation.Tables[dstable_DDR].Columns.Add(dstDDR_direction);
            DisciplinesDirectionsRelation.Tables[dstable_DDR].Columns.Add(dstDDR_discipline);
            DisciplinesDirectionsRelation.Tables[dstable_DDR].PrimaryKey =
                new DataColumn[]
                {
                    DisciplinesDirectionsRelation.Tables[dstable_DDR].Columns[dstDDR_discipline],
                    DisciplinesDirectionsRelation.Tables[dstable_DDR].Columns[dstDDR_direction]
                };

            string dsrDiscipline = "Discipline";
            string dsrDirection = "Direction";
            DisciplinesDirectionsRelation.Relations.Add
                (
                new DataRelation
                (
                    dsrDiscipline,
                    DisciplinesDirectionsRelation.Tables[dstable_Disciplines].Columns[dstDisciplines_discipline_id],
                    DisciplinesDirectionsRelation.Tables[dstable_DDR].Columns[dstDDR_discipline]
                    )
                );
            DisciplinesDirectionsRelation.Relations.Add
                (
                new DataRelation
                    (
                    dsrDirection,
                    DisciplinesDirectionsRelation.Tables[dsTable_Directions].Columns[dsDirections_direction_id],
                    DisciplinesDirectionsRelation.Tables[dstable_DDR].Columns[dstDDR_direction]
                    )
                );

            string cmd_disciplines = "SELECT * FROM Disciplines";
            string cmd_directions = "SELECT * FROM Directions";
            string cmd_DDR = $"SELECT * FROM {dstable_DDR}";

            SqlDataAdapter disciplinesAdapter = new SqlDataAdapter(cmd_disciplines, connection);
            SqlDataAdapter directionAdapter = new SqlDataAdapter(cmd_directions, connection);
            SqlDataAdapter DDRAdapetr = new SqlDataAdapter(cmd_DDR, connection);

            disciplinesAdapter.Fill(DisciplinesDirectionsRelation.Tables[dstable_Disciplines]);
            directionAdapter.Fill(DisciplinesDirectionsRelation.Tables[dsTable_Directions]);
            DDRAdapetr.Fill(DisciplinesDirectionsRelation.Tables[dstable_DDR]);

            //////////////////////////////////////////////////////////////////////////
            ///
            dataGridViewDisciplines.DataSource = DisciplinesDirectionsRelation.Tables[dstable_Disciplines];
            comboBoxDisciplinesForDirection.DataSource = DisciplinesDirectionsRelation.Tables[dsTable_Directions];
            comboBoxDisciplinesForDirection.DisplayMember = DisciplinesDirectionsRelation.Tables[dsTable_Directions].Columns[dsDirections_direction_name].ToString();
            comboBoxDisciplinesForDirection.ValueMember = DisciplinesDirectionsRelation.Tables[dsTable_Directions].Columns[dsDirections_direction_id].ToString();

        }
        void GetKeyvalue(object sender, EventArgs e)
        {
            Console.WriteLine($"{(sender as ComboBox).Name}:\t{(sender as ComboBox).SelectedValue}");
        }

        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

        private void comboBoxStudentsDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
           // comboBoxStudendsGroup.DataSource = GroupsRelatedData.Tables["Groups"];
            try
            {
                comboBoxStudendsGroup.Enabled = true;
                comboBoxStudendsGroup.DataSource =
                        GroupsRelatedData.Tables["Groups"].Select
                        (
                        $"direction={comboBoxStudentsDirection.SelectedValue}"
                        ).CopyToDataTable();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Группы отсутсвуют", MessageBoxButtons.OK);
               // comboBoxStudendsGroup.DataSource = null;
                //comboBoxStudendsGroup.Items.Clear();
                comboBoxStudendsGroup.Enabled = false;
            }
        }

        private void comboBoxDisciplinesForDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////1)Получаем набор значений из связующей таблицы
            //DataRow[] ddr = DisciplinesDirectionsRelation.Tables["DisciplinesDirectionsRelation"]
            //    .Select($"direction={comboBoxDisciplinesForDirection.SelectedValue}");

            ////2)Клонируем таблицу с дисциплинами
            //DataTable dtDisciplinesForDirection = DisciplinesDirectionsRelation.Tables["Disciplines"].Clone();
            //foreach (DataRow row in ddr)
            //{
            //    DataRow discipline = DisciplinesDirectionsRelation.Tables["Disciplines"].Rows.Find(row["discipline"]);
            //    dtDisciplinesForDirection.ImportRow(discipline);
            //}


            ////4)Отображаем выбранные дисциплины:
            //dataGridViewDisciplines.DataSource = dtDisciplinesForDirection;


            /////////////////////////////////////////////////////////////////////////////////////////////////////

            //DataRow[] ddr = DisciplinesDirectionsRelation.Tables["DisciplinesDirectionsRelation"]
            //    .Select($"direction={comboBoxDisciplinesForDirection.SelectedValue}");
            //DataTable dtDisciplines = DisciplinesDirectionsRelation.Tables["Disciplines"].Clone();

            //object[] discipline_ids = ddr.Select(row => row["discipline"]).Distinct().ToArray();
            //string filter = $"discipline_id IN ({string.Join(",", discipline_ids)})";
            //dataGridViewDisciplines.DataSource =
            //    DisciplinesDirectionsRelation.Tables["Disciplines"].Select(filter).CopyToDataTable();


            //var disciplines = from discipline in DisciplinesDirectionsRelation.Tables["Disciplines"] select(" ");
            //Console.WriteLine(disciplines.GetType());
            
        }
    }
}
