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
using System.Windows.Forms.Integration;

namespace Client
{
    public partial class answerQuerieForm : Form
    {
        public int playerId { get; set; }
        public string playerName { get; set; }
        private ServiceClient server { get; set; }
        public int questionNum { get; set; }
        public ElementHost elementHost { get; set; }
        public Q1 q1 { get; set; }
        public Q2 q2 { get; set; }
        public Q3 q3 { get; set; }
        public Q2 q4 { get; set; }
        public Q3 q5 { get; set; }
        public Q1 q6 { get; set; }
        public Q1 q7 { get; set; }
        public Q1 q8 { get; set; }
        public Q4 q9 { get; set; }
        public Q5 q10 { get; set; }
        public bool editFlag { get; set; }

        private Players[] allPlayers;
        private Champpion[] allChamp;
        private MyGames[] allgames;
        public static ManualResetEvent manualResetEvent { get; set; }
       
        public answerQuerieForm(int playerid, string playername, ServiceClient Server,int questionnum)
        {
            InitializeComponent();
            comboBox1.Visible = false;
            label1.Visible = false;
            elementHost = new ElementHost();
            this.playerId = playerid;
            this.playerName = playername;
            this.server = Server;
            this.questionNum = questionnum;
            editFlag = true;
            manualResetEvent = new ManualResetEvent(true);
            
            
            SuspendLayout();
            switch (questionnum)
            {
                case 1:
                    question1();
                    break;
                case 2:
                    question2();
                    break;
                case 3:
                    question3();
                    break;
                    
                case 4:
                    question4();
                    break;
                case 5:
                    question5();
                    break;
                case 6:
                    question6();
                    break;
                case 7:
                    question7();
                    break;

                case 8:
                    question8();
                    break;
                case 9:
                    question9();
                    break;
                case 10:
                    question10();
                    break;
            }
            ResumeLayout(false);




        }

      
      

     

        //Create wpf user control for querie number 1
        private void question1()
        {
            delete_btn.Enabled = false;
            Update_btn.Enabled = false;
            q1 = new Q1(playerId, playerName, server,0);
            this.ClientSize = new System.Drawing.Size(400, 380);
            elementHost.Location = new System.Drawing.Point(15, 40);
            elementHost.Name = "elementHost";
            elementHost.Size = new System.Drawing.Size(400, 320);
            elementHost.TabIndex = 2;
            elementHost.Text = "elementHost";
            elementHost.Child = q1;
            Controls.Add(this.elementHost);
        }

        private void question2()
        {
            delete_btn.Enabled = false;
            Update_btn.Enabled = false;
            Edit_btn.Enabled = false;
            q2 = new Q2(server,0);
            this.ClientSize = new System.Drawing.Size(400, 400);
            elementHost.Location = new System.Drawing.Point(5, 50);
            elementHost.Name = "elementHost";
            elementHost.Size = new System.Drawing.Size(395, 300);
            elementHost.TabIndex = 2;
            elementHost.Text = "elementHost";
            elementHost.Child = q2;
            Controls.Add(this.elementHost);
        }

        private void question3()
        {
            delete_btn.Enabled = false;
            Update_btn.Enabled = false;
            Edit_btn.Enabled = true;
            q3 = new Q3(server,0,playerId);
            this.ClientSize = new System.Drawing.Size(400, 400);
            elementHost.Location = new System.Drawing.Point(5, 50);
            elementHost.Name = "elementHost";
            elementHost.Size = new System.Drawing.Size(395, 300);
            elementHost.TabIndex = 2;
            elementHost.Text = "elementHost";
            elementHost.Child = q3;
            Controls.Add(this.elementHost);
        }

        private void question4()
        {
            delete_btn.Visible = false;
            Update_btn.Visible = false;
            Edit_btn.Visible = false;
            comboBox1.Visible = true;
            label1.Text = "Choose Player:";
            label1.Visible = true;

            allPlayers = server.wpfGetAllPlayers();
            comboBox1.Items.Clear();
            foreach (var item in allPlayers)
            {
                comboBox1.Items.Add(item.playerId + " || " + item.firstName);
            }

            q4 = new Q2(server, 1);
            this.ClientSize = new System.Drawing.Size(400, 400);
            elementHost.Location = new System.Drawing.Point(5, 70);
            elementHost.Name = "elementHost";
            elementHost.Size = new System.Drawing.Size(395, 300);
            elementHost.TabIndex = 2;
            elementHost.Text = "elementHost";
            elementHost.Child = q4;
            Controls.Add(this.elementHost);
        }

        private void question5()
        {
            delete_btn.Visible = false;
            Update_btn.Visible = false;
            Edit_btn.Visible = false;
            comboBox1.Visible = true;
            label1.Text = "Choose Player:";
            label1.Visible = true;

            allPlayers = server.getAllPlayers();
            comboBox1.Items.Clear();
            foreach (var item in allPlayers)
            {
                comboBox1.Items.Add(item.playerId + " || " + item.firstName);
            }
            delete_btn.Enabled = false;
            Update_btn.Enabled = false;
            Edit_btn.Enabled = false;
            q5 = new Q3(server, 1);
            this.ClientSize = new System.Drawing.Size(400, 400);
            elementHost.Location = new System.Drawing.Point(5, 70);
            elementHost.Name = "elementHost";
            elementHost.Size = new System.Drawing.Size(395, 300);
            elementHost.TabIndex = 2;
            elementHost.Text = "elementHost";
            elementHost.Child = q5;
            Controls.Add(this.elementHost);
        }

        private void question6()
        {
            delete_btn.Visible = false;
            Update_btn.Visible = false;
            Edit_btn.Visible = false;
            comboBox1.Visible = true;
            label1.Text = "Choose Game:";
            label1.Visible = true;


            allgames = server.wpfGetAllGames();
            comboBox1.Items.Clear();
            foreach (var item in allgames)
            {
                comboBox1.Items.Add(item.gameId + " || " + item.firstPlayer + " VS " + item.secondPlayer + " Mode: " + item.gameMode);
            }

            q6 = new Q1(playerId, playerName, server, 1);
            this.ClientSize = new System.Drawing.Size(400, 400);
            elementHost.Location = new System.Drawing.Point(5, 70);
            elementHost.Name = "elementHost";
            elementHost.Size = new System.Drawing.Size(395, 300);
            elementHost.TabIndex = 2;
            elementHost.Text = "elementHost";
            elementHost.Child = q6;
            Controls.Add(this.elementHost);
        }

        private void question7()
        {
            delete_btn.Visible = false;
            Update_btn.Visible = false;
            Edit_btn.Visible = false;
            comboBox1.Visible = true;
            label1.Text = "Choose Game:";
            label1.Visible = true;


            allgames = server.wpfGetAllGames();
            comboBox1.Items.Clear();
            foreach (var item in allgames)
            {
                comboBox1.Items.Add(item.gameId + " || " + item.firstPlayer + " VS " + item.secondPlayer + " Mode: " + item.gameMode);
            }

            q7 = new Q1(playerId, playerName, server, 2);
            this.ClientSize = new System.Drawing.Size(400, 400);
            elementHost.Location = new System.Drawing.Point(5, 70);
            elementHost.Name = "elementHost";
            elementHost.Size = new System.Drawing.Size(395, 300);
            elementHost.TabIndex = 2;
            elementHost.Text = "elementHost";
            elementHost.Child = q7;
            Controls.Add(this.elementHost);
        }

        private void question8()
        {
            delete_btn.Visible = false;
            Update_btn.Visible = false;
            Edit_btn.Visible = false;
            comboBox1.Visible = true;
            label1.Text = "Choose Champ:";
            label1.Visible = true;


            allChamp = server.getChampList();
            comboBox1.Items.Clear();
            foreach (var item in allChamp)
            {
                comboBox1.Items.Add(item.id + " || " + item.name + " Location: " + item.location);
            }

            q8 = new Q1(playerId, playerName, server, 3);
            this.ClientSize = new System.Drawing.Size(400, 400);
            elementHost.Location = new System.Drawing.Point(5, 70);
            elementHost.Name = "elementHost";
            elementHost.Size = new System.Drawing.Size(395, 300);
            elementHost.TabIndex = 2;
            elementHost.Text = "elementHost";
            elementHost.Child = q8;
            Controls.Add(this.elementHost);
        }

        private void question9()
        {
            delete_btn.Visible = false;
            Update_btn.Visible = false;
            Edit_btn.Visible = false;
            comboBox1.Visible = false;
            label1.Visible = false;

            q9 = new Q4(server);
            this.ClientSize = new System.Drawing.Size(400, 400);
            elementHost.Location = new System.Drawing.Point(5, 70);
            elementHost.Name = "elementHost";
            elementHost.Size = new System.Drawing.Size(395, 300);
            elementHost.TabIndex = 2;
            elementHost.Text = "elementHost";
            elementHost.Child = q9;
            Controls.Add(this.elementHost);
        }


        private void question10()
        {
            delete_btn.Visible = false;
            Update_btn.Visible = false;
            Edit_btn.Visible = false;
            comboBox1.Visible = false;
            label1.Visible = false;

            q10 = new Q5(server);
            this.ClientSize = new System.Drawing.Size(400, 400);
            elementHost.Location = new System.Drawing.Point(5, 70);
            elementHost.Name = "elementHost";
            elementHost.Size = new System.Drawing.Size(395, 300);
            elementHost.TabIndex = 2;
            elementHost.Text = "elementHost";
            elementHost.Child = q10;
            Controls.Add(this.elementHost);
        }



        private void Edit_btn_Click(object sender, EventArgs e)
        {
            if (editFlag==false)
            {
              
                delete_btn.Enabled = false;
                Update_btn.Enabled = false;

            }
            else
            {
               
                delete_btn.Enabled = true;
                Update_btn.Enabled = true;
            }
            switch (questionNum)
            {
                case 1:
                    q1.setDataGrid(editFlag);
                    editFlag = !editFlag;
                    break;
                case 3:
                    q3.setDataGrid(editFlag);
                    editFlag = !editFlag;
                    break;
            }

        }

        private void Update_btn_Click(object sender, EventArgs e)
        {
            switch (questionNum)
            {
                case 1:
                    q1.updateData();
                    break;
                case 3:
                    q3.updateData();
                    break;
            }
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            switch (questionNum)
            {
                case 1:
                   DialogResult dr = MessageBox.Show("if you will delete your self you will be logged out ", "are you sure?", MessageBoxButtons.YesNo);
                   if (dr == DialogResult.Yes) { 
                        q1.deleteData();
                        var lastOpenedForm1 = (chooseQueriesForm)Application.OpenForms[Application.OpenForms.Count - 2];
                        lastOpenedForm1.Close();
                        var lastOpenedForm = (MainMenu)Application.OpenForms[Application.OpenForms.Count - 3];
                        lastOpenedForm.Close();
                        this.Close();
                  }
                    break;
                case 3:
                    q3.deleteData();


                    break;
            
            }
        }

        private void answerQuerieForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           manualResetEvent.WaitOne();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (questionNum)
            {
                case 4:
                    q4.updateDataGrid(allPlayers[comboBox1.SelectedIndex].playerId);
                    break;
                case 5:
                    q5.updateDataGrid(allPlayers[comboBox1.SelectedIndex].playerId);
                    break;
                case 6:
                    q6.updateGrid(allgames[comboBox1.SelectedIndex].gameId);
                    break;
                case 7:
                    q7.updateGrid(allgames[comboBox1.SelectedIndex].gameId);
                    break;
                case 8:
                    q8.updateGrid(allChamp[comboBox1.SelectedIndex].id);
                    break;

            }
        }
    }
}
