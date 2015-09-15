using Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    /*
    /Choose a querie from this 'from' and will show the server  answer in the main 'answerQuerie' form
    */
    public partial class chooseQueriesForm : Form
    {
        public int playerId { get; set; }
        public string playerName { get; set; }
        private ServiceClient server { get; set; }
        public static bool slowServer { get; set; }
        
        public chooseQueriesForm(Form parent, int playerid,string playername,ServiceClient Server) 
        {
            InitializeComponent();
            this.Location = parent.Location;
            this.playerId = playerid;
            this.playerName = playername;
            this.server = Server;
            slowServer = false;
        }

        private void firstQ_btn_Click(object sender, EventArgs e){
            this.Hide();
 
                answerQuerieForm aqf = new answerQuerieForm(playerId, playerName, server, 1);
                aqf.ShowDialog();
              

        
          
         
            this.Show();
    }

        private void secondQ_btn_Click(object sender, EventArgs e)
        {
            this.Hide();

            answerQuerieForm aqf = new answerQuerieForm(playerId, playerName, server, 2);
            aqf.ShowDialog();

            this.Show();
        }

    

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            slowServer = !slowServer;
        }

        private void thiredQ_btn_Click(object sender, EventArgs e)
        {
            this.Hide();

            answerQuerieForm aqf = new answerQuerieForm(playerId, playerName, server, 3);
            aqf.ShowDialog();

            this.Show();
        }

        private void forthQ_btn_Click(object sender, EventArgs e)
        {
            this.Hide();

            answerQuerieForm aqf = new answerQuerieForm(playerId, playerName, server, 4);
            aqf.ShowDialog();

            this.Show();

        }

        private void fifthQ_btn_Click(object sender, EventArgs e)
        {
            this.Hide();

            answerQuerieForm aqf = new answerQuerieForm(playerId, playerName, server, 5);
            aqf.ShowDialog();

            this.Show();

        }

        private void sixthQ_btn_Click(object sender, EventArgs e)
        {
            this.Hide();

            answerQuerieForm aqf = new answerQuerieForm(playerId, playerName, server, 6);
            aqf.ShowDialog();

            this.Show();
        }

        private void sevethQ_btn_Click(object sender, EventArgs e)
        {
            this.Hide();

            answerQuerieForm aqf = new answerQuerieForm(playerId, playerName, server, 7);
            aqf.ShowDialog();

            this.Show();
        }

        private void eighthQ_btn_Click(object sender, EventArgs e)
        {
            this.Hide();

            answerQuerieForm aqf = new answerQuerieForm(playerId, playerName, server, 8);
            aqf.ShowDialog();

            this.Show();

        }

        private void nineQ_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            answerQuerieForm aqf = new answerQuerieForm(playerId, playerName, server, 9);
            aqf.ShowDialog();
            this.Show();

        }

        private void tenQ_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            answerQuerieForm aqf = new answerQuerieForm(playerId, playerName, server, 10);
            aqf.ShowDialog();
            this.Show();

        }
        
    }
}
