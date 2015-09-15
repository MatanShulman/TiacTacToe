using Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{

    public partial class onlinePlayersForm : Form
    {

        public int playerId { get; set; }
        public string playerName { get; set; }

        private ServiceClient server;
        private Players[] avilablePlayers;
        private int rivalId;
        private string mode;

        //main constructor
        public onlinePlayersForm(Form parent, ServiceClient server, int id, string playerName)
        {
            InitializeComponent();
            this.Location = parent.Location;
            playerId = id;
            this.server = server;
            this.playerName = playerName;
        }

        //onload show online players
        private void onlinePlayersForm_Load(object sender, EventArgs e)
        {
            loadPlayerList();

        }

        //submit invite to other player or PC
        private void Submit_Click(object sender, EventArgs e)
        {

            replayTxt_btn.Text = "please wait...";
            if (comboBox.SelectedIndex != 0)
            {
                Players rival = avilablePlayers[comboBox.SelectedIndex - 1];
                rivalId = rival.playerId;
            }
            else
                rivalId = 0;


            var checkedButton = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
            server.sendMsgToRival(playerId, rivalId, playerName, checkedButton.Text);
            this.Hide();
            waitingForm wf = new waitingForm(this);
            wf.ShowDialog();

            mode = checkedButton.Text;

            if (waitingForm.ansFromRival)
            {
                this.Close();


                Thread th = new Thread(startGameBoard);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else
            {
                loadPlayerList();
                this.Show();
                replayTxt_btn.Text = "the other player has reject your offer please select new one ";

            }
        }

        //start game in new thread
        private void startGameBoard(object obj)
        {

            Application.Run(MainMenu.activeGame = new gameBoardForm(server, playerId, rivalId, mode, true));

        }

        //load the player list from server
        private void loadPlayerList()
        {
            avilablePlayers = server.getAviableOnlinePlayers(playerId);
            comboBox.Items.Clear();
            comboBox.Items.Add(0 + " || Play Against PC");
            comboBox.SelectedIndex = 0;
            foreach (var item in avilablePlayers)
            {
                comboBox.Items.Add(item.playerId + " || " + item.firstName);
            }
            easy_btn.Checked = true;

        }

    }


}
