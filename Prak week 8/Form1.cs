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
        public static string sqlconnection = "server=localhost;uid=root;pwd=;database=premier_league";
        public MySqlConnection sqlConnect = new MySqlConnection(sqlconnection);
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter sqlAdapter;
        string sqlQuery;
        private void FormHasil_Load(object sender, EventArgs e)
        {
            DataTable dtTeamHome = new DataTable();
            sqlQuery = "select team_name as `Team Name`, t.team_id as `Team ID`, m.manager_name as `Nama Man`, p.player_name from team t, manager m, player p where m.manager_id = t.manager_id and t.captain_id = p.player_id";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeamHome);
            DataTable dtTeamAway = new DataTable();
            sqlQuery = "select team_name as `Team Name`, t.team_id as `Team ID`, m.manager_name as `Nama Man`, p.player_name from team t, manager m, player p where m.manager_id = t.manager_id and t.captain_id = p.player_id";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeamAway);
            comboBoxShow1.DataSource = dtTeamHome;
            comboBoxShow1.DisplayMember = "Team Name";
            comboBoxShow1.ValueMember = "Team ID";
            comboBoxShow2.DataSource = dtTeamAway;
            comboBoxShow2.DisplayMember = "Team Name";
            comboBoxShow2.ValueMember = "Team ID";
        }

        private void comboBoxShow1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTeam1 = new DataTable();
                sqlQuery = "SELECT t.team_name as `Team Name`, m.manager_name as `Manager Name`, p.player_name, t.team_id as `Team ID` , t.capacity, concat(t.home_stadium, ',', t.city) " + "FROM team t, manager m, player p WHERE m.manager_id = t.manager_id and t.captain_id = p.player_id and t.team_id = '" + comboBoxShow1.SelectedValue.ToString() + "'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtTeam1);

                labelShowManager1.Text = dtTeam1.Rows[0][1].ToString();
                labelShowCaptain1.Text = dtTeam1.Rows[0][2].ToString();
                labelShowStadium.Text = dtTeam1.Rows[0][5].ToString();
                labelShowCapacity.Text = dtTeam1.Rows[0][4].ToString();
            }
            catch (Exception)
            {


            }
        }

        private void comboBoxShow2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable timlawan = new DataTable();
                sqlQuery = "select t.team_name as `Team Name`, m.manager_name as `Manager Name`, p.player_name, t.team_id as `Team ID` FROM team t, manager m, player p where m.manager_id = t.manager_id and t.captain_id = p.player_id and t.team_id = '" + comboBoxShow2.SelectedValue.ToString() + "'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(timlawan);

                labelShowManager2.Text = timlawan.Rows[0][1].ToString();
                labelShowCaptain2.Text = timlawan.Rows[0][2].ToString();
            }
            catch (Exception)
            {


            }
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtMatch1 = new DataTable();
                DataTable dtMatch2 = new DataTable();
                sqlQuery = $"select m.match_id , date_format(m.match_date, '%d %M %Y' ), concat(m.goal_home, ' - ', m.goal_away) as 'Skor' FROM `match` m where m.team_home = '{comboBoxShow1.SelectedValue}' AND m.team_away = '{comboBoxShow2.SelectedValue}';";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtMatch2);
                labelShowTgl.Text = dtMatch2.Rows[0][1].ToString();
                labelShowSkor.Text = dtMatch2.Rows[0][2].ToString();
                sqlQuery = "select d.minute as Minute, p.player_name as 'Player Name 1', if (d.type = 'CY', 'YELLOW CARD', if (d.type = 'CR', 'RED CARD', if (d.type = 'GO', 'GOAL', if (d.type = 'GP', 'GOAL PENALTY', if (d.type = 'GW', 'OWN GOAL', 'PENALTY MISS'))))) as 'Type 1', p2.player_name as 'Player Name 2', if (d2.type = 'CY', 'YELLOW CARD', if (d2.type = 'CR', 'RED CARD', if (d2.type = 'GO', 'GOAL', if (d2.type = 'GP', 'GOAL PENALTY', if (d2.type = 'GW', 'OWN GOAL', 'PENALTY MISS'))))) as 'Type 2' from dmatch d, dmatch d2, player p, player p2 where p.player_id = d.player_id and p2.player_id = d2.player_id and d.match_id = " + dtMatch2.Rows[0][0].ToString() + " and d2.match_id = " + dtMatch2.Rows[0][0].ToString() + " ";

                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtMatch1);
                dataGridViewOutput.DataSource = dtMatch1;
            }
            catch (Exception)
            {


            }
        }
    }
}
