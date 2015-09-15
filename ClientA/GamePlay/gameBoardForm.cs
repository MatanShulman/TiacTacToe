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
using System.Windows.Forms.Integration;

namespace Client
{
    public partial class gameBoardForm : Form
    {
        public static bool viewButton = false;
        public static bool closeByX = true;
        public static int gameId;
        public static int[,] boardGame3x3 = new int[3, 3];
        public static int[,] boardGame4x4 = new int[4, 4];
        public static int[,] boardGame5x5 = new int[5, 5];

        public int playerId { get; set; }
        public int rivalId { get; set; }
        public string mode { get; set; }

        public Board4x4 board4;
        public Board3x3 board3;
        public Board5x5 board5;

        public int moveCounter;
        public int[] allMoves;

        private bool Turn;
        private ElementHost elementHost1;
        private ServiceClient server;

        //main constructor
        public gameBoardForm(ServiceClient server, int playerId, int rivalId, string mode, bool turn)
        {
            InitializeComponent();
            MainMenu.inGame = true;
            elementHost1 = new ElementHost();
            this.Turn = turn;
            this.server = server;
            this.playerId = playerId;
            this.rivalId = rivalId;
            this.mode = mode;

            if (Turn)
                this.turn_label.Text = "Please make a move";
            else
                this.turn_label.Text = "Please wait to your turn";

            SuspendLayout();
            next_btn.Enabled = false;
            back_btn.Enabled = false;
            next_btn.Visible = false;
            back_btn.Visible = false;
            switch (mode)
            {
                case "Easy":
                    build3on3();
                    break;

                case "Medium":
                    build4on4();
                    break;

                case "Hard":
                    build5on5();
                    break;
            }
            ResumeLayout(false);

            if (viewButton)
            {
                this.turn_label.Text = "";
                moveCounter = 0;
                allMoves = server.getMovesGameByGameId(gameBoardForm.gameId);
                if (allMoves.Length == 0)
                {
                    showMsg("this game has no moves");
                    next_btn.Enabled = false;
                    back_btn.Enabled = false;
                }
            }

            this.Text = "Game #" + gameId;
        }
      
        //build 3x3 game board
        private void build3on3()
        {
            board3 = new Board3x3(Turn, server, playerId, rivalId, this);
            this.ClientSize = new System.Drawing.Size(400, 400);
            elementHost1.Location = new System.Drawing.Point(15, 15);
            elementHost1.Name = "elementHost1";
            elementHost1.Size = new System.Drawing.Size(372, 321);
            elementHost1.TabIndex = 2;
            elementHost1.Text = "elementHost1";
            elementHost1.Child = board3;
            this.turn_label.Location = new System.Drawing.Point(82, 350);
            if (viewButton)
            {
                board3.IsEnabled = false;
                next_btn.Enabled = true;
                back_btn.Enabled = false;
                next_btn.Visible = true;
                back_btn.Visible = true;
                next_btn.Location = new System.Drawing.Point(320, 370);
                back_btn.Location = new System.Drawing.Point(10, 370);
            }
            Controls.Add(this.elementHost1);
        }

        //build 4x4 game board
        private void build4on4()
        {
            board4 = new Board4x4(Turn, server, playerId, rivalId, this);
            this.ClientSize = new System.Drawing.Size(520, 500);
            elementHost1.Location = new System.Drawing.Point(15, 15);
            elementHost1.Name = "elementHost1";
            elementHost1.Size = new System.Drawing.Size(492, 420);
            elementHost1.TabIndex = 2;
            elementHost1.Text = "elementHost1";
            elementHost1.Child = board4;
            this.turn_label.Location = new System.Drawing.Point(82, 450);
            if (viewButton)
            {
                board4.IsEnabled = false;
                next_btn.Enabled = true;
                back_btn.Enabled = false;
                next_btn.Visible = true;
                back_btn.Visible = true;
                next_btn.Location = new System.Drawing.Point(440, 440);
                back_btn.Location = new System.Drawing.Point(10, 440);
            }
            Controls.Add(this.elementHost1);
        }

        //build 5x5 game board
        private void build5on5()
        {
            board5 = new Board5x5(Turn, server, playerId, rivalId, this);
            this.ClientSize = new System.Drawing.Size(645, 605);
            elementHost1.Location = new System.Drawing.Point(15, 15);
            elementHost1.Name = "elementHost1";
            elementHost1.Size = new System.Drawing.Size(615, 525);
            elementHost1.TabIndex = 2;
            elementHost1.Text = "elementHost1";
            elementHost1.Child = board5;
            this.turn_label.Location = new System.Drawing.Point(82, 550);
            if (viewButton)
            {
                board5.IsEnabled = false;
                next_btn.Enabled = true;
                back_btn.Enabled = false;
                next_btn.Visible = true;
                back_btn.Visible = true;
                next_btn.Location = new System.Drawing.Point(550, 545);
                back_btn.Location = new System.Drawing.Point(10, 545);
            }
            Controls.Add(this.elementHost1);

        }

        public void showMsg(string msg)
        {
            MessageBox.Show("" + msg);


        }

        //onlclose make sure client wants to forfit
        private void gameBoardForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!viewButton)
            {
                if (closeByX)
                {
                    DialogResult dr = MessageBox.Show(this, "Are you sure?", "If you quit now you will forfit game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {

                        switch (mode)
                        {
                            case "Easy":
                                if (!board3.win && board3.moveCounter < 8)
                                    server.forfitGame(rivalId, gameId);
                                break;

                            case "Medium":
                                if (!board4.win && board4.moveCounter < 15)
                                    server.forfitGame(rivalId, gameId);
                                break;

                            case "Hard":
                                if (!board5.win && board5.moveCounter < 24)
                                    server.forfitGame(rivalId, gameId);
                                break;
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                server.setPlayerOnline(playerId);
            }
            MainMenu.activeGame = null;
            MainMenu.inGame = false;
        }

        //go back in history moves
        private void back_btn_Click(object sender, EventArgs e)
        {
         
            if (moveCounter > 0)
            {

                moveCounter--;

                switch (mode)
                {
                    case "Easy":
                        board3.HistoryDelete(allMoves[moveCounter]);
                        break;
                    case "Medium":
                        board4.HistoryDelete(allMoves[moveCounter]);
                        break;
                    case "Hard":
                        board5.HistoryDelete(allMoves[moveCounter]);
                        break;
                }

                Turn = !Turn;

                if (moveCounter < allMoves.Length && !next_btn.Enabled)
                    next_btn.Enabled = true;
                if (moveCounter == 0)
                    back_btn.Enabled = false;
            }

        }

        //go to next history move
        private void next_btn_Click(object sender, EventArgs e)
        {
           
            if (moveCounter < allMoves.Length)
            {
                switch (mode)
                {
                    case "Easy":
                        board3.histroyDraw(Turn, allMoves[moveCounter]);
                        break;
                    case "Medium":
                        board4.histroyDraw(Turn, allMoves[moveCounter]);
                        break;
                    case "Hard":
                        board5.histroyDraw(Turn, allMoves[moveCounter]);
                        break;
                }

                Turn = !Turn;
                moveCounter++;

                if (moveCounter == allMoves.Length)
                {               
                    next_btn.Enabled = false;
                }

                if (moveCounter > 0)
                    back_btn.Enabled = true;
            }

        }

    }
}
