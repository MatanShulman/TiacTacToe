using Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;

namespace Client
{
    public partial class MainProgram : Form
    {
        private ServiceClient server;

        //main constructor 
        public MainProgram()
        {
            InitializeComponent();
            GameICallBack callBack = new GameICallBack();
            InstanceContext context = new InstanceContext(callBack);
            server = new ServiceClient(context);
            callBack.server = server;

        }

        //register new player
        private void New_PLayer_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reg_Form regForm = new Reg_Form(this, server);
            regForm.ShowDialog();

            this.Show();

        }

        //login player
        private void existing_player_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInPlayer temp = new LogInPlayer(this, server);
            temp.ShowDialog();

            this.Show();
        }


        //server callback class
        class GameICallBack : IServiceCallback
        {
            public bool ans { get; set; }
            public int idPlayer { get; set; }
            public ServiceClient server { get; set; }
            public string playerName { get; set; }
            public string gameType { get; set; }
            public int myId { get; set; }
            public int rivalId { get; set; }


            //invite player callback
            public void invitePlayer(string playerName, string gameType, int rivalId, int myId)
            {
                InviteGameForm igf = new InviteGameForm(playerName, gameType, server, rivalId, myId);
                igf.ShowDialog();
            }

            //callback for error msg
            public void ErrorCallBack(string str, int id)
            {
                MessageBox.Show(str);
            }

            //answer from invite callback
            public void answerFromRiavl(bool answer, int gameId)
            {

                gameBoardForm.gameId = gameId;
                waitingForm.receivedAnswer(answer);

            }
            // set game id for new games
            public void setGameId(int gameId)
            {
                gameBoardForm.gameId = gameId;
            }

            //get move from rival
            public void moveFromRival(int x, string type, bool win)
            {

                var lastOpenedForm = (gameBoardForm)Application.OpenForms[Application.OpenForms.Count - 1];

                switch (type)
                {
                    case "Easy":
                        Board3x3.refernceArr[x].Dispatcher.Invoke((Action)(() =>
                        {

                            lastOpenedForm.board3.rivalDraw(Board3x3.refernceArr[x], win);

                        }));
                        break;
                    case "Medium":
                        Board4x4.refernceArr[x].Dispatcher.Invoke((Action)(() =>
                        {

                            lastOpenedForm.board4.rivalDraw(Board4x4.refernceArr[x], win);

                        }));
                        break;
                    case "Hard":
                        Board5x5.refernceArr[x].Dispatcher.Invoke((Action)(() =>
                        {

                            lastOpenedForm.board5.rivalDraw(Board5x5.refernceArr[x], win);

                        }));
                        break;
                }


            }

            //rival quit 
            public void rivalForfit()
            {

                
                gameBoardForm gbf = (gameBoardForm)Application.OpenForms[Application.OpenForms.Count - 1];
                gbf.Invoke((Action)(() => gbf.showMsg("Rival Quit Game ... You Won")));
                switch (gbf.mode)
                {
                    case "Easy":
                        gbf.board3.win = true;
                        break;
                    case "Medium":
                        gbf.board4.win = true;
                        break;
                    case "Hard":
                        gbf.board5.win = true;
                        break;
                }
                gameBoardForm.closeByX = false;
                gbf.Invoke((Action)(() => gbf.Close()));

            }
        }
    }
}

        
    
    

   

