using Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainMenu : Form
    {
        public static string playerName { get; set; }
        public static gameBoardForm activeGame;

        public int playerId { get; set; }
        public static bool inGame = false;

        private ServiceClient server;


        //main constructor
        public MainMenu(Form parent, ServiceClient server, int playerId)
        {
            InitializeComponent();
            this.Location = parent.Location;
            this.playerId = playerId;
            this.server = server;
        }

        //onload set player name
        private void MainMenu_Load(object sender, EventArgs e)
        {
            server.regToServer(playerId);
            playerName = server.getPlayerName(playerId);
            label1.Text = "Welcome " + playerName;

        }

        //enter online game chooser
        private void online_btn_Click(object sender, EventArgs e)
        {
            if (!inGame)
            {
                this.Hide();
                onlinePlayersForm temp = new onlinePlayersForm(this, server, playerId, playerName);
                temp.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Please Finish Previous Game Before Starting A New One");

        }

        //onclose check if theres an active game and close it + logout player
        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (activeGame != null)
                activeGame.Close();
            server.logOut(playerId);

        }



        //Open a new history form
        private void history_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            historyForm hf = new historyForm(this, server, playerId);
            hf.ShowDialog();
            gameBoardForm.viewButton = false; ;
            this.Show();

        }
        //open query form
        private void queries_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            chooseQueriesForm qf = new chooseQueriesForm(this, playerId, playerName, server);
            qf.ShowDialog();
            this.Show();
        }


        // reload player name
        private void MainMenu_VisibleChanged(object sender, EventArgs e)
        {
            label1.Text = "Welcome " + playerName;
        }

        //logout
        private void LogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutUsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutUs about = new AboutUs();
            about.ShowDialog(this);
            this.Show();
        }

    }
}
