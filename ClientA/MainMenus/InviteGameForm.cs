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
    public partial class InviteGameForm : Form
    {
        public static Form lastOpenedForm;

        public string playerName { get; set; }
        public string gameType { get; set; }
        public bool ans { get; set; }
        public System.Windows.Forms.Timer timer { get; set; }

        private ServiceClient server;
        private int rivalId;
        private int myId;

        //main constructor
        public InviteGameForm(string playerName, string gameType, ServiceClient server, int rivalId, int myId)
        {
            InitializeComponent();
            timer = new System.Windows.Forms.Timer();
            this.playerName = playerName;
            this.gameType = gameType;
            this.server = server;
            this.rivalId = rivalId;
            this.myId = myId;
            timer.Interval = 20000;
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Start();
        }
        //timer for invite
        private void TimerEventProcessor(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }
        //onload set info
        private void InviteGameForm_Load(object sender, EventArgs e)
        {
            label2.Text = playerName;
            label3.Text = gameType;
        }

        //answer ok
        private void button1_Click(object sender, EventArgs e)
        {
            timer.Stop();
            ans = true;
            server.acceptAnswerFromRival(rivalId, ans, myId, gameType);
            this.Close();
            Thread th = new Thread(startGame);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }

        //start gameboard on thread
        private void startGame(object obj)
        {
            Application.Run(MainMenu.activeGame = new gameBoardForm(server, myId, rivalId, gameType, false));
        }

        //answer no
        private void button2_Click(object sender, EventArgs e)
        {
            timer.Stop();
            ans = false;
            server.acceptAnswerFromRival(rivalId, ans, myId, gameType);
            this.Close();
        }
       
    }
}
