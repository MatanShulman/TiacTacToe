using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.ServiceReference1;

namespace Client
{
    public partial class historyForm : Form
    {

        private ServiceClient server;
        private int playerId;       
        private MyGames[] games;
      
        //main constructor
        public historyForm(Form parent, ServiceClient server, int playerId)
        {
          
            InitializeComponent();
            this.Location = parent.Location;
            this.server = server;
            this.playerId = playerId;
        }

        //onload get games from server
        private void historyForm_Load(object sender, EventArgs e)
        {
            games = server.getPlayerGames(playerId);
            
           
            foreach (var item in games)
            {
                comboBox.Items.Add(item.gameId + " || " + item.firstPlayer + " VS " + item.secondPlayer +" Mode: "+ item.gameMode);
            }
            comboBox.SelectedIndex = 0;
            
        }

        //choose game to watch
        private void submit_btn_Click(object sender, EventArgs e)
        {
            gameBoardForm.viewButton = true;
            gameBoardForm.gameId = games[comboBox.SelectedIndex].gameId;
            string mode = games[comboBox.SelectedIndex].gameMode.Trim();
            gameBoardForm temp = new gameBoardForm(server, -1, -1, mode, true);
            
            this.Hide();
            temp.ShowDialog(this);
            this.Close();

        }
      

      
    }
}
