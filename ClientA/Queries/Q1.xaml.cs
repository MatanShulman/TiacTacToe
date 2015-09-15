using Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for Q1.xaml
    /// </summary>
    public partial class Q1 : System.Windows.Controls.UserControl
    {

        public int playerId { get; set; }
        public string playerName { get; set; }
        private ServiceClient server { get; set; }
        public List<Players> editList { get; set; }
        public Players[] list { get; set; }
        public byte[] picByte;

        private int formMode { get; set; }

        private bool picFlag = false;
        
        public Q1(int playerid, string playername, ServiceClient Server, int mode)
        {
            InitializeComponent();
           
            this.playerId = playerid;
            this.playerName = playername;
            this.server = Server;
            editList = new List<Players>();
            dgv.CanUserAddRows = false;
            dgv.IsReadOnly = true;
            picByte = null;
            this.formMode = mode;
            
            
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (picFlag) { 
             Bitmap bm;
             OpenFileDialog fileChooser = new OpenFileDialog();
             if (fileChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
             {
                  var str = fileChooser.FileName;
                  try
                  {
                      FileStream fs = new FileStream(str, FileMode.Open, FileAccess.Read);
                      BinaryReader br = new BinaryReader(fs);
                      MemoryStream ms = new MemoryStream(br.ReadBytes((int)fs.Length));
                      System.Drawing.Image imageIn = System.Drawing.Image.FromStream(ms);
                      bm = new Bitmap(imageIn);
                      picByte = ms.ToArray();
                      editList[0].pictureArrByte = bm;
                  }
                  catch{}
             }
           }
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (formMode == 0)
            {
                list = await Task<Players[]>.Factory.StartNew(getPlayers);

                //Generate a list that contain only player id for editing
                foreach (var item in list)
                    if (item.playerId == this.playerId)
                        editList.Add(item);
                dgv.AutoGenerateColumns = false;
                dgv.ItemsSource = list;
            }
            
            
            
            
         
        }

        public  void updateGrid(int id)
        {
            dgv.ItemsSource = null;
            if (formMode == 1)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((_) => getPlayersByGame(id)));
            }
            if (formMode == 2)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((_) => getAdvisorsByGame(id)));
            }
            if (formMode == 3)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((_) => getPlayersByChamp(id)));
            }

            dgv.AutoGenerateColumns = false;
       
           
        }

        public void getAdvisorsByGame(int gameId)
        {

            answerQuerieForm.manualResetEvent.Reset();
            if (chooseQueriesForm.slowServer)
                Thread.Sleep(3000);
            Players[] lst = server.wpfGetAdvisorsByGame(gameId);
            answerQuerieForm.manualResetEvent.Set();
            var lastOpenedForm = (answerQuerieForm)System.Windows.Forms.Application.OpenForms[System.Windows.Forms.Application.OpenForms.Count - 1];
            lastOpenedForm.BeginInvoke((Action)(() =>
            {
                dgv.ItemsSource = lst;

            }));


        }
        public void getPlayersByChamp(int champId)
        {

            answerQuerieForm.manualResetEvent.Reset();
            if (chooseQueriesForm.slowServer)
                Thread.Sleep(3000);
            Players[] lst = server.wpfGetPlayersByChamp(champId);
            answerQuerieForm.manualResetEvent.Set();
            var lastOpenedForm = (answerQuerieForm)System.Windows.Forms.Application.OpenForms[System.Windows.Forms.Application.OpenForms.Count - 1];
            lastOpenedForm.BeginInvoke((Action)(() =>
            {
                dgv.ItemsSource = lst;

            }));


        }

        public void getPlayersByGame(int gameId)
        {
            
            answerQuerieForm.manualResetEvent.Reset();
            if (chooseQueriesForm.slowServer)
                Thread.Sleep(3000);
            Players[] lst = server.wpfGetPlayerByGames(gameId);
            answerQuerieForm.manualResetEvent.Set();
            var lastOpenedForm = (answerQuerieForm)System.Windows.Forms.Application.OpenForms[System.Windows.Forms.Application.OpenForms.Count - 1];
             lastOpenedForm.BeginInvoke((Action)(() =>
            {
              dgv.ItemsSource = lst;
              
            }));
        
            
         }



        public Players[] getPlayers()
        {
            answerQuerieForm.manualResetEvent.Reset();
            if(chooseQueriesForm.slowServer)
                Thread.Sleep(3000);
            Players[] lst = server.wpfGetAllPlayers();
            answerQuerieForm.manualResetEvent.Set();
            return lst;

        }
        public void setDataGrid(bool flag)
        {
            if (flag)
            {
               
                dgv.ItemsSource = editList;
                dgv.IsReadOnly = false;
                dgv.Columns[0].IsReadOnly = true;
            }
            else
            {
                list = server.wpfGetAllPlayers();
                dgv.ItemsSource = list;
                dgv.IsReadOnly = true;
               
                
            }
            picFlag = flag;
        }
        public void updateData()
        {
            if (chooseQueriesForm.slowServer)
                Thread.Sleep(3000);
            server.updatePlayerInfo(editList[0].firstName, editList[0].lastName, editList[0].playerId,picByte);
            MainMenu.playerName = editList[0].firstName;
            
        }
        public void deleteData()
        {
            server.deletePlayer(editList[0].playerId);
            server.logOut(editList[0].playerId);


        }
    }
}
