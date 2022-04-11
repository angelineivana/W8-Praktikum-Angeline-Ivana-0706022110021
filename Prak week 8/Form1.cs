using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Prak_week_8
{
    public partial class FormHasil : Form
    {
        public FormHasil()
        {
            InitializeComponent();
        }
        public static string sqlConnection = "server=localhost;uid=root;pwd=;database=premier_league";
        public MySqlConnection sqlConnect = new MySqlConnection(sqlConnection);//sbg data koneksi ke dbms nya
        public MySqlCommand sqlCommand;//mengirimkan query baru dr workbench ke database
        public MySqlDataAdapter sqlAdapter;//hasil dari query disimpan ke sqlAdapter, hasil retrieving data menggunakan select
        public static string sqlQuery;
        public static int cek = 0;
        private void FormHasil_Load(object sender, EventArgs e)
        {
            DataTable dtTeam1 = new DataTable();
            sqlQuery = "select team_name as 'Team Name', team_id as 'Team ID' from team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeam1);
            comboBoxShow1.DataSource = dtTeam1;
            comboBoxShow1.DisplayMember = "Team Name";
            comboBoxShow1.ValueMember = "Team Name";

            DataTable dtTeam2 = new DataTable();
            sqlQuery = "select team_name as 'Team Name' from team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeam2);
            comboBoxShow2.DataSource = dtTeam2;
            comboBoxShow2.DisplayMember = "Team Name";
            comboBoxShow2.ValueMember = "Team Name";
        }

        private void comboBoxShow1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                DataTable dtMC1 = new DataTable();
                sqlQuery = "select m.manager_name as 'Manager Name', p.player_name as 'Player Name' from manager m, player p, team t where t.manager_id = m.manager_id and t.team_name = '" + comboBoxShow1.SelectedValue.ToString() + "' and p.player_id =  t.captain_id";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtMC1);

                labelShowManager1.Text = dtMC1.Rows[0][0].ToString();
                labelShowCaptain1.Text = dtMC1.Rows[0][1].ToString();
                cek++;
            }
            catch (Exception)
            {

            }
        }

        private void comboBoxShow2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtMC2 = new DataTable();
                sqlQuery = "select m.manager_name as 'Manager Name', p.player_name as 'Player Name' from manager m, player p, team t where t.manager_id = m.manager_id and t.team_name = '" + comboBoxShow2.SelectedValue.ToString() + "' and p.player_id =  t.captain_id";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtMC2);

                labelShowManager2.Text = dtMC2.Rows[0][0].ToString();
                labelShowCaptain2.Text = dtMC2.Rows[0][1].ToString();
                cek++;
                if (cek % 2 == 0)
                {
                    //buat stadium & capacity
                    DataTable dtSC = new DataTable();
                    sqlQuery = "select concat(t.home_stadium, ', ', t.city) as 'Stadium', t.capacity as 'Capacity' from team t where t.team_name = '" + comboBoxShow1.SelectedValue.ToString() + "' ";
                    sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                    sqlAdapter = new MySqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(dtSC);

                    labelShowStadium.Text = dtSC.Rows[0][0].ToString();
                    labelShowCapacity.Text = dtSC.Rows[0][1].ToString();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
